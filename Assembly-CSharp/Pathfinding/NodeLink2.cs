using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052B RID: 1323
	[AddComponentMenu("Pathfinding/Link2")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_node_link2.php")]
	public class NodeLink2 : GraphModifier
	{
		// Token: 0x060022D6 RID: 8918 RVA: 0x00191C34 File Offset: 0x0018FE34
		public static NodeLink2 GetNodeLink(GraphNode node)
		{
			NodeLink2 result;
			NodeLink2.reference.TryGetValue(node, out result);
			return result;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x00191A71 File Offset: 0x0018FC71
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x00191C50 File Offset: 0x0018FE50
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x00191C58 File Offset: 0x0018FE58
		// (set) Token: 0x060022DA RID: 8922 RVA: 0x00191C60 File Offset: 0x0018FE60
		public PointNode startNode { get; private set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x00191C69 File Offset: 0x0018FE69
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x00191C71 File Offset: 0x0018FE71
		public PointNode endNode { get; private set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x00191C7A File Offset: 0x0018FE7A
		[Obsolete("Use startNode instead (lowercase s)")]
		public GraphNode StartNode
		{
			get
			{
				return this.startNode;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x00191C82 File Offset: 0x0018FE82
		[Obsolete("Use endNode instead (lowercase e)")]
		public GraphNode EndNode
		{
			get
			{
				return this.endNode;
			}
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x00191C8A File Offset: 0x0018FE8A
		public override void OnPostScan()
		{
			this.InternalOnPostScan();
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00191C94 File Offset: 0x0018FE94
		public void InternalOnPostScan()
		{
			if (this.EndTransform == null || this.StartTransform == null)
			{
				return;
			}
			if (AstarPath.active.data.pointGraph == null)
			{
				(AstarPath.active.data.AddGraph(typeof(PointGraph)) as PointGraph).name = "PointGraph (used for node links)";
			}
			if (this.startNode != null && this.startNode.Destroyed)
			{
				NodeLink2.reference.Remove(this.startNode);
				this.startNode = null;
			}
			if (this.endNode != null && this.endNode.Destroyed)
			{
				NodeLink2.reference.Remove(this.endNode);
				this.endNode = null;
			}
			if (this.startNode == null)
			{
				this.startNode = AstarPath.active.data.pointGraph.AddNode((Int3)this.StartTransform.position);
			}
			if (this.endNode == null)
			{
				this.endNode = AstarPath.active.data.pointGraph.AddNode((Int3)this.EndTransform.position);
			}
			this.connectedNode1 = null;
			this.connectedNode2 = null;
			if (this.startNode == null || this.endNode == null)
			{
				this.startNode = null;
				this.endNode = null;
				return;
			}
			this.postScanCalled = true;
			NodeLink2.reference[this.startNode] = this;
			NodeLink2.reference[this.endNode] = this;
			this.Apply(true);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00191E14 File Offset: 0x00190014
		public override void OnGraphsPostUpdate()
		{
			if (AstarPath.active.isScanning)
			{
				return;
			}
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

		// Token: 0x060022E2 RID: 8930 RVA: 0x00191E7C File Offset: 0x0019007C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (Application.isPlaying && AstarPath.active != null && AstarPath.active.data != null && AstarPath.active.data.pointGraph != null && !AstarPath.active.isScanning)
			{
				AstarPath.active.AddWorkItem(new Action(this.OnGraphsPostUpdate));
			}
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00191EE4 File Offset: 0x001900E4
		protected override void OnDisable()
		{
			base.OnDisable();
			this.postScanCalled = false;
			if (this.startNode != null)
			{
				NodeLink2.reference.Remove(this.startNode);
			}
			if (this.endNode != null)
			{
				NodeLink2.reference.Remove(this.endNode);
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

		// Token: 0x060022E4 RID: 8932 RVA: 0x00191FB6 File Offset: 0x001901B6
		private void RemoveConnections(GraphNode node)
		{
			node.ClearConnections(true);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00191FBF File Offset: 0x001901BF
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

		// Token: 0x060022E6 RID: 8934 RVA: 0x00191FE8 File Offset: 0x001901E8
		public void Apply(bool forceNewCheck)
		{
			NNConstraint none = NNConstraint.None;
			int graphIndex = (int)this.startNode.GraphIndex;
			none.graphMask = ~(1 << graphIndex);
			this.startNode.SetPosition((Int3)this.StartTransform.position);
			this.endNode.SetPosition((Int3)this.EndTransform.position);
			this.RemoveConnections(this.startNode);
			this.RemoveConnections(this.endNode);
			uint cost = (uint)Mathf.RoundToInt((float)((Int3)(this.StartTransform.position - this.EndTransform.position)).costMagnitude * this.costFactor);
			this.startNode.AddConnection(this.endNode, cost);
			this.endNode.AddConnection(this.startNode, cost);
			if (this.connectedNode1 == null || forceNewCheck)
			{
				NNInfo nearest = AstarPath.active.GetNearest(this.StartTransform.position, none);
				this.connectedNode1 = nearest.node;
				this.clamped1 = nearest.position;
			}
			if (this.connectedNode2 == null || forceNewCheck)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.EndTransform.position, none);
				this.connectedNode2 = nearest2.node;
				this.clamped2 = nearest2.position;
			}
			if (this.connectedNode2 == null || this.connectedNode1 == null)
			{
				return;
			}
			this.connectedNode1.AddConnection(this.startNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
			if (!this.oneWay)
			{
				this.connectedNode2.AddConnection(this.endNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
			}
			if (!this.oneWay)
			{
				this.startNode.AddConnection(this.connectedNode1, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
			}
			this.endNode.AddConnection(this.connectedNode2, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0019225F File Offset: 0x0019045F
		public virtual void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x00192268 File Offset: 0x00190468
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00192274 File Offset: 0x00190474
		public void OnDrawGizmos(bool selected)
		{
			Color color = selected ? NodeLink2.GizmosColorSelected : NodeLink2.GizmosColor;
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

		// Token: 0x060022EA RID: 8938 RVA: 0x001923F0 File Offset: 0x001905F0
		internal static void SerializeReferences(GraphSerializationContext ctx)
		{
			List<NodeLink2> modifiersOfType = GraphModifier.GetModifiersOfType<NodeLink2>();
			ctx.writer.Write(modifiersOfType.Count);
			foreach (NodeLink2 nodeLink in modifiersOfType)
			{
				ctx.writer.Write(nodeLink.uniqueID);
				ctx.SerializeNodeReference(nodeLink.startNode);
				ctx.SerializeNodeReference(nodeLink.endNode);
				ctx.SerializeNodeReference(nodeLink.connectedNode1);
				ctx.SerializeNodeReference(nodeLink.connectedNode2);
				ctx.SerializeVector3(nodeLink.clamped1);
				ctx.SerializeVector3(nodeLink.clamped2);
				ctx.writer.Write(nodeLink.postScanCalled);
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x001924B8 File Offset: 0x001906B8
		internal static void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				ulong key = ctx.reader.ReadUInt64();
				GraphNode graphNode = ctx.DeserializeNodeReference();
				GraphNode graphNode2 = ctx.DeserializeNodeReference();
				GraphNode graphNode3 = ctx.DeserializeNodeReference();
				GraphNode graphNode4 = ctx.DeserializeNodeReference();
				Vector3 vector = ctx.DeserializeVector3();
				Vector3 vector2 = ctx.DeserializeVector3();
				bool flag = ctx.reader.ReadBoolean();
				GraphModifier graphModifier;
				if (!GraphModifier.usedIDs.TryGetValue(key, out graphModifier))
				{
					throw new Exception("Tried to deserialize a NodeLink2 reference, but the link could not be found in the scene.\nIf a NodeLink2 is included in serialized graph data, the same NodeLink2 component must be present in the scene when loading the graph data.");
				}
				NodeLink2 nodeLink = graphModifier as NodeLink2;
				if (!(nodeLink != null))
				{
					throw new Exception("Tried to deserialize a NodeLink2 reference, but the link was not of the correct type or it has been destroyed.\nIf a NodeLink2 is included in serialized graph data, the same NodeLink2 component must be present in the scene when loading the graph data.");
				}
				if (graphNode != null)
				{
					NodeLink2.reference[graphNode] = nodeLink;
				}
				if (graphNode2 != null)
				{
					NodeLink2.reference[graphNode2] = nodeLink;
				}
				if (nodeLink.startNode != null)
				{
					NodeLink2.reference.Remove(nodeLink.startNode);
				}
				if (nodeLink.endNode != null)
				{
					NodeLink2.reference.Remove(nodeLink.endNode);
				}
				nodeLink.startNode = (graphNode as PointNode);
				nodeLink.endNode = (graphNode2 as PointNode);
				nodeLink.connectedNode1 = graphNode3;
				nodeLink.connectedNode2 = graphNode4;
				nodeLink.postScanCalled = flag;
				nodeLink.clamped1 = vector;
				nodeLink.clamped2 = vector2;
			}
		}

		// Token: 0x04003F1C RID: 16156
		protected static Dictionary<GraphNode, NodeLink2> reference = new Dictionary<GraphNode, NodeLink2>();

		// Token: 0x04003F1D RID: 16157
		public Transform end;

		// Token: 0x04003F1E RID: 16158
		public float costFactor = 1f;

		// Token: 0x04003F1F RID: 16159
		public bool oneWay;

		// Token: 0x04003F22 RID: 16162
		private GraphNode connectedNode1;

		// Token: 0x04003F23 RID: 16163
		private GraphNode connectedNode2;

		// Token: 0x04003F24 RID: 16164
		private Vector3 clamped1;

		// Token: 0x04003F25 RID: 16165
		private Vector3 clamped2;

		// Token: 0x04003F26 RID: 16166
		private bool postScanCalled;

		// Token: 0x04003F27 RID: 16167
		private static readonly Color GizmosColor = new Color(0.80784315f, 0.53333336f, 0.1882353f, 0.5f);

		// Token: 0x04003F28 RID: 16168
		private static readonly Color GizmosColorSelected = new Color(0.92156863f, 0.48235294f, 0.1254902f, 1f);
	}
}
