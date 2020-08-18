using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005E1 RID: 1505
	public class GraphGizmoHelper : IAstarPooledObject, IDisposable
	{
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x001BECDB File Offset: 0x001BCEDB
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x001BECE3 File Offset: 0x001BCEE3
		public RetainedGizmos.Hasher hasher { get; private set; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x001BECEC File Offset: 0x001BCEEC
		// (set) Token: 0x06002984 RID: 10628 RVA: 0x001BECF4 File Offset: 0x001BCEF4
		public RetainedGizmos.Builder builder { get; private set; }

		// Token: 0x06002985 RID: 10629 RVA: 0x001BECFD File Offset: 0x001BCEFD
		public GraphGizmoHelper()
		{
			this.drawConnection = new Action<GraphNode>(this.DrawConnection);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x001BED18 File Offset: 0x001BCF18
		public void Init(AstarPath active, RetainedGizmos.Hasher hasher, RetainedGizmos gizmos)
		{
			if (active != null)
			{
				this.debugData = active.debugPathData;
				this.debugPathID = active.debugPathID;
				this.debugMode = active.debugMode;
				this.debugFloor = active.debugFloor;
				this.debugRoof = active.debugRoof;
				this.showSearchTree = (active.showSearchTree && this.debugData != null);
			}
			this.gizmos = gizmos;
			this.hasher = hasher;
			this.builder = ObjectPool<RetainedGizmos.Builder>.Claim();
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x001BEDA0 File Offset: 0x001BCFA0
		public void OnEnterPool()
		{
			RetainedGizmos.Builder builder = this.builder;
			ObjectPool<RetainedGizmos.Builder>.Release(ref builder);
			this.builder = null;
			this.debugData = null;
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x001BEDCC File Offset: 0x001BCFCC
		public void DrawConnections(GraphNode node)
		{
			if (this.showSearchTree)
			{
				if (GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID) && this.debugData.GetPathNode(node).parent != null)
				{
					this.builder.DrawLine((Vector3)node.position, (Vector3)this.debugData.GetPathNode(node).parent.node.position, this.NodeColor(node));
					return;
				}
			}
			else
			{
				this.drawConnectionColor = this.NodeColor(node);
				this.drawConnectionStart = (Vector3)node.position;
				node.GetConnections(this.drawConnection);
			}
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x001BEE70 File Offset: 0x001BD070
		private void DrawConnection(GraphNode other)
		{
			this.builder.DrawLine(this.drawConnectionStart, Vector3.Lerp((Vector3)other.position, this.drawConnectionStart, 0.5f), this.drawConnectionColor);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x001BEEA4 File Offset: 0x001BD0A4
		public Color NodeColor(GraphNode node)
		{
			if (this.showSearchTree && !GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID))
			{
				return Color.clear;
			}
			Color result;
			if (node.Walkable)
			{
				switch (this.debugMode)
				{
				case GraphDebugMode.Areas:
					return AstarColor.GetAreaColor(node.Area);
				case GraphDebugMode.Penalty:
					return Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (node.Penalty - this.debugFloor) / (this.debugRoof - this.debugFloor));
				case GraphDebugMode.Connections:
					return AstarColor.NodeConnection;
				case GraphDebugMode.Tags:
					return AstarColor.GetAreaColor(node.Tag);
				}
				if (this.debugData == null)
				{
					result = AstarColor.NodeConnection;
				}
				else
				{
					PathNode pathNode = this.debugData.GetPathNode(node);
					float num;
					if (this.debugMode == GraphDebugMode.G)
					{
						num = pathNode.G;
					}
					else if (this.debugMode == GraphDebugMode.H)
					{
						num = pathNode.H;
					}
					else
					{
						num = pathNode.F;
					}
					result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (num - this.debugFloor) / (this.debugRoof - this.debugFloor));
				}
			}
			else
			{
				result = AstarColor.UnwalkableNode;
			}
			return result;
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x001BEFE2 File Offset: 0x001BD1E2
		public static bool InSearchTree(GraphNode node, PathHandler handler, ushort pathID)
		{
			return handler.GetPathNode(node).pathID == pathID;
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x001BEFF3 File Offset: 0x001BD1F3
		public void DrawWireTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
			this.builder.DrawLine(a, b, color);
			this.builder.DrawLine(b, c, color);
			this.builder.DrawLine(c, a, color);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x001BF024 File Offset: 0x001BD224
		public void DrawTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			List<int> list = ListPool<int>.Claim(numTriangles);
			for (int i = 0; i < numTriangles * 3; i++)
			{
				list.Add(i);
			}
			this.builder.DrawMesh(this.gizmos, vertices, list, colors);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x001BF068 File Offset: 0x001BD268
		public void DrawWireTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			for (int i = 0; i < numTriangles; i++)
			{
				this.DrawWireTriangle(vertices[i * 3], vertices[i * 3 + 1], vertices[i * 3 + 2], colors[i * 3]);
			}
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x001BF0AF File Offset: 0x001BD2AF
		public void Submit()
		{
			this.builder.Submit(this.gizmos, this.hasher);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x001BF0C8 File Offset: 0x001BD2C8
		void IDisposable.Dispose()
		{
			GraphGizmoHelper graphGizmoHelper = this;
			this.Submit();
			ObjectPool<GraphGizmoHelper>.Release(ref graphGizmoHelper);
		}

		// Token: 0x04004338 RID: 17208
		private RetainedGizmos gizmos;

		// Token: 0x04004339 RID: 17209
		private PathHandler debugData;

		// Token: 0x0400433A RID: 17210
		private ushort debugPathID;

		// Token: 0x0400433B RID: 17211
		private GraphDebugMode debugMode;

		// Token: 0x0400433C RID: 17212
		private bool showSearchTree;

		// Token: 0x0400433D RID: 17213
		private float debugFloor;

		// Token: 0x0400433E RID: 17214
		private float debugRoof;

		// Token: 0x04004340 RID: 17216
		private Vector3 drawConnectionStart;

		// Token: 0x04004341 RID: 17217
		private Color drawConnectionColor;

		// Token: 0x04004342 RID: 17218
		private readonly Action<GraphNode> drawConnection;
	}
}
