using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200058C RID: 1420
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_tile_handler_helper.php")]
	public class TileHandlerHelper : VersionedMonoBehaviour
	{
		// Token: 0x060026A5 RID: 9893 RVA: 0x001A8D18 File Offset: 0x001A6F18
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

		// Token: 0x060026A6 RID: 9894 RVA: 0x001A8DE4 File Offset: 0x001A6FE4
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

		// Token: 0x060026A7 RID: 9895 RVA: 0x001A8E50 File Offset: 0x001A7050
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

		// Token: 0x060026A8 RID: 9896 RVA: 0x001A8EBC File Offset: 0x001A70BC
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

		// Token: 0x060026A9 RID: 9897 RVA: 0x001A8F11 File Offset: 0x001A7111
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

		// Token: 0x060026AA RID: 9898 RVA: 0x001A8F48 File Offset: 0x001A7148
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

		// Token: 0x060026AB RID: 9899 RVA: 0x001A8F9B File Offset: 0x001A719B
		private void OnRecalculatedTiles(NavmeshTile[] tiles)
		{
			if (!this.handler.isValid)
			{
				this.UseSpecifiedHandler(new TileHandler(this.handler.graph));
			}
			this.handler.OnRecalculatedTiles(tiles);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x001A8FCC File Offset: 0x001A71CC
		private void HandleOnEnableCallback(NavmeshClipper obj)
		{
			Rect bounds = obj.GetBounds(this.handler.graph.transform);
			IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
			this.handler.cuts.Add(obj, touchingTilesInGraphSpace);
			obj.ForceUpdate();
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x001A901C File Offset: 0x001A721C
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

		// Token: 0x060026AE RID: 9902 RVA: 0x001A906C File Offset: 0x001A726C
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

		// Token: 0x060026AF RID: 9903 RVA: 0x001A90D0 File Offset: 0x001A72D0
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

		// Token: 0x04004182 RID: 16770
		private TileHandler handler;

		// Token: 0x04004183 RID: 16771
		public float updateInterval;

		// Token: 0x04004184 RID: 16772
		private float lastUpdateTime = float.NegativeInfinity;

		// Token: 0x04004185 RID: 16773
		private readonly List<IntRect> forcedReloadRects = new List<IntRect>();
	}
}
