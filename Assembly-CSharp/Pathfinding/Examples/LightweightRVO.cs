using System;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005E5 RID: 1509
	[RequireComponent(typeof(MeshFilter))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_lightweight_r_v_o.php")]
	public class LightweightRVO : MonoBehaviour
	{
		// Token: 0x060029A6 RID: 10662 RVA: 0x001BF9A4 File Offset: 0x001BDBA4
		public void Start()
		{
			this.mesh = new Mesh();
			RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
			if (rvosimulator == null)
			{
				Debug.LogError("No RVOSimulator could be found in the scene. Please add a RVOSimulator component to any GameObject");
				return;
			}
			this.sim = rvosimulator.GetSimulator();
			base.GetComponent<MeshFilter>().mesh = this.mesh;
			this.CreateAgents(this.agentCount);
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x001BFA10 File Offset: 0x001BDC10
		public void OnGUI()
		{
			if (GUILayout.Button("2", Array.Empty<GUILayoutOption>()))
			{
				this.CreateAgents(2);
			}
			if (GUILayout.Button("10", Array.Empty<GUILayoutOption>()))
			{
				this.CreateAgents(10);
			}
			if (GUILayout.Button("100", Array.Empty<GUILayoutOption>()))
			{
				this.CreateAgents(100);
			}
			if (GUILayout.Button("500", Array.Empty<GUILayoutOption>()))
			{
				this.CreateAgents(500);
			}
			if (GUILayout.Button("1000", Array.Empty<GUILayoutOption>()))
			{
				this.CreateAgents(1000);
			}
			if (GUILayout.Button("5000", Array.Empty<GUILayoutOption>()))
			{
				this.CreateAgents(5000);
			}
			GUILayout.Space(5f);
			if (GUILayout.Button("Random Streams", Array.Empty<GUILayoutOption>()))
			{
				this.type = LightweightRVO.RVOExampleType.RandomStreams;
				this.CreateAgents((this.agents != null) ? this.agents.Count : 100);
			}
			if (GUILayout.Button("Line", Array.Empty<GUILayoutOption>()))
			{
				this.type = LightweightRVO.RVOExampleType.Line;
				this.CreateAgents((this.agents != null) ? Mathf.Min(this.agents.Count, 100) : 10);
			}
			if (GUILayout.Button("Circle", Array.Empty<GUILayoutOption>()))
			{
				this.type = LightweightRVO.RVOExampleType.Circle;
				this.CreateAgents((this.agents != null) ? this.agents.Count : 100);
			}
			if (GUILayout.Button("Point", Array.Empty<GUILayoutOption>()))
			{
				this.type = LightweightRVO.RVOExampleType.Point;
				this.CreateAgents((this.agents != null) ? this.agents.Count : 100);
			}
			if (GUILayout.Button("Crossing", Array.Empty<GUILayoutOption>()))
			{
				this.type = LightweightRVO.RVOExampleType.Crossing;
				this.CreateAgents((this.agents != null) ? this.agents.Count : 100);
			}
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x001BFBD8 File Offset: 0x001BDDD8
		private float uniformDistance(float radius)
		{
			float num = UnityEngine.Random.value + UnityEngine.Random.value;
			if (num > 1f)
			{
				return radius * (2f - num);
			}
			return radius * num;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x001BFC08 File Offset: 0x001BDE08
		public void CreateAgents(int num)
		{
			this.agentCount = num;
			this.agents = new List<IAgent>(this.agentCount);
			this.goals = new List<Vector3>(this.agentCount);
			this.colors = new List<Color>(this.agentCount);
			this.sim.ClearAgents();
			if (this.type == LightweightRVO.RVOExampleType.Circle)
			{
				float d = Mathf.Sqrt((float)this.agentCount * this.radius * this.radius * 4f / 3.1415927f) * this.exampleScale * 0.05f;
				for (int i = 0; i < this.agentCount; i++)
				{
					Vector3 vector = new Vector3(Mathf.Cos((float)i * 3.1415927f * 2f / (float)this.agentCount), 0f, Mathf.Sin((float)i * 3.1415927f * 2f / (float)this.agentCount)) * d * (1f + UnityEngine.Random.value * 0.01f);
					IAgent item = this.sim.AddAgent(new Vector2(vector.x, vector.z), vector.y);
					this.agents.Add(item);
					this.goals.Add(-vector);
					this.colors.Add(AstarMath.HSVToRGB((float)i * 360f / (float)this.agentCount, 0.8f, 0.6f));
				}
			}
			else if (this.type == LightweightRVO.RVOExampleType.Line)
			{
				for (int j = 0; j < this.agentCount; j++)
				{
					Vector3 vector2 = new Vector3((float)((j % 2 == 0) ? 1 : -1) * this.exampleScale, 0f, (float)(j / 2) * this.radius * 2.5f);
					IAgent item2 = this.sim.AddAgent(new Vector2(vector2.x, vector2.z), vector2.y);
					this.agents.Add(item2);
					this.goals.Add(new Vector3(-vector2.x, vector2.y, vector2.z));
					this.colors.Add((j % 2 == 0) ? Color.red : Color.blue);
				}
			}
			else if (this.type == LightweightRVO.RVOExampleType.Point)
			{
				for (int k = 0; k < this.agentCount; k++)
				{
					Vector3 vector3 = new Vector3(Mathf.Cos((float)k * 3.1415927f * 2f / (float)this.agentCount), 0f, Mathf.Sin((float)k * 3.1415927f * 2f / (float)this.agentCount)) * this.exampleScale;
					IAgent item3 = this.sim.AddAgent(new Vector2(vector3.x, vector3.z), vector3.y);
					this.agents.Add(item3);
					this.goals.Add(new Vector3(0f, vector3.y, 0f));
					this.colors.Add(AstarMath.HSVToRGB((float)k * 360f / (float)this.agentCount, 0.8f, 0.6f));
				}
			}
			else if (this.type == LightweightRVO.RVOExampleType.RandomStreams)
			{
				float num2 = Mathf.Sqrt((float)this.agentCount * this.radius * this.radius * 4f / 3.1415927f) * this.exampleScale * 0.05f;
				for (int l = 0; l < this.agentCount; l++)
				{
					float f = UnityEngine.Random.value * 3.1415927f * 2f;
					float num3 = UnityEngine.Random.value * 3.1415927f * 2f;
					Vector3 vector4 = new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * this.uniformDistance(num2);
					IAgent item4 = this.sim.AddAgent(new Vector2(vector4.x, vector4.z), vector4.y);
					this.agents.Add(item4);
					this.goals.Add(new Vector3(Mathf.Cos(num3), 0f, Mathf.Sin(num3)) * this.uniformDistance(num2));
					this.colors.Add(AstarMath.HSVToRGB(num3 * 57.29578f, 0.8f, 0.6f));
				}
			}
			else if (this.type == LightweightRVO.RVOExampleType.Crossing)
			{
				float num4 = this.exampleScale * this.radius * 0.5f;
				int num5 = (int)Mathf.Sqrt((float)this.agentCount / 25f);
				num5 = Mathf.Max(num5, 2);
				for (int m = 0; m < this.agentCount; m++)
				{
					float num6 = (float)(m % num5) / (float)num5 * 3.1415927f * 2f;
					float d2 = num4 * ((float)(m / (num5 * 10) + 1) + 0.3f * UnityEngine.Random.value);
					Vector3 vector5 = new Vector3(Mathf.Cos(num6), 0f, Mathf.Sin(num6)) * d2;
					IAgent agent = this.sim.AddAgent(new Vector2(vector5.x, vector5.z), vector5.y);
					agent.Priority = ((m % num5 == 0) ? 1f : 0.01f);
					this.agents.Add(agent);
					this.goals.Add(-vector5.normalized * num4 * 3f);
					this.colors.Add(AstarMath.HSVToRGB(num6 * 57.29578f, 0.8f, 0.6f));
				}
			}
			this.SetAgentSettings();
			this.verts = new Vector3[4 * this.agents.Count];
			this.uv = new Vector2[this.verts.Length];
			this.tris = new int[this.agents.Count * 2 * 3];
			this.meshColors = new Color[this.verts.Length];
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x001C0224 File Offset: 0x001BE424
		private void SetAgentSettings()
		{
			for (int i = 0; i < this.agents.Count; i++)
			{
				IAgent agent = this.agents[i];
				agent.Radius = this.radius;
				agent.AgentTimeHorizon = this.agentTimeHorizon;
				agent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
				agent.MaxNeighbours = this.maxNeighbours;
				agent.DebugDraw = (i == 0 && this.debug);
			}
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x001C0294 File Offset: 0x001BE494
		public void Update()
		{
			if (this.agents == null || this.mesh == null)
			{
				return;
			}
			if (this.agents.Count != this.goals.Count)
			{
				Debug.LogError("Agent count does not match goal count");
				return;
			}
			this.SetAgentSettings();
			if (this.interpolatedVelocities == null || this.interpolatedVelocities.Length < this.agents.Count)
			{
				Vector2[] array = new Vector2[this.agents.Count];
				Vector2[] array2 = new Vector2[this.agents.Count];
				if (this.interpolatedVelocities != null)
				{
					for (int i = 0; i < this.interpolatedVelocities.Length; i++)
					{
						array[i] = this.interpolatedVelocities[i];
					}
				}
				if (this.interpolatedRotations != null)
				{
					for (int j = 0; j < this.interpolatedRotations.Length; j++)
					{
						array2[j] = this.interpolatedRotations[j];
					}
				}
				this.interpolatedVelocities = array;
				this.interpolatedRotations = array2;
			}
			for (int k = 0; k < this.agents.Count; k++)
			{
				IAgent agent = this.agents[k];
				Vector2 vector = agent.Position;
				Vector2 b = Vector2.ClampMagnitude(agent.CalculatedTargetPoint - vector, agent.CalculatedSpeed * Time.deltaTime);
				vector += b;
				agent.Position = vector;
				agent.ElevationCoordinate = 0f;
				Vector2 vector2 = new Vector2(this.goals[k].x, this.goals[k].z);
				float magnitude = (vector2 - vector).magnitude;
				agent.SetTarget(vector2, Mathf.Min(magnitude, this.maxSpeed), this.maxSpeed * 1.1f);
				this.interpolatedVelocities[k] += b;
				if (this.interpolatedVelocities[k].magnitude > this.maxSpeed * 0.1f)
				{
					this.interpolatedVelocities[k] = Vector2.ClampMagnitude(this.interpolatedVelocities[k], this.maxSpeed * 0.1f);
					this.interpolatedRotations[k] = Vector2.Lerp(this.interpolatedRotations[k], this.interpolatedVelocities[k], agent.CalculatedSpeed * Time.deltaTime * 4f);
				}
				Vector3 vector3 = new Vector3(this.interpolatedRotations[k].x, 0f, this.interpolatedRotations[k].y).normalized * agent.Radius;
				if (vector3 == Vector3.zero)
				{
					vector3 = new Vector3(0f, 0f, agent.Radius);
				}
				Vector3 b2 = Vector3.Cross(Vector3.up, vector3);
				Vector3 a = new Vector3(agent.Position.x, agent.ElevationCoordinate, agent.Position.y) + this.renderingOffset;
				int num = 4 * k;
				int num2 = 6 * k;
				this.verts[num] = a + vector3 - b2;
				this.verts[num + 1] = a + vector3 + b2;
				this.verts[num + 2] = a - vector3 + b2;
				this.verts[num + 3] = a - vector3 - b2;
				this.uv[num] = new Vector2(0f, 1f);
				this.uv[num + 1] = new Vector2(1f, 1f);
				this.uv[num + 2] = new Vector2(1f, 0f);
				this.uv[num + 3] = new Vector2(0f, 0f);
				this.meshColors[num] = this.colors[k];
				this.meshColors[num + 1] = this.colors[k];
				this.meshColors[num + 2] = this.colors[k];
				this.meshColors[num + 3] = this.colors[k];
				this.tris[num2] = num;
				this.tris[num2 + 1] = num + 1;
				this.tris[num2 + 2] = num + 2;
				this.tris[num2 + 3] = num;
				this.tris[num2 + 4] = num + 2;
				this.tris[num2 + 5] = num + 3;
			}
			this.mesh.Clear();
			this.mesh.vertices = this.verts;
			this.mesh.uv = this.uv;
			this.mesh.colors = this.meshColors;
			this.mesh.triangles = this.tris;
			this.mesh.RecalculateNormals();
		}

		// Token: 0x04004357 RID: 17239
		public int agentCount = 100;

		// Token: 0x04004358 RID: 17240
		public float exampleScale = 100f;

		// Token: 0x04004359 RID: 17241
		public LightweightRVO.RVOExampleType type;

		// Token: 0x0400435A RID: 17242
		public float radius = 3f;

		// Token: 0x0400435B RID: 17243
		public float maxSpeed = 2f;

		// Token: 0x0400435C RID: 17244
		public float agentTimeHorizon = 10f;

		// Token: 0x0400435D RID: 17245
		[HideInInspector]
		public float obstacleTimeHorizon = 10f;

		// Token: 0x0400435E RID: 17246
		public int maxNeighbours = 10;

		// Token: 0x0400435F RID: 17247
		public Vector3 renderingOffset = Vector3.up * 0.1f;

		// Token: 0x04004360 RID: 17248
		public bool debug;

		// Token: 0x04004361 RID: 17249
		private Mesh mesh;

		// Token: 0x04004362 RID: 17250
		private Simulator sim;

		// Token: 0x04004363 RID: 17251
		private List<IAgent> agents;

		// Token: 0x04004364 RID: 17252
		private List<Vector3> goals;

		// Token: 0x04004365 RID: 17253
		private List<Color> colors;

		// Token: 0x04004366 RID: 17254
		private Vector3[] verts;

		// Token: 0x04004367 RID: 17255
		private Vector2[] uv;

		// Token: 0x04004368 RID: 17256
		private int[] tris;

		// Token: 0x04004369 RID: 17257
		private Color[] meshColors;

		// Token: 0x0400436A RID: 17258
		private Vector2[] interpolatedVelocities;

		// Token: 0x0400436B RID: 17259
		private Vector2[] interpolatedRotations;

		// Token: 0x0200077E RID: 1918
		public enum RVOExampleType
		{
			// Token: 0x04004AA6 RID: 19110
			Circle,
			// Token: 0x04004AA7 RID: 19111
			Line,
			// Token: 0x04004AA8 RID: 19112
			Point,
			// Token: 0x04004AA9 RID: 19113
			RandomStreams,
			// Token: 0x04004AAA RID: 19114
			Crossing
		}
	}
}
