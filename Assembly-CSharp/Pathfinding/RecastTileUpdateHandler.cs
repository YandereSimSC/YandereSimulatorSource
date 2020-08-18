using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055E RID: 1374
	[AddComponentMenu("Pathfinding/Navmesh/RecastTileUpdateHandler")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_tile_update_handler.php")]
	public class RecastTileUpdateHandler : MonoBehaviour
	{
		// Token: 0x06002448 RID: 9288 RVA: 0x00196F13 File Offset: 0x00195113
		public void SetGraph(RecastGraph graph)
		{
			this.graph = graph;
			if (graph == null)
			{
				return;
			}
			this.dirtyTiles = new bool[graph.tileXCount * graph.tileZCount];
			this.anyDirtyTiles = false;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00196F40 File Offset: 0x00195140
		public void ScheduleUpdate(Bounds bounds)
		{
			if (this.graph == null)
			{
				if (AstarPath.active != null)
				{
					this.SetGraph(AstarPath.active.data.recastGraph);
				}
				if (this.graph == null)
				{
					Debug.LogError("Received tile update request (from RecastTileUpdate), but no RecastGraph could be found to handle it");
					return;
				}
			}
			int num = Mathf.CeilToInt(this.graph.characterRadius / this.graph.cellSize) + 3;
			bounds.Expand(new Vector3((float)num, 0f, (float)num) * this.graph.cellSize * 2f);
			IntRect touchingTiles = this.graph.GetTouchingTiles(bounds);
			if (touchingTiles.Width * touchingTiles.Height > 0)
			{
				if (!this.anyDirtyTiles)
				{
					this.earliestDirty = Time.time;
					this.anyDirtyTiles = true;
				}
				for (int i = touchingTiles.ymin; i <= touchingTiles.ymax; i++)
				{
					for (int j = touchingTiles.xmin; j <= touchingTiles.xmax; j++)
					{
						this.dirtyTiles[i * this.graph.tileXCount + j] = true;
					}
				}
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00197054 File Offset: 0x00195254
		private void OnEnable()
		{
			RecastTileUpdate.OnNeedUpdates += this.ScheduleUpdate;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00197067 File Offset: 0x00195267
		private void OnDisable()
		{
			RecastTileUpdate.OnNeedUpdates -= this.ScheduleUpdate;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0019707A File Offset: 0x0019527A
		private void Update()
		{
			if (this.anyDirtyTiles && Time.time - this.earliestDirty >= this.maxThrottlingDelay && this.graph != null)
			{
				this.UpdateDirtyTiles();
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x001970A8 File Offset: 0x001952A8
		public void UpdateDirtyTiles()
		{
			if (this.graph == null)
			{
				new InvalidOperationException("No graph is set on this object");
			}
			if (this.graph.tileXCount * this.graph.tileZCount != this.dirtyTiles.Length)
			{
				Debug.LogError("Graph has changed dimensions. Clearing queued graph updates and resetting.");
				this.SetGraph(this.graph);
				return;
			}
			for (int i = 0; i < this.graph.tileZCount; i++)
			{
				for (int j = 0; j < this.graph.tileXCount; j++)
				{
					if (this.dirtyTiles[i * this.graph.tileXCount + j])
					{
						this.dirtyTiles[i * this.graph.tileXCount + j] = false;
						Bounds tileBounds = this.graph.GetTileBounds(j, i, 1, 1);
						tileBounds.extents *= 0.5f;
						GraphUpdateObject graphUpdateObject = new GraphUpdateObject(tileBounds);
						graphUpdateObject.nnConstraint.graphMask = 1 << (int)this.graph.graphIndex;
						AstarPath.active.UpdateGraphs(graphUpdateObject);
					}
				}
			}
			this.anyDirtyTiles = false;
		}

		// Token: 0x04004029 RID: 16425
		private RecastGraph graph;

		// Token: 0x0400402A RID: 16426
		private bool[] dirtyTiles;

		// Token: 0x0400402B RID: 16427
		private bool anyDirtyTiles;

		// Token: 0x0400402C RID: 16428
		private float earliestDirty = float.NegativeInfinity;

		// Token: 0x0400402D RID: 16429
		public float maxThrottlingDelay = 0.5f;
	}
}
