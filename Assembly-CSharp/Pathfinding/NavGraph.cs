using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000561 RID: 1377
	public abstract class NavGraph : IGraphInternals
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x00197325 File Offset: 0x00195525
		internal bool exists
		{
			get
			{
				return this.active != null;
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00197334 File Offset: 0x00195534
		public virtual int CountNodes()
		{
			int count = 0;
			this.GetNodes(delegate(GraphNode node)
			{
				int count = count;
				count++;
			});
			return count;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00197368 File Offset: 0x00195568
		public void GetNodes(Func<GraphNode, bool> action)
		{
			bool cont = true;
			this.GetNodes(delegate(GraphNode node)
			{
				if (cont)
				{
					cont &= action(node);
				}
			});
		}

		// Token: 0x06002460 RID: 9312
		public abstract void GetNodes(Action<GraphNode> action);

		// Token: 0x06002461 RID: 9313 RVA: 0x0019739B File Offset: 0x0019559B
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public void SetMatrix(Matrix4x4 m)
		{
			this.matrix = m;
			this.inverseMatrix = m.inverse;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x001973B1 File Offset: 0x001955B1
		[Obsolete("Use RelocateNodes(Matrix4x4) instead. To keep the same behavior you can call RelocateNodes(newMatrix * oldMatrix.inverse).")]
		public void RelocateNodes(Matrix4x4 oldMatrix, Matrix4x4 newMatrix)
		{
			this.RelocateNodes(newMatrix * oldMatrix.inverse);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x001973C8 File Offset: 0x001955C8
		public virtual void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.position = (Int3)deltaMatrix.MultiplyPoint((Vector3)node.position);
			});
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x001973F4 File Offset: 0x001955F4
		public NNInfoInternal GetNearest(Vector3 position)
		{
			return this.GetNearest(position, NNConstraint.None);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00197402 File Offset: 0x00195602
		public NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint, null);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00197410 File Offset: 0x00195610
		public virtual NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			float maxDistSqr = (constraint == null || constraint.constrainDistance) ? AstarPath.active.maxNearestNodeDistanceSqr : float.PositiveInfinity;
			float minDist = float.PositiveInfinity;
			GraphNode minNode = null;
			float minConstDist = float.PositiveInfinity;
			GraphNode minConstNode = null;
			this.GetNodes(delegate(GraphNode node)
			{
				float sqrMagnitude = (position - (Vector3)node.position).sqrMagnitude;
				if (sqrMagnitude < minDist)
				{
					minDist = sqrMagnitude;
					minNode = node;
				}
				if (sqrMagnitude < minConstDist && sqrMagnitude < maxDistSqr && (constraint == null || constraint.Suitable(node)))
				{
					minConstDist = sqrMagnitude;
					minConstNode = node;
				}
			});
			NNInfoInternal result = new NNInfoInternal(minNode);
			result.constrainedNode = minConstNode;
			if (minConstNode != null)
			{
				result.constClampedPosition = (Vector3)minConstNode.position;
			}
			else if (minNode != null)
			{
				result.constrainedNode = minNode;
				result.constClampedPosition = (Vector3)minNode.position;
			}
			return result;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x001974FB File Offset: 0x001956FB
		public virtual NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00197505 File Offset: 0x00195705
		protected virtual void OnDestroy()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0019750D File Offset: 0x0019570D
		protected virtual void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.Destroy();
			});
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00197534 File Offset: 0x00195734
		[Obsolete("Use AstarPath.Scan instead")]
		public void ScanGraph()
		{
			this.Scan();
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0019753C File Offset: 0x0019573C
		public void Scan()
		{
			this.active.Scan(this);
		}

		// Token: 0x0600246C RID: 9324
		protected abstract IEnumerable<Progress> ScanInternal();

		// Token: 0x0600246D RID: 9325 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void SerializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void PostDeserialization(GraphSerializationContext ctx)
		{
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0019754C File Offset: 0x0019574C
		protected virtual void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.guid = new Pathfinding.Util.Guid(ctx.reader.ReadBytes(16));
			this.initialPenalty = ctx.reader.ReadUInt32();
			this.open = ctx.reader.ReadBoolean();
			this.name = ctx.reader.ReadString();
			this.drawGizmos = ctx.reader.ReadBoolean();
			this.infoScreenOpen = ctx.reader.ReadBoolean();
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x001975C8 File Offset: 0x001957C8
		public virtual void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
			this.GetNodes(delegate(GraphNode node)
			{
				hasher.HashNode(node);
			});
			if (!gizmos.Draw(hasher))
			{
				using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
				{
					this.GetNodes(new Action<GraphNode>(gizmoHelper.DrawConnections));
				}
			}
			if (this.active.showUnwalkableNodes)
			{
				this.DrawUnwalkableNodes(this.active.unwalkableNodeDebugSize);
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00197670 File Offset: 0x00195870
		protected void DrawUnwalkableNodes(float size)
		{
			Gizmos.color = AstarColor.UnwalkableNode;
			this.GetNodes(delegate(GraphNode node)
			{
				if (!node.Walkable)
				{
					Gizmos.DrawCube((Vector3)node.position, Vector3.one * size);
				}
			});
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x001976A6 File Offset: 0x001958A6
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x001976AE File Offset: 0x001958AE
		string IGraphInternals.SerializedEditorSettings
		{
			get
			{
				return this.serializedEditorSettings;
			}
			set
			{
				this.serializedEditorSettings = value;
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x001976B7 File Offset: 0x001958B7
		void IGraphInternals.OnDestroy()
		{
			this.OnDestroy();
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00197505 File Offset: 0x00195705
		void IGraphInternals.DestroyAllNodes()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x001976BF File Offset: 0x001958BF
		IEnumerable<Progress> IGraphInternals.ScanInternal()
		{
			return this.ScanInternal();
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x001976C7 File Offset: 0x001958C7
		void IGraphInternals.SerializeExtraInfo(GraphSerializationContext ctx)
		{
			this.SerializeExtraInfo(ctx);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x001976D0 File Offset: 0x001958D0
		void IGraphInternals.DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			this.DeserializeExtraInfo(ctx);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x001976D9 File Offset: 0x001958D9
		void IGraphInternals.PostDeserialization(GraphSerializationContext ctx)
		{
			this.PostDeserialization(ctx);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x001976E2 File Offset: 0x001958E2
		void IGraphInternals.DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.DeserializeSettingsCompatibility(ctx);
		}

		// Token: 0x04004034 RID: 16436
		public AstarPath active;

		// Token: 0x04004035 RID: 16437
		[JsonMember]
		public Pathfinding.Util.Guid guid;

		// Token: 0x04004036 RID: 16438
		[JsonMember]
		public uint initialPenalty;

		// Token: 0x04004037 RID: 16439
		[JsonMember]
		public bool open;

		// Token: 0x04004038 RID: 16440
		public uint graphIndex;

		// Token: 0x04004039 RID: 16441
		[JsonMember]
		public string name;

		// Token: 0x0400403A RID: 16442
		[JsonMember]
		public bool drawGizmos = true;

		// Token: 0x0400403B RID: 16443
		[JsonMember]
		public bool infoScreenOpen;

		// Token: 0x0400403C RID: 16444
		[JsonMember]
		private string serializedEditorSettings;

		// Token: 0x0400403D RID: 16445
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 matrix = Matrix4x4.identity;

		// Token: 0x0400403E RID: 16446
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 inverseMatrix = Matrix4x4.identity;
	}
}
