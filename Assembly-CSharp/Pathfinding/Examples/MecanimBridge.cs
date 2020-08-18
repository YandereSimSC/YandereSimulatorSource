using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F0 RID: 1520
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mecanim_bridge.php")]
	public class MecanimBridge : VersionedMonoBehaviour
	{
		// Token: 0x060029E0 RID: 10720 RVA: 0x001C1950 File Offset: 0x001BFB50
		protected override void Awake()
		{
			base.Awake();
			this.ai = base.GetComponent<IAstarAI>();
			this.anim = base.GetComponent<Animator>();
			this.tr = base.transform;
			this.footTransforms = new Transform[]
			{
				this.anim.GetBoneTransform(HumanBodyBones.LeftFoot),
				this.anim.GetBoneTransform(HumanBodyBones.RightFoot)
			};
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x001C19B1 File Offset: 0x001BFBB1
		private void Update()
		{
			(this.ai as AIBase).canMove = false;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x001C19C4 File Offset: 0x001BFBC4
		private Vector3 CalculateBlendPoint()
		{
			if (this.footTransforms[0] == null || this.footTransforms[1] == null)
			{
				return this.tr.position;
			}
			Vector3 position = this.footTransforms[0].position;
			Vector3 position2 = this.footTransforms[1].position;
			Vector3 vector = (position - this.prevFootPos[0]) / Time.deltaTime;
			Vector3 vector2 = (position2 - this.prevFootPos[1]) / Time.deltaTime;
			float num = vector.magnitude + vector2.magnitude;
			float t = (num > 0f) ? (vector.magnitude / num) : 0.5f;
			this.prevFootPos[0] = position;
			this.prevFootPos[1] = position2;
			return Vector3.Lerp(position, position2, t);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x001C1AA4 File Offset: 0x001BFCA4
		private void OnAnimatorMove()
		{
			Vector3 vector;
			Quaternion quaternion;
			this.ai.MovementUpdate(Time.deltaTime, out vector, out quaternion);
			Vector3 desiredVelocity = this.ai.desiredVelocity;
			Vector3 direction = desiredVelocity;
			direction.y = 0f;
			this.anim.SetFloat("InputMagnitude", (this.ai.reachedEndOfPath || direction.magnitude < 0.1f) ? 0f : 1f);
			Vector3 b = this.tr.InverseTransformDirection(direction);
			this.smoothedVelocity = Vector3.Lerp(this.smoothedVelocity, b, (this.velocitySmoothing > 0f) ? (Time.deltaTime / this.velocitySmoothing) : 1f);
			if (this.smoothedVelocity.magnitude < 0.4f)
			{
				this.smoothedVelocity = this.smoothedVelocity.normalized * 0.4f;
			}
			this.anim.SetFloat("X", this.smoothedVelocity.x);
			this.anim.SetFloat("Y", this.smoothedVelocity.z);
			Quaternion quaternion2 = this.RotateTowards(direction, Time.deltaTime * (this.ai as AIPath).rotationSpeed);
			vector = this.ai.position;
			quaternion = this.ai.rotation;
			vector = MecanimBridge.RotatePointAround(vector, this.CalculateBlendPoint(), quaternion2 * Quaternion.Inverse(quaternion));
			quaternion = quaternion2;
			quaternion = this.anim.deltaRotation * quaternion;
			Vector3 deltaPosition = this.anim.deltaPosition;
			deltaPosition.y = desiredVelocity.y * Time.deltaTime;
			vector += deltaPosition;
			this.ai.FinalizeMovement(vector, quaternion);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x001C1C56 File Offset: 0x001BFE56
		private static Vector3 RotatePointAround(Vector3 point, Vector3 around, Quaternion rotation)
		{
			return rotation * (point - around) + around;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x001C1C6C File Offset: 0x001BFE6C
		protected virtual Quaternion RotateTowards(Vector3 direction, float maxDegrees)
		{
			if (direction != Vector3.zero)
			{
				Quaternion to = Quaternion.LookRotation(direction);
				return Quaternion.RotateTowards(this.tr.rotation, to, maxDegrees);
			}
			return this.tr.rotation;
		}

		// Token: 0x0400439E RID: 17310
		public float velocitySmoothing = 1f;

		// Token: 0x0400439F RID: 17311
		private IAstarAI ai;

		// Token: 0x040043A0 RID: 17312
		private Animator anim;

		// Token: 0x040043A1 RID: 17313
		private Transform tr;

		// Token: 0x040043A2 RID: 17314
		private Vector3 smoothedVelocity;

		// Token: 0x040043A3 RID: 17315
		private Vector3[] prevFootPos = new Vector3[2];

		// Token: 0x040043A4 RID: 17316
		private Transform[] footTransforms;
	}
}
