using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000511 RID: 1297
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/AI/AILerp (2D,3D)")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_lerp.php")]
	public class AILerp : VersionedMonoBehaviour, IAstarAI
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0018726F File Offset: 0x0018546F
		// (set) Token: 0x060020A4 RID: 8356 RVA: 0x00187277 File Offset: 0x00185477
		public bool reachedEndOfPath { get; private set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x00187280 File Offset: 0x00185480
		// (set) Token: 0x060020A6 RID: 8358 RVA: 0x00187288 File Offset: 0x00185488
		public Vector3 destination { get; set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x00187294 File Offset: 0x00185494
		// (set) Token: 0x060020A8 RID: 8360 RVA: 0x001872BC File Offset: 0x001854BC
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

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0018731E File Offset: 0x0018551E
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0018733A File Offset: 0x0018553A
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

		// Token: 0x060020AB RID: 8363 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IAstarAI.Move(Vector3 deltaPosition)
		{
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x00187356 File Offset: 0x00185556
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x0018735E File Offset: 0x0018555E
		float IAstarAI.maxSpeed
		{
			get
			{
				return this.speed;
			}
			set
			{
				this.speed = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x00187367 File Offset: 0x00185567
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x0018736F File Offset: 0x0018556F
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

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x00187378 File Offset: 0x00185578
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x00187380 File Offset: 0x00185580
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

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00187389 File Offset: 0x00185589
		Vector3 IAstarAI.velocity
		{
			get
			{
				if (Time.deltaTime <= 1E-05f)
				{
					return Vector3.zero;
				}
				return (this.previousPosition1 - this.previousPosition2) / Time.deltaTime;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x001873B8 File Offset: 0x001855B8
		Vector3 IAstarAI.desiredVelocity
		{
			get
			{
				return ((IAstarAI)this).velocity;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x001873C0 File Offset: 0x001855C0
		Vector3 IAstarAI.steeringTarget
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return this.simulatedPosition;
				}
				return this.interpolator.position + this.interpolator.tangent;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x001873F1 File Offset: 0x001855F1
		// (set) Token: 0x060020B6 RID: 8374 RVA: 0x00187408 File Offset: 0x00185608
		public float remainingDistance
		{
			get
			{
				return Mathf.Max(this.interpolator.remainingDistance, 0f);
			}
			set
			{
				this.interpolator.remainingDistance = Mathf.Max(value, 0f);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x00187420 File Offset: 0x00185620
		public bool hasPath
		{
			get
			{
				return this.interpolator.valid;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x0018742D File Offset: 0x0018562D
		public bool pathPending
		{
			get
			{
				return !this.canSearchAgain;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00187438 File Offset: 0x00185638
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x00187440 File Offset: 0x00185640
		public bool isStopped { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x00187449 File Offset: 0x00185649
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x00187451 File Offset: 0x00185651
		public Action onSearchPath { get; set; }

		// Token: 0x060020BD RID: 8381 RVA: 0x0018745C File Offset: 0x0018565C
		protected AILerp()
		{
			this.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x001874FC File Offset: 0x001856FC
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
			this.seeker = base.GetComponent<Seeker>();
			this.seeker.startEndModifier.adjustStartPoint = (() => this.simulatedPosition);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00187538 File Offset: 0x00185738
		protected virtual void Start()
		{
			this.startHasRun = true;
			this.Init();
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00187547 File Offset: 0x00185747
		protected virtual void OnEnable()
		{
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.Init();
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00187577 File Offset: 0x00185777
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

		// Token: 0x060020C2 RID: 8386 RVA: 0x001875A8 File Offset: 0x001857A8
		public void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.CancelCurrentPathRequest(true);
			}
			this.canSearchAgain = true;
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = null;
			this.interpolator.SetPath(null);
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00187628 File Offset: 0x00185828
		public void Teleport(Vector3 position, bool clearPath = true)
		{
			if (clearPath)
			{
				this.interpolator.SetPath(null);
			}
			this.previousPosition2 = position;
			this.previousPosition1 = position;
			this.simulatedPosition = position;
			if (this.updatePosition)
			{
				this.tr.position = position;
			}
			this.reachedEndOfPath = false;
			if (clearPath)
			{
				this.SearchPath();
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x00187681 File Offset: 0x00185881
		protected virtual bool shouldRecalculatePath
		{
			get
			{
				return Time.time - this.lastRepath >= this.repathRate && this.canSearchAgain && this.canSearch && !float.IsPositiveInfinity(this.destination.x);
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x001876BC File Offset: 0x001858BC
		[Obsolete("Use SearchPath instead")]
		public virtual void ForceSearchPath()
		{
			this.SearchPath();
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x001876C4 File Offset: 0x001858C4
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
			Vector3 feetPosition = this.GetFeetPosition();
			this.canSearchAgain = false;
			this.seeker.StartPath(feetPosition, this.destination);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnTargetReached()
		{
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00187724 File Offset: 0x00185924
		protected virtual void OnPathComplete(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.canSearchAgain = true;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				return;
			}
			if (this.interpolatePathSwitches)
			{
				this.ConfigurePathSwitchInterpolation();
			}
			ABPath abpath2 = this.path;
			this.path = abpath;
			this.reachedEndOfPath = false;
			if (this.path.vectorPath != null && this.path.vectorPath.Count == 1)
			{
				this.path.vectorPath.Insert(0, this.GetFeetPosition());
			}
			this.ConfigureNewPath();
			if (abpath2 != null)
			{
				abpath2.Release(this, false);
			}
			if (this.interpolator.remainingDistance < 0.0001f && !this.reachedEndOfPath)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x001877F8 File Offset: 0x001859F8
		public void SetPath(Path path)
		{
			if (path.PipelineState == PathState.Created)
			{
				this.lastRepath = Time.time;
				this.canSearchAgain = false;
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

		// Token: 0x060020CA RID: 8394 RVA: 0x0018787C File Offset: 0x00185A7C
		protected virtual void ConfigurePathSwitchInterpolation()
		{
			bool flag = this.interpolator.valid && this.interpolator.remainingDistance < 0.0001f;
			if (this.interpolator.valid && !flag)
			{
				this.previousMovementOrigin = this.interpolator.position;
				this.previousMovementDirection = this.interpolator.tangent.normalized * this.interpolator.remainingDistance;
				this.pathSwitchInterpolationTime = 0f;
				return;
			}
			this.previousMovementOrigin = Vector3.zero;
			this.previousMovementDirection = Vector3.zero;
			this.pathSwitchInterpolationTime = float.PositiveInfinity;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00187923 File Offset: 0x00185B23
		public virtual Vector3 GetFeetPosition()
		{
			return this.position;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0018792C File Offset: 0x00185B2C
		protected virtual void ConfigureNewPath()
		{
			bool valid = this.interpolator.valid;
			Vector3 vector = valid ? this.interpolator.tangent : Vector3.zero;
			this.interpolator.SetPath(this.path.vectorPath);
			this.interpolator.MoveToClosestPoint(this.GetFeetPosition());
			if (this.interpolatePathSwitches && this.switchPathInterpolationSpeed > 0.01f && valid)
			{
				float num = Mathf.Max(-Vector3.Dot(vector.normalized, this.interpolator.tangent.normalized), 0f);
				this.interpolator.distance -= this.speed * num * (1f / this.switchPathInterpolationSpeed);
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x001879F0 File Offset: 0x00185BF0
		protected virtual void Update()
		{
			if (this.shouldRecalculatePath)
			{
				this.SearchPath();
			}
			if (this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x00187A2C File Offset: 0x00185C2C
		public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			Vector3 direction;
			nextPosition = this.CalculateNextPosition(out direction, this.isStopped ? 0f : deltaTime);
			if (this.enableRotation)
			{
				nextRotation = this.SimulateRotationTowards(direction, deltaTime);
				return;
			}
			nextRotation = this.simulatedRotation;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00187AAC File Offset: 0x00185CAC
		public void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
		{
			this.previousPosition2 = this.previousPosition1;
			this.simulatedPosition = nextPosition;
			this.previousPosition1 = nextPosition;
			this.simulatedRotation = nextRotation;
			if (this.updatePosition)
			{
				this.tr.position = nextPosition;
			}
			if (this.updateRotation)
			{
				this.tr.rotation = nextRotation;
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00187B04 File Offset: 0x00185D04
		private Quaternion SimulateRotationTowards(Vector3 direction, float deltaTime)
		{
			if (direction != Vector3.zero)
			{
				Quaternion quaternion = Quaternion.LookRotation(direction, this.rotationIn2D ? Vector3.back : Vector3.up);
				if (this.rotationIn2D)
				{
					quaternion *= Quaternion.Euler(90f, 0f, 0f);
				}
				return Quaternion.Slerp(this.simulatedRotation, quaternion, deltaTime * this.rotationSpeed);
			}
			return this.simulatedRotation;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00187B78 File Offset: 0x00185D78
		protected virtual Vector3 CalculateNextPosition(out Vector3 direction, float deltaTime)
		{
			if (!this.interpolator.valid)
			{
				direction = Vector3.zero;
				return this.simulatedPosition;
			}
			this.interpolator.distance += deltaTime * this.speed;
			if (this.interpolator.remainingDistance < 0.0001f && !this.reachedEndOfPath)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
			direction = this.interpolator.tangent;
			this.pathSwitchInterpolationTime += deltaTime;
			float num = this.switchPathInterpolationSpeed * this.pathSwitchInterpolationTime;
			if (this.interpolatePathSwitches && num < 1f)
			{
				return Vector3.Lerp(this.previousMovementOrigin + Vector3.ClampMagnitude(this.previousMovementDirection, this.speed * this.pathSwitchInterpolationTime), this.interpolator.position, num);
			}
			return this.interpolator.position;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x00187C62 File Offset: 0x00185E62
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.targetCompatibility != null)
			{
				this.target = this.targetCompatibility;
			}
			return 2;
		}

		// Token: 0x04003E3C RID: 15932
		public float repathRate = 0.5f;

		// Token: 0x04003E3D RID: 15933
		public bool canSearch = true;

		// Token: 0x04003E3E RID: 15934
		public bool canMove = true;

		// Token: 0x04003E3F RID: 15935
		public float speed = 3f;

		// Token: 0x04003E40 RID: 15936
		public bool enableRotation = true;

		// Token: 0x04003E41 RID: 15937
		public bool rotationIn2D;

		// Token: 0x04003E42 RID: 15938
		public float rotationSpeed = 10f;

		// Token: 0x04003E43 RID: 15939
		public bool interpolatePathSwitches = true;

		// Token: 0x04003E44 RID: 15940
		public float switchPathInterpolationSpeed = 5f;

		// Token: 0x04003E47 RID: 15943
		[NonSerialized]
		public bool updatePosition = true;

		// Token: 0x04003E48 RID: 15944
		[NonSerialized]
		public bool updateRotation = true;

		// Token: 0x04003E4B RID: 15947
		protected Seeker seeker;

		// Token: 0x04003E4C RID: 15948
		protected Transform tr;

		// Token: 0x04003E4D RID: 15949
		protected float lastRepath = -9999f;

		// Token: 0x04003E4E RID: 15950
		protected ABPath path;

		// Token: 0x04003E4F RID: 15951
		protected bool canSearchAgain = true;

		// Token: 0x04003E50 RID: 15952
		protected Vector3 previousMovementOrigin;

		// Token: 0x04003E51 RID: 15953
		protected Vector3 previousMovementDirection;

		// Token: 0x04003E52 RID: 15954
		protected float pathSwitchInterpolationTime;

		// Token: 0x04003E53 RID: 15955
		protected PathInterpolator interpolator = new PathInterpolator();

		// Token: 0x04003E54 RID: 15956
		private bool startHasRun;

		// Token: 0x04003E55 RID: 15957
		private Vector3 previousPosition1;

		// Token: 0x04003E56 RID: 15958
		private Vector3 previousPosition2;

		// Token: 0x04003E57 RID: 15959
		private Vector3 simulatedPosition;

		// Token: 0x04003E58 RID: 15960
		private Quaternion simulatedRotation;

		// Token: 0x04003E59 RID: 15961
		[FormerlySerializedAs("target")]
		[SerializeField]
		[HideInInspector]
		private Transform targetCompatibility;
	}
}
