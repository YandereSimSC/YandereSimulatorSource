using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Legacy
{
	// Token: 0x020005A6 RID: 1446
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/Legacy/AI/Legacy AIPath (3D)")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_legacy_1_1_legacy_a_i_path.php")]
	public class LegacyAIPath : AIPath
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x001ADE57 File Offset: 0x001AC057
		protected override void Awake()
		{
			base.Awake();
			if (this.rvoController != null)
			{
				if (this.rvoController is LegacyRVOController)
				{
					(this.rvoController as LegacyRVOController).enableRotation = false;
					return;
				}
				Debug.LogError("The LegacyAIPath component only works with the legacy RVOController, not the latest one. Please upgrade this component", this);
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x001ADE98 File Offset: 0x001AC098
		protected override void OnPathComplete(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.waitingForPathCalculation = false;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				return;
			}
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = abpath;
			this.currentWaypointIndex = 0;
			base.reachedEndOfPath = false;
			if (this.closestOnPathCheck)
			{
				Vector3 vector = (Time.time - this.lastFoundWaypointTime < 0.3f) ? this.lastFoundWaypointPosition : abpath.originalStartPoint;
				Vector3 vector2 = this.GetFeetPosition() - vector;
				float magnitude = vector2.magnitude;
				vector2 /= magnitude;
				int num = (int)(magnitude / this.pickNextWaypointDist);
				for (int i = 0; i <= num; i++)
				{
					this.CalculateVelocity(vector);
					vector += vector2;
				}
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x001ADF78 File Offset: 0x001AC178
		protected override void Update()
		{
			if (!this.canMove)
			{
				return;
			}
			Vector3 vector = this.CalculateVelocity(this.GetFeetPosition());
			this.RotateTowards(this.targetDirection);
			if (this.rvoController != null)
			{
				this.rvoController.Move(vector);
				return;
			}
			if (this.controller != null)
			{
				this.controller.SimpleMove(vector);
				return;
			}
			if (this.rigid != null)
			{
				this.rigid.AddForce(vector);
				return;
			}
			this.tr.Translate(vector * Time.deltaTime, Space.World);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x001AE010 File Offset: 0x001AC210
		protected float XZSqrMagnitude(Vector3 a, Vector3 b)
		{
			float num = b.x - a.x;
			float num2 = b.z - a.z;
			return num * num + num2 * num2;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x001AE040 File Offset: 0x001AC240
		protected new Vector3 CalculateVelocity(Vector3 currentPosition)
		{
			if (this.path == null || this.path.vectorPath == null || this.path.vectorPath.Count == 0)
			{
				return Vector3.zero;
			}
			List<Vector3> vectorPath = this.path.vectorPath;
			if (vectorPath.Count == 1)
			{
				vectorPath.Insert(0, currentPosition);
			}
			if (this.currentWaypointIndex >= vectorPath.Count)
			{
				this.currentWaypointIndex = vectorPath.Count - 1;
			}
			if (this.currentWaypointIndex <= 1)
			{
				this.currentWaypointIndex = 1;
			}
			while (this.currentWaypointIndex < vectorPath.Count - 1 && this.XZSqrMagnitude(vectorPath[this.currentWaypointIndex], currentPosition) < this.pickNextWaypointDist * this.pickNextWaypointDist)
			{
				this.lastFoundWaypointPosition = currentPosition;
				this.lastFoundWaypointTime = Time.time;
				this.currentWaypointIndex++;
			}
			Vector3 vector = vectorPath[this.currentWaypointIndex] - vectorPath[this.currentWaypointIndex - 1];
			vector = this.CalculateTargetPoint(currentPosition, vectorPath[this.currentWaypointIndex - 1], vectorPath[this.currentWaypointIndex]) - currentPosition;
			vector.y = 0f;
			float magnitude = vector.magnitude;
			float num = Mathf.Clamp01(magnitude / this.slowdownDistance);
			this.targetDirection = vector;
			if (this.currentWaypointIndex == vectorPath.Count - 1 && magnitude <= this.endReachedDistance)
			{
				if (!base.reachedEndOfPath)
				{
					base.reachedEndOfPath = true;
					this.OnTargetReached();
				}
				return Vector3.zero;
			}
			Vector3 forward = this.tr.forward;
			float a = Vector3.Dot(vector.normalized, forward);
			float num2 = this.maxSpeed * Mathf.Max(a, this.minMoveScale) * num;
			if (Time.deltaTime > 0f)
			{
				num2 = Mathf.Clamp(num2, 0f, magnitude / (Time.deltaTime * 2f));
			}
			return forward * num2;
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x001AE224 File Offset: 0x001AC424
		protected void RotateTowards(Vector3 dir)
		{
			if (dir == Vector3.zero)
			{
				return;
			}
			Quaternion quaternion = this.tr.rotation;
			Quaternion b = Quaternion.LookRotation(dir);
			Vector3 eulerAngles = Quaternion.Slerp(quaternion, b, base.turningSpeed * Time.deltaTime).eulerAngles;
			eulerAngles.z = 0f;
			eulerAngles.x = 0f;
			quaternion = Quaternion.Euler(eulerAngles);
			this.tr.rotation = quaternion;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x001AE29C File Offset: 0x001AC49C
		protected Vector3 CalculateTargetPoint(Vector3 p, Vector3 a, Vector3 b)
		{
			a.y = p.y;
			b.y = p.y;
			float magnitude = (a - b).magnitude;
			if (magnitude == 0f)
			{
				return a;
			}
			float num = Mathf.Clamp01(VectorMath.ClosestPointOnLineFactor(a, b, p));
			float magnitude2 = ((b - a) * num + a - p).magnitude;
			float num2 = Mathf.Clamp(this.forwardLook - magnitude2, 0f, this.forwardLook) / magnitude;
			num2 = Mathf.Clamp(num2 + num, 0f, 1f);
			return (b - a) * num2 + a;
		}

		// Token: 0x040041CC RID: 16844
		public float forwardLook = 1f;

		// Token: 0x040041CD RID: 16845
		public bool closestOnPathCheck = true;

		// Token: 0x040041CE RID: 16846
		protected float minMoveScale = 0.05f;

		// Token: 0x040041CF RID: 16847
		protected int currentWaypointIndex;

		// Token: 0x040041D0 RID: 16848
		protected Vector3 lastFoundWaypointPosition;

		// Token: 0x040041D1 RID: 16849
		protected float lastFoundWaypointTime = -9999f;

		// Token: 0x040041D2 RID: 16850
		protected new Vector3 targetDirection;
	}
}
