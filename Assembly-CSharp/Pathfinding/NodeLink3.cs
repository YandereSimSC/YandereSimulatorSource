using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052D RID: 1325
	[AddComponentMenu("Pathfinding/Link3")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_node_link3.php")]
	public class NodeLink3 : GraphModifier
	{
		// Token: 0x060022F2 RID: 8946 RVA: 0x001927C0 File Offset: 0x001909C0
		public static NodeLink3 GetNodeLink(GraphNode node)
		{
			NodeLink3 result;
			NodeLink3.reference.TryGetValue(node, out result);
			return result;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x00191A71 File Offset: 0x0018FC71
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x001927DC File Offset: 0x001909DC
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x001927E4 File Offset: 0x001909E4
		public GraphNode StartNode
		{
			get
			{
				return this.startNode;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x001927EC File Offset: 0x001909EC
		public GraphNode EndNode
		{
			get
			{
				return this.endNode;
			}
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x001927F4 File Offset: 0x001909F4
		public override void OnPostScan()
		{
			if (AstarPath.active.isScanning)
			{
				this.InternalOnPostScan();
				return;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
			{
				this.InternalOnPostScan();
				return true;
			}));
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00192824 File Offset: 0x00190A24
		public void InternalOnPostScan()
		{
			if (AstarPath.active.data.pointGraph == null)
			{
				AstarPath.active.data.AddGraph(typeof(PointGraph));
			}
			this.startNode = AstarPath.active.data.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.StartTransform.position);
			this.startNode.link = this;
			this.endNode = AstarPath.active.data.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.EndTransform.position);
			this.endNode.link = this;
			this.connectedNode1 = null;
			this.connectedNode2 = null;
			if (this.startNode == null || this.endNode == null)
			{
				this.startNode = null;
				this.endNode = null;
				return;
			}
			this.postScanCalled = true;
			NodeLink3.reference[this.startNode] = this;
			NodeLink3.reference[this.endNode] = this;
			this.Apply(true);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x0019293C File Offset: 0x00190B3C
		public override void OnGraphsPostUpdate()
		{
			if (!AstarPath.active.isScanning)
			{
				if (this.connectedNode1 != null && this.connectedNode1.Destroyed)
				{
					this.connectedNode1 = null;
				}
				if (this.connectedNode2 != null && this.connectedNode2.Destroyed)
				{
					this.connectedNode2 = null;
				}
				if (!this.postScanCalled)
				{
					this.OnPostScan();
					return;
				}
				this.Apply(false);
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x001929A3 File Offset: 0x00190BA3
		protected override void OnEnable()
		{
			base.OnEnable();
			if (Application.isPlaying && AstarPath.active != null && AstarPath.active.data != null && AstarPath.active.data.pointGraph != null)
			{
				this.OnGraphsPostUpdate();
			}
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x001929E4 File Offset: 0x00190BE4
		protected override void OnDisable()
		{
			base.OnDisable();
			this.postScanCalled = false;
			if (this.startNode != null)
			{
				NodeLink3.reference.Remove(this.startNode);
			}
			if (this.endNode != null)
			{
				NodeLink3.reference.Remove(this.endNode);
			}
			if (this.startNode != null && this.endNode != null)
			{
				this.startNode.RemoveConnection(this.endNode);
				this.endNode.RemoveConnection(this.startNode);
				if (this.connectedNode1 != null && this.connectedNode2 != null)
				{
					this.startNode.RemoveConnection(this.connectedNode1);
					this.connectedNode1.RemoveConnection(this.startNode);
					this.endNode.RemoveConnection(this.connectedNode2);
					this.connectedNode2.RemoveConnection(this.endNode);
				}
			}
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00191FB6 File Offset: 0x001901B6
		private void RemoveConnections(GraphNode node)
		{
			node.ClearConnections(true);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00192AB6 File Offset: 0x00190CB6
		[ContextMenu("Recalculate neighbours")]
		private void ContextApplyForce()
		{
			if (Application.isPlaying)
			{
				this.Apply(true);
				if (AstarPath.active != null)
				{
					AstarPath.active.FloodFill();
				}
			}
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x00192AE0 File Offset: 0x00190CE0
		public void Apply(bool forceNewCheck)
		{
			NNConstraint none = NNConstraint.None;
			none.distanceXZ = true;
			int graphIndex = (int)this.startNode.GraphIndex;
			none.graphMask = ~(1 << graphIndex);
			bool flag = true;
			NNInfo nearest = AstarPath.active.GetNearest(this.StartTransform.position, none);
			flag &= (nearest.node == this.connectedNode1 && nearest.node != null);
			this.connectedNode1 = (nearest.node as MeshNode);
			this.clamped1 = nearest.position;
			if (this.connectedNode1 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode1.position, Vector3.up * 5f, Color.red);
			}
			NNInfo nearest2 = AstarPath.active.GetNearest(this.EndTransform.position, none);
			flag &= (nearest2.node == this.connectedNode2 && nearest2.node != null);
			this.connectedNode2 = (nearest2.node as MeshNode);
			this.clamped2 = nearest2.position;
			if (this.connectedNode2 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode2.position, Vector3.up * 5f, Color.cyan);
			}
			if (this.connectedNode2 == null || this.connectedNode1 == null)
			{
				return;
			}
			this.startNode.SetPosition((Int3)this.StartTransform.position);
			this.endNode.SetPosition((Int3)this.EndTransform.position);
			if (flag && !forceNewCheck)
			{
				return;
			}
			this.RemoveConnections(this.startNode);
			this.RemoveConnections(this.endNode);
			uint cost = (uint)Mathf.RoundToInt((float)((Int3)(this.StartTransform.position - this.EndTransform.position)).costMagnitude * this.costFactor);
			this.startNode.AddConnection(this.endNode, cost);
			this.endNode.AddConnection(this.startNode, cost);
			Int3 rhs = this.connectedNode2.position - this.connectedNode1.position;
			for (int i = 0; i < this.connectedNode1.GetVertexCount(); i++)
			{
				Int3 vertex = this.connectedNode1.GetVertex(i);
				Int3 vertex2 = this.connectedNode1.GetVertex((i + 1) % this.connectedNode1.GetVertexCount());
				if (Int3.DotLong((vertex2 - vertex).Normal2D(), rhs) <= 0L)
				{
					for (int j = 0; j < this.connectedNode2.GetVertexCount(); j++)
					{
						Int3 vertex3 = this.connectedNode2.GetVertex(j);
						Int3 vertex4 = this.connectedNode2.GetVertex((j + 1) % this.connectedNode2.GetVertexCount());
						if (Int3.DotLong((vertex4 - vertex3).Normal2D(), rhs) >= 0L && (double)Int3.Angle(vertex4 - vertex3, vertex2 - vertex) > 2.967059810956319)
						{
							float num = 0f;
							float num2 = 1f;
							num2 = Math.Min(num2, VectorMath.ClosestPointOnLineFactor(vertex, vertex2, vertex3));
							num = Math.Max(num, VectorMath.ClosestPointOnLineFactor(vertex, vertex2, vertex4));
							if (num2 >= num)
							{
								Vector3 vector = (Vector3)(vertex2 - vertex) * num + (Vector3)vertex;
								Vector3 vector2 = (Vector3)(vertex2 - vertex) * num2 + (Vector3)vertex;
								this.startNode.portalA = vector;
								this.startNode.portalB = vector2;
								this.endNode.portalA = vector2;
								this.endNode.portalB = vector;
								this.connectedNode1.AddConnection(this.startNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
								this.connectedNode2.AddConnection(this.endNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
								this.startNode.AddConnection(this.connectedNode1, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
								this.endNode.AddConnection(this.connectedNode2, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
								return;
							}
							Debug.LogError(string.Concat(new object[]
							{
								"Something went wrong! ",
								num,
								" ",
								num2,
								" ",
								vertex,
								" ",
								vertex2,
								" ",
								vertex3,
								" ",
								vertex4,
								"\nTODO, how can this happen?"
							}));
						}
					}
				}
			}
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0019304F File Offset: 0x0019124F
		public virtual void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00193058 File Offset: 0x00191258
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x00193064 File Offset: 0x00191264
		public void OnDrawGizmos(bool selected)
		{
			Color color = selected ? NodeLink3.GizmosColorSelected : NodeLink3.GizmosColor;
			if (this.StartTransform != null)
			{
				Draw.Gizmos.CircleXZ(this.StartTransform.position, 0.4f, color, 0f, 6.2831855f);
			}
			if (this.EndTransform != null)
			{
				Draw.Gizmos.CircleXZ(this.EndTransform.position, 0.4f, color, 0f, 6.2831855f);
			}
			if (this.StartTransform != null && this.EndTransform != null)
			{
				Draw.Gizmos.Bezier(this.StartTransform.position, this.EndTransform.position, color);
				if (selected)
				{
					Vector3 normalized = Vector3.Cross(Vector3.up, this.EndTransform.position - this.StartTransform.position).normalized;
					Draw.Gizmos.Bezier(this.StartTransform.position + normalized * 0.1f, this.EndTransform.position + normalized * 0.1f, color);
					Draw.Gizmos.Bezier(this.StartTransform.position - normalized * 0.1f, this.EndTransform.position - normalized * 0.1f, color);
				}
			}
		}

		// Token: 0x04003F2C RID: 16172
		protected static Dictionary<GraphNode, NodeLink3> reference = new Dictionary<GraphNode, NodeLink3>();

		// Token: 0x04003F2D RID: 16173
		public Transform end;

		// Token: 0x04003F2E RID: 16174
		public float costFactor = 1f;

		// Token: 0x04003F2F RID: 16175
		public bool oneWay;

		// Token: 0x04003F30 RID: 16176
		private NodeLink3Node startNode;

		// Token: 0x04003F31 RID: 16177
		private NodeLink3Node endNode;

		// Token: 0x04003F32 RID: 16178
		private MeshNode connectedNode1;

		// Token: 0x04003F33 RID: 16179
		private MeshNode connectedNode2;

		// Token: 0x04003F34 RID: 16180
		private Vector3 clamped1;

		// Token: 0x04003F35 RID: 16181
		private Vector3 clamped2;

		// Token: 0x04003F36 RID: 16182
		private bool postScanCalled;

		// Token: 0x04003F37 RID: 16183
		private static readonly Color GizmosColor = new Color(0.80784315f, 0.53333336f, 0.1882353f, 0.5f);

		// Token: 0x04003F38 RID: 16184
		private static readonly Color GizmosColorSelected = new Color(0.92156863f, 0.48235294f, 0.1254902f, 1f);
	}
}
