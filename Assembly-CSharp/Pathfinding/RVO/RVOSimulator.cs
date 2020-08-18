using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005CA RID: 1482
	[ExecuteInEditMode]
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Simulator")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_simulator.php")]
	public class RVOSimulator : VersionedMonoBehaviour
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x001B9496 File Offset: 0x001B7696
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x001B949D File Offset: 0x001B769D
		public static RVOSimulator active { get; private set; }

		// Token: 0x060028B2 RID: 10418 RVA: 0x001B94A5 File Offset: 0x001B76A5
		public Simulator GetSimulator()
		{
			if (this.simulator == null)
			{
				this.Awake();
			}
			return this.simulator;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x001B94BB File Offset: 0x001B76BB
		private void OnEnable()
		{
			RVOSimulator.active = this;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x001B94C4 File Offset: 0x001B76C4
		protected override void Awake()
		{
			base.Awake();
			if (this.simulator == null && Application.isPlaying)
			{
				int workers = AstarPath.CalculateThreadCount(this.workerThreads);
				this.simulator = new Simulator(workers, this.doubleBuffering, this.movementPlane);
			}
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x001B950C File Offset: 0x001B770C
		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.desiredSimulationFPS < 1)
			{
				this.desiredSimulationFPS = 1;
			}
			Simulator simulator = this.GetSimulator();
			simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulationFPS;
			simulator.symmetryBreakingBias = this.symmetryBreakingBias;
			simulator.Update();
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x001B955B File Offset: 0x001B775B
		private void OnDestroy()
		{
			RVOSimulator.active = null;
			if (this.simulator != null)
			{
				this.simulator.OnDestroy();
			}
		}

		// Token: 0x040042BD RID: 17085
		[Tooltip("Desired FPS for rvo simulation. It is usually not necessary to run a crowd simulation at a very high fps.\nUsually 10-30 fps is enough, but can be increased for better quality.\nThe rvo simulation will never run at a higher fps than the game")]
		public int desiredSimulationFPS = 20;

		// Token: 0x040042BE RID: 17086
		[Tooltip("Number of RVO worker threads. If set to None, no multithreading will be used.")]
		public ThreadCount workerThreads = ThreadCount.Two;

		// Token: 0x040042BF RID: 17087
		[Tooltip("Calculate local avoidance in between frames.\nThis can increase jitter in the agents' movement so use it only if you really need the performance boost. It will also reduce the responsiveness of the agents to the commands you send to them.")]
		public bool doubleBuffering;

		// Token: 0x040042C0 RID: 17088
		[Tooltip("Bias agents to pass each other on the right side.\nIf the desired velocity of an agent puts it on a collision course with another agent or an obstacle its desired velocity will be rotated this number of radians (1 radian is approximately 57°) to the right. This helps to break up symmetries and makes it possible to resolve some situations much faster.\n\nWhen many agents have the same goal this can however have the side effect that the group clustered around the target point may as a whole start to spin around the target point.")]
		[Range(0f, 0.2f)]
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x040042C1 RID: 17089
		[Tooltip("Determines if the XY (2D) or XZ (3D) plane is used for movement")]
		public MovementPlane movementPlane;

		// Token: 0x040042C2 RID: 17090
		public bool drawObstacles;

		// Token: 0x040042C3 RID: 17091
		private Simulator simulator;
	}
}
