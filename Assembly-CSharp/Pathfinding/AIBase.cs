using System;
using Pathfinding.RVO;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000510 RID: 1296
	[RequireComponent(typeof(Seeker))]
	public abstract class AIBase : VersionedMonoBehaviour
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00186561 File Offset: 0x00184761
		public Vector3 position
		{
			get
			{
				if (!this.updatePosition)
				{
					return this.simulatedPosition;
				}
				return this.tr.position;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0018657D File Offset: 0x0018477D
		public Quaternion rotation
		{
			get
			{
				if (!this.updateRotation)
				{
					return this.simulatedRotation;
				}
				return this.tr.rotation;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x00186599 File Offset: 0x00184799
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x001865A1 File Offset: 0x001847A1
		private protected bool usingGravity { protected get; private set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x001865AC File Offset: 0x001847AC
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x001865D4 File Offset: 0x001847D4
		[Obsolete("Use the destination property or the AIDestinationSetter component instead")]
		public Transform target
		{
			get
			{
				AIDestinationSetter component = base.GetComponent<AIDestinationSetter>();
				if (!(component != null))
				{
					return null;
				}
				return component.target;
			}
			set
			{
				this.targetCompatibility = null;
				AIDestinationSetter aidestinationSetter = base.GetComponent<AIDestinationSetter>();
				if (aidestinationSetter == null)
				{
					aidestinationSetter = base.gameObject.AddComponent<AIDestinationSetter>();
				}
				aidestinationSetter.target = value;
				this.destination = ((value != null) ? value.position : new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity));
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x00186636 File Offset: 0x00184836
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x0018663E File Offset: 0x0018483E
		public Vector3 destination { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00186647 File Offset: 0x00184847
		public Vector3 velocity
		{
			get
			{
				if (this.lastDeltaTime <= 1E-06f)
				{
					return Vector3.zero;
				}
				return (this.prevPosition1 - this.prevPosition2) / this.lastDeltaTime;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x00186678 File Offset: 0x00184878
		public Vector3 desiredVelocity
		{
			get
			{
				if (this.lastDeltaTime <= 1E-05f)
				{
					return Vector3.zero;
				}
				return this.movementPlane.ToWorld(this.lastDeltaPosition / this.lastDeltaTime, this.verticalVelocity);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x001866AF File Offset: 0x001848AF
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x001866B7 File Offset: 0x001848B7
		public bool isStopped { get; set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x001866C0 File Offset: 0x001848C0
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x001866C8 File Offset: 0x001848C8
		public Action onSearchPath { get; set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x001866D1 File Offset: 0x001848D1
		protected virtual bool shouldRecalculatePath
		{
			get
			{
				return Time.time - this.lastRepath >= this.repathRate && !this.waitingForPathCalculation && this.canSearch && !float.IsPositiveInfinity(this.destination.x);
			}
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0018670C File Offset: 0x0018490C
		protected AIBase()
		{
			this.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x001867C0 File Offset: 0x001849C0
		public virtual void FindComponents()
		{
			this.tr = base.transform;
			this.seeker = base.GetComponent<Seeker>();
			this.rvoController = base.GetComponent<RVOController>();
			this.controller = base.GetComponent<CharacterController>();
			this.rigid = base.GetComponent<Rigidbody>();
			this.rigid2D = base.GetComponent<Rigidbody2D>();
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00186815 File Offset: 0x00184A15
		protected virtual void OnEnable()
		{
			this.FindComponents();
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.Init();
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0018684B File Offset: 0x00184A4B
		protected virtual void Start()
		{
			this.startHasRun = true;
			this.Init();
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0018685A File Offset: 0x00184A5A
		private void Init()
		{
			if (this.startHasRun)
			{
				this.Teleport(this.position, false);
				this.lastRepath = float.NegativeInfinity;
				if (this.shouldRecalculatePath)
				{
					this.SearchPath();
				}
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x0018688C File Offset: 0x00184A8C
		public virtual void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			if (clearPath)
			{
				this.CancelCurrentPathRequest();
			}
			this.simulatedPosition = newPosition;
			this.prevPosition2 = newPosition;
			this.prevPosition1 = newPosition;
			if (this.updatePosition)
			{
				this.tr.position = newPosition;
			}
			if (this.rvoController != null)
			{
				this.rvoController.Move(Vector3.zero);
			}
			if (clearPath)
			{
				this.SearchPath();
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x001868F6 File Offset: 0x00184AF6
		protected void CancelCurrentPathRequest()
		{
			this.waitingForPathCalculation = false;
			if (this.seeker != null)
			{
				this.seeker.CancelCurrentPathRequest(true);
			}
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0018691C File Offset: 0x00184B1C
		protected virtual void OnDisable()
		{
			this.CancelCurrentPathRequest();
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.velocity2D = Vector3.zero;
			this.accumulatedMovementDelta = Vector3.zero;
			this.verticalVelocity = 0f;
			this.lastDeltaTime = 0f;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00186988 File Offset: 0x00184B88
		protected virtual void Update()
		{
			if (this.shouldRecalculatePath)
			{
				this.SearchPath();
			}
			this.usingGravity = (!(this.gravity == Vector3.zero) && (!this.updatePosition || ((this.rigid == null || this.rigid.isKinematic) && (this.rigid2D == null || this.rigid2D.isKinematic))));
			if (this.rigid == null && this.rigid2D == null && this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00186A40 File Offset: 0x00184C40
		protected virtual void FixedUpdate()
		{
			if ((!(this.rigid == null) || !(this.rigid2D == null)) && this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.fixedDeltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00186A88 File Offset: 0x00184C88
		public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			this.lastDeltaTime = deltaTime;
			this.MovementUpdateInternal(deltaTime, out nextPosition, out nextRotation);
		}

		// Token: 0x0600208E RID: 8334
		protected abstract void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation);

		// Token: 0x0600208F RID: 8335 RVA: 0x00186A9A File Offset: 0x00184C9A
		protected virtual void CalculatePathRequestEndpoints(out Vector3 start, out Vector3 end)
		{
			start = this.GetFeetPosition();
			end = this.destination;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00186AB4 File Offset: 0x00184CB4
		public virtual void SearchPath()
		{
			if (float.IsPositiveInfinity(this.destination.x))
			{
				return;
			}
			if (this.onSearchPath != null)
			{
				this.onSearchPath();
			}
			this.lastRepath = Time.time;
			this.waitingForPathCalculation = true;
			this.seeker.CancelCurrentPathRequest(true);
			Vector3 start;
			Vector3 end;
			this.CalculatePathRequestEndpoints(out start, out end);
			this.seeker.StartPath(start, end);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00186B20 File Offset: 0x00184D20
		public virtual Vector3 GetFeetPosition()
		{
			if (this.rvoController != null && this.rvoController.enabled && this.rvoController.movementPlane == MovementPlane.XZ)
			{
				return this.position + this.rotation * Vector3.up * (this.rvoController.center - this.rvoController.height * 0.5f);
			}
			if (this.controller != null && this.controller.enabled && this.updatePosition)
			{
				return this.tr.TransformPoint(this.controller.center) - Vector3.up * this.controller.height * 0.5f;
			}
			return this.position;
		}

		// Token: 0x06002092 RID: 8338
		protected abstract void OnPathComplete(Path newPath);

		// Token: 0x06002093 RID: 8339 RVA: 0x00186BF8 File Offset: 0x00184DF8
		public void SetPath(Path path)
		{
			if (path.PipelineState == PathState.Created)
			{
				this.lastRepath = Time.time;
				this.waitingForPathCalculation = true;
				this.seeker.CancelCurrentPathRequest(true);
				this.seeker.StartPath(path, null);
				return;
			}
			if (path.PipelineState != PathState.Returned)
			{
				throw new ArgumentException("You must call the SetPath method with a path that either has been completely calculated or one whose path calculation has not been started at all. It looks like the path calculation for the path you tried to use has been started, but is not yet finished.");
			}
			if (this.seeker.GetCurrentPath() != path)
			{
				this.seeker.CancelCurrentPathRequest(true);
				this.OnPathComplete(path);
				return;
			}
			throw new ArgumentException("If you calculate the path using seeker.StartPath then this script will pick up the calculated path anyway as it listens for all paths the Seeker finishes calculating. You should not call SetPath in that case.");
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00186C7C File Offset: 0x00184E7C
		protected void ApplyGravity(float deltaTime)
		{
			if (this.usingGravity)
			{
				float num;
				this.velocity2D += this.movementPlane.ToPlane(deltaTime * (float.IsNaN(this.gravity.x) ? Physics.gravity : this.gravity), out num);
				this.verticalVelocity += num;
				return;
			}
			this.verticalVelocity = 0f;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00186CF0 File Offset: 0x00184EF0
		protected Vector2 CalculateDeltaToMoveThisFrame(Vector2 position, float distanceToEndOfPath, float deltaTime)
		{
			if (this.rvoController != null && this.rvoController.enabled)
			{
				return this.movementPlane.ToPlane(this.rvoController.CalculateMovementDelta(this.movementPlane.ToWorld(position, 0f), deltaTime));
			}
			return Vector2.ClampMagnitude(this.velocity2D * deltaTime, distanceToEndOfPath);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00186D53 File Offset: 0x00184F53
		public Quaternion SimulateRotationTowards(Vector3 direction, float maxDegrees)
		{
			return this.SimulateRotationTowards(this.movementPlane.ToPlane(direction), maxDegrees);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00186D68 File Offset: 0x00184F68
		protected Quaternion SimulateRotationTowards(Vector2 direction, float maxDegrees)
		{
			if (direction != Vector2.zero)
			{
				Quaternion quaternion = Quaternion.LookRotation(this.movementPlane.ToWorld(direction, 0f), this.movementPlane.ToWorld(Vector2.zero, 1f));
				if (this.rotationIn2D)
				{
					quaternion *= Quaternion.Euler(90f, 0f, 0f);
				}
				return Quaternion.RotateTowards(this.simulatedRotation, quaternion, maxDegrees);
			}
			return this.simulatedRotation;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00186DE5 File Offset: 0x00184FE5
		public virtual void Move(Vector3 deltaPosition)
		{
			this.accumulatedMovementDelta += deltaPosition;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00186DF9 File Offset: 0x00184FF9
		public virtual void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
		{
			this.FinalizeRotation(nextRotation);
			this.FinalizePosition(nextPosition);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00186E0C File Offset: 0x0018500C
		private void FinalizeRotation(Quaternion nextRotation)
		{
			this.simulatedRotation = nextRotation;
			if (this.updateRotation)
			{
				if (this.rigid != null)
				{
					this.rigid.MoveRotation(nextRotation);
					return;
				}
				if (this.rigid2D != null)
				{
					this.rigid2D.MoveRotation(nextRotation.eulerAngles.z);
					return;
				}
				this.tr.rotation = nextRotation;
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00186E78 File Offset: 0x00185078
		private void FinalizePosition(Vector3 nextPosition)
		{
			Vector3 vector = this.simulatedPosition;
			bool flag = false;
			if (this.controller != null && this.controller.enabled && this.updatePosition)
			{
				this.tr.position = vector;
				this.controller.Move(nextPosition - vector + this.accumulatedMovementDelta);
				vector = this.tr.position;
				if (this.controller.isGrounded)
				{
					this.verticalVelocity = 0f;
				}
			}
			else
			{
				float lastElevation;
				this.movementPlane.ToPlane(vector, out lastElevation);
				vector = nextPosition + this.accumulatedMovementDelta;
				if (this.usingGravity)
				{
					vector = this.RaycastPosition(vector, lastElevation);
				}
				flag = true;
			}
			bool flag2 = false;
			vector = this.ClampToNavmesh(vector, out flag2);
			if ((flag || flag2) && this.updatePosition)
			{
				if (this.rigid != null)
				{
					this.rigid.MovePosition(vector);
				}
				else if (this.rigid2D != null)
				{
					this.rigid2D.MovePosition(vector);
				}
				else
				{
					this.tr.position = vector;
				}
			}
			this.accumulatedMovementDelta = Vector3.zero;
			this.simulatedPosition = vector;
			this.UpdateVelocity();
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00186FAC File Offset: 0x001851AC
		protected void UpdateVelocity()
		{
			int frameCount = Time.frameCount;
			if (frameCount != this.prevFrame)
			{
				this.prevPosition2 = this.prevPosition1;
			}
			this.prevPosition1 = this.position;
			this.prevFrame = frameCount;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00186FE7 File Offset: 0x001851E7
		protected virtual Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			positionChanged = false;
			return position;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00186FF0 File Offset: 0x001851F0
		protected Vector3 RaycastPosition(Vector3 position, float lastElevation)
		{
			float num;
			this.movementPlane.ToPlane(position, out num);
			float num2 = this.centerOffset + Mathf.Max(0f, lastElevation - num);
			Vector3 vector = this.movementPlane.ToWorld(Vector2.zero, num2);
			RaycastHit raycastHit;
			if (Physics.Raycast(position + vector, -vector, out raycastHit, num2, this.groundMask, QueryTriggerInteraction.Ignore))
			{
				this.verticalVelocity *= Math.Max(0f, 1f - 5f * this.lastDeltaTime);
				return raycastHit.point;
			}
			return position;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00187088 File Offset: 0x00185288
		protected virtual void OnDrawGizmosSelected()
		{
			if (Application.isPlaying)
			{
				this.FindComponents();
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00187098 File Offset: 0x00185298
		protected virtual void OnDrawGizmos()
		{
			if (!Application.isPlaying || !base.enabled)
			{
				this.FindComponents();
			}
			if (!(this.gravity == Vector3.zero) && (!this.updatePosition || ((this.rigid == null || this.rigid.isKinematic) && (this.rigid2D == null || this.rigid2D.isKinematic))) && (this.controller == null || !this.controller.enabled))
			{
				Gizmos.color = AIBase.GizmoColorRaycast;
				Gizmos.DrawLine(this.position, this.position + base.transform.up * this.centerOffset);
				Gizmos.DrawLine(this.position - base.transform.right * 0.1f, this.position + base.transform.right * 0.1f);
				Gizmos.DrawLine(this.position - base.transform.forward * 0.1f, this.position + base.transform.forward * 0.1f);
			}
			if (!float.IsPositiveInfinity(this.destination.x) && Application.isPlaying)
			{
				Draw.Gizmos.CircleXZ(this.destination, 0.2f, Color.blue, 0f, 6.2831855f);
			}
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00187234 File Offset: 0x00185434
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.targetCompatibility != null)
			{
				this.target = this.targetCompatibility;
			}
			return 1;
		}

		// Token: 0x04003E18 RID: 15896
		public float repathRate = 0.5f;

		// Token: 0x04003E19 RID: 15897
		[FormerlySerializedAs("repeatedlySearchPaths")]
		public bool canSearch = true;

		// Token: 0x04003E1A RID: 15898
		public bool canMove = true;

		// Token: 0x04003E1B RID: 15899
		[FormerlySerializedAs("speed")]
		public float maxSpeed = 1f;

		// Token: 0x04003E1C RID: 15900
		public Vector3 gravity = new Vector3(float.NaN, float.NaN, float.NaN);

		// Token: 0x04003E1D RID: 15901
		public LayerMask groundMask = -1;

		// Token: 0x04003E1E RID: 15902
		public float centerOffset = 1f;

		// Token: 0x04003E1F RID: 15903
		public bool rotationIn2D;

		// Token: 0x04003E20 RID: 15904
		protected Vector3 simulatedPosition;

		// Token: 0x04003E21 RID: 15905
		protected Quaternion simulatedRotation;

		// Token: 0x04003E22 RID: 15906
		private Vector3 accumulatedMovementDelta = Vector3.zero;

		// Token: 0x04003E23 RID: 15907
		protected Vector2 velocity2D;

		// Token: 0x04003E24 RID: 15908
		protected float verticalVelocity;

		// Token: 0x04003E25 RID: 15909
		public Seeker seeker;

		// Token: 0x04003E26 RID: 15910
		public Transform tr;

		// Token: 0x04003E27 RID: 15911
		protected Rigidbody rigid;

		// Token: 0x04003E28 RID: 15912
		protected Rigidbody2D rigid2D;

		// Token: 0x04003E29 RID: 15913
		protected CharacterController controller;

		// Token: 0x04003E2A RID: 15914
		protected RVOController rvoController;

		// Token: 0x04003E2B RID: 15915
		public IMovementPlane movementPlane = GraphTransform.identityTransform;

		// Token: 0x04003E2C RID: 15916
		[NonSerialized]
		public bool updatePosition = true;

		// Token: 0x04003E2D RID: 15917
		[NonSerialized]
		public bool updateRotation = true;

		// Token: 0x04003E2F RID: 15919
		protected float lastDeltaTime;

		// Token: 0x04003E30 RID: 15920
		protected int prevFrame;

		// Token: 0x04003E31 RID: 15921
		protected Vector3 prevPosition1;

		// Token: 0x04003E32 RID: 15922
		protected Vector3 prevPosition2;

		// Token: 0x04003E33 RID: 15923
		protected Vector2 lastDeltaPosition;

		// Token: 0x04003E34 RID: 15924
		protected bool waitingForPathCalculation;

		// Token: 0x04003E35 RID: 15925
		protected float lastRepath = float.NegativeInfinity;

		// Token: 0x04003E36 RID: 15926
		[FormerlySerializedAs("target")]
		[SerializeField]
		[HideInInspector]
		private Transform targetCompatibility;

		// Token: 0x04003E37 RID: 15927
		private bool startHasRun;

		// Token: 0x04003E3B RID: 15931
		protected static readonly Color GizmoColorRaycast = new Color(0.4627451f, 0.80784315f, 0.4392157f);
	}
}
