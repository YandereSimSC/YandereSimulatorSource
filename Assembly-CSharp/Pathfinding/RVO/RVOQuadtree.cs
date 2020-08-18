using System;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005C6 RID: 1478
	public class RVOQuadtree
	{
		// Token: 0x0600286C RID: 10348 RVA: 0x001B7FDD File Offset: 0x001B61DD
		public void Clear()
		{
			this.nodes[0] = default(RVOQuadtree.Node);
			this.filledNodes = 1;
			this.maxRadius = 0f;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x001B8003 File Offset: 0x001B6203
		public void SetBounds(Rect r)
		{
			this.bounds = r;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x001B800C File Offset: 0x001B620C
		private int GetNodeIndex()
		{
			if (this.filledNodes == this.nodes.Length)
			{
				RVOQuadtree.Node[] array = new RVOQuadtree.Node[this.nodes.Length * 2];
				for (int i = 0; i < this.nodes.Length; i++)
				{
					array[i] = this.nodes[i];
				}
				this.nodes = array;
			}
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			return this.filledNodes - 1;
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x001B80B0 File Offset: 0x001B62B0
		public void Insert(Agent agent)
		{
			int num = 0;
			Rect r = this.bounds;
			Vector2 vector = new Vector2(agent.position.x, agent.position.y);
			agent.next = null;
			this.maxRadius = Math.Max(agent.radius, this.maxRadius);
			int num2 = 0;
			for (;;)
			{
				num2++;
				if (this.nodes[num].child00 == num)
				{
					if (this.nodes[num].count < 15 || num2 > 10)
					{
						break;
					}
					RVOQuadtree.Node node = this.nodes[num];
					node.child00 = this.GetNodeIndex();
					node.child01 = this.GetNodeIndex();
					node.child10 = this.GetNodeIndex();
					node.child11 = this.GetNodeIndex();
					this.nodes[num] = node;
					this.nodes[num].Distribute(this.nodes, r);
				}
				if (this.nodes[num].child00 != num)
				{
					Vector2 center = r.center;
					if (vector.x > center.x)
					{
						if (vector.y > center.y)
						{
							num = this.nodes[num].child11;
							r = Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax);
						}
						else
						{
							num = this.nodes[num].child10;
							r = Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y);
						}
					}
					else if (vector.y > center.y)
					{
						num = this.nodes[num].child01;
						r = Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax);
					}
					else
					{
						num = this.nodes[num].child00;
						r = Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y);
					}
				}
			}
			this.nodes[num].Add(agent);
			RVOQuadtree.Node[] array = this.nodes;
			int num3 = num;
			array[num3].count = array[num3].count + 1;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x001B82FD File Offset: 0x001B64FD
		public void CalculateSpeeds()
		{
			this.nodes[0].CalculateMaxSpeed(this.nodes, 0);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x001B8318 File Offset: 0x001B6518
		public void Query(Vector2 p, float speed, float timeHorizon, float agentRadius, Agent agent)
		{
			RVOQuadtree.QuadtreeQuery quadtreeQuery = default(RVOQuadtree.QuadtreeQuery);
			quadtreeQuery.p = p;
			quadtreeQuery.speed = speed;
			quadtreeQuery.timeHorizon = timeHorizon;
			quadtreeQuery.maxRadius = float.PositiveInfinity;
			quadtreeQuery.agentRadius = agentRadius;
			quadtreeQuery.agent = agent;
			quadtreeQuery.nodes = this.nodes;
			quadtreeQuery.QueryRec(0, this.bounds);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x001B837E File Offset: 0x001B657E
		public void DebugDraw()
		{
			this.DebugDrawRec(0, this.bounds);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x001B8390 File Offset: 0x001B6590
		private void DebugDrawRec(int i, Rect r)
		{
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMin), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMin), Color.white);
			if (this.nodes[i].child00 != i)
			{
				Vector2 center = r.center;
				this.DebugDrawRec(this.nodes[i].child11, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
				this.DebugDrawRec(this.nodes[i].child10, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
				this.DebugDrawRec(this.nodes[i].child01, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
				this.DebugDrawRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
			}
			for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
			{
				Vector2 position = this.nodes[i].linkedList.position;
				Debug.DrawLine(new Vector3(position.x, 0f, position.y) + Vector3.up, new Vector3(agent.position.x, 0f, agent.position.y) + Vector3.up, new Color(1f, 1f, 0f, 0.5f));
			}
		}

		// Token: 0x04004298 RID: 17048
		private const int LeafSize = 15;

		// Token: 0x04004299 RID: 17049
		private float maxRadius;

		// Token: 0x0400429A RID: 17050
		private RVOQuadtree.Node[] nodes = new RVOQuadtree.Node[42];

		// Token: 0x0400429B RID: 17051
		private int filledNodes = 1;

		// Token: 0x0400429C RID: 17052
		private Rect bounds;

		// Token: 0x0200076B RID: 1899
		private struct Node
		{
			// Token: 0x06002D88 RID: 11656 RVA: 0x001CF324 File Offset: 0x001CD524
			public void Add(Agent agent)
			{
				agent.next = this.linkedList;
				this.linkedList = agent;
			}

			// Token: 0x06002D89 RID: 11657 RVA: 0x001CF33C File Offset: 0x001CD53C
			public void Distribute(RVOQuadtree.Node[] nodes, Rect r)
			{
				Vector2 center = r.center;
				while (this.linkedList != null)
				{
					Agent next = this.linkedList.next;
					if (this.linkedList.position.x > center.x)
					{
						if (this.linkedList.position.y > center.y)
						{
							nodes[this.child11].Add(this.linkedList);
						}
						else
						{
							nodes[this.child10].Add(this.linkedList);
						}
					}
					else if (this.linkedList.position.y > center.y)
					{
						nodes[this.child01].Add(this.linkedList);
					}
					else
					{
						nodes[this.child00].Add(this.linkedList);
					}
					this.linkedList = next;
				}
				this.count = 0;
			}

			// Token: 0x06002D8A RID: 11658 RVA: 0x001CF428 File Offset: 0x001CD628
			public float CalculateMaxSpeed(RVOQuadtree.Node[] nodes, int index)
			{
				if (this.child00 == index)
				{
					for (Agent next = this.linkedList; next != null; next = next.next)
					{
						this.maxSpeed = Math.Max(this.maxSpeed, next.CalculatedSpeed);
					}
				}
				else
				{
					this.maxSpeed = Math.Max(nodes[this.child00].CalculateMaxSpeed(nodes, this.child00), nodes[this.child01].CalculateMaxSpeed(nodes, this.child01));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child10].CalculateMaxSpeed(nodes, this.child10));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child11].CalculateMaxSpeed(nodes, this.child11));
				}
				return this.maxSpeed;
			}

			// Token: 0x04004A4A RID: 19018
			public int child00;

			// Token: 0x04004A4B RID: 19019
			public int child01;

			// Token: 0x04004A4C RID: 19020
			public int child10;

			// Token: 0x04004A4D RID: 19021
			public int child11;

			// Token: 0x04004A4E RID: 19022
			public Agent linkedList;

			// Token: 0x04004A4F RID: 19023
			public byte count;

			// Token: 0x04004A50 RID: 19024
			public float maxSpeed;
		}

		// Token: 0x0200076C RID: 1900
		private struct QuadtreeQuery
		{
			// Token: 0x06002D8B RID: 11659 RVA: 0x001CF500 File Offset: 0x001CD700
			public void QueryRec(int i, Rect r)
			{
				float num = Math.Min(Math.Max((this.nodes[i].maxSpeed + this.speed) * this.timeHorizon, this.agentRadius) + this.agentRadius, this.maxRadius);
				if (this.nodes[i].child00 == i)
				{
					for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
					{
						float num2 = this.agent.InsertAgentNeighbour(agent, num * num);
						if (num2 < this.maxRadius * this.maxRadius)
						{
							this.maxRadius = Mathf.Sqrt(num2);
						}
					}
					return;
				}
				Vector2 center = r.center;
				if (this.p.x - num < center.x)
				{
					if (this.p.y - num < center.y)
					{
						this.QueryRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
						num = Math.Min(num, this.maxRadius);
					}
					if (this.p.y + num > center.y)
					{
						this.QueryRec(this.nodes[i].child01, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
						num = Math.Min(num, this.maxRadius);
					}
				}
				if (this.p.x + num > center.x)
				{
					if (this.p.y - num < center.y)
					{
						this.QueryRec(this.nodes[i].child10, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
						num = Math.Min(num, this.maxRadius);
					}
					if (this.p.y + num > center.y)
					{
						this.QueryRec(this.nodes[i].child11, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
					}
				}
			}

			// Token: 0x04004A51 RID: 19025
			public Vector2 p;

			// Token: 0x04004A52 RID: 19026
			public float speed;

			// Token: 0x04004A53 RID: 19027
			public float timeHorizon;

			// Token: 0x04004A54 RID: 19028
			public float agentRadius;

			// Token: 0x04004A55 RID: 19029
			public float maxRadius;

			// Token: 0x04004A56 RID: 19030
			public Agent agent;

			// Token: 0x04004A57 RID: 19031
			public RVOQuadtree.Node[] nodes;
		}
	}
}
