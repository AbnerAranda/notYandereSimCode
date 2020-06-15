using System;
using System.Collections.Generic;
using Pathfinding.ClipperLib;
using Pathfinding.Poly2Tri;
using Pathfinding.Voxels;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005F2 RID: 1522
	public class TileHandler
	{
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x001C274C File Offset: 0x001C094C
		public bool isValid
		{
			get
			{
				return this.graph != null && this.graph.exists && this.tileXCount == this.graph.tileXCount && this.tileZCount == this.graph.tileZCount;
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x001C278C File Offset: 0x001C098C
		public TileHandler(NavmeshBase graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			if (graph.GetTiles() == null)
			{
				Debug.LogWarning("Creating a TileHandler for a graph with no tiles. Please scan the graph before creating a TileHandler");
			}
			this.tileXCount = graph.tileXCount;
			this.tileZCount = graph.tileZCount;
			this.activeTileTypes = new TileHandler.TileType[this.tileXCount * this.tileZCount];
			this.activeTileRotations = new int[this.activeTileTypes.Length];
			this.activeTileOffsets = new int[this.activeTileTypes.Length];
			this.reloadedInBatch = new bool[this.activeTileTypes.Length];
			this.cuts = new GridLookup<NavmeshClipper>(new Int2(this.tileXCount, this.tileZCount));
			this.graph = graph;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x001C2864 File Offset: 0x001C0A64
		public void OnRecalculatedTiles(NavmeshTile[] recalculatedTiles)
		{
			for (int i = 0; i < recalculatedTiles.Length; i++)
			{
				this.UpdateTileType(recalculatedTiles[i]);
			}
			bool flag = this.StartBatchLoad();
			for (int j = 0; j < recalculatedTiles.Length; j++)
			{
				this.ReloadTile(recalculatedTiles[j].x, recalculatedTiles[j].z);
			}
			if (flag)
			{
				this.EndBatchLoad();
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x001C28BC File Offset: 0x001C0ABC
		public int GetActiveRotation(Int2 p)
		{
			return this.activeTileRotations[p.x + p.y * this.tileXCount];
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x001C28D9 File Offset: 0x001C0AD9
		[Obsolete("Use the result from RegisterTileType instead")]
		public TileHandler.TileType GetTileType(int index)
		{
			throw new Exception("This method has been deprecated. Use the result from RegisterTileType instead.");
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x001C28D9 File Offset: 0x001C0AD9
		[Obsolete("Use the result from RegisterTileType instead")]
		public int GetTileTypeCount()
		{
			throw new Exception("This method has been deprecated. Use the result from RegisterTileType instead.");
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x001C28E5 File Offset: 0x001C0AE5
		public TileHandler.TileType RegisterTileType(Mesh source, Int3 centerOffset, int width = 1, int depth = 1)
		{
			return new TileHandler.TileType(source, (Int3)new Vector3(this.graph.TileWorldSizeX, 0f, this.graph.TileWorldSizeZ), centerOffset, width, depth);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x001C2918 File Offset: 0x001C0B18
		public void CreateTileTypesFromGraph()
		{
			NavmeshTile[] tiles = this.graph.GetTiles();
			if (tiles == null)
			{
				return;
			}
			if (!this.isValid)
			{
				throw new InvalidOperationException("Graph tiles are invalid (number of tiles is not equal to width*depth of the graph). You need to create a new tile handler if you have changed the graph.");
			}
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					NavmeshTile tile = tiles[j + i * this.tileXCount];
					this.UpdateTileType(tile);
				}
			}
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x001C2980 File Offset: 0x001C0B80
		private void UpdateTileType(NavmeshTile tile)
		{
			int x = tile.x;
			int z = tile.z;
			Int3 @int = (Int3)new Vector3(this.graph.TileWorldSizeX, 0f, this.graph.TileWorldSizeZ);
			Int3 centerOffset = -((Int3)this.graph.GetTileBoundsInGraphSpace(x, z, 1, 1).min + new Int3(@int.x * tile.w / 2, 0, @int.z * tile.d / 2));
			TileHandler.TileType tileType = new TileHandler.TileType(tile.vertsInGraphSpace, tile.tris, @int, centerOffset, tile.w, tile.d);
			int num = x + z * this.tileXCount;
			this.activeTileTypes[num] = tileType;
			this.activeTileRotations[num] = 0;
			this.activeTileOffsets[num] = 0;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x001C2A5A File Offset: 0x001C0C5A
		public bool StartBatchLoad()
		{
			if (this.isBatching)
			{
				return false;
			}
			this.isBatching = true;
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
			{
				this.graph.StartBatchTileUpdate();
				return true;
			}));
			return true;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x001C2A8C File Offset: 0x001C0C8C
		public void EndBatchLoad()
		{
			if (!this.isBatching)
			{
				throw new Exception("Ending batching when batching has not been started");
			}
			for (int i = 0; i < this.reloadedInBatch.Length; i++)
			{
				this.reloadedInBatch[i] = false;
			}
			this.isBatching = false;
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
			{
				this.graph.EndBatchTileUpdate();
				GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
				return true;
			}));
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x001C2AEC File Offset: 0x001C0CEC
		private TileHandler.CuttingResult CutPoly(Int3[] verts, int[] tris, Int3[] extraShape, GraphTransform graphTransform, IntRect tiles, TileHandler.CutMode mode = TileHandler.CutMode.CutAll | TileHandler.CutMode.CutDual, int perturbate = -1)
		{
			if (verts.Length == 0 || tris.Length == 0)
			{
				TileHandler.CuttingResult result = new TileHandler.CuttingResult
				{
					verts = ArrayPool<Int3>.Claim(0),
					tris = ArrayPool<int>.Claim(0)
				};
				return result;
			}
			if (perturbate > 10)
			{
				Debug.LogError("Too many perturbations aborting.\nThis may cause a tile in the navmesh to become empty. Try to see see if any of your NavmeshCut or NavmeshAdd components use invalid custom meshes.");
				TileHandler.CuttingResult result = new TileHandler.CuttingResult
				{
					verts = verts,
					tris = tris
				};
				return result;
			}
			List<IntPoint> list = null;
			if (extraShape == null && (mode & TileHandler.CutMode.CutExtra) != (TileHandler.CutMode)0)
			{
				throw new Exception("extraShape is null and the CutMode specifies that it should be used. Cannot use null shape.");
			}
			Bounds tileBoundsInGraphSpace = this.graph.GetTileBoundsInGraphSpace(tiles);
			Vector3 min = tileBoundsInGraphSpace.min;
			GraphTransform graphTransform2 = graphTransform * Matrix4x4.TRS(min, Quaternion.identity, Vector3.one);
			Vector2 v = new Vector2(tileBoundsInGraphSpace.size.x, tileBoundsInGraphSpace.size.z);
			if ((mode & TileHandler.CutMode.CutExtra) != (TileHandler.CutMode)0)
			{
				list = ListPool<IntPoint>.Claim(extraShape.Length);
				for (int i = 0; i < extraShape.Length; i++)
				{
					Int3 @int = graphTransform2.InverseTransform(extraShape[i]);
					list.Add(new IntPoint((long)@int.x, (long)@int.z));
				}
			}
			IntRect cutSpaceBounds = new IntRect(verts[0].x, verts[0].z, verts[0].x, verts[0].z);
			for (int j = 0; j < verts.Length; j++)
			{
				cutSpaceBounds = cutSpaceBounds.ExpandToContain(verts[j].x, verts[j].z);
			}
			List<NavmeshCut> list2;
			if (mode == TileHandler.CutMode.CutExtra)
			{
				list2 = ListPool<NavmeshCut>.Claim();
			}
			else
			{
				list2 = this.cuts.QueryRect<NavmeshCut>(tiles);
			}
			List<NavmeshAdd> list3 = this.cuts.QueryRect<NavmeshAdd>(tiles);
			List<int> list4 = ListPool<int>.Claim();
			List<TileHandler.Cut> list5 = TileHandler.PrepareNavmeshCutsForCutting(list2, graphTransform2, cutSpaceBounds, perturbate, list3.Count > 0);
			List<Int3> list6 = ListPool<Int3>.Claim(verts.Length * 2);
			List<int> list7 = ListPool<int>.Claim(tris.Length);
			if (list2.Count == 0 && list3.Count == 0 && (mode & ~(TileHandler.CutMode.CutAll | TileHandler.CutMode.CutDual)) == (TileHandler.CutMode)0 && (mode & TileHandler.CutMode.CutAll) != (TileHandler.CutMode)0)
			{
				TileHandler.CopyMesh(verts, tris, list6, list7);
			}
			else
			{
				List<IntPoint> list8 = ListPool<IntPoint>.Claim();
				Dictionary<TriangulationPoint, int> dictionary = new Dictionary<TriangulationPoint, int>();
				List<PolygonPoint> list9 = ListPool<PolygonPoint>.Claim();
				PolyTree polyTree = new PolyTree();
				List<List<IntPoint>> intermediateResult = ListPool<List<IntPoint>>.Claim();
				Stack<Polygon> stack = StackPool<Polygon>.Claim();
				this.clipper.StrictlySimple = (perturbate > -1);
				this.clipper.ReverseSolution = true;
				Int3[] array = null;
				Int3[] clipOut = null;
				Int2 size = default(Int2);
				if (list3.Count > 0)
				{
					array = new Int3[7];
					clipOut = new Int3[7];
					size = new Int2(((Int3)v).x, ((Int3)v).y);
				}
				Int3[] array2 = null;
				for (int k = -1; k < list3.Count; k++)
				{
					Int3[] array3;
					int[] array4;
					if (k == -1)
					{
						array3 = verts;
						array4 = tris;
					}
					else
					{
						list3[k].GetMesh(ref array2, out array4, graphTransform2);
						array3 = array2;
					}
					for (int l = 0; l < array4.Length; l += 3)
					{
						Int3 int2 = array3[array4[l]];
						Int3 int3 = array3[array4[l + 1]];
						Int3 int4 = array3[array4[l + 2]];
						if (VectorMath.IsColinearXZ(int2, int3, int4))
						{
							Debug.LogWarning("Skipping degenerate triangle.");
						}
						else
						{
							IntRect a = new IntRect(int2.x, int2.z, int2.x, int2.z);
							a = a.ExpandToContain(int3.x, int3.z);
							a = a.ExpandToContain(int4.x, int4.z);
							int num = Math.Min(int2.y, Math.Min(int3.y, int4.y));
							int num2 = Math.Max(int2.y, Math.Max(int3.y, int4.y));
							list4.Clear();
							bool flag = false;
							for (int m = 0; m < list5.Count; m++)
							{
								int x = list5[m].boundsY.x;
								int y = list5[m].boundsY.y;
								if (IntRect.Intersects(a, list5[m].bounds) && y >= num && x <= num2 && (list5[m].cutsAddedGeom || k == -1))
								{
									Int3 int5 = int2;
									int5.y = x;
									Int3 int6 = int2;
									int6.y = y;
									list4.Add(m);
									flag |= list5[m].isDual;
								}
							}
							if (list4.Count == 0 && (mode & TileHandler.CutMode.CutExtra) == (TileHandler.CutMode)0 && (mode & TileHandler.CutMode.CutAll) != (TileHandler.CutMode)0 && k == -1)
							{
								list7.Add(list6.Count);
								list7.Add(list6.Count + 1);
								list7.Add(list6.Count + 2);
								list6.Add(int2);
								list6.Add(int3);
								list6.Add(int4);
							}
							else
							{
								list8.Clear();
								if (k == -1)
								{
									list8.Add(new IntPoint((long)int2.x, (long)int2.z));
									list8.Add(new IntPoint((long)int3.x, (long)int3.z));
									list8.Add(new IntPoint((long)int4.x, (long)int4.z));
								}
								else
								{
									array[0] = int2;
									array[1] = int3;
									array[2] = int4;
									int num3 = this.ClipAgainstRectangle(array, clipOut, size);
									if (num3 == 0)
									{
										goto IL_8D1;
									}
									for (int n = 0; n < num3; n++)
									{
										list8.Add(new IntPoint((long)array[n].x, (long)array[n].z));
									}
								}
								dictionary.Clear();
								for (int num4 = 0; num4 < 16; num4++)
								{
									if ((mode >> (num4 & 31) & TileHandler.CutMode.CutAll) != (TileHandler.CutMode)0)
									{
										if (1 << num4 == 1)
										{
											this.CutAll(list8, list4, list5, polyTree);
										}
										else if (1 << num4 == 2)
										{
											if (!flag)
											{
												goto IL_8C2;
											}
											this.CutDual(list8, list4, list5, flag, intermediateResult, polyTree);
										}
										else if (1 << num4 == 4)
										{
											this.CutExtra(list8, list, polyTree);
										}
										for (int num5 = 0; num5 < polyTree.ChildCount; num5++)
										{
											PolyNode polyNode = polyTree.Childs[num5];
											List<IntPoint> contour = polyNode.Contour;
											List<PolyNode> childs = polyNode.Childs;
											if (childs.Count == 0 && contour.Count == 3 && k == -1)
											{
												for (int num6 = 0; num6 < 3; num6++)
												{
													Int3 int7 = new Int3((int)contour[num6].X, 0, (int)contour[num6].Y);
													int7.y = Polygon.SampleYCoordinateInTriangle(int2, int3, int4, int7);
													list7.Add(list6.Count);
													list6.Add(int7);
												}
											}
											else
											{
												Polygon polygon = null;
												int num7 = -1;
												for (List<IntPoint> list10 = contour; list10 != null; list10 = ((num7 < childs.Count) ? childs[num7].Contour : null))
												{
													list9.Clear();
													for (int num8 = 0; num8 < list10.Count; num8++)
													{
														PolygonPoint polygonPoint = new PolygonPoint((double)list10[num8].X, (double)list10[num8].Y);
														list9.Add(polygonPoint);
														Int3 int8 = new Int3((int)list10[num8].X, 0, (int)list10[num8].Y);
														int8.y = Polygon.SampleYCoordinateInTriangle(int2, int3, int4, int8);
														dictionary[polygonPoint] = list6.Count;
														list6.Add(int8);
													}
													Polygon polygon2;
													if (stack.Count > 0)
													{
														polygon2 = stack.Pop();
														polygon2.AddPoints(list9);
													}
													else
													{
														polygon2 = new Polygon(list9);
													}
													if (num7 == -1)
													{
														polygon = polygon2;
													}
													else
													{
														polygon.AddHole(polygon2);
													}
													num7++;
												}
												try
												{
													P2T.Triangulate(polygon);
												}
												catch (PointOnEdgeException)
												{
													Debug.LogWarning("PointOnEdgeException, perturbating vertices slightly.\nThis is usually fine. It happens sometimes because of rounding errors. Cutting will be retried a few more times.");
													return this.CutPoly(verts, tris, extraShape, graphTransform, tiles, mode, perturbate + 1);
												}
												try
												{
													for (int num9 = 0; num9 < polygon.Triangles.Count; num9++)
													{
														DelaunayTriangle delaunayTriangle = polygon.Triangles[num9];
														list7.Add(dictionary[delaunayTriangle.Points._0]);
														list7.Add(dictionary[delaunayTriangle.Points._1]);
														list7.Add(dictionary[delaunayTriangle.Points._2]);
													}
												}
												catch (KeyNotFoundException)
												{
													Debug.LogWarning("KeyNotFoundException, perturbating vertices slightly.\nThis is usually fine. It happens sometimes because of rounding errors. Cutting will be retried a few more times.");
													return this.CutPoly(verts, tris, extraShape, graphTransform, tiles, mode, perturbate + 1);
												}
												TileHandler.PoolPolygon(polygon, stack);
											}
										}
									}
									IL_8C2:;
								}
							}
						}
						IL_8D1:;
					}
				}
				if (array2 != null)
				{
					ArrayPool<Int3>.Release(ref array2, false);
				}
				StackPool<Polygon>.Release(stack);
				ListPool<List<IntPoint>>.Release(ref intermediateResult);
				ListPool<IntPoint>.Release(ref list8);
				ListPool<PolygonPoint>.Release(ref list9);
			}
			TileHandler.CuttingResult result2 = default(TileHandler.CuttingResult);
			Polygon.CompressMesh(list6, list7, out result2.verts, out result2.tris);
			for (int num10 = 0; num10 < list2.Count; num10++)
			{
				list2[num10].UsedForCut();
			}
			ListPool<Int3>.Release(ref list6);
			ListPool<int>.Release(ref list7);
			ListPool<int>.Release(ref list4);
			for (int num11 = 0; num11 < list5.Count; num11++)
			{
				ListPool<IntPoint>.Release(list5[num11].contour);
			}
			ListPool<TileHandler.Cut>.Release(ref list5);
			ListPool<NavmeshCut>.Release(ref list2);
			return result2;
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x001C34C8 File Offset: 0x001C16C8
		private static List<TileHandler.Cut> PrepareNavmeshCutsForCutting(List<NavmeshCut> navmeshCuts, GraphTransform transform, IntRect cutSpaceBounds, int perturbate, bool anyNavmeshAdds)
		{
			System.Random random = null;
			if (perturbate > 0)
			{
				random = new System.Random();
			}
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			List<TileHandler.Cut> list2 = ListPool<TileHandler.Cut>.Claim();
			for (int i = 0; i < navmeshCuts.Count; i++)
			{
				Int2 @int = new Int2(0, 0);
				if (perturbate > 0)
				{
					@int.x = random.Next() % 6 * perturbate - 3 * perturbate;
					if (@int.x >= 0)
					{
						@int.x++;
					}
					@int.y = random.Next() % 6 * perturbate - 3 * perturbate;
					if (@int.y >= 0)
					{
						@int.y++;
					}
				}
				list.Clear();
				navmeshCuts[i].GetContour(list);
				int num = (int)(navmeshCuts[i].GetY(transform) * 1000f);
				for (int j = 0; j < list.Count; j++)
				{
					List<Vector3> list3 = list[j];
					if (list3.Count == 0)
					{
						Debug.LogError("A NavmeshCut component had a zero length contour. Ignoring that contour.");
					}
					else
					{
						List<IntPoint> list4 = ListPool<IntPoint>.Claim(list3.Count);
						for (int k = 0; k < list3.Count; k++)
						{
							Int3 int2 = (Int3)transform.InverseTransform(list3[k]);
							if (perturbate > 0)
							{
								int2.x += @int.x;
								int2.z += @int.y;
							}
							list4.Add(new IntPoint((long)int2.x, (long)int2.z));
						}
						IntRect bounds = new IntRect((int)list4[0].X, (int)list4[0].Y, (int)list4[0].X, (int)list4[0].Y);
						for (int l = 0; l < list4.Count; l++)
						{
							IntPoint intPoint = list4[l];
							bounds = bounds.ExpandToContain((int)intPoint.X, (int)intPoint.Y);
						}
						TileHandler.Cut cut = new TileHandler.Cut();
						int num2 = (int)(navmeshCuts[i].height * 0.5f * 1000f);
						cut.boundsY = new Int2(num - num2, num + num2);
						cut.bounds = bounds;
						cut.isDual = navmeshCuts[i].isDual;
						cut.cutsAddedGeom = navmeshCuts[i].cutsAddedGeom;
						cut.contour = list4;
						list2.Add(cut);
					}
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
			return list2;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x001C3744 File Offset: 0x001C1944
		private static void PoolPolygon(Polygon polygon, Stack<Polygon> pool)
		{
			if (polygon.Holes != null)
			{
				for (int i = 0; i < polygon.Holes.Count; i++)
				{
					polygon.Holes[i].Points.Clear();
					polygon.Holes[i].ClearTriangles();
					if (polygon.Holes[i].Holes != null)
					{
						polygon.Holes[i].Holes.Clear();
					}
					pool.Push(polygon.Holes[i]);
				}
			}
			polygon.ClearTriangles();
			if (polygon.Holes != null)
			{
				polygon.Holes.Clear();
			}
			polygon.Points.Clear();
			pool.Push(polygon);
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x001C37FC File Offset: 0x001C19FC
		private void CutAll(List<IntPoint> poly, List<int> intersectingCutIndices, List<TileHandler.Cut> cuts, PolyTree result)
		{
			this.clipper.Clear();
			this.clipper.AddPolygon(poly, PolyType.ptSubject);
			for (int i = 0; i < intersectingCutIndices.Count; i++)
			{
				this.clipper.AddPolygon(cuts[intersectingCutIndices[i]].contour, PolyType.ptClip);
			}
			result.Clear();
			this.clipper.Execute(ClipType.ctDifference, result, PolyFillType.pftNonZero, PolyFillType.pftNonZero);
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x001C386C File Offset: 0x001C1A6C
		private void CutDual(List<IntPoint> poly, List<int> tmpIntersectingCuts, List<TileHandler.Cut> cuts, bool hasDual, List<List<IntPoint>> intermediateResult, PolyTree result)
		{
			this.clipper.Clear();
			this.clipper.AddPolygon(poly, PolyType.ptSubject);
			for (int i = 0; i < tmpIntersectingCuts.Count; i++)
			{
				if (cuts[tmpIntersectingCuts[i]].isDual)
				{
					this.clipper.AddPolygon(cuts[tmpIntersectingCuts[i]].contour, PolyType.ptClip);
				}
			}
			this.clipper.Execute(ClipType.ctIntersection, intermediateResult, PolyFillType.pftEvenOdd, PolyFillType.pftNonZero);
			this.clipper.Clear();
			if (intermediateResult != null)
			{
				for (int j = 0; j < intermediateResult.Count; j++)
				{
					this.clipper.AddPolygon(intermediateResult[j], Clipper.Orientation(intermediateResult[j]) ? PolyType.ptClip : PolyType.ptSubject);
				}
			}
			for (int k = 0; k < tmpIntersectingCuts.Count; k++)
			{
				if (!cuts[tmpIntersectingCuts[k]].isDual)
				{
					this.clipper.AddPolygon(cuts[tmpIntersectingCuts[k]].contour, PolyType.ptClip);
				}
			}
			result.Clear();
			this.clipper.Execute(ClipType.ctDifference, result, PolyFillType.pftEvenOdd, PolyFillType.pftNonZero);
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x001C398B File Offset: 0x001C1B8B
		private void CutExtra(List<IntPoint> poly, List<IntPoint> extraClipShape, PolyTree result)
		{
			this.clipper.Clear();
			this.clipper.AddPolygon(poly, PolyType.ptSubject);
			this.clipper.AddPolygon(extraClipShape, PolyType.ptClip);
			result.Clear();
			this.clipper.Execute(ClipType.ctIntersection, result, PolyFillType.pftEvenOdd, PolyFillType.pftNonZero);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x001C39CC File Offset: 0x001C1BCC
		private int ClipAgainstRectangle(Int3[] clipIn, Int3[] clipOut, Int2 size)
		{
			int num = this.simpleClipper.ClipPolygon(clipIn, 3, clipOut, 1, 0, 0);
			if (num == 0)
			{
				return num;
			}
			num = this.simpleClipper.ClipPolygon(clipOut, num, clipIn, -1, size.x, 0);
			if (num == 0)
			{
				return num;
			}
			num = this.simpleClipper.ClipPolygon(clipIn, num, clipOut, 1, 0, 2);
			if (num == 0)
			{
				return num;
			}
			return this.simpleClipper.ClipPolygon(clipOut, num, clipIn, -1, size.y, 2);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x001C3A48 File Offset: 0x001C1C48
		private static void CopyMesh(Int3[] vertices, int[] triangles, List<Int3> outVertices, List<int> outTriangles)
		{
			outTriangles.Capacity = Math.Max(outTriangles.Capacity, triangles.Length);
			outVertices.Capacity = Math.Max(outVertices.Capacity, vertices.Length);
			for (int i = 0; i < vertices.Length; i++)
			{
				outVertices.Add(vertices[i]);
			}
			for (int j = 0; j < triangles.Length; j++)
			{
				outTriangles.Add(triangles[j]);
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x001C3AB0 File Offset: 0x001C1CB0
		private void DelaunayRefinement(Int3[] verts, int[] tris, ref int tCount, bool delaunay, bool colinear)
		{
			if (tCount % 3 != 0)
			{
				throw new ArgumentException("Triangle array length must be a multiple of 3");
			}
			Dictionary<Int2, int> dictionary = this.cached_Int2_int_dict;
			dictionary.Clear();
			for (int i = 0; i < tCount; i += 3)
			{
				if (!VectorMath.IsClockwiseXZ(verts[tris[i]], verts[tris[i + 1]], verts[tris[i + 2]]))
				{
					int num = tris[i];
					tris[i] = tris[i + 2];
					tris[i + 2] = num;
				}
				dictionary[new Int2(tris[i], tris[i + 1])] = i + 2;
				dictionary[new Int2(tris[i + 1], tris[i + 2])] = i;
				dictionary[new Int2(tris[i + 2], tris[i])] = i + 1;
			}
			for (int j = 0; j < tCount; j += 3)
			{
				for (int k = 0; k < 3; k++)
				{
					int num2;
					if (dictionary.TryGetValue(new Int2(tris[j + (k + 1) % 3], tris[j + k % 3]), out num2))
					{
						Int3 @int = verts[tris[j + (k + 2) % 3]];
						Int3 int2 = verts[tris[j + (k + 1) % 3]];
						Int3 int3 = verts[tris[j + (k + 3) % 3]];
						Int3 int4 = verts[tris[num2]];
						@int.y = 0;
						int2.y = 0;
						int3.y = 0;
						int4.y = 0;
						bool flag = false;
						if (!VectorMath.RightOrColinearXZ(@int, int3, int4) || VectorMath.RightXZ(@int, int2, int4))
						{
							if (!colinear)
							{
								goto IL_3C6;
							}
							flag = true;
						}
						if (colinear && VectorMath.SqrDistancePointSegmentApproximate(@int, int4, int2) < 9f && !dictionary.ContainsKey(new Int2(tris[j + (k + 2) % 3], tris[j + (k + 1) % 3])) && !dictionary.ContainsKey(new Int2(tris[j + (k + 1) % 3], tris[num2])))
						{
							tCount -= 3;
							int num3 = num2 / 3 * 3;
							tris[j + (k + 1) % 3] = tris[num2];
							if (num3 != tCount)
							{
								tris[num3] = tris[tCount];
								tris[num3 + 1] = tris[tCount + 1];
								tris[num3 + 2] = tris[tCount + 2];
								dictionary[new Int2(tris[num3], tris[num3 + 1])] = num3 + 2;
								dictionary[new Int2(tris[num3 + 1], tris[num3 + 2])] = num3;
								dictionary[new Int2(tris[num3 + 2], tris[num3])] = num3 + 1;
								tris[tCount] = 0;
								tris[tCount + 1] = 0;
								tris[tCount + 2] = 0;
							}
							else
							{
								tCount += 3;
							}
							dictionary[new Int2(tris[j], tris[j + 1])] = j + 2;
							dictionary[new Int2(tris[j + 1], tris[j + 2])] = j;
							dictionary[new Int2(tris[j + 2], tris[j])] = j + 1;
						}
						else if (delaunay && !flag)
						{
							float num4 = Int3.Angle(int2 - @int, int3 - @int);
							if (Int3.Angle(int2 - int4, int3 - int4) > 6.28318548f - 2f * num4)
							{
								tris[j + (k + 1) % 3] = tris[num2];
								int num5 = num2 / 3 * 3;
								int num6 = num2 - num5;
								tris[num5 + (num6 - 1 + 3) % 3] = tris[j + (k + 2) % 3];
								dictionary[new Int2(tris[j], tris[j + 1])] = j + 2;
								dictionary[new Int2(tris[j + 1], tris[j + 2])] = j;
								dictionary[new Int2(tris[j + 2], tris[j])] = j + 1;
								dictionary[new Int2(tris[num5], tris[num5 + 1])] = num5 + 2;
								dictionary[new Int2(tris[num5 + 1], tris[num5 + 2])] = num5;
								dictionary[new Int2(tris[num5 + 2], tris[num5])] = num5 + 1;
							}
						}
					}
					IL_3C6:;
				}
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x001C3EA0 File Offset: 0x001C20A0
		public void ClearTile(int x, int z)
		{
			if (AstarPath.active == null)
			{
				return;
			}
			if (x < 0 || z < 0 || x >= this.tileXCount || z >= this.tileZCount)
			{
				return;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext context, bool force)
			{
				this.graph.ReplaceTile(x, z, new Int3[0], new int[0]);
				this.activeTileTypes[x + z * this.tileXCount] = null;
				if (!this.isBatching)
				{
					GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
				}
				context.QueueFloodFill();
				return true;
			}));
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x001C3F20 File Offset: 0x001C2120
		public void ReloadInBounds(Bounds bounds)
		{
			this.ReloadInBounds(this.graph.GetTouchingTiles(bounds));
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x001C3F34 File Offset: 0x001C2134
		public void ReloadInBounds(IntRect tiles)
		{
			tiles = IntRect.Intersection(tiles, new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
			if (!tiles.IsValid())
			{
				return;
			}
			for (int i = tiles.ymin; i <= tiles.ymax; i++)
			{
				for (int j = tiles.xmin; j <= tiles.xmax; j++)
				{
					this.ReloadTile(j, i);
				}
			}
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x001C3FA0 File Offset: 0x001C21A0
		public void ReloadTile(int x, int z)
		{
			if (x < 0 || z < 0 || x >= this.tileXCount || z >= this.tileZCount)
			{
				return;
			}
			int num = x + z * this.tileXCount;
			if (this.activeTileTypes[num] != null)
			{
				this.LoadTile(this.activeTileTypes[num], x, z, this.activeTileRotations[num], this.activeTileOffsets[num]);
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x001C4000 File Offset: 0x001C2200
		public void LoadTile(TileHandler.TileType tile, int x, int z, int rotation, int yoffset)
		{
			if (tile == null)
			{
				throw new ArgumentNullException("tile");
			}
			if (AstarPath.active == null)
			{
				return;
			}
			int index = x + z * this.tileXCount;
			rotation %= 4;
			if (this.isBatching && this.reloadedInBatch[index] && this.activeTileOffsets[index] == yoffset && this.activeTileRotations[index] == rotation && this.activeTileTypes[index] == tile)
			{
				return;
			}
			this.reloadedInBatch[index] |= this.isBatching;
			this.activeTileOffsets[index] = yoffset;
			this.activeTileRotations[index] = rotation;
			this.activeTileTypes[index] = tile;
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext context, bool force)
			{
				if (this.activeTileOffsets[index] != yoffset || this.activeTileRotations[index] != rotation || this.activeTileTypes[index] != tile)
				{
					return true;
				}
				GraphModifier.TriggerEvent(GraphModifier.EventType.PreUpdate);
				Int3[] verts;
				int[] tris;
				tile.Load(out verts, out tris, rotation, yoffset);
				TileHandler.CuttingResult cuttingResult = this.CutPoly(verts, tris, null, this.graph.transform, new IntRect(x, z, x + tile.Width - 1, z + tile.Depth - 1), TileHandler.CutMode.CutAll | TileHandler.CutMode.CutDual, -1);
				int num = cuttingResult.tris.Length;
				this.DelaunayRefinement(cuttingResult.verts, cuttingResult.tris, ref num, true, false);
				if (num != cuttingResult.tris.Length)
				{
					cuttingResult.tris = Memory.ShrinkArray<int>(cuttingResult.tris, num);
				}
				int num2 = (rotation % 2 == 0) ? tile.Width : tile.Depth;
				int num3 = (rotation % 2 == 0) ? tile.Depth : tile.Width;
				if (num2 != 1 || num3 != 1)
				{
					throw new Exception("Only tiles of width = depth = 1 are supported at this time");
				}
				this.graph.ReplaceTile(x, z, cuttingResult.verts, cuttingResult.tris);
				if (!this.isBatching)
				{
					GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
				}
				context.QueueFloodFill();
				return true;
			}));
		}

		// Token: 0x040043FB RID: 17403
		public readonly NavmeshBase graph;

		// Token: 0x040043FC RID: 17404
		private readonly int tileXCount;

		// Token: 0x040043FD RID: 17405
		private readonly int tileZCount;

		// Token: 0x040043FE RID: 17406
		private readonly Clipper clipper = new Clipper(0);

		// Token: 0x040043FF RID: 17407
		private readonly Dictionary<Int2, int> cached_Int2_int_dict = new Dictionary<Int2, int>();

		// Token: 0x04004400 RID: 17408
		private readonly TileHandler.TileType[] activeTileTypes;

		// Token: 0x04004401 RID: 17409
		private readonly int[] activeTileRotations;

		// Token: 0x04004402 RID: 17410
		private readonly int[] activeTileOffsets;

		// Token: 0x04004403 RID: 17411
		private readonly bool[] reloadedInBatch;

		// Token: 0x04004404 RID: 17412
		public readonly GridLookup<NavmeshClipper> cuts;

		// Token: 0x04004405 RID: 17413
		private bool isBatching;

		// Token: 0x04004406 RID: 17414
		private readonly VoxelPolygonClipper simpleClipper;

		// Token: 0x0200078E RID: 1934
		public class TileType
		{
			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x06002DFD RID: 11773 RVA: 0x001D5D00 File Offset: 0x001D3F00
			public int Width
			{
				get
				{
					return this.width;
				}
			}

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x06002DFE RID: 11774 RVA: 0x001D5D08 File Offset: 0x001D3F08
			public int Depth
			{
				get
				{
					return this.depth;
				}
			}

			// Token: 0x06002DFF RID: 11775 RVA: 0x001D5D10 File Offset: 0x001D3F10
			public TileType(Int3[] sourceVerts, int[] sourceTris, Int3 tileSize, Int3 centerOffset, int width = 1, int depth = 1)
			{
				if (sourceVerts == null)
				{
					throw new ArgumentNullException("sourceVerts");
				}
				if (sourceTris == null)
				{
					throw new ArgumentNullException("sourceTris");
				}
				this.tris = new int[sourceTris.Length];
				for (int i = 0; i < this.tris.Length; i++)
				{
					this.tris[i] = sourceTris[i];
				}
				this.verts = new Int3[sourceVerts.Length];
				for (int j = 0; j < sourceVerts.Length; j++)
				{
					this.verts[j] = sourceVerts[j] + centerOffset;
				}
				this.offset = tileSize / 2f;
				this.offset.x = this.offset.x * width;
				this.offset.z = this.offset.z * depth;
				this.offset.y = 0;
				for (int k = 0; k < sourceVerts.Length; k++)
				{
					this.verts[k] = this.verts[k] + this.offset;
				}
				this.lastRotation = 0;
				this.lastYOffset = 0;
				this.width = width;
				this.depth = depth;
			}

			// Token: 0x06002E00 RID: 11776 RVA: 0x001D5E30 File Offset: 0x001D4030
			public TileType(Mesh source, Int3 tileSize, Int3 centerOffset, int width = 1, int depth = 1)
			{
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				Vector3[] vertices = source.vertices;
				this.tris = source.triangles;
				this.verts = new Int3[vertices.Length];
				for (int i = 0; i < vertices.Length; i++)
				{
					this.verts[i] = (Int3)vertices[i] + centerOffset;
				}
				this.offset = tileSize / 2f;
				this.offset.x = this.offset.x * width;
				this.offset.z = this.offset.z * depth;
				this.offset.y = 0;
				for (int j = 0; j < vertices.Length; j++)
				{
					this.verts[j] = this.verts[j] + this.offset;
				}
				this.lastRotation = 0;
				this.lastYOffset = 0;
				this.width = width;
				this.depth = depth;
			}

			// Token: 0x06002E01 RID: 11777 RVA: 0x001D5F34 File Offset: 0x001D4134
			public void Load(out Int3[] verts, out int[] tris, int rotation, int yoffset)
			{
				rotation = (rotation % 4 + 4) % 4;
				int num = rotation;
				rotation = (rotation - this.lastRotation % 4 + 4) % 4;
				this.lastRotation = num;
				verts = this.verts;
				int num2 = yoffset - this.lastYOffset;
				this.lastYOffset = yoffset;
				if (rotation != 0 || num2 != 0)
				{
					for (int i = 0; i < verts.Length; i++)
					{
						Int3 @int = verts[i] - this.offset;
						Int3 lhs = @int;
						lhs.y += num2;
						lhs.x = @int.x * TileHandler.TileType.Rotations[rotation * 4] + @int.z * TileHandler.TileType.Rotations[rotation * 4 + 1];
						lhs.z = @int.x * TileHandler.TileType.Rotations[rotation * 4 + 2] + @int.z * TileHandler.TileType.Rotations[rotation * 4 + 3];
						verts[i] = lhs + this.offset;
					}
				}
				tris = this.tris;
			}

			// Token: 0x04004B64 RID: 19300
			private Int3[] verts;

			// Token: 0x04004B65 RID: 19301
			private int[] tris;

			// Token: 0x04004B66 RID: 19302
			private Int3 offset;

			// Token: 0x04004B67 RID: 19303
			private int lastYOffset;

			// Token: 0x04004B68 RID: 19304
			private int lastRotation;

			// Token: 0x04004B69 RID: 19305
			private int width;

			// Token: 0x04004B6A RID: 19306
			private int depth;

			// Token: 0x04004B6B RID: 19307
			private static readonly int[] Rotations = new int[]
			{
				1,
				0,
				0,
				1,
				0,
				1,
				-1,
				0,
				-1,
				0,
				0,
				-1,
				0,
				-1,
				1,
				0
			};
		}

		// Token: 0x0200078F RID: 1935
		[Flags]
		public enum CutMode
		{
			// Token: 0x04004B6D RID: 19309
			CutAll = 1,
			// Token: 0x04004B6E RID: 19310
			CutDual = 2,
			// Token: 0x04004B6F RID: 19311
			CutExtra = 4
		}

		// Token: 0x02000790 RID: 1936
		private class Cut
		{
			// Token: 0x04004B70 RID: 19312
			public IntRect bounds;

			// Token: 0x04004B71 RID: 19313
			public Int2 boundsY;

			// Token: 0x04004B72 RID: 19314
			public bool isDual;

			// Token: 0x04004B73 RID: 19315
			public bool cutsAddedGeom;

			// Token: 0x04004B74 RID: 19316
			public List<IntPoint> contour;
		}

		// Token: 0x02000791 RID: 1937
		private struct CuttingResult
		{
			// Token: 0x04004B75 RID: 19317
			public Int3[] verts;

			// Token: 0x04004B76 RID: 19318
			public int[] tris;
		}
	}
}
