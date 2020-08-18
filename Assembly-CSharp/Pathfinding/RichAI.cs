using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Examples;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000514 RID: 1300
	[AddComponentMenu("Pathfinding/AI/RichAI (3D, for navmesh)")]
	public class RichAI : AIBase, IAstarAI
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x00188488 File Offset: 0x00186688
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x00188490 File Offset: 0x00186690
		public bool traversingOffMeshLink { get; protected set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x00188499 File Offset: 0x00186699
		public float remainingDistance
		{
			get
			{
				return this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, this.richPath.Endpoint);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x001884B8 File Offset: 0x001866B8
		public bool reachedEndOfPath
		{
			get
			{
				return this.approachingPathEndpoint && this.distanceToSteeringTarget < this.endReachedDistance;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x001884D2 File Offset: 0x001866D2
		public bool hasPath
		{
			get
			{
				return this.richPath.GetCurrentPart() != null;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x001884E2 File Offset: 0x001866E2
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation || this.delayUpdatePath;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x001884F4 File Offset: 0x001866F4
		// (set) Token: 0x06002113 RID: 8467 RVA: 0x001884FC File Offset: 0x001866FC
		public Vector3 steeringTarget { get; protected set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x00187D48 File Offset: 0x00185F48
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x00187D50 File Offset: 0x00185F50
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

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x00187D59 File Offset: 0x00185F59
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x00187D61 File Offset: 0x00185F61
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

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x00187D6A File Offset: 0x00185F6A
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x00187D72 File Offset: 0x00185F72
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

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00188505 File Offset: 0x00186705
		Vector3 IAstarAI.position
		{
			get
			{
				return this.tr.position;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x00188512 File Offset: 0x00186712
		public bool approachingPartEndpoint
		{
			get
			{
				return this.lastCorner && this.nextCorners.Count == 1;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0018852C File Offset: 0x0018672C
		public bool approachingPathEndpoint
		{
			get
			{
				return this.approachingPartEndpoint && this.richPath.IsLastPart;
			}
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00188544 File Offset: 0x00186744
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			NNInfo nninfo = (AstarPath.active != null) ? AstarPath.active.GetNearest(newPosition) : default(NNInfo);
			float elevation;
			this.movementPlane.ToPlane(newPosition, out elevation);
			newPosition = this.movementPlane.ToWorld(this.movementPlane.ToPlane((nninfo.node != null) ? nninfo.position : newPosition), elevation);
			if (clearPath)
			{
				this.richPath.Clear();
			}
			base.Teleport(newPosition, clearPath);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x001885C4 File Offset: 0x001867C4
		protected override void OnDisable()
		{
			base.OnDisable();
			this.lastCorner = false;
			this.distanceToSteeringTarget = float.PositiveInfinity;
			this.traversingOffMeshLink = false;
			this.delayUpdatePath = false;
			base.StopAllCoroutines();
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x001885F2 File Offset: 0x001867F2
		protected override bool shouldRecalculatePath
		{
			get
			{
				return base.shouldRecalculatePath && !this.traversingOffMeshLink;
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00188607 File Offset: 0x00186807
		public override void SearchPath()
		{
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
				return;
			}
			base.SearchPath();
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00188620 File Offset: 0x00186820
		protected override void OnPathComplete(Path p)
		{
			this.waitingForPathCalculation = false;
			p.Claim(this);
			if (p.error)
			{
				p.Release(this, false);
				return;
			}
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
			}
			else
			{
				this.richPath.Initialize(this.seeker, p, true, this.funnelSimplification);
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					if (this.updatePosition)
					{
						this.simulatedPosition = this.tr.position;
					}
					Vector2 b = this.movementPlane.ToPlane(this.UpdateTarget(richFunnel));
					if (this.lastCorner && this.nextCorners.Count == 1)
					{
						this.steeringTarget = this.nextCorners[0];
						Vector2 a = this.movementPlane.ToPlane(this.steeringTarget);
						this.distanceToSteeringTarget = (a - b).magnitude;
						if (this.distanceToSteeringTarget <= this.endReachedDistance)
						{
							this.NextPart();
						}
					}
				}
			}
			p.Release(this, false);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00188728 File Offset: 0x00186928
		protected void NextPart()
		{
			if (!this.richPath.CompletedAllParts)
			{
				if (!this.richPath.IsLastPart)
				{
					this.lastCorner = false;
				}
				this.richPath.NextPart();
				if (this.richPath.CompletedAllParts)
				{
					this.OnTargetReached();
				}
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void OnTargetReached()
		{
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00188774 File Offset: 0x00186974
		protected virtual Vector3 UpdateTarget(RichFunnel fn)
		{
			this.nextCorners.Clear();
			bool flag;
			Vector3 result = fn.Update(this.simulatedPosition, this.nextCorners, 2, out this.lastCorner, out flag);
			if (flag && !this.waitingForPathCalculation && this.canSearch)
			{
				this.SearchPath();
			}
			return result;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x001887C0 File Offset: 0x001869C0
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			RichPathPart currentPart = this.richPath.GetCurrentPart();
			if (currentPart is RichSpecial)
			{
				if (!this.traversingOffMeshLink)
				{
					base.StartCoroutine(this.TraverseSpecial(currentPart as RichSpecial));
				}
				nextPosition = (this.steeringTarget = this.simulatedPosition);
				nextRotation = base.rotation;
				return;
			}
			RichFunnel richFunnel = currentPart as RichFunnel;
			if (richFunnel != null && !base.isStopped)
			{
				this.TraverseFunnel(richFunnel, deltaTime, out nextPosition, out nextRotation);
				return;
			}
			this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, this.acceleration * deltaTime);
			this.FinalMovement(this.simulatedPosition, deltaTime, float.PositiveInfinity, 1f, out nextPosition, out nextRotation);
			this.steeringTarget = this.simulatedPosition;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x001888B8 File Offset: 0x00186AB8
		private void TraverseFunnel(RichFunnel fn, float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector3 vector = this.UpdateTarget(fn);
			float elevation;
			Vector2 vector2 = this.movementPlane.ToPlane(vector, out elevation);
			if (Time.frameCount % 5 == 0 && this.wallForce > 0f && this.wallDist > 0f)
			{
				this.wallBuffer.Clear();
				fn.FindWalls(this.wallBuffer, this.wallDist);
			}
			this.steeringTarget = this.nextCorners[0];
			Vector2 vector3 = this.movementPlane.ToPlane(this.steeringTarget);
			Vector2 vector4 = vector3 - vector2;
			Vector2 vector5 = VectorMath.Normalize(vector4, out this.distanceToSteeringTarget);
			Vector2 a = this.CalculateWallForce(vector2, elevation, vector5);
			Vector2 targetVelocity;
			if (this.approachingPartEndpoint)
			{
				targetVelocity = ((this.slowdownTime > 0f) ? Vector2.zero : (vector5 * this.maxSpeed));
				a *= Math.Min(this.distanceToSteeringTarget / 0.5f, 1f);
				if (this.distanceToSteeringTarget <= this.endReachedDistance)
				{
					this.NextPart();
				}
			}
			else
			{
				targetVelocity = (((this.nextCorners.Count > 1) ? this.movementPlane.ToPlane(this.nextCorners[1]) : (vector2 + 2f * vector4)) - vector3).normalized * this.maxSpeed;
			}
			Vector2 forwardsVector = this.movementPlane.ToPlane(this.simulatedRotation * (this.rotationIn2D ? Vector3.up : Vector3.forward));
			Vector2 a2 = MovementUtilities.CalculateAccelerationToReachPoint(vector3 - vector2, targetVelocity, this.velocity2D, this.acceleration, this.rotationSpeed, this.maxSpeed, forwardsVector);
			this.velocity2D += (a2 + a * this.wallForce) * deltaTime;
			float num = this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, fn.exactEnd);
			float slowdownFactor = (num < this.maxSpeed * this.slowdownTime) ? Mathf.Sqrt(num / (this.maxSpeed * this.slowdownTime)) : 1f;
			this.FinalMovement(vector, deltaTime, num, slowdownFactor, out nextPosition, out nextRotation);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00188AF4 File Offset: 0x00186CF4
		private void FinalMovement(Vector3 position3D, float deltaTime, float distanceToEndOfPath, float slowdownFactor, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector2 forward = this.movementPlane.ToPlane(this.simulatedRotation * (this.rotationIn2D ? Vector3.up : Vector3.forward));
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, slowdownFactor, this.slowWhenNotFacingTarget, forward);
			base.ApplyGravity(deltaTime);
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 pos = position3D + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, distanceToEndOfPath), 0f);
				this.rvoController.SetTarget(pos, this.velocity2D.magnitude, this.maxSpeed);
			}
			Vector2 vector = this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(this.movementPlane.ToPlane(position3D), distanceToEndOfPath, deltaTime);
			float num = this.approachingPartEndpoint ? Mathf.Clamp01(1.1f * slowdownFactor - 0.1f) : 1f;
			nextRotation = base.SimulateRotationTowards(vector, this.rotationSpeed * num * deltaTime);
			nextPosition = position3D + this.movementPlane.ToWorld(vector, this.verticalVelocity * deltaTime);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00188C2C File Offset: 0x00186E2C
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.richPath != null)
			{
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					Vector3 a = richFunnel.ClampToNavmesh(position);
					Vector2 vector = this.movementPlane.ToPlane(a - position);
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
			}
			positionChanged = false;
			return position;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00188CFC File Offset: 0x00186EFC
		private Vector2 CalculateWallForce(Vector2 position, float elevation, Vector2 directionToTarget)
		{
			if (this.wallForce <= 0f || this.wallDist <= 0f)
			{
				return Vector2.zero;
			}
			float num = 0f;
			float num2 = 0f;
			Vector3 vector = this.movementPlane.ToWorld(position, elevation);
			for (int i = 0; i < this.wallBuffer.Count; i += 2)
			{
				float sqrMagnitude = (VectorMath.ClosestPointOnSegment(this.wallBuffer[i], this.wallBuffer[i + 1], vector) - vector).sqrMagnitude;
				if (sqrMagnitude <= this.wallDist * this.wallDist)
				{
					Vector2 normalized = this.movementPlane.ToPlane(this.wallBuffer[i + 1] - this.wallBuffer[i]).normalized;
					float num3 = Vector2.Dot(directionToTarget, normalized);
					float num4 = 1f - Math.Max(0f, 2f * (sqrMagnitude / (this.wallDist * this.wallDist)) - 1f);
					if (num3 > 0f)
					{
						num2 = Math.Max(num2, num3 * num4);
					}
					else
					{
						num = Math.Max(num, -num3 * num4);
					}
				}
			}
			return new Vector2(directionToTarget.y, -directionToTarget.x) * (num2 - num);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00188E4F File Offset: 0x0018704F
		protected virtual IEnumerator TraverseSpecial(RichSpecial link)
		{
			this.traversingOffMeshLink = true;
			this.velocity2D = Vector3.zero;
			IEnumerator routine = (this.onTraverseOffMeshLink != null) ? this.onTraverseOffMeshLink(link) : this.TraverseOffMeshLinkFallback(link);
			yield return base.StartCoroutine(routine);
			this.traversingOffMeshLink = false;
			this.NextPart();
			if (this.delayUpdatePath)
			{
				this.delayUpdatePath = false;
				if (this.canSearch)
				{
					this.SearchPath();
				}
			}
			yield break;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00188E65 File Offset: 0x00187065
		protected IEnumerator TraverseOffMeshLinkFallback(RichSpecial link)
		{
			float duration = (this.maxSpeed > 0f) ? (Vector3.Distance(link.second.position, link.first.position) / this.maxSpeed) : 1f;
			float startTime = Time.time;
			for (;;)
			{
				Vector3 vector = Vector3.Lerp(link.first.position, link.second.position, Mathf.InverseLerp(startTime, startTime + duration, Time.time));
				if (this.updatePosition)
				{
					this.tr.position = vector;
				}
				else
				{
					this.simulatedPosition = vector;
				}
				if (Time.time >= startTime + duration)
				{
					break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00188E7C File Offset: 0x0018707C
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (this.tr != null)
			{
				Gizmos.color = RichAI.GizmoColorPath;
				Vector3 from = base.position;
				for (int i = 0; i < this.nextCorners.Count; i++)
				{
					Gizmos.DrawLine(from, this.nextCorners[i]);
					from = this.nextCorners[i];
				}
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00188EE3 File Offset: 0x001870E3
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.animCompatibility != null)
			{
				this.anim = this.animCompatibility;
			}
			return base.OnUpgradeSerializedData(version, unityThread);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00188F0A File Offset: 0x0018710A
		[Obsolete("Use SearchPath instead. [AstarUpgradable: 'UpdatePath' -> 'SearchPath']")]
		public void UpdatePath()
		{
			this.SearchPath();
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x00188F12 File Offset: 0x00187112
		[Obsolete("Use velocity instead (lowercase 'v'). [AstarUpgradable: 'Velocity' -> 'velocity']")]
		public Vector3 Velocity
		{
			get
			{
				return base.velocity;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x00188F1A File Offset: 0x0018711A
		[Obsolete("Use steeringTarget instead. [AstarUpgradable: 'NextWaypoint' -> 'steeringTarget']")]
		public Vector3 NextWaypoint
		{
			get
			{
				return this.steeringTarget;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x00188F22 File Offset: 0x00187122
		[Obsolete("Use Vector3.Distance(transform.position, ai.steeringTarget) instead.")]
		public float DistanceToNextWaypoint
		{
			get
			{
				return this.distanceToSteeringTarget;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x00187D59 File Offset: 0x00185F59
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x00187D61 File Offset: 0x00185F61
		[Obsolete("Use canSearch instead. [AstarUpgradable: 'repeatedlySearchPaths' -> 'canSearch']")]
		public bool repeatedlySearchPaths
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

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x00188F2A File Offset: 0x0018712A
		[Obsolete("When unifying the interfaces for different movement scripts, this property has been renamed to reachedEndOfPath (lowercase t).  [AstarUpgradable: 'TargetReached' -> 'reachedEndOfPath']")]
		public bool TargetReached
		{
			get
			{
				return this.reachedEndOfPath;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00188F32 File Offset: 0x00187132
		[Obsolete("Use pathPending instead (lowercase 'p'). [AstarUpgradable: 'PathPending' -> 'pathPending']")]
		public bool PathPending
		{
			get
			{
				return this.pathPending;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00188F3A File Offset: 0x0018713A
		[Obsolete("Use approachingPartEndpoint (lowercase 'a') instead")]
		public bool ApproachingPartEndpoint
		{
			get
			{
				return this.approachingPartEndpoint;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x00188F42 File Offset: 0x00187142
		[Obsolete("Use approachingPathEndpoint (lowercase 'a') instead")]
		public bool ApproachingPathEndpoint
		{
			get
			{
				return this.approachingPathEndpoint;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x00188F4A File Offset: 0x0018714A
		[Obsolete("This property has been renamed to 'traversingOffMeshLink'. [AstarUpgradable: 'TraversingSpecial' -> 'traversingOffMeshLink']")]
		public bool TraversingSpecial
		{
			get
			{
				return this.traversingOffMeshLink;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x00188F1A File Offset: 0x0018711A
		[Obsolete("This property has been renamed to steeringTarget")]
		public Vector3 TargetPoint
		{
			get
			{
				return this.steeringTarget;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x00188F54 File Offset: 0x00187154
		// (set) Token: 0x0600213B RID: 8507 RVA: 0x00188F7C File Offset: 0x0018717C
		[Obsolete("Use the onTraverseOffMeshLink event or the ... component instead. Setting this value will add a ... component")]
		public Animation anim
		{
			get
			{
				AnimationLinkTraverser component = base.GetComponent<AnimationLinkTraverser>();
				if (!(component != null))
				{
					return null;
				}
				return component.anim;
			}
			set
			{
				this.animCompatibility = null;
				AnimationLinkTraverser animationLinkTraverser = base.GetComponent<AnimationLinkTraverser>();
				if (animationLinkTraverser == null)
				{
					animationLinkTraverser = base.gameObject.AddComponent<AnimationLinkTraverser>();
				}
				animationLinkTraverser.anim = value;
			}
		}

		// Token: 0x04003E67 RID: 15975
		public float acceleration = 5f;

		// Token: 0x04003E68 RID: 15976
		public float rotationSpeed = 360f;

		// Token: 0x04003E69 RID: 15977
		public float slowdownTime = 0.5f;

		// Token: 0x04003E6A RID: 15978
		public float endReachedDistance = 0.01f;

		// Token: 0x04003E6B RID: 15979
		public float wallForce = 3f;

		// Token: 0x04003E6C RID: 15980
		public float wallDist = 1f;

		// Token: 0x04003E6D RID: 15981
		public bool funnelSimplification;

		// Token: 0x04003E6E RID: 15982
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x04003E6F RID: 15983
		public Func<RichSpecial, IEnumerator> onTraverseOffMeshLink;

		// Token: 0x04003E70 RID: 15984
		protected readonly RichPath richPath = new RichPath();

		// Token: 0x04003E71 RID: 15985
		protected bool delayUpdatePath;

		// Token: 0x04003E72 RID: 15986
		protected bool lastCorner;

		// Token: 0x04003E73 RID: 15987
		protected float distanceToSteeringTarget = float.PositiveInfinity;

		// Token: 0x04003E74 RID: 15988
		protected readonly List<Vector3> nextCorners = new List<Vector3>();

		// Token: 0x04003E75 RID: 15989
		protected readonly List<Vector3> wallBuffer = new List<Vector3>();

		// Token: 0x04003E78 RID: 15992
		protected static readonly Color GizmoColorPath = new Color(0.03137255f, 0.30588236f, 0.7607843f);

		// Token: 0x04003E79 RID: 15993
		[FormerlySerializedAs("anim")]
		[SerializeField]
		[HideInInspector]
		private Animation animCompatibility;
	}
}
