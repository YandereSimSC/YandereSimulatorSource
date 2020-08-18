using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055C RID: 1372
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_navmesh_clamp.php")]
	public class NavmeshClamp : MonoBehaviour
	{
		// Token: 0x06002440 RID: 9280 RVA: 0x00196CB8 File Offset: 0x00194EB8
		private void LateUpdate()
		{
			if (this.prevNode == null)
			{
				NNInfo nearest = AstarPath.active.GetNearest(base.transform.position);
				this.prevNode = nearest.node;
				this.prevPos = base.transform.position;
			}
			if (this.prevNode == null)
			{
				return;
			}
			if (this.prevNode != null)
			{
				IRaycastableGraph raycastableGraph = AstarData.GetGraph(this.prevNode) as IRaycastableGraph;
				if (raycastableGraph != null)
				{
					GraphHitInfo graphHitInfo;
					if (raycastableGraph.Linecast(this.prevPos, base.transform.position, this.prevNode, out graphHitInfo))
					{
						graphHitInfo.point.y = base.transform.position.y;
						Vector3 vector = VectorMath.ClosestPointOnLine(graphHitInfo.tangentOrigin, graphHitInfo.tangentOrigin + graphHitInfo.tangent, base.transform.position);
						Vector3 vector2 = graphHitInfo.point;
						vector2 += Vector3.ClampMagnitude((Vector3)graphHitInfo.node.position - vector2, 0.008f);
						if (raycastableGraph.Linecast(vector2, vector, graphHitInfo.node, out graphHitInfo))
						{
							graphHitInfo.point.y = base.transform.position.y;
							base.transform.position = graphHitInfo.point;
						}
						else
						{
							vector.y = base.transform.position.y;
							base.transform.position = vector;
						}
					}
					this.prevNode = graphHitInfo.node;
				}
			}
			this.prevPos = base.transform.position;
		}

		// Token: 0x04004026 RID: 16422
		private GraphNode prevNode;

		// Token: 0x04004027 RID: 16423
		private Vector3 prevPos;
	}
}
