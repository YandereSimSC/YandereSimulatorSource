using System;
using System.Collections.Generic;
using AmplifyMotion;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000B6 RID: 182
[AddComponentMenu("")]
public class AmplifyMotionObjectBase : MonoBehaviour
{
	// Token: 0x170001FE RID: 510
	// (get) Token: 0x0600099A RID: 2458 RVA: 0x0004B258 File Offset: 0x00049458
	internal bool FixedStep
	{
		get
		{
			return this.m_fixedStep;
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x0600099B RID: 2459 RVA: 0x0004B260 File Offset: 0x00049460
	internal int ObjectId
	{
		get
		{
			return this.m_objectId;
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x0600099C RID: 2460 RVA: 0x0004B268 File Offset: 0x00049468
	public ObjectType Type
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0004B270 File Offset: 0x00049470
	internal void RegisterCamera(AmplifyMotionCamera camera)
	{
		Camera component = camera.GetComponent<Camera>();
		if ((component.cullingMask & 1 << base.gameObject.layer) != 0 && !this.m_states.ContainsKey(component))
		{
			MotionState value;
			switch (this.m_type)
			{
			case ObjectType.Solid:
				value = new SolidState(camera, this);
				break;
			case ObjectType.Skinned:
				value = new SkinnedState(camera, this);
				break;
			case ObjectType.Cloth:
				value = new ClothState(camera, this);
				break;
			case ObjectType.Particle:
				value = new ParticleState(camera, this);
				break;
			default:
				throw new Exception("[AmplifyMotion] Invalid object type.");
			}
			camera.RegisterObject(this);
			this.m_states.Add(component, value);
		}
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0004B318 File Offset: 0x00049518
	internal void UnregisterCamera(AmplifyMotionCamera camera)
	{
		Camera component = camera.GetComponent<Camera>();
		MotionState motionState;
		if (this.m_states.TryGetValue(component, out motionState))
		{
			camera.UnregisterObject(this);
			if (this.m_states.TryGetValue(component, out motionState))
			{
				motionState.Shutdown();
			}
			this.m_states.Remove(component);
		}
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0004B368 File Offset: 0x00049568
	private bool InitializeType()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (AmplifyMotionEffectBase.CanRegister(base.gameObject, false))
		{
			if (base.GetComponent<ParticleSystem>() != null)
			{
				this.m_type = ObjectType.Particle;
				AmplifyMotionEffectBase.RegisterObject(this);
			}
			else if (component != null)
			{
				if (component.GetType() == typeof(MeshRenderer))
				{
					this.m_type = ObjectType.Solid;
				}
				else if (component.GetType() == typeof(SkinnedMeshRenderer))
				{
					if (base.GetComponent<Cloth>() != null)
					{
						this.m_type = ObjectType.Cloth;
					}
					else
					{
						this.m_type = ObjectType.Skinned;
					}
				}
				AmplifyMotionEffectBase.RegisterObject(this);
			}
		}
		return component != null;
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0004B418 File Offset: 0x00049618
	private void OnEnable()
	{
		bool flag = this.InitializeType();
		if (flag)
		{
			if (this.m_type == ObjectType.Cloth)
			{
				this.m_fixedStep = false;
			}
			else if (this.m_type == ObjectType.Solid)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				if (component != null && component.interpolation == RigidbodyInterpolation.None && !component.isKinematic)
				{
					this.m_fixedStep = true;
				}
			}
		}
		if (this.m_applyToChildren)
		{
			foreach (object obj in base.gameObject.transform)
			{
				AmplifyMotionEffectBase.RegisterRecursivelyS(((Transform)obj).gameObject);
			}
		}
		if (!flag)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0004B4D8 File Offset: 0x000496D8
	private void OnDisable()
	{
		AmplifyMotionEffectBase.UnregisterObject(this);
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0004B4E0 File Offset: 0x000496E0
	private void TryInitializeStates()
	{
		foreach (KeyValuePair<Camera, MotionState> keyValuePair in this.m_states)
		{
			MotionState value = keyValuePair.Value;
			if (value.Owner.Initialized && !value.Error && !value.Initialized)
			{
				value.Initialize();
			}
		}
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0004B537 File Offset: 0x00049737
	private void Start()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			this.TryInitializeStates();
		}
		this.m_lastPosition = base.transform.position;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0004B55D File Offset: 0x0004975D
	private void Update()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			this.TryInitializeStates();
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0004B574 File Offset: 0x00049774
	private static void RecursiveResetMotionAtFrame(Transform transform, AmplifyMotionObjectBase obj, int frame)
	{
		if (obj != null)
		{
			obj.m_resetAtFrame = frame;
		}
		foreach (object obj2 in transform)
		{
			Transform transform2 = (Transform)obj2;
			AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(transform2, transform2.GetComponent<AmplifyMotionObjectBase>(), frame);
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0004B5DC File Offset: 0x000497DC
	public void ResetMotionNow()
	{
		AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, Time.frameCount);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0004B5EF File Offset: 0x000497EF
	public void ResetMotionAtFrame(int frame)
	{
		AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, frame);
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0004B5FE File Offset: 0x000497FE
	private void CheckTeleportReset(AmplifyMotionEffectBase inst)
	{
		if (Vector3.SqrMagnitude(base.transform.position - this.m_lastPosition) > inst.MinResetDeltaDistSqr)
		{
			AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, Time.frameCount + inst.ResetFrameDelay);
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0004B63C File Offset: 0x0004983C
	internal void OnUpdateTransform(AmplifyMotionEffectBase inst, Camera camera, CommandBuffer updateCB, bool starting)
	{
		MotionState motionState;
		if (this.m_states.TryGetValue(camera, out motionState) && !motionState.Error)
		{
			this.CheckTeleportReset(inst);
			bool flag = this.m_resetAtFrame > 0 && Time.frameCount >= this.m_resetAtFrame;
			motionState.UpdateTransform(updateCB, starting || flag);
		}
		this.m_lastPosition = base.transform.position;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0004B6A4 File Offset: 0x000498A4
	internal void OnRenderVectors(Camera camera, CommandBuffer renderCB, float scale, Quality quality)
	{
		MotionState motionState;
		if (this.m_states.TryGetValue(camera, out motionState) && !motionState.Error)
		{
			motionState.RenderVectors(camera, renderCB, scale, quality);
			if (this.m_resetAtFrame > 0 && Time.frameCount >= this.m_resetAtFrame)
			{
				this.m_resetAtFrame = -1;
			}
		}
	}

	// Token: 0x04000810 RID: 2064
	internal static bool ApplyToChildren = true;

	// Token: 0x04000811 RID: 2065
	[SerializeField]
	private bool m_applyToChildren = AmplifyMotionObjectBase.ApplyToChildren;

	// Token: 0x04000812 RID: 2066
	private ObjectType m_type;

	// Token: 0x04000813 RID: 2067
	private Dictionary<Camera, MotionState> m_states = new Dictionary<Camera, MotionState>();

	// Token: 0x04000814 RID: 2068
	private bool m_fixedStep;

	// Token: 0x04000815 RID: 2069
	private int m_objectId;

	// Token: 0x04000816 RID: 2070
	private Vector3 m_lastPosition = Vector3.zero;

	// Token: 0x04000817 RID: 2071
	private int m_resetAtFrame = -1;

	// Token: 0x02000691 RID: 1681
	public enum MinMaxCurveState
	{
		// Token: 0x040045FC RID: 17916
		Scalar,
		// Token: 0x040045FD RID: 17917
		Curve,
		// Token: 0x040045FE RID: 17918
		TwoCurves,
		// Token: 0x040045FF RID: 17919
		TwoScalars
	}
}
