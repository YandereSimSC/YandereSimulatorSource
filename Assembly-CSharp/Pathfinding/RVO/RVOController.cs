using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005C7 RID: 1479
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Controller")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_controller.php")]
	public class RVOController : VersionedMonoBehaviour
	{
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x001B863B File Offset: 0x001B683B
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public LayerMask mask
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x0002D171 File Offset: 0x0002B371
		// (set) Token: 0x06002878 RID: 10360 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public bool enableRotation
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x00194851 File Offset: 0x00192A51
		// (set) Token: 0x0600287A RID: 10362 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float rotationSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x00194851 File Offset: 0x00192A51
		// (set) Token: 0x0600287C RID: 10364 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float maxSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x001B8643 File Offset: 0x001B6843
		public MovementPlane movementPlane
		{
			get
			{
				if (this.simulator != null)
				{
					return this.simulator.movementPlane;
				}
				if (RVOSimulator.active)
				{
					return RVOSimulator.active.movementPlane;
				}
				return MovementPlane.XZ;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x001B8671 File Offset: 0x001B6871
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x001B8679 File Offset: 0x001B6879
		public IAgent rvoAgent { get; private set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x001B8682 File Offset: 0x001B6882
		// (set) Token: 0x06002881 RID: 10369 RVA: 0x001B868A File Offset: 0x001B688A
		public Simulator simulator { get; private set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x001B8693 File Offset: 0x001B6893
		public Vector3 position
		{
			get
			{
				return this.To3D(this.rvoAgent.Position, this.rvoAgent.ElevationCoordinate);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x001B86B4 File Offset: 0x001B68B4
		// (set) Token: 0x06002884 RID: 10372 RVA: 0x001B86E7 File Offset: 0x001B68E7
		public Vector3 velocity
		{
			get
			{
				float num = (Time.deltaTime > 0.0001f) ? Time.deltaTime : 0.02f;
				return this.CalculateMovementDelta(num) / num;
			}
			set
			{
				this.rvoAgent.ForceSetVelocity(this.To2D(value));
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x001B86FC File Offset: 0x001B68FC
		public Vector3 CalculateMovementDelta(float deltaTime)
		{
			if (this.rvoAgent == null)
			{
				return Vector3.zero;
			}
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D((this.ai != null) ? this.ai.position : this.tr.position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x001B876A File Offset: 0x001B696A
		public Vector3 CalculateMovementDelta(Vector3 position, float deltaTime)
		{
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D(position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x001B87A0 File Offset: 0x001B69A0
		public void SetCollisionNormal(Vector3 normal)
		{
			this.rvoAgent.SetCollisionNormal(this.To2D(normal));
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x001B87B4 File Offset: 0x001B69B4
		[Obsolete("Set the 'velocity' property instead")]
		public void ForceSetVelocity(Vector3 velocity)
		{
			this.velocity = velocity;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x001B87C0 File Offset: 0x001B69C0
		public Vector2 To2D(Vector3 p)
		{
			float num;
			return this.To2D(p, out num);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x001B87D6 File Offset: 0x001B69D6
		public Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x001B8815 File Offset: 0x001B6A15
		public Vector3 To3D(Vector2 p, float elevationCoordinate)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				return new Vector3(p.x, p.y, -elevationCoordinate);
			}
			return new Vector3(p.x, elevationCoordinate, p.y);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x001B8846 File Offset: 0x001B6A46
		private void OnDisable()
		{
			if (this.simulator == null)
			{
				return;
			}
			this.simulator.RemoveAgent(this.rvoAgent);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x001B8864 File Offset: 0x001B6A64
		private void OnEnable()
		{
			this.tr = base.transform;
			this.ai = base.GetComponent<IAstarAI>();
			if (RVOSimulator.active == null)
			{
				Debug.LogError("No RVOSimulator component found in the scene. Please add one.");
				base.enabled = false;
				return;
			}
			this.simulator = RVOSimulator.active.GetSimulator();
			if (this.rvoAgent != null)
			{
				this.simulator.AddAgent(this.rvoAgent);
				return;
			}
			this.rvoAgent = this.simulator.AddAgent(Vector2.zero, 0f);
			this.rvoAgent.PreCalculationCallback = new Action(this.UpdateAgentProperties);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x001B8908 File Offset: 0x001B6B08
		protected void UpdateAgentProperties()
		{
			this.rvoAgent.Radius = Mathf.Max(0.001f, this.radius);
			this.rvoAgent.AgentTimeHorizon = this.agentTimeHorizon;
			this.rvoAgent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			this.rvoAgent.Locked = this.locked;
			this.rvoAgent.MaxNeighbours = this.maxNeighbours;
			this.rvoAgent.DebugDraw = this.debug;
			this.rvoAgent.Layer = this.layer;
			this.rvoAgent.CollidesWith = this.collidesWith;
			this.rvoAgent.Priority = this.priority;
			float num;
			this.rvoAgent.Position = this.To2D((this.ai != null) ? this.ai.position : this.tr.position, out num);
			if (this.movementPlane == MovementPlane.XZ)
			{
				this.rvoAgent.Height = this.height;
				this.rvoAgent.ElevationCoordinate = num + this.center - 0.5f * this.height;
				return;
			}
			this.rvoAgent.Height = 1f;
			this.rvoAgent.ElevationCoordinate = 0f;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x001B8A45 File Offset: 0x001B6C45
		public void SetTarget(Vector3 pos, float speed, float maxSpeed)
		{
			if (this.simulator == null)
			{
				return;
			}
			this.rvoAgent.SetTarget(this.To2D(pos), speed, maxSpeed);
			if (this.lockWhenNotMoving)
			{
				this.locked = (speed < 0.001f);
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x001B8A7C File Offset: 0x001B6C7C
		public void Move(Vector3 vel)
		{
			if (this.simulator == null)
			{
				return;
			}
			Vector2 b = this.To2D(vel);
			float magnitude = b.magnitude;
			this.rvoAgent.SetTarget(this.To2D((this.ai != null) ? this.ai.position : this.tr.position) + b, magnitude, magnitude);
			if (this.lockWhenNotMoving)
			{
				this.locked = (magnitude < 0.001f);
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x001B8AF1 File Offset: 0x001B6CF1
		[Obsolete("Use transform.position instead, the RVOController can now handle that without any issues.")]
		public void Teleport(Vector3 pos)
		{
			this.tr.position = pos;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x001B8B00 File Offset: 0x001B6D00
		private void OnDrawGizmos()
		{
			Color color = RVOController.GizmoColor * (this.locked ? 0.5f : 1f);
			Vector3 vector = (this.ai != null) ? this.ai.position : base.transform.position;
			if (this.movementPlane == MovementPlane.XY)
			{
				Draw.Gizmos.Cylinder(vector, Vector3.forward, 0f, this.radius, color);
				return;
			}
			Draw.Gizmos.Cylinder(vector + this.To3D(Vector2.zero, this.center - this.height * 0.5f), this.To3D(Vector2.zero, 1f), this.height, this.radius, color);
		}

		// Token: 0x0400429D RID: 17053
		[Tooltip("Radius of the agent")]
		public float radius = 0.5f;

		// Token: 0x0400429E RID: 17054
		[Tooltip("Height of the agent. In world units")]
		public float height = 2f;

		// Token: 0x0400429F RID: 17055
		[Tooltip("A locked unit cannot move. Other units will still avoid it. But avoidance quality is not the best")]
		public bool locked;

		// Token: 0x040042A0 RID: 17056
		[Tooltip("Automatically set #locked to true when desired velocity is approximately zero")]
		public bool lockWhenNotMoving;

		// Token: 0x040042A1 RID: 17057
		[Tooltip("How far into the future to look for collisions with other agents (in seconds)")]
		public float agentTimeHorizon = 2f;

		// Token: 0x040042A2 RID: 17058
		[Tooltip("How far into the future to look for collisions with obstacles (in seconds)")]
		public float obstacleTimeHorizon = 2f;

		// Token: 0x040042A3 RID: 17059
		[Tooltip("Max number of other agents to take into account.\nA smaller value can reduce CPU load, a higher value can lead to better local avoidance quality.")]
		public int maxNeighbours = 10;

		// Token: 0x040042A4 RID: 17060
		public RVOLayer layer = RVOLayer.DefaultAgent;

		// Token: 0x040042A5 RID: 17061
		[EnumFlag]
		public RVOLayer collidesWith = (RVOLayer)(-1);

		// Token: 0x040042A6 RID: 17062
		[HideInInspector]
		[Obsolete]
		public float wallAvoidForce = 1f;

		// Token: 0x040042A7 RID: 17063
		[HideInInspector]
		[Obsolete]
		public float wallAvoidFalloff = 1f;

		// Token: 0x040042A8 RID: 17064
		[Tooltip("How strongly other agents will avoid this agent")]
		[Range(0f, 1f)]
		public float priority = 0.5f;

		// Token: 0x040042A9 RID: 17065
		[Tooltip("Center of the agent relative to the pivot point of this game object")]
		public float center = 1f;

		// Token: 0x040042AC RID: 17068
		protected Transform tr;

		// Token: 0x040042AD RID: 17069
		protected IAstarAI ai;

		// Token: 0x040042AE RID: 17070
		public bool debug;

		// Token: 0x040042AF RID: 17071
		private static readonly Color GizmoColor = new Color(0.9411765f, 0.8352941f, 0.11764706f);
	}
}
