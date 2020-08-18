using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000512 RID: 1298
	[AddComponentMenu("Pathfinding/AI/AIPath (2D,3D)")]
	public class AIPath : AIBase, IAstarAI
	{
		// Token: 0x060020D4 RID: 8404 RVA: 0x00187C8A File Offset: 0x00185E8A
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			if (clearPath)
			{
				this.interpolator.SetPath(null);
			}
			this.reachedEndOfPath = false;
			base.Teleport(newPosition, clearPath);
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00187CAC File Offset: 0x00185EAC
		public float remainingDistance
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return float.PositiveInfinity;
				}
				return this.interpolator.remainingDistance + this.movementPlane.ToPlane(this.interpolator.position - base.position).magnitude;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x00187D01 File Offset: 0x00185F01
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x00187D09 File Offset: 0x00185F09
		public bool reachedEndOfPath { get; protected set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x00187D12 File Offset: 0x00185F12
		public bool hasPath
		{
			get
			{
				return this.interpolator.valid;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00187D1F File Offset: 0x00185F1F
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x00187D27 File Offset: 0x00185F27
		public Vector3 steeringTarget
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return base.position;
				}
				return this.interpolator.position;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x00187D48 File Offset: 0x00185F48
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x00187D50 File Offset: 0x00185F50
		float IAstarAI.maxSpeed
		{
			get
			{
				return this.maxSpeed;
			}
			set
			{
				this.maxSpeed = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x00187D59 File Offset: 0x00185F59
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x00187D61 File Offset: 0x00185F61
		bool IAstarAI.canSearch
		{
			get
			{
				return this.canSearch;
			}
			set
			{
				this.canSearch = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x00187D6A File Offset: 0x00185F6A
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x00187D72 File Offset: 0x00185F72
		bool IAstarAI.canMove
		{
			get
			{
				return this.canMove;
			}
			set
			{
				this.canMove = value;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00187D7B File Offset: 0x00185F7B
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = null;
			this.interpolator.SetPath(null);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnTargetReached()
		{
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00187DAC File Offset: 0x00185FAC
		protected override void OnPathComplete(Path newPath)
		{
			ABPath abpath = newPath as ABPath;
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
			if (this.path.vectorPath.Count == 1)
			{
				this.path.vectorPath.Add(this.path.vectorPath[0]);
			}
			this.interpolator.SetPath(this.path.vectorPath);
			ITransformedGraph transformedGraph = AstarData.GetGraph(this.path.path[0]) as ITransformedGraph;
			this.movementPlane = ((transformedGraph != null) ? transformedGraph.transform : (this.rotationIn2D ? new GraphTransform(Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 270f, 90f), Vector3.one)) : GraphTransform.identityTransform));
			this.reachedEndOfPath = false;
			this.interpolator.MoveToLocallyClosestPoint((this.GetFeetPosition() + abpath.originalStartPoint) * 0.5f, true, true);
			this.interpolator.MoveToLocallyClosestPoint(this.GetFeetPosition(), true, true);
			this.interpolator.MoveToCircleIntersection2D(base.position, this.pickNextWaypointDist, this.movementPlane);
			if (this.remainingDistance <= this.endReachedDistance)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00187F34 File Offset: 0x00186134
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			float num = this.maxAcceleration;
			if (num < 0f)
			{
				num *= -this.maxSpeed;
			}
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			Vector3 simulatedPosition = this.simulatedPosition;
			this.interpolator.MoveToCircleIntersection2D(simulatedPosition, this.pickNextWaypointDist, this.movementPlane);
			Vector2 deltaPosition = this.movementPlane.ToPlane(this.steeringTarget - simulatedPosition);
			float num2 = deltaPosition.magnitude + Mathf.Max(0f, this.interpolator.remainingDistance);
			bool reachedEndOfPath = this.reachedEndOfPath;
			this.reachedEndOfPath = (num2 <= this.endReachedDistance && this.interpolator.valid);
			if (!reachedEndOfPath && this.reachedEndOfPath)
			{
				this.OnTargetReached();
			}
			Vector2 vector = this.movementPlane.ToPlane(this.simulatedRotation * (this.rotationIn2D ? Vector3.up : Vector3.forward));
			float num3;
			if (this.interpolator.valid && !base.isStopped)
			{
				num3 = ((num2 < this.slowdownDistance) ? Mathf.Sqrt(num2 / this.slowdownDistance) : 1f);
				if (this.reachedEndOfPath && this.whenCloseToDestination == CloseToDestinationMode.Stop)
				{
					this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, num * deltaTime);
				}
				else
				{
					this.velocity2D += MovementUtilities.CalculateAccelerationToReachPoint(deltaPosition, deltaPosition.normalized * this.maxSpeed, this.velocity2D, num, this.rotationSpeed, this.maxSpeed, vector) * deltaTime;
				}
			}
			else
			{
				num3 = 1f;
				this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, num * deltaTime);
			}
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, num3, this.slowWhenNotFacingTarget, vector);
			base.ApplyGravity(deltaTime);
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 pos = simulatedPosition + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, num2), 0f);
				this.rvoController.SetTarget(pos, this.velocity2D.magnitude, this.maxSpeed);
			}
			Vector2 p = this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(this.movementPlane.ToPlane(simulatedPosition), num2, deltaTime);
			nextPosition = simulatedPosition + this.movementPlane.ToWorld(p, this.verticalVelocity * this.lastDeltaTime);
			this.CalculateNextRotation(num3, out nextRotation);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x001881EC File Offset: 0x001863EC
		protected virtual void CalculateNextRotation(float slowdown, out Quaternion nextRotation)
		{
			if (this.lastDeltaTime > 1E-05f)
			{
				Vector2 direction;
				if (this.rvoController != null && this.rvoController.enabled)
				{
					Vector2 b = this.lastDeltaPosition / this.lastDeltaTime;
					direction = Vector2.Lerp(this.velocity2D, b, 4f * b.magnitude / (this.maxSpeed + 0.0001f));
				}
				else
				{
					direction = this.velocity2D;
				}
				float num = this.rotationSpeed * Mathf.Max(0f, (slowdown - 0.3f) / 0.7f);
				nextRotation = base.SimulateRotationTowards(direction, num * this.lastDeltaTime);
				return;
			}
			nextRotation = base.rotation;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x001882A8 File Offset: 0x001864A8
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.constrainInsideGraph)
			{
				AIPath.cachedNNConstraint.tags = this.seeker.traversableTags;
				AIPath.cachedNNConstraint.graphMask = this.seeker.graphMask;
				AIPath.cachedNNConstraint.distanceXZ = true;
				Vector3 position2 = AstarPath.active.GetNearest(position, AIPath.cachedNNConstraint).position;
				Vector2 vector = this.movementPlane.ToPlane(position2 - position);
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude > 1.0000001E-06f)
				{
					this.velocity2D -= vector * Vector2.Dot(vector, this.velocity2D) / sqrMagnitude;
					if (this.rvoController != null && this.rvoController.enabled)
					{
						this.rvoController.SetCollisionNormal(vector);
					}
					positionChanged = true;
					return position + this.movementPlane.ToWorld(vector, 0f);
				}
			}
			positionChanged = false;
			return position;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x001883A2 File Offset: 0x001865A2
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			base.OnUpgradeSerializedData(version, unityThread);
			if (version < 1)
			{
				this.rotationSpeed *= 90f;
			}
			return 2;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x001883C4 File Offset: 0x001865C4
		[Obsolete("When unifying the interfaces for different movement scripts, this property has been renamed to reachedEndOfPath.  [AstarUpgradable: 'TargetReached' -> 'reachedEndOfPath']")]
		public bool TargetReached
		{
			get
			{
				return this.reachedEndOfPath;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x001883CC File Offset: 0x001865CC
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x001883DA File Offset: 0x001865DA
		[Obsolete("This field has been renamed to #rotationSpeed and is now in degrees per second instead of a damping factor")]
		public float turningSpeed
		{
			get
			{
				return this.rotationSpeed / 90f;
			}
			set
			{
				this.rotationSpeed = value * 90f;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x00187D48 File Offset: 0x00185F48
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x00187D50 File Offset: 0x00185F50
		[Obsolete("This member has been deprecated. Use 'maxSpeed' instead. [AstarUpgradable: 'speed' -> 'maxSpeed']")]
		public float speed
		{
			get
			{
				return this.maxSpeed;
			}
			set
			{
				this.maxSpeed = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x001883EC File Offset: 0x001865EC
		[Obsolete("Only exists for compatibility reasons. Use desiredVelocity or steeringTarget instead.")]
		public Vector3 targetDirection
		{
			get
			{
				return (this.steeringTarget - this.tr.position).normalized;
			}
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00188417 File Offset: 0x00186617
		[Obsolete("This method no longer calculates the velocity. Use the desiredVelocity property instead")]
		public Vector3 CalculateVelocity(Vector3 position)
		{
			return base.desiredVelocity;
		}

		// Token: 0x04003E5A RID: 15962
		public float maxAcceleration = -2.5f;

		// Token: 0x04003E5B RID: 15963
		[FormerlySerializedAs("turningSpeed")]
		public float rotationSpeed = 360f;

		// Token: 0x04003E5C RID: 15964
		public float slowdownDistance = 0.6f;

		// Token: 0x04003E5D RID: 15965
		public float pickNextWaypointDist = 2f;

		// Token: 0x04003E5E RID: 15966
		public float endReachedDistance = 0.2f;

		// Token: 0x04003E5F RID: 15967
		public bool alwaysDrawGizmos;

		// Token: 0x04003E60 RID: 15968
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x04003E61 RID: 15969
		public CloseToDestinationMode whenCloseToDestination;

		// Token: 0x04003E62 RID: 15970
		public bool constrainInsideGraph;

		// Token: 0x04003E63 RID: 15971
		protected Path path;

		// Token: 0x04003E64 RID: 15972
		public PathInterpolator interpolator = new PathInterpolator();

		// Token: 0x04003E66 RID: 15974
		private static NNConstraint cachedNNConstraint = NNConstraint.Default;
	}
}
