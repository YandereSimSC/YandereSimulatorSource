using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO.Sampled
{
	// Token: 0x020005CC RID: 1484
	public class Agent : IAgent
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x001B9724 File Offset: 0x001B7924
		// (set) Token: 0x060028C0 RID: 10432 RVA: 0x001B972C File Offset: 0x001B792C
		public Vector2 Position { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x001B9735 File Offset: 0x001B7935
		// (set) Token: 0x060028C2 RID: 10434 RVA: 0x001B973D File Offset: 0x001B793D
		public float ElevationCoordinate { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x001B9746 File Offset: 0x001B7946
		// (set) Token: 0x060028C4 RID: 10436 RVA: 0x001B974E File Offset: 0x001B794E
		public Vector2 CalculatedTargetPoint { get; private set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x001B9757 File Offset: 0x001B7957
		// (set) Token: 0x060028C6 RID: 10438 RVA: 0x001B975F File Offset: 0x001B795F
		public float CalculatedSpeed { get; private set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x001B9768 File Offset: 0x001B7968
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x001B9770 File Offset: 0x001B7970
		public bool Locked { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x001B9779 File Offset: 0x001B7979
		// (set) Token: 0x060028CA RID: 10442 RVA: 0x001B9781 File Offset: 0x001B7981
		public float Radius { get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x001B978A File Offset: 0x001B798A
		// (set) Token: 0x060028CC RID: 10444 RVA: 0x001B9792 File Offset: 0x001B7992
		public float Height { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x001B979B File Offset: 0x001B799B
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x001B97A3 File Offset: 0x001B79A3
		public float AgentTimeHorizon { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x001B97AC File Offset: 0x001B79AC
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x001B97B4 File Offset: 0x001B79B4
		public float ObstacleTimeHorizon { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x001B97BD File Offset: 0x001B79BD
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x001B97C5 File Offset: 0x001B79C5
		public int MaxNeighbours { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x001B97CE File Offset: 0x001B79CE
		// (set) Token: 0x060028D4 RID: 10452 RVA: 0x001B97D6 File Offset: 0x001B79D6
		public int NeighbourCount { get; private set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x001B97DF File Offset: 0x001B79DF
		// (set) Token: 0x060028D6 RID: 10454 RVA: 0x001B97E7 File Offset: 0x001B79E7
		public RVOLayer Layer { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x001B97F0 File Offset: 0x001B79F0
		// (set) Token: 0x060028D8 RID: 10456 RVA: 0x001B97F8 File Offset: 0x001B79F8
		public RVOLayer CollidesWith { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x001B9801 File Offset: 0x001B7A01
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x001B9809 File Offset: 0x001B7A09
		public bool DebugDraw
		{
			get
			{
				return this.debugDraw;
			}
			set
			{
				this.debugDraw = (value && this.simulator != null && !this.simulator.Multithreading);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060028DB RID: 10459 RVA: 0x001B982D File Offset: 0x001B7A2D
		// (set) Token: 0x060028DC RID: 10460 RVA: 0x001B9835 File Offset: 0x001B7A35
		public float Priority { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x001B983E File Offset: 0x001B7A3E
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x001B9846 File Offset: 0x001B7A46
		public Action PreCalculationCallback { private get; set; }

		// Token: 0x060028DF RID: 10463 RVA: 0x001B984F File Offset: 0x001B7A4F
		public void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed)
		{
			maxSpeed = Math.Max(maxSpeed, 0f);
			desiredSpeed = Math.Min(Math.Max(desiredSpeed, 0f), maxSpeed);
			this.nextTargetPoint = targetPoint;
			this.nextDesiredSpeed = desiredSpeed;
			this.nextMaxSpeed = maxSpeed;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x001B9886 File Offset: 0x001B7A86
		public void SetCollisionNormal(Vector2 normal)
		{
			this.collisionNormal = normal;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x001B9890 File Offset: 0x001B7A90
		public void ForceSetVelocity(Vector2 velocity)
		{
			this.nextTargetPoint = (this.CalculatedTargetPoint = this.position + velocity * 1000f);
			this.nextDesiredSpeed = (this.CalculatedSpeed = velocity.magnitude);
			this.manuallyControlled = true;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x0015596F File Offset: 0x00153B6F
		public List<ObstacleVertex> NeighbourObstacles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x001B98E0 File Offset: 0x001B7AE0
		public Agent(Vector2 pos, float elevationCoordinate)
		{
			this.AgentTimeHorizon = 2f;
			this.ObstacleTimeHorizon = 2f;
			this.Height = 5f;
			this.Radius = 5f;
			this.MaxNeighbours = 10;
			this.Locked = false;
			this.Position = pos;
			this.ElevationCoordinate = elevationCoordinate;
			this.Layer = RVOLayer.DefaultAgent;
			this.CollidesWith = (RVOLayer)(-1);
			this.Priority = 0.5f;
			this.CalculatedTargetPoint = pos;
			this.CalculatedSpeed = 0f;
			this.SetTarget(pos, 0f, 0f);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x001B99A4 File Offset: 0x001B7BA4
		public void BufferSwitch()
		{
			this.radius = this.Radius;
			this.height = this.Height;
			this.maxSpeed = this.nextMaxSpeed;
			this.desiredSpeed = this.nextDesiredSpeed;
			this.agentTimeHorizon = this.AgentTimeHorizon;
			this.obstacleTimeHorizon = this.ObstacleTimeHorizon;
			this.maxNeighbours = this.MaxNeighbours;
			this.locked = (this.Locked && !this.manuallyControlled);
			this.position = this.Position;
			this.elevationCoordinate = this.ElevationCoordinate;
			this.collidesWith = this.CollidesWith;
			this.layer = this.Layer;
			if (this.locked)
			{
				this.desiredTargetPointInVelocitySpace = this.position;
				this.desiredVelocity = (this.currentVelocity = Vector2.zero);
				return;
			}
			this.desiredTargetPointInVelocitySpace = this.nextTargetPoint - this.position;
			this.currentVelocity = (this.CalculatedTargetPoint - this.position).normalized * this.CalculatedSpeed;
			this.desiredVelocity = this.desiredTargetPointInVelocitySpace.normalized * this.desiredSpeed;
			if (this.collisionNormal != Vector2.zero)
			{
				this.collisionNormal.Normalize();
				float num = Vector2.Dot(this.currentVelocity, this.collisionNormal);
				if (num < 0f)
				{
					this.currentVelocity -= this.collisionNormal * num;
				}
				this.collisionNormal = Vector2.zero;
			}
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x001B9B34 File Offset: 0x001B7D34
		public void PreCalculation()
		{
			if (this.PreCalculationCallback != null)
			{
				this.PreCalculationCallback();
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x001B9B4C File Offset: 0x001B7D4C
		public void PostCalculation()
		{
			if (!this.manuallyControlled)
			{
				this.CalculatedTargetPoint = this.calculatedTargetPoint;
				this.CalculatedSpeed = this.calculatedSpeed;
			}
			List<ObstacleVertex> list = this.obstaclesBuffered;
			this.obstaclesBuffered = this.obstacles;
			this.obstacles = list;
			this.manuallyControlled = false;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x001B9B9C File Offset: 0x001B7D9C
		public void CalculateNeighbours()
		{
			this.neighbours.Clear();
			this.neighbourDists.Clear();
			if (this.MaxNeighbours > 0 && !this.locked)
			{
				this.simulator.Quadtree.Query(this.position, this.maxSpeed, this.agentTimeHorizon, this.radius, this);
			}
			this.NeighbourCount = this.neighbours.Count;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x001B9C0A File Offset: 0x001B7E0A
		private static float Sqr(float x)
		{
			return x * x;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x001B9C10 File Offset: 0x001B7E10
		internal float InsertAgentNeighbour(Agent agent, float rangeSq)
		{
			if (this == agent || (agent.layer & this.collidesWith) == (RVOLayer)0)
			{
				return rangeSq;
			}
			float sqrMagnitude = (agent.position - this.position).sqrMagnitude;
			if (sqrMagnitude < rangeSq)
			{
				if (this.neighbours.Count < this.maxNeighbours)
				{
					this.neighbours.Add(null);
					this.neighbourDists.Add(float.PositiveInfinity);
				}
				int num = this.neighbours.Count - 1;
				if (sqrMagnitude < this.neighbourDists[num])
				{
					while (num != 0 && sqrMagnitude < this.neighbourDists[num - 1])
					{
						this.neighbours[num] = this.neighbours[num - 1];
						this.neighbourDists[num] = this.neighbourDists[num - 1];
						num--;
					}
					this.neighbours[num] = agent;
					this.neighbourDists[num] = sqrMagnitude;
				}
				if (this.neighbours.Count == this.maxNeighbours)
				{
					rangeSq = this.neighbourDists[this.neighbourDists.Count - 1];
				}
			}
			return rangeSq;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x001ACF24 File Offset: 0x001AB124
		private static Vector3 FromXZ(Vector2 p)
		{
			return new Vector3(p.x, 0f, p.y);
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x001ACF11 File Offset: 0x001AB111
		private static Vector2 ToXZ(Vector3 p)
		{
			return new Vector2(p.x, p.z);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x001B9D38 File Offset: 0x001B7F38
		private Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.simulator.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x001B9D88 File Offset: 0x001B7F88
		private static void DrawVO(Vector2 circleCenter, float radius, Vector2 origin)
		{
			float num = Mathf.Atan2((origin - circleCenter).y, (origin - circleCenter).x);
			float num2 = radius / (origin - circleCenter).magnitude;
			float num3 = (num2 <= 1f) ? Mathf.Abs(Mathf.Acos(num2)) : 0f;
			Draw.Debug.CircleXZ(Agent.FromXZ(circleCenter), radius, Color.black, num - num3, num + num3);
			Vector2 vector = new Vector2(Mathf.Cos(num - num3), Mathf.Sin(num - num3)) * radius;
			Vector2 vector2 = new Vector2(Mathf.Cos(num + num3), Mathf.Sin(num + num3)) * radius;
			Vector2 p = -new Vector2(-vector.y, vector.x);
			Vector2 p2 = new Vector2(-vector2.y, vector2.x);
			vector += circleCenter;
			vector2 += circleCenter;
			Debug.DrawRay(Agent.FromXZ(vector), Agent.FromXZ(p).normalized * 100f, Color.black);
			Debug.DrawRay(Agent.FromXZ(vector2), Agent.FromXZ(p2).normalized * 100f, Color.black);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x001B9ECC File Offset: 0x001B80CC
		internal void CalculateVelocity(Simulator.WorkerContext context)
		{
			if (this.manuallyControlled)
			{
				return;
			}
			if (this.locked)
			{
				this.calculatedSpeed = 0f;
				this.calculatedTargetPoint = this.position;
				return;
			}
			Agent.VOBuffer vos = context.vos;
			vos.Clear();
			this.GenerateObstacleVOs(vos);
			this.GenerateNeighbourAgentVOs(vos);
			if (!Agent.BiasDesiredVelocity(vos, ref this.desiredVelocity, ref this.desiredTargetPointInVelocitySpace, this.simulator.symmetryBreakingBias))
			{
				this.calculatedTargetPoint = this.desiredTargetPointInVelocitySpace + this.position;
				this.calculatedSpeed = this.desiredSpeed;
				if (this.DebugDraw)
				{
					Draw.Debug.CrossXZ(Agent.FromXZ(this.calculatedTargetPoint), Color.white, 1f);
				}
				return;
			}
			Vector2 vector = Vector2.zero;
			vector = this.GradientDescent(vos, this.currentVelocity, this.desiredVelocity);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.white, 1f);
			}
			this.calculatedTargetPoint = this.position + vector;
			this.calculatedSpeed = Mathf.Min(vector.magnitude, this.maxSpeed);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x001B9FF8 File Offset: 0x001B81F8
		private static Color Rainbow(float v)
		{
			Color color = new Color(v, 0f, 0f);
			if (color.r > 1f)
			{
				color.g = color.r - 1f;
				color.r = 1f;
			}
			if (color.g > 1f)
			{
				color.b = color.g - 1f;
				color.g = 1f;
			}
			return color;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x001BA070 File Offset: 0x001B8270
		private void GenerateObstacleVOs(Agent.VOBuffer vos)
		{
			float num = this.maxSpeed * this.obstacleTimeHorizon;
			for (int i = 0; i < this.simulator.obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = this.simulator.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				do
				{
					if (obstacleVertex2.ignore || (obstacleVertex2.layer & this.collidesWith) == (RVOLayer)0)
					{
						obstacleVertex2 = obstacleVertex2.next;
					}
					else
					{
						float a;
						Vector2 vector = this.To2D(obstacleVertex2.position, out a);
						float b;
						Vector2 vector2 = this.To2D(obstacleVertex2.next.position, out b);
						Vector2 normalized = (vector2 - vector).normalized;
						float num2 = Agent.VO.SignedDistanceFromLine(vector, normalized, this.position);
						if (num2 >= -0.01f && num2 < num)
						{
							float t = Vector2.Dot(this.position - vector, vector2 - vector) / (vector2 - vector).sqrMagnitude;
							float num3 = Mathf.Lerp(a, b, t);
							if ((Vector2.Lerp(vector, vector2, t) - this.position).sqrMagnitude < num * num && (this.simulator.movementPlane == MovementPlane.XY || (this.elevationCoordinate <= num3 + obstacleVertex2.height && this.elevationCoordinate + this.height >= num3)))
							{
								vos.Add(Agent.VO.SegmentObstacle(vector2 - this.position, vector - this.position, Vector2.zero, this.radius * 0.01f, 1f / this.ObstacleTimeHorizon, 1f / this.simulator.DeltaTime));
							}
						}
						obstacleVertex2 = obstacleVertex2.next;
					}
				}
				while (obstacleVertex2 != obstacleVertex && obstacleVertex2 != null && obstacleVertex2.next != null);
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x001BA244 File Offset: 0x001B8444
		private void GenerateNeighbourAgentVOs(Agent.VOBuffer vos)
		{
			float num = 1f / this.agentTimeHorizon;
			Vector2 a = this.currentVelocity;
			for (int i = 0; i < this.neighbours.Count; i++)
			{
				Agent agent = this.neighbours[i];
				if (agent != this)
				{
					float num2 = Math.Min(this.elevationCoordinate + this.height, agent.elevationCoordinate + agent.height);
					float num3 = Math.Max(this.elevationCoordinate, agent.elevationCoordinate);
					if (num2 - num3 >= 0f)
					{
						float num4 = this.radius + agent.radius;
						Vector2 vector = agent.position - this.position;
						float num5;
						if (agent.locked || agent.manuallyControlled)
						{
							num5 = 1f;
						}
						else if (agent.Priority > 1E-05f || this.Priority > 1E-05f)
						{
							num5 = agent.Priority / (this.Priority + agent.Priority);
						}
						else
						{
							num5 = 0.5f;
						}
						Vector2 b = Vector2.Lerp(agent.currentVelocity, agent.desiredVelocity, 2f * num5 - 1f);
						Vector2 vector2 = Vector2.Lerp(a, b, num5);
						vos.Add(new Agent.VO(vector, vector2, num4, num, 1f / this.simulator.DeltaTime));
						if (this.DebugDraw)
						{
							Agent.DrawVO(this.position + vector * num + vector2, num4 * num, this.position + vector2);
						}
					}
				}
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x001BA3D0 File Offset: 0x001B85D0
		private Vector2 GradientDescent(Agent.VOBuffer vos, Vector2 sampleAround1, Vector2 sampleAround2)
		{
			float num;
			Vector2 vector = this.Trace(vos, sampleAround1, out num);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.yellow, 0.5f);
			}
			float num2;
			Vector2 vector2 = this.Trace(vos, sampleAround2, out num2);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector2 + this.position), Color.magenta, 0.5f);
			}
			if (num >= num2)
			{
				return vector2;
			}
			return vector;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x001BA454 File Offset: 0x001B8654
		private static bool BiasDesiredVelocity(Agent.VOBuffer vos, ref Vector2 desiredVelocity, ref Vector2 targetPointInVelocitySpace, float maxBiasRadians)
		{
			float magnitude = desiredVelocity.magnitude;
			float num = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float b;
				vos.buffer[i].Gradient(desiredVelocity, out b);
				num = Mathf.Max(num, b);
			}
			bool result = num > 0f;
			if (magnitude < 0.001f)
			{
				return result;
			}
			float d = Mathf.Min(maxBiasRadians, num / magnitude);
			desiredVelocity += new Vector2(desiredVelocity.y, -desiredVelocity.x) * d;
			targetPointInVelocitySpace += new Vector2(targetPointInVelocitySpace.y, -targetPointInVelocitySpace.x) * d;
			return result;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x001BA518 File Offset: 0x001B8718
		private Vector2 EvaluateGradient(Agent.VOBuffer vos, Vector2 p, out float value)
		{
			Vector2 vector = Vector2.zero;
			value = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float num;
				Vector2 vector2 = vos.buffer[i].ScaledGradient(p, out num);
				if (num > value)
				{
					value = num;
					vector = vector2;
				}
			}
			Vector2 a = this.desiredVelocity - p;
			float magnitude = a.magnitude;
			if (magnitude > 0.0001f)
			{
				vector += a * (0.1f / magnitude);
				value += magnitude * 0.1f;
			}
			float sqrMagnitude = p.sqrMagnitude;
			if (sqrMagnitude > this.desiredSpeed * this.desiredSpeed)
			{
				float num2 = Mathf.Sqrt(sqrMagnitude);
				if (num2 > this.maxSpeed)
				{
					value += 3f * (num2 - this.maxSpeed);
					vector -= 3f * (p / num2);
				}
				float num3 = 0.2f;
				value += num3 * (num2 - this.desiredSpeed);
				vector -= num3 * (p / num2);
			}
			return vector;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x001BA630 File Offset: 0x001B8830
		private Vector2 Trace(Agent.VOBuffer vos, Vector2 p, out float score)
		{
			float num = Mathf.Max(this.radius, 0.2f * this.desiredSpeed);
			float num2 = float.PositiveInfinity;
			Vector2 result = p;
			for (int i = 0; i < 50; i++)
			{
				float num3 = 1f - (float)i / 50f;
				num3 = Agent.Sqr(num3) * num;
				float num4;
				Vector2 vector = this.EvaluateGradient(vos, p, out num4);
				if (num4 < num2)
				{
					num2 = num4;
					result = p;
				}
				vector.Normalize();
				vector *= num3;
				Vector2 a = p;
				p += vector;
				if (this.DebugDraw)
				{
					Debug.DrawLine(Agent.FromXZ(a + this.position), Agent.FromXZ(p + this.position), Agent.Rainbow((float)i * 0.1f) * new Color(1f, 1f, 1f, 1f));
				}
			}
			score = num2;
			return result;
		}

		// Token: 0x040042C7 RID: 17095
		internal float radius;

		// Token: 0x040042C8 RID: 17096
		internal float height;

		// Token: 0x040042C9 RID: 17097
		internal float desiredSpeed;

		// Token: 0x040042CA RID: 17098
		internal float maxSpeed;

		// Token: 0x040042CB RID: 17099
		internal float agentTimeHorizon;

		// Token: 0x040042CC RID: 17100
		internal float obstacleTimeHorizon;

		// Token: 0x040042CD RID: 17101
		internal bool locked;

		// Token: 0x040042CE RID: 17102
		private RVOLayer layer;

		// Token: 0x040042CF RID: 17103
		private RVOLayer collidesWith;

		// Token: 0x040042D0 RID: 17104
		private int maxNeighbours;

		// Token: 0x040042D1 RID: 17105
		internal Vector2 position;

		// Token: 0x040042D2 RID: 17106
		private float elevationCoordinate;

		// Token: 0x040042D3 RID: 17107
		private Vector2 currentVelocity;

		// Token: 0x040042D4 RID: 17108
		private Vector2 desiredTargetPointInVelocitySpace;

		// Token: 0x040042D5 RID: 17109
		private Vector2 desiredVelocity;

		// Token: 0x040042D6 RID: 17110
		private Vector2 nextTargetPoint;

		// Token: 0x040042D7 RID: 17111
		private float nextDesiredSpeed;

		// Token: 0x040042D8 RID: 17112
		private float nextMaxSpeed;

		// Token: 0x040042D9 RID: 17113
		private Vector2 collisionNormal;

		// Token: 0x040042DA RID: 17114
		private bool manuallyControlled;

		// Token: 0x040042DB RID: 17115
		private bool debugDraw;

		// Token: 0x040042EB RID: 17131
		internal Agent next;

		// Token: 0x040042EC RID: 17132
		private float calculatedSpeed;

		// Token: 0x040042ED RID: 17133
		private Vector2 calculatedTargetPoint;

		// Token: 0x040042EE RID: 17134
		internal Simulator simulator;

		// Token: 0x040042EF RID: 17135
		private List<Agent> neighbours = new List<Agent>();

		// Token: 0x040042F0 RID: 17136
		private List<float> neighbourDists = new List<float>();

		// Token: 0x040042F1 RID: 17137
		private List<ObstacleVertex> obstaclesBuffered = new List<ObstacleVertex>();

		// Token: 0x040042F2 RID: 17138
		private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040042F3 RID: 17139
		private const float DesiredVelocityWeight = 0.1f;

		// Token: 0x040042F4 RID: 17140
		private const float WallWeight = 5f;

		// Token: 0x02000770 RID: 1904
		internal struct VO
		{
			// Token: 0x06002D90 RID: 11664 RVA: 0x001CF7D4 File Offset: 0x001CD9D4
			public VO(Vector2 center, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				this.weightFactor = 1f;
				this.weightBonus = 0f;
				this.circleCenter = center * inverseDt + offset;
				this.weightFactor = 4f * Mathf.Exp(-Agent.Sqr(center.sqrMagnitude / (radius * radius))) + 1f;
				if (center.magnitude < radius)
				{
					this.colliding = true;
					this.line1 = center.normalized * (center.magnitude - radius - 0.001f) * 0.3f * inverseDeltaTime;
					this.dir1 = new Vector2(this.line1.y, -this.line1.x).normalized;
					this.line1 += offset;
					this.cutoffDir = Vector2.zero;
					this.cutoffLine = Vector2.zero;
					this.dir2 = Vector2.zero;
					this.line2 = Vector2.zero;
					this.radius = 0f;
				}
				else
				{
					this.colliding = false;
					center *= inverseDt;
					radius *= inverseDt;
					Vector2 b = center + offset;
					float d = center.magnitude - radius + 0.001f;
					this.cutoffLine = center.normalized * d;
					this.cutoffDir = new Vector2(-this.cutoffLine.y, this.cutoffLine.x).normalized;
					this.cutoffLine += offset;
					float num = Mathf.Atan2(-center.y, -center.x);
					float num2 = Mathf.Abs(Mathf.Acos(radius / center.magnitude));
					this.radius = radius;
					this.line1 = new Vector2(Mathf.Cos(num + num2), Mathf.Sin(num + num2));
					this.dir1 = new Vector2(this.line1.y, -this.line1.x);
					this.line2 = new Vector2(Mathf.Cos(num - num2), Mathf.Sin(num - num2));
					this.dir2 = new Vector2(this.line2.y, -this.line2.x);
					this.line1 = this.line1 * radius + b;
					this.line2 = this.line2 * radius + b;
				}
				this.segmentStart = Vector2.zero;
				this.segmentEnd = Vector2.zero;
				this.segment = false;
			}

			// Token: 0x06002D91 RID: 11665 RVA: 0x001CFA6C File Offset: 0x001CDC6C
			public static Agent.VO SegmentObstacle(Vector2 segmentStart, Vector2 segmentEnd, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				Agent.VO vo = default(Agent.VO);
				vo.weightFactor = 1f;
				vo.weightBonus = Mathf.Max(radius, 1f) * 40f;
				Vector3 vector = VectorMath.ClosestPointOnSegment(segmentStart, segmentEnd, Vector2.zero);
				if (vector.magnitude <= radius)
				{
					vo.colliding = true;
					vo.line1 = vector.normalized * (vector.magnitude - radius) * 0.3f * inverseDeltaTime;
					vo.dir1 = new Vector2(vo.line1.y, -vo.line1.x).normalized;
					vo.line1 += offset;
					vo.cutoffDir = Vector2.zero;
					vo.cutoffLine = Vector2.zero;
					vo.dir2 = Vector2.zero;
					vo.line2 = Vector2.zero;
					vo.radius = 0f;
					vo.segmentStart = Vector2.zero;
					vo.segmentEnd = Vector2.zero;
					vo.segment = false;
				}
				else
				{
					vo.colliding = false;
					segmentStart *= inverseDt;
					segmentEnd *= inverseDt;
					radius *= inverseDt;
					Vector2 normalized = (segmentEnd - segmentStart).normalized;
					vo.cutoffDir = normalized;
					vo.cutoffLine = segmentStart + new Vector2(-normalized.y, normalized.x) * radius;
					vo.cutoffLine += offset;
					float sqrMagnitude = segmentStart.sqrMagnitude;
					Vector2 vector2 = -VectorMath.ComplexMultiply(segmentStart, new Vector2(radius, Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude - radius * radius)))) / sqrMagnitude;
					float sqrMagnitude2 = segmentEnd.sqrMagnitude;
					Vector2 vector3 = -VectorMath.ComplexMultiply(segmentEnd, new Vector2(radius, -Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude2 - radius * radius)))) / sqrMagnitude2;
					vo.line1 = segmentStart + vector2 * radius + offset;
					vo.line2 = segmentEnd + vector3 * radius + offset;
					vo.dir1 = new Vector2(vector2.y, -vector2.x);
					vo.dir2 = new Vector2(vector3.y, -vector3.x);
					vo.segmentStart = segmentStart;
					vo.segmentEnd = segmentEnd;
					vo.radius = radius;
					vo.segment = true;
				}
				return vo;
			}

			// Token: 0x06002D92 RID: 11666 RVA: 0x001CFD21 File Offset: 0x001CDF21
			public static float SignedDistanceFromLine(Vector2 a, Vector2 dir, Vector2 p)
			{
				return (p.x - a.x) * dir.y - dir.x * (p.y - a.y);
			}

			// Token: 0x06002D93 RID: 11667 RVA: 0x001CFD4C File Offset: 0x001CDF4C
			public Vector2 ScaledGradient(Vector2 p, out float weight)
			{
				Vector2 vector = this.Gradient(p, out weight);
				if (weight > 0f)
				{
					vector *= 2f * this.weightFactor;
					weight *= 2f * this.weightFactor;
					weight += 1f + this.weightBonus;
				}
				return vector;
			}

			// Token: 0x06002D94 RID: 11668 RVA: 0x001CFDA4 File Offset: 0x001CDFA4
			public Vector2 Gradient(Vector2 p, out float weight)
			{
				if (this.colliding)
				{
					float num = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					if (num >= 0f)
					{
						weight = num;
						return new Vector2(-this.dir1.y, this.dir1.x);
					}
					weight = 0f;
					return new Vector2(0f, 0f);
				}
				else
				{
					float num2 = Agent.VO.SignedDistanceFromLine(this.cutoffLine, this.cutoffDir, p);
					if (num2 <= 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					float num3 = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					float num4 = Agent.VO.SignedDistanceFromLine(this.line2, this.dir2, p);
					if (num3 < 0f || num4 < 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					Vector2 result;
					if (Vector2.Dot(p - this.line1, this.dir1) > 0f && Vector2.Dot(p - this.line2, this.dir2) < 0f)
					{
						if (!this.segment)
						{
							float num5;
							result = VectorMath.Normalize(p - this.circleCenter, out num5);
							weight = this.radius - num5;
							return result;
						}
						if (num2 < this.radius)
						{
							Vector2 b = VectorMath.ClosestPointOnSegment(this.segmentStart, this.segmentEnd, p);
							float num6;
							result = VectorMath.Normalize(p - b, out num6);
							weight = this.radius - num6;
							return result;
						}
					}
					if (this.segment && num2 < num3 && num2 < num4)
					{
						weight = num2;
						result = new Vector2(-this.cutoffDir.y, this.cutoffDir.x);
						return result;
					}
					if (num3 < num4)
					{
						weight = num3;
						result = new Vector2(-this.dir1.y, this.dir1.x);
					}
					else
					{
						weight = num4;
						result = new Vector2(-this.dir2.y, this.dir2.x);
					}
					return result;
				}
			}

			// Token: 0x04004A60 RID: 19040
			private Vector2 line1;

			// Token: 0x04004A61 RID: 19041
			private Vector2 line2;

			// Token: 0x04004A62 RID: 19042
			private Vector2 dir1;

			// Token: 0x04004A63 RID: 19043
			private Vector2 dir2;

			// Token: 0x04004A64 RID: 19044
			private Vector2 cutoffLine;

			// Token: 0x04004A65 RID: 19045
			private Vector2 cutoffDir;

			// Token: 0x04004A66 RID: 19046
			private Vector2 circleCenter;

			// Token: 0x04004A67 RID: 19047
			private bool colliding;

			// Token: 0x04004A68 RID: 19048
			private float radius;

			// Token: 0x04004A69 RID: 19049
			private float weightFactor;

			// Token: 0x04004A6A RID: 19050
			private float weightBonus;

			// Token: 0x04004A6B RID: 19051
			private Vector2 segmentStart;

			// Token: 0x04004A6C RID: 19052
			private Vector2 segmentEnd;

			// Token: 0x04004A6D RID: 19053
			private bool segment;
		}

		// Token: 0x02000771 RID: 1905
		internal class VOBuffer
		{
			// Token: 0x06002D95 RID: 11669 RVA: 0x001CFFB4 File Offset: 0x001CE1B4
			public void Clear()
			{
				this.length = 0;
			}

			// Token: 0x06002D96 RID: 11670 RVA: 0x001CFFBD File Offset: 0x001CE1BD
			public VOBuffer(int n)
			{
				this.buffer = new Agent.VO[n];
				this.length = 0;
			}

			// Token: 0x06002D97 RID: 11671 RVA: 0x001CFFD8 File Offset: 0x001CE1D8
			public void Add(Agent.VO vo)
			{
				if (this.length >= this.buffer.Length)
				{
					Agent.VO[] array = new Agent.VO[this.buffer.Length * 2];
					this.buffer.CopyTo(array, 0);
					this.buffer = array;
				}
				Agent.VO[] array2 = this.buffer;
				int num = this.length;
				this.length = num + 1;
				array2[num] = vo;
			}

			// Token: 0x04004A6E RID: 19054
			public Agent.VO[] buffer;

			// Token: 0x04004A6F RID: 19055
			public int length;
		}
	}
}
