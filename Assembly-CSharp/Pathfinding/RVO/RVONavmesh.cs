using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005C8 RID: 1480
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Navmesh")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_navmesh.php")]
	public class RVONavmesh : GraphModifier
	{
		// Token: 0x06002895 RID: 10389 RVA: 0x001B8C5C File Offset: 0x001B6E5C
		public override void OnPostCacheLoad()
		{
			this.OnLatePostScan();
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x001B8C5C File Offset: 0x001B6E5C
		public override void OnGraphsPostUpdate()
		{
			this.OnLatePostScan();
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x001B8C64 File Offset: 0x001B6E64
		public override void OnLatePostScan()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.RemoveObstacles();
			NavGraph[] graphs = AstarPath.active.graphs;
			RVOSimulator active = RVOSimulator.active;
			if (active == null)
			{
				throw new NullReferenceException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.lastSim = active.GetSimulator();
			for (int i = 0; i < graphs.Length; i++)
			{
				RecastGraph recastGraph = graphs[i] as RecastGraph;
				INavmesh navmesh = graphs[i] as INavmesh;
				GridGraph gridGraph = graphs[i] as GridGraph;
				if (recastGraph != null)
				{
					foreach (NavmeshTile navmesh2 in recastGraph.GetTiles())
					{
						this.AddGraphObstacles(this.lastSim, navmesh2);
					}
				}
				else if (navmesh != null)
				{
					this.AddGraphObstacles(this.lastSim, navmesh);
				}
				else if (gridGraph != null)
				{
					this.AddGraphObstacles(this.lastSim, gridGraph);
				}
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x001B8D39 File Offset: 0x001B6F39
		protected override void OnDisable()
		{
			base.OnDisable();
			this.RemoveObstacles();
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x001B8D48 File Offset: 0x001B6F48
		public void RemoveObstacles()
		{
			if (this.lastSim != null)
			{
				for (int i = 0; i < this.obstacles.Count; i++)
				{
					this.lastSim.RemoveObstacle(this.obstacles[i]);
				}
				this.lastSim = null;
			}
			this.obstacles.Clear();
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x001B8D9C File Offset: 0x001B6F9C
		private void AddGraphObstacles(Simulator sim, GridGraph grid)
		{
			bool reverse = Vector3.Dot(grid.transform.TransformVector(Vector3.up), (sim.movementPlane == MovementPlane.XY) ? Vector3.back : Vector3.up) > 0f;
			GraphUtilities.GetContours(grid, delegate(Vector3[] vertices)
			{
				if (reverse)
				{
					Array.Reverse(vertices);
				}
				this.obstacles.Add(sim.AddObstacle(vertices, this.wallHeight, true));
			}, this.wallHeight * 0.4f, null);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x001B8E18 File Offset: 0x001B7018
		private void AddGraphObstacles(Simulator simulator, INavmesh navmesh)
		{
			GraphUtilities.GetContours(navmesh, delegate(List<Int3> vertices, bool cycle)
			{
				Vector3[] array = new Vector3[vertices.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (Vector3)vertices[i];
				}
				ListPool<Int3>.Release(vertices);
				this.obstacles.Add(simulator.AddObstacle(array, this.wallHeight, cycle));
			});
		}

		// Token: 0x040042B0 RID: 17072
		public float wallHeight = 5f;

		// Token: 0x040042B1 RID: 17073
		private readonly List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040042B2 RID: 17074
		private Simulator lastSim;
	}
}
