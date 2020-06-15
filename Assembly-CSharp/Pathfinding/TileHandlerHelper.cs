using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005A2 RID: 1442
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_tile_handler_helper.php")]
	public class TileHandlerHelper : VersionedMonoBehaviour
	{
		// Token: 0x060026FB RID: 9979 RVA: 0x001AE904 File Offset: 0x001ACB04
		public void UseSpecifiedHandler(TileHandler newHandler)
		{
			if (!base.enabled)
			{
				throw new InvalidOperationException("TileHandlerHelper is disabled");
			}
			if (this.handler != null)
			{
				NavmeshClipper.RemoveEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				NavmeshBase graph = this.handler.graph;
				graph.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Remove(graph.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
			this.handler = newHandler;
			if (this.handler != null)
			{
				NavmeshClipper.AddEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				NavmeshBase graph2 = this.handler.graph;
				graph2.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Combine(graph2.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x001AE9D0 File Offset: 0x001ACBD0
		private void OnEnable()
		{
			if (this.handler != null)
			{
				NavmeshClipper.AddEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				NavmeshBase graph = this.handler.graph;
				graph.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Combine(graph.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
			this.forcedReloadRects.Clear();
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x001AEA3C File Offset: 0x001ACC3C
		private void OnDisable()
		{
			if (this.handler != null)
			{
				NavmeshClipper.RemoveEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				this.forcedReloadRects.Clear();
				NavmeshBase graph = this.handler.graph;
				graph.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Remove(graph.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x001AEAA8 File Offset: 0x001ACCA8
		public void DiscardPending()
		{
			if (this.handler != null)
			{
				for (GridLookup<NavmeshClipper>.Root root = this.handler.cuts.AllItems; root != null; root = root.next)
				{
					if (root.obj.RequiresUpdate())
					{
						root.obj.NotifyUpdated();
					}
				}
			}
			this.forcedReloadRects.Clear();
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x001AEAFD File Offset: 0x001ACCFD
		private void Start()
		{
			if (UnityEngine.Object.FindObjectsOfType(typeof(TileHandlerHelper)).Length > 1)
			{
				Debug.LogError("There should only be one TileHandlerHelper per scene. Destroying.");
				UnityEngine.Object.Destroy(this);
				return;
			}
			if (this.handler == null)
			{
				this.FindGraph();
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x001AEB34 File Offset: 0x001ACD34
		private void FindGraph()
		{
			if (AstarPath.active != null)
			{
				NavmeshBase navmeshBase = AstarPath.active.data.FindGraphWhichInheritsFrom(typeof(NavmeshBase)) as NavmeshBase;
				if (navmeshBase != null)
				{
					this.UseSpecifiedHandler(new TileHandler(navmeshBase));
					this.handler.CreateTileTypesFromGraph();
				}
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x001AEB87 File Offset: 0x001ACD87
		private void OnRecalculatedTiles(NavmeshTile[] tiles)
		{
			if (!this.handler.isValid)
			{
				this.UseSpecifiedHandler(new TileHandler(this.handler.graph));
			}
			this.handler.OnRecalculatedTiles(tiles);
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x001AEBB8 File Offset: 0x001ACDB8
		private void HandleOnEnableCallback(NavmeshClipper obj)
		{
			Rect bounds = obj.GetBounds(this.handler.graph.transform);
			IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
			this.handler.cuts.Add(obj, touchingTilesInGraphSpace);
			obj.ForceUpdate();
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x001AEC08 File Offset: 0x001ACE08
		private void HandleOnDisableCallback(NavmeshClipper obj)
		{
			GridLookup<NavmeshClipper>.Root root = this.handler.cuts.GetRoot(obj);
			if (root != null)
			{
				this.forcedReloadRects.Add(root.previousBounds);
				this.handler.cuts.Remove(obj);
			}
			this.lastUpdateTime = float.NegativeInfinity;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x001AEC58 File Offset: 0x001ACE58
		private void Update()
		{
			if (this.handler == null)
			{
				this.FindGraph();
			}
			if (this.handler != null && !AstarPath.active.isScanning && ((this.updateInterval >= 0f && Time.realtimeSinceStartup - this.lastUpdateTime > this.updateInterval) || !this.handler.isValid))
			{
				this.ForceUpdate();
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x001AECBC File Offset: 0x001ACEBC
		public void ForceUpdate()
		{
			if (this.handler == null)
			{
				throw new Exception("Cannot update graphs. No TileHandler. Do not call the ForceUpdate method in Awake.");
			}
			this.lastUpdateTime = Time.realtimeSinceStartup;
			if (!this.handler.isValid)
			{
				if (!this.handler.graph.exists)
				{
					this.UseSpecifiedHandler(null);
					return;
				}
				Debug.Log("TileHandler no longer matched the underlaying graph (possibly because of a graph scan). Recreating TileHandler...");
				this.UseSpecifiedHandler(new TileHandler(this.handler.graph));
				this.handler.CreateTileTypesFromGraph();
				this.forcedReloadRects.Add(new IntRect(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue));
			}
			GridLookup<NavmeshClipper>.Root allItems = this.handler.cuts.AllItems;
			if (this.forcedReloadRects.Count == 0)
			{
				int num = 0;
				for (GridLookup<NavmeshClipper>.Root root = allItems; root != null; root = root.next)
				{
					if (root.obj.RequiresUpdate())
					{
						num++;
						break;
					}
				}
				if (num == 0)
				{
					return;
				}
			}
			bool flag = this.handler.StartBatchLoad();
			for (int i = 0; i < this.forcedReloadRects.Count; i++)
			{
				this.handler.ReloadInBounds(this.forcedReloadRects[i]);
			}
			this.forcedReloadRects.Clear();
			for (GridLookup<NavmeshClipper>.Root root2 = allItems; root2 != null; root2 = root2.next)
			{
				if (root2.obj.RequiresUpdate())
				{
					this.handler.ReloadInBounds(root2.previousBounds);
					Rect bounds = root2.obj.GetBounds(this.handler.graph.transform);
					IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
					this.handler.cuts.Move(root2.obj, touchingTilesInGraphSpace);
					this.handler.ReloadInBounds(touchingTilesInGraphSpace);
				}
			}
			for (GridLookup<NavmeshClipper>.Root root3 = allItems; root3 != null; root3 = root3.next)
			{
				if (root3.obj.RequiresUpdate())
				{
					root3.obj.NotifyUpdated();
				}
			}
			if (flag)
			{
				this.handler.EndBatchLoad();
			}
		}

		// Token: 0x04004260 RID: 16992
		private TileHandler handler;

		// Token: 0x04004261 RID: 16993
		public float updateInterval;

		// Token: 0x04004262 RID: 16994
		private float lastUpdateTime = float.NegativeInfinity;

		// Token: 0x04004263 RID: 16995
		private readonly List<IntRect> forcedReloadRects = new List<IntRect>();
	}
}
