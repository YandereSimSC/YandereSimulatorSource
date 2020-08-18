using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005C4 RID: 1476
	public class Simulator
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x001B756C File Offset: 0x001B576C
		// (set) Token: 0x0600284F RID: 10319 RVA: 0x001B7574 File Offset: 0x001B5774
		public RVOQuadtree Quadtree { get; private set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x001B757D File Offset: 0x001B577D
		public float DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x001B7585 File Offset: 0x001B5785
		public bool Multithreading
		{
			get
			{
				return this.workers != null && this.workers.Length != 0;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x001B759B File Offset: 0x001B579B
		// (set) Token: 0x06002853 RID: 10323 RVA: 0x001B75A3 File Offset: 0x001B57A3
		public float DesiredDeltaTime
		{
			get
			{
				return this.desiredDeltaTime;
			}
			set
			{
				this.desiredDeltaTime = Math.Max(value, 0f);
			}
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x001B75B6 File Offset: 0x001B57B6
		public List<Agent> GetAgents()
		{
			return this.agents;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x001B75BE File Offset: 0x001B57BE
		public List<ObstacleVertex> GetObstacles()
		{
			return this.obstacles;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x001B75C8 File Offset: 0x001B57C8
		public Simulator(int workers, bool doubleBuffering, MovementPlane movementPlane)
		{
			this.workers = new Simulator.Worker[workers];
			this.doubleBuffering = doubleBuffering;
			this.DesiredDeltaTime = 1f;
			this.movementPlane = movementPlane;
			this.Quadtree = new RVOQuadtree();
			for (int i = 0; i < workers; i++)
			{
				this.workers[i] = new Simulator.Worker(this);
			}
			this.agents = new List<Agent>();
			this.obstacles = new List<ObstacleVertex>();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x001B7670 File Offset: 0x001B5870
		public void ClearAgents()
		{
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].simulator = null;
			}
			this.agents.Clear();
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x001B76B8 File Offset: 0x001B58B8
		public void OnDestroy()
		{
			if (this.workers != null)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].Terminate();
				}
			}
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x001B76F0 File Offset: 0x001B58F0
		~Simulator()
		{
			this.OnDestroy();
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x001B771C File Offset: 0x001B591C
		public IAgent AddAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != null && agent2.simulator == this)
			{
				throw new ArgumentException("The agent is already in the simulation");
			}
			if (agent2.simulator != null)
			{
				throw new ArgumentException("The agent is already added to another simulation");
			}
			agent2.simulator = this;
			this.BlockUntilSimulationStepIsDone();
			this.agents.Add(agent2);
			return agent;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x001B77A0 File Offset: 0x001B59A0
		[Obsolete("Use AddAgent(Vector2,float) instead")]
		public IAgent AddAgent(Vector3 position)
		{
			return this.AddAgent(new Vector2(position.x, position.z), position.y);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x001B77BF File Offset: 0x001B59BF
		public IAgent AddAgent(Vector2 position, float elevationCoordinate)
		{
			return this.AddAgent(new Agent(position, elevationCoordinate));
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x001B77D0 File Offset: 0x001B59D0
		public void RemoveAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != this)
			{
				throw new ArgumentException("The agent is not added to this simulation");
			}
			this.BlockUntilSimulationStepIsDone();
			agent2.simulator = null;
			if (!this.agents.Remove(agent2))
			{
				throw new ArgumentException("Critical Bug! This should not happen. Please report this.");
			}
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x001B7845 File Offset: 0x001B5A45
		public ObstacleVertex AddObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(v);
			this.UpdateObstacles();
			return v;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x001B786E File Offset: 0x001B5A6E
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, bool cycle = true)
		{
			return this.AddObstacle(vertices, height, Matrix4x4.identity, RVOLayer.DefaultObstacle, cycle);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x001B7880 File Offset: 0x001B5A80
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, Matrix4x4 matrix, RVOLayer layer = RVOLayer.DefaultObstacle, bool cycle = true)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			ObstacleVertex obstacleVertex = null;
			ObstacleVertex obstacleVertex2 = null;
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < vertices.Length; i++)
			{
				ObstacleVertex obstacleVertex3 = new ObstacleVertex
				{
					prev = obstacleVertex2,
					layer = layer,
					height = height
				};
				if (obstacleVertex == null)
				{
					obstacleVertex = obstacleVertex3;
				}
				else
				{
					obstacleVertex2.next = obstacleVertex3;
				}
				obstacleVertex2 = obstacleVertex3;
			}
			if (cycle)
			{
				obstacleVertex2.next = obstacleVertex;
				obstacleVertex.prev = obstacleVertex2;
			}
			this.UpdateObstacle(obstacleVertex, vertices, matrix);
			this.obstacles.Add(obstacleVertex);
			return obstacleVertex;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x001B7918 File Offset: 0x001B5B18
		public ObstacleVertex AddObstacle(Vector3 a, Vector3 b, float height)
		{
			ObstacleVertex obstacleVertex = new ObstacleVertex();
			ObstacleVertex obstacleVertex2 = new ObstacleVertex();
			obstacleVertex.layer = RVOLayer.DefaultObstacle;
			obstacleVertex2.layer = RVOLayer.DefaultObstacle;
			obstacleVertex.prev = obstacleVertex2;
			obstacleVertex2.prev = obstacleVertex;
			obstacleVertex.next = obstacleVertex2;
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.position = a;
			obstacleVertex2.position = b;
			obstacleVertex.height = height;
			obstacleVertex2.height = height;
			obstacleVertex2.ignore = true;
			obstacleVertex.dir = new Vector2(b.x - a.x, b.z - a.z).normalized;
			obstacleVertex2.dir = -obstacleVertex.dir;
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x001B79D8 File Offset: 0x001B5BD8
		public void UpdateObstacle(ObstacleVertex obstacle, Vector3[] vertices, Matrix4x4 matrix)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (obstacle == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			bool flag = matrix == Matrix4x4.identity;
			this.BlockUntilSimulationStepIsDone();
			int i = 0;
			ObstacleVertex obstacleVertex = obstacle;
			while (i < vertices.Length)
			{
				obstacleVertex.position = (flag ? vertices[i] : matrix.MultiplyPoint3x4(vertices[i]));
				obstacleVertex = obstacleVertex.next;
				i++;
				if (obstacleVertex == obstacle || obstacleVertex == null)
				{
					obstacleVertex = obstacle;
					do
					{
						if (obstacleVertex.next == null)
						{
							obstacleVertex.dir = Vector2.zero;
						}
						else
						{
							Vector3 vector = obstacleVertex.next.position - obstacleVertex.position;
							obstacleVertex.dir = new Vector2(vector.x, vector.z).normalized;
						}
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacle && obstacleVertex != null);
					this.ScheduleCleanObstacles();
					this.UpdateObstacles();
					return;
				}
			}
			Debug.DrawLine(obstacleVertex.prev.position, obstacleVertex.position, Color.red);
			throw new ArgumentException("Obstacle has more vertices than supplied for updating (" + vertices.Length + " supplied)");
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x001B7B00 File Offset: 0x001B5D00
		private void ScheduleCleanObstacles()
		{
			this.doCleanObstacles = true;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x00002ACE File Offset: 0x00000CCE
		private void CleanObstacles()
		{
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x001B7B09 File Offset: 0x001B5D09
		public void RemoveObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Vertex must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Remove(v);
			this.UpdateObstacles();
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x001B7B32 File Offset: 0x001B5D32
		public void UpdateObstacles()
		{
			this.doUpdateObstacles = true;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x001B7B3C File Offset: 0x001B5D3C
		private void BuildQuadtree()
		{
			this.Quadtree.Clear();
			if (this.agents.Count > 0)
			{
				Rect bounds = Rect.MinMaxRect(this.agents[0].position.x, this.agents[0].position.y, this.agents[0].position.x, this.agents[0].position.y);
				for (int i = 1; i < this.agents.Count; i++)
				{
					Vector2 position = this.agents[i].position;
					bounds = Rect.MinMaxRect(Mathf.Min(bounds.xMin, position.x), Mathf.Min(bounds.yMin, position.y), Mathf.Max(bounds.xMax, position.x), Mathf.Max(bounds.yMax, position.y));
				}
				this.Quadtree.SetBounds(bounds);
				for (int j = 0; j < this.agents.Count; j++)
				{
					this.Quadtree.Insert(this.agents[j]);
				}
			}
			this.Quadtree.CalculateSpeeds();
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x001B7C80 File Offset: 0x001B5E80
		private void BlockUntilSimulationStepIsDone()
		{
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x001B7CC0 File Offset: 0x001B5EC0
		private void PreCalculation()
		{
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].PreCalculation();
			}
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x001B7CF4 File Offset: 0x001B5EF4
		private void CleanAndUpdateObstaclesIfNecessary()
		{
			if (this.doCleanObstacles)
			{
				this.CleanObstacles();
				this.doCleanObstacles = false;
				this.doUpdateObstacles = true;
			}
			if (this.doUpdateObstacles)
			{
				this.doUpdateObstacles = false;
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x001B7D24 File Offset: 0x001B5F24
		public void Update()
		{
			if (this.lastStep < 0f)
			{
				this.lastStep = Time.time;
				this.deltaTime = this.DesiredDeltaTime;
			}
			if (Time.time - this.lastStep >= this.DesiredDeltaTime)
			{
				this.deltaTime = Time.time - this.lastStep;
				this.lastStep = Time.time;
				this.deltaTime = Math.Max(this.deltaTime, 0.0005f);
				if (this.Multithreading)
				{
					if (this.doubleBuffering)
					{
						for (int i = 0; i < this.workers.Length; i++)
						{
							this.workers[i].WaitOne();
						}
						for (int j = 0; j < this.agents.Count; j++)
						{
							this.agents[j].PostCalculation();
						}
					}
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int k = 0; k < this.workers.Length; k++)
					{
						this.workers[k].start = k * this.agents.Count / this.workers.Length;
						this.workers[k].end = (k + 1) * this.agents.Count / this.workers.Length;
					}
					for (int l = 0; l < this.workers.Length; l++)
					{
						this.workers[l].Execute(1);
					}
					for (int m = 0; m < this.workers.Length; m++)
					{
						this.workers[m].WaitOne();
					}
					for (int n = 0; n < this.workers.Length; n++)
					{
						this.workers[n].Execute(0);
					}
					if (!this.doubleBuffering)
					{
						for (int num = 0; num < this.workers.Length; num++)
						{
							this.workers[num].WaitOne();
						}
						for (int num2 = 0; num2 < this.agents.Count; num2++)
						{
							this.agents[num2].PostCalculation();
						}
						return;
					}
				}
				else
				{
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int num3 = 0; num3 < this.agents.Count; num3++)
					{
						this.agents[num3].BufferSwitch();
					}
					for (int num4 = 0; num4 < this.agents.Count; num4++)
					{
						this.agents[num4].CalculateNeighbours();
						this.agents[num4].CalculateVelocity(this.coroutineWorkerContext);
					}
					for (int num5 = 0; num5 < this.agents.Count; num5++)
					{
						this.agents[num5].PostCalculation();
					}
				}
			}
		}

		// Token: 0x04004289 RID: 17033
		private readonly bool doubleBuffering = true;

		// Token: 0x0400428A RID: 17034
		private float desiredDeltaTime = 0.05f;

		// Token: 0x0400428B RID: 17035
		private readonly Simulator.Worker[] workers;

		// Token: 0x0400428C RID: 17036
		private List<Agent> agents;

		// Token: 0x0400428D RID: 17037
		public List<ObstacleVertex> obstacles;

		// Token: 0x0400428F RID: 17039
		private float deltaTime;

		// Token: 0x04004290 RID: 17040
		private float lastStep = -99999f;

		// Token: 0x04004291 RID: 17041
		private bool doUpdateObstacles;

		// Token: 0x04004292 RID: 17042
		private bool doCleanObstacles;

		// Token: 0x04004293 RID: 17043
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x04004294 RID: 17044
		public readonly MovementPlane movementPlane;

		// Token: 0x04004295 RID: 17045
		private Simulator.WorkerContext coroutineWorkerContext = new Simulator.WorkerContext();

		// Token: 0x02000769 RID: 1897
		internal class WorkerContext
		{
			// Token: 0x04004A3B RID: 19003
			public Agent.VOBuffer vos = new Agent.VOBuffer(16);

			// Token: 0x04004A3C RID: 19004
			public const int KeepCount = 3;

			// Token: 0x04004A3D RID: 19005
			public Vector2[] bestPos = new Vector2[3];

			// Token: 0x04004A3E RID: 19006
			public float[] bestSizes = new float[3];

			// Token: 0x04004A3F RID: 19007
			public float[] bestScores = new float[4];

			// Token: 0x04004A40 RID: 19008
			public Vector2[] samplePos = new Vector2[50];

			// Token: 0x04004A41 RID: 19009
			public float[] sampleSize = new float[50];
		}

		// Token: 0x0200076A RID: 1898
		private class Worker
		{
			// Token: 0x06002D83 RID: 11651 RVA: 0x001CF150 File Offset: 0x001CD350
			public Worker(Simulator sim)
			{
				this.simulator = sim;
				new Thread(new ThreadStart(this.Run))
				{
					IsBackground = true,
					Name = "RVO Simulator Thread"
				}.Start();
			}

			// Token: 0x06002D84 RID: 11652 RVA: 0x001CF1B5 File Offset: 0x001CD3B5
			public void Execute(int task)
			{
				this.task = task;
				this.waitFlag.Reset();
				this.runFlag.Set();
			}

			// Token: 0x06002D85 RID: 11653 RVA: 0x001CF1D6 File Offset: 0x001CD3D6
			public void WaitOne()
			{
				if (!this.terminate)
				{
					this.waitFlag.WaitOne();
				}
			}

			// Token: 0x06002D86 RID: 11654 RVA: 0x001CF1EC File Offset: 0x001CD3EC
			public void Terminate()
			{
				this.WaitOne();
				this.terminate = true;
				this.Execute(-1);
			}

			// Token: 0x06002D87 RID: 11655 RVA: 0x001CF204 File Offset: 0x001CD404
			public void Run()
			{
				this.runFlag.WaitOne();
				while (!this.terminate)
				{
					try
					{
						List<Agent> agents = this.simulator.GetAgents();
						if (this.task == 0)
						{
							for (int i = this.start; i < this.end; i++)
							{
								agents[i].CalculateNeighbours();
								agents[i].CalculateVelocity(this.context);
							}
						}
						else if (this.task == 1)
						{
							for (int j = this.start; j < this.end; j++)
							{
								agents[j].BufferSwitch();
							}
						}
						else
						{
							if (this.task != 2)
							{
								Debug.LogError("Invalid Task Number: " + this.task);
								throw new Exception("Invalid Task Number: " + this.task);
							}
							this.simulator.BuildQuadtree();
						}
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
					this.waitFlag.Set();
					this.runFlag.WaitOne();
				}
			}

			// Token: 0x04004A42 RID: 19010
			public int start;

			// Token: 0x04004A43 RID: 19011
			public int end;

			// Token: 0x04004A44 RID: 19012
			private readonly AutoResetEvent runFlag = new AutoResetEvent(false);

			// Token: 0x04004A45 RID: 19013
			private readonly ManualResetEvent waitFlag = new ManualResetEvent(true);

			// Token: 0x04004A46 RID: 19014
			private readonly Simulator simulator;

			// Token: 0x04004A47 RID: 19015
			private int task;

			// Token: 0x04004A48 RID: 19016
			private bool terminate;

			// Token: 0x04004A49 RID: 19017
			private Simulator.WorkerContext context = new Simulator.WorkerContext();
		}
	}
}
