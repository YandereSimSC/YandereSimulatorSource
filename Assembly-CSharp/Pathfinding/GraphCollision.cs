using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000562 RID: 1378
	[Serializable]
	public class GraphCollision
	{
		// Token: 0x0600247D RID: 9341 RVA: 0x00197710 File Offset: 0x00195910
		public void Initialize(GraphTransform transform, float scale)
		{
			this.up = (transform.Transform(Vector3.up) - transform.Transform(Vector3.zero)).normalized;
			this.upheight = this.up * this.height;
			this.finalRadius = this.diameter * scale * 0.5f;
			this.finalRaycastRadius = this.thickRaycastDiameter * scale * 0.5f;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x00197788 File Offset: 0x00195988
		public bool Check(Vector3 position)
		{
			if (!this.collisionCheck)
			{
				return true;
			}
			if (this.use2D)
			{
				ColliderType colliderType = this.type;
				if (colliderType <= ColliderType.Capsule)
				{
					return Physics2D.OverlapCircle(position, this.finalRadius, this.mask) == null;
				}
				return Physics2D.OverlapPoint(position, this.mask) == null;
			}
			else
			{
				position += this.up * this.collisionOffset;
				ColliderType colliderType = this.type;
				if (colliderType == ColliderType.Sphere)
				{
					return !Physics.CheckSphere(position, this.finalRadius, this.mask, QueryTriggerInteraction.Collide);
				}
				if (colliderType == ColliderType.Capsule)
				{
					return !Physics.CheckCapsule(position, position + this.upheight, this.finalRadius, this.mask, QueryTriggerInteraction.Collide);
				}
				RayDirection rayDirection = this.rayDirection;
				if (rayDirection == RayDirection.Up)
				{
					return !Physics.Raycast(position, this.up, this.height, this.mask, QueryTriggerInteraction.Collide);
				}
				if (rayDirection == RayDirection.Both)
				{
					return !Physics.Raycast(position, this.up, this.height, this.mask, QueryTriggerInteraction.Collide) && !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask, QueryTriggerInteraction.Collide);
				}
				return !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask, QueryTriggerInteraction.Collide);
			}
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00197914 File Offset: 0x00195B14
		public Vector3 CheckHeight(Vector3 position)
		{
			RaycastHit raycastHit;
			bool flag;
			return this.CheckHeight(position, out raycastHit, out flag);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0019792C File Offset: 0x00195B2C
		public Vector3 CheckHeight(Vector3 position, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck || this.use2D)
			{
				hit = default(RaycastHit);
				return position;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(position + this.up * this.fromHeight, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return VectorMath.ClosestPointOnLine(ray.origin, ray.origin + ray.direction, hit.point);
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			else
			{
				if (Physics.Raycast(position + this.up * this.fromHeight, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return hit.point;
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			return position;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x00197A40 File Offset: 0x00195C40
		public Vector3 Raycast(Vector3 origin, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck || this.use2D)
			{
				hit = default(RaycastHit);
				return origin - this.up * this.fromHeight;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(origin, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return VectorMath.ClosestPointOnLine(ray.origin, ray.origin + ray.direction, hit.point);
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			else
			{
				if (Physics.Raycast(origin, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return hit.point;
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			return origin - this.up * this.fromHeight;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00197B50 File Offset: 0x00195D50
		public RaycastHit[] CheckHeightAll(Vector3 position)
		{
			if (!this.heightCheck || this.use2D)
			{
				return new RaycastHit[]
				{
					new RaycastHit
					{
						point = position,
						distance = 0f
					}
				};
			}
			if (this.thickRaycast)
			{
				return new RaycastHit[0];
			}
			List<RaycastHit> list = new List<RaycastHit>();
			Vector3 vector = position + this.up * this.fromHeight;
			Vector3 vector2 = Vector3.zero;
			int num = 0;
			for (;;)
			{
				RaycastHit item;
				bool flag;
				this.Raycast(vector, out item, out flag);
				if (item.transform == null)
				{
					goto IL_131;
				}
				if (item.point != vector2 || list.Count == 0)
				{
					vector = item.point - this.up * 0.005f;
					vector2 = item.point;
					num = 0;
					list.Add(item);
				}
				else
				{
					vector -= this.up * 0.001f;
					num++;
					if (num > 10)
					{
						break;
					}
				}
			}
			Debug.LogError(string.Concat(new object[]
			{
				"Infinite Loop when raycasting. Please report this error (arongranberg.com)\n",
				vector,
				" : ",
				vector2
			}));
			IL_131:
			return list.ToArray();
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x00197C94 File Offset: 0x00195E94
		public void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.type = (ColliderType)ctx.reader.ReadInt32();
			this.diameter = ctx.reader.ReadSingle();
			this.height = ctx.reader.ReadSingle();
			this.collisionOffset = ctx.reader.ReadSingle();
			this.rayDirection = (RayDirection)ctx.reader.ReadInt32();
			this.mask = ctx.reader.ReadInt32();
			this.heightMask = ctx.reader.ReadInt32();
			this.fromHeight = ctx.reader.ReadSingle();
			this.thickRaycast = ctx.reader.ReadBoolean();
			this.thickRaycastDiameter = ctx.reader.ReadSingle();
			this.unwalkableWhenNoGround = ctx.reader.ReadBoolean();
			this.use2D = ctx.reader.ReadBoolean();
			this.collisionCheck = ctx.reader.ReadBoolean();
			this.heightCheck = ctx.reader.ReadBoolean();
		}

		// Token: 0x0400403F RID: 16447
		public ColliderType type = ColliderType.Capsule;

		// Token: 0x04004040 RID: 16448
		public float diameter = 1f;

		// Token: 0x04004041 RID: 16449
		public float height = 2f;

		// Token: 0x04004042 RID: 16450
		public float collisionOffset;

		// Token: 0x04004043 RID: 16451
		public RayDirection rayDirection = RayDirection.Both;

		// Token: 0x04004044 RID: 16452
		public LayerMask mask;

		// Token: 0x04004045 RID: 16453
		public LayerMask heightMask = -1;

		// Token: 0x04004046 RID: 16454
		public float fromHeight = 100f;

		// Token: 0x04004047 RID: 16455
		public bool thickRaycast;

		// Token: 0x04004048 RID: 16456
		public float thickRaycastDiameter = 1f;

		// Token: 0x04004049 RID: 16457
		public bool unwalkableWhenNoGround = true;

		// Token: 0x0400404A RID: 16458
		public bool use2D;

		// Token: 0x0400404B RID: 16459
		public bool collisionCheck = true;

		// Token: 0x0400404C RID: 16460
		public bool heightCheck = true;

		// Token: 0x0400404D RID: 16461
		public Vector3 up;

		// Token: 0x0400404E RID: 16462
		private Vector3 upheight;

		// Token: 0x0400404F RID: 16463
		private float finalRadius;

		// Token: 0x04004050 RID: 16464
		private float finalRaycastRadius;

		// Token: 0x04004051 RID: 16465
		public const float RaycastErrorMargin = 0.005f;
	}
}
