using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005FE RID: 1534
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_procedural_world.php")]
	public class ProceduralWorld : MonoBehaviour
	{
		// Token: 0x06002A0D RID: 10765 RVA: 0x001C6B25 File Offset: 0x001C4D25
		private void Start()
		{
			this.Update();
			AstarPath.active.Scan(null);
			base.StartCoroutine(this.GenerateTiles());
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x001C6B48 File Offset: 0x001C4D48
		private void Update()
		{
			Int2 @int = new Int2(Mathf.RoundToInt((this.target.position.x - this.tileSize * 0.5f) / this.tileSize), Mathf.RoundToInt((this.target.position.z - this.tileSize * 0.5f) / this.tileSize));
			this.range = ((this.range < 1) ? 1 : this.range);
			bool flag = true;
			while (flag)
			{
				flag = false;
				foreach (KeyValuePair<Int2, ProceduralWorld.ProceduralTile> keyValuePair in this.tiles)
				{
					if (Mathf.Abs(keyValuePair.Key.x - @int.x) > this.range || Mathf.Abs(keyValuePair.Key.y - @int.y) > this.range)
					{
						keyValuePair.Value.Destroy();
						this.tiles.Remove(keyValuePair.Key);
						flag = true;
						break;
					}
				}
			}
			for (int i = @int.x - this.range; i <= @int.x + this.range; i++)
			{
				for (int j = @int.y - this.range; j <= @int.y + this.range; j++)
				{
					if (!this.tiles.ContainsKey(new Int2(i, j)))
					{
						ProceduralWorld.ProceduralTile proceduralTile = new ProceduralWorld.ProceduralTile(this, i, j);
						IEnumerator enumerator2 = proceduralTile.Generate();
						enumerator2.MoveNext();
						this.tileGenerationQueue.Enqueue(enumerator2);
						this.tiles.Add(new Int2(i, j), proceduralTile);
					}
				}
			}
			for (int k = @int.x - 1; k <= @int.x + 1; k++)
			{
				for (int l = @int.y - 1; l <= @int.y + 1; l++)
				{
					this.tiles[new Int2(k, l)].ForceFinish();
				}
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x001C6D78 File Offset: 0x001C4F78
		private IEnumerator GenerateTiles()
		{
			for (;;)
			{
				if (this.tileGenerationQueue.Count > 0)
				{
					IEnumerator routine = this.tileGenerationQueue.Dequeue();
					yield return base.StartCoroutine(routine);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x0400445F RID: 17503
		public Transform target;

		// Token: 0x04004460 RID: 17504
		public ProceduralWorld.ProceduralPrefab[] prefabs;

		// Token: 0x04004461 RID: 17505
		public int range = 1;

		// Token: 0x04004462 RID: 17506
		public float tileSize = 100f;

		// Token: 0x04004463 RID: 17507
		public int subTiles = 20;

		// Token: 0x04004464 RID: 17508
		public bool staticBatching;

		// Token: 0x04004465 RID: 17509
		private Queue<IEnumerator> tileGenerationQueue = new Queue<IEnumerator>();

		// Token: 0x04004466 RID: 17510
		private Dictionary<Int2, ProceduralWorld.ProceduralTile> tiles = new Dictionary<Int2, ProceduralWorld.ProceduralTile>();

		// Token: 0x0200079A RID: 1946
		[Serializable]
		public class ProceduralPrefab
		{
			// Token: 0x04004B99 RID: 19353
			public GameObject prefab;

			// Token: 0x04004B9A RID: 19354
			public float density;

			// Token: 0x04004B9B RID: 19355
			public float perlin;

			// Token: 0x04004B9C RID: 19356
			public float perlinPower = 1f;

			// Token: 0x04004B9D RID: 19357
			public Vector2 perlinOffset = Vector2.zero;

			// Token: 0x04004B9E RID: 19358
			public float perlinScale = 1f;

			// Token: 0x04004B9F RID: 19359
			public float random = 1f;

			// Token: 0x04004BA0 RID: 19360
			public bool singleFixed;
		}

		// Token: 0x0200079B RID: 1947
		private class ProceduralTile
		{
			// Token: 0x1700069A RID: 1690
			// (get) Token: 0x06002E23 RID: 11811 RVA: 0x001D6DAF File Offset: 0x001D4FAF
			// (set) Token: 0x06002E24 RID: 11812 RVA: 0x001D6DB7 File Offset: 0x001D4FB7
			public bool destroyed { get; private set; }

			// Token: 0x06002E25 RID: 11813 RVA: 0x001D6DC0 File Offset: 0x001D4FC0
			public ProceduralTile(ProceduralWorld world, int x, int z)
			{
				this.x = x;
				this.z = z;
				this.world = world;
				this.rnd = new System.Random(x * 10007 ^ z * 36007);
			}

			// Token: 0x06002E26 RID: 11814 RVA: 0x001D6DF7 File Offset: 0x001D4FF7
			public IEnumerator Generate()
			{
				this.ie = this.InternalGenerate();
				GameObject gameObject = new GameObject(string.Concat(new object[]
				{
					"Tile ",
					this.x,
					" ",
					this.z
				}));
				this.root = gameObject.transform;
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
					yield return this.ie.Current;
				}
				this.ie = null;
				yield break;
			}

			// Token: 0x06002E27 RID: 11815 RVA: 0x001D6E06 File Offset: 0x001D5006
			public void ForceFinish()
			{
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
				}
				this.ie = null;
			}

			// Token: 0x06002E28 RID: 11816 RVA: 0x001D6E34 File Offset: 0x001D5034
			private Vector3 RandomInside()
			{
				return new Vector3
				{
					x = ((float)this.x + (float)this.rnd.NextDouble()) * this.world.tileSize,
					z = ((float)this.z + (float)this.rnd.NextDouble()) * this.world.tileSize
				};
			}

			// Token: 0x06002E29 RID: 11817 RVA: 0x001D6E98 File Offset: 0x001D5098
			private Vector3 RandomInside(float px, float pz)
			{
				return new Vector3
				{
					x = (px + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize,
					z = (pz + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize
				};
			}

			// Token: 0x06002E2A RID: 11818 RVA: 0x001D6F0A File Offset: 0x001D510A
			private Quaternion RandomYRot()
			{
				return Quaternion.Euler(360f * (float)this.rnd.NextDouble(), 0f, 360f * (float)this.rnd.NextDouble());
			}

			// Token: 0x06002E2B RID: 11819 RVA: 0x001D6F3A File Offset: 0x001D513A
			private IEnumerator InternalGenerate()
			{
				Debug.Log(string.Concat(new object[]
				{
					"Generating tile ",
					this.x,
					", ",
					this.z
				}));
				int counter = 0;
				float[,] ditherMap = new float[this.world.subTiles + 2, this.world.subTiles + 2];
				int num3;
				for (int i = 0; i < this.world.prefabs.Length; i = num3 + 1)
				{
					ProceduralWorld.ProceduralPrefab pref = this.world.prefabs[i];
					if (pref.singleFixed)
					{
						Vector3 position = new Vector3(((float)this.x + 0.5f) * this.world.tileSize, 0f, ((float)this.z + 0.5f) * this.world.tileSize);
						UnityEngine.Object.Instantiate<GameObject>(pref.prefab, position, Quaternion.identity).transform.parent = this.root;
					}
					else
					{
						float subSize = this.world.tileSize / (float)this.world.subTiles;
						for (int k = 0; k < this.world.subTiles; k++)
						{
							for (int l = 0; l < this.world.subTiles; l++)
							{
								ditherMap[k + 1, l + 1] = 0f;
							}
						}
						for (int sx = 0; sx < this.world.subTiles; sx = num3 + 1)
						{
							for (int sz = 0; sz < this.world.subTiles; sz = num3 + 1)
							{
								float px = (float)this.x + (float)sx / (float)this.world.subTiles;
								float pz = (float)this.z + (float)sz / (float)this.world.subTiles;
								float b = Mathf.Pow(Mathf.PerlinNoise((px + pref.perlinOffset.x) * pref.perlinScale, (pz + pref.perlinOffset.y) * pref.perlinScale), pref.perlinPower);
								float num = pref.density * Mathf.Lerp(1f, b, pref.perlin) * Mathf.Lerp(1f, (float)this.rnd.NextDouble(), pref.random);
								float num2 = subSize * subSize * num + ditherMap[sx + 1, sz + 1];
								int count = Mathf.RoundToInt(num2);
								ditherMap[sx + 1 + 1, sz + 1] += 0.4375f * (num2 - (float)count);
								ditherMap[sx + 1 - 1, sz + 1 + 1] += 0.1875f * (num2 - (float)count);
								ditherMap[sx + 1, sz + 1 + 1] += 0.3125f * (num2 - (float)count);
								ditherMap[sx + 1 + 1, sz + 1 + 1] += 0.0625f * (num2 - (float)count);
								for (int j = 0; j < count; j = num3 + 1)
								{
									Vector3 position2 = this.RandomInside(px, pz);
									UnityEngine.Object.Instantiate<GameObject>(pref.prefab, position2, this.RandomYRot()).transform.parent = this.root;
									num3 = counter;
									counter = num3 + 1;
									if (counter % 2 == 0)
									{
										yield return null;
									}
									num3 = j;
								}
								num3 = sz;
							}
							num3 = sx;
						}
					}
					pref = null;
					num3 = i;
				}
				ditherMap = null;
				yield return null;
				yield return null;
				if (Application.HasProLicense() && this.world.staticBatching)
				{
					StaticBatchingUtility.Combine(this.root.gameObject);
				}
				yield break;
			}

			// Token: 0x06002E2C RID: 11820 RVA: 0x001D6F4C File Offset: 0x001D514C
			public void Destroy()
			{
				if (this.root != null)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Destroying tile ",
						this.x,
						", ",
						this.z
					}));
					UnityEngine.Object.Destroy(this.root.gameObject);
					this.root = null;
				}
				this.ie = null;
			}

			// Token: 0x04004BA1 RID: 19361
			private int x;

			// Token: 0x04004BA2 RID: 19362
			private int z;

			// Token: 0x04004BA3 RID: 19363
			private System.Random rnd;

			// Token: 0x04004BA4 RID: 19364
			private ProceduralWorld world;

			// Token: 0x04004BA6 RID: 19366
			private Transform root;

			// Token: 0x04004BA7 RID: 19367
			private IEnumerator ie;
		}
	}
}
