using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : MonoBehaviour
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060001D3 RID: 467 RVA: 0x000165EB File Offset: 0x000147EB
	// (set) Token: 0x060001D4 RID: 468 RVA: 0x000165F3 File Offset: 0x000147F3
	public Vector3 dragMovement
	{
		get
		{
			return this.scale;
		}
		set
		{
			this.scale = value;
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x000165FC File Offset: 0x000147FC
	private void OnEnable()
	{
		if (this.scrollWheelFactor != 0f)
		{
			this.scrollMomentum = this.scale * this.scrollWheelFactor;
			this.scrollWheelFactor = 0f;
		}
		if (this.contentRect == null && this.target != null && Application.isPlaying)
		{
			UIWidget component = this.target.GetComponent<UIWidget>();
			if (component != null)
			{
				this.contentRect = component;
			}
		}
		this.mTargetPos = ((this.target != null) ? this.target.position : Vector3.zero);
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0001669D File Offset: 0x0001489D
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000166A8 File Offset: 0x000148A8
	private void FindPanel()
	{
		this.panelRegion = ((this.target != null) ? UIPanel.Find(this.target.transform.parent) : null);
		if (this.panelRegion == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000166F8 File Offset: 0x000148F8
	private void UpdateBounds()
	{
		if (this.contentRect)
		{
			Matrix4x4 worldToLocalMatrix = this.panelRegion.cachedTransform.worldToLocalMatrix;
			Vector3[] worldCorners = this.contentRect.worldCorners;
			for (int i = 0; i < 4; i++)
			{
				worldCorners[i] = worldToLocalMatrix.MultiplyPoint3x4(worldCorners[i]);
			}
			this.mBounds = new Bounds(worldCorners[0], Vector3.zero);
			for (int j = 1; j < 4; j++)
			{
				this.mBounds.Encapsulate(worldCorners[j]);
			}
			return;
		}
		this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.panelRegion.cachedTransform, this.target);
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x000167A4 File Offset: 0x000149A4
	private void OnPress(bool pressed)
	{
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		float timeScale = Time.timeScale;
		if (timeScale < 0.01f && timeScale != 0f)
		{
			return;
		}
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			if (pressed)
			{
				if (!this.mPressed)
				{
					this.mTouchID = UICamera.currentTouchID;
					this.mPressed = true;
					this.mStarted = false;
					this.CancelMovement();
					if (this.restrictWithinPanel && this.panelRegion == null)
					{
						this.FindPanel();
					}
					if (this.restrictWithinPanel)
					{
						this.UpdateBounds();
					}
					this.CancelSpring();
					Transform transform = UICamera.currentCamera.transform;
					this.mPlane = new Plane(((this.panelRegion != null) ? this.panelRegion.cachedTransform.rotation : transform.rotation) * Vector3.back, UICamera.lastWorldPosition);
					return;
				}
			}
			else if (this.mPressed && this.mTouchID == UICamera.currentTouchID)
			{
				this.mPressed = false;
				if (this.restrictWithinPanel && this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring && this.panelRegion.ConstrainTargetToBounds(this.target, ref this.mBounds, false))
				{
					this.CancelMovement();
				}
			}
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00016900 File Offset: 0x00014B00
	private void OnDrag(Vector2 delta)
	{
		if (this.mPressed && this.mTouchID == UICamera.currentTouchID && base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float distance = 0f;
			if (this.mPlane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (!this.mStarted)
				{
					this.mStarted = true;
					vector = Vector3.zero;
				}
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.target.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.target.TransformDirection(vector);
				}
				if (this.dragEffect != UIDragObject.DragEffect.None)
				{
					this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				}
				Vector3 localPosition = this.target.localPosition;
				this.Move(vector);
				if (this.restrictWithinPanel)
				{
					this.mBounds.center = this.mBounds.center + (this.target.localPosition - localPosition);
					if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.panelRegion.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
					{
						this.CancelMovement();
					}
				}
			}
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00016AB4 File Offset: 0x00014CB4
	private void Move(Vector3 worldDelta)
	{
		if (this.panelRegion != null)
		{
			this.mTargetPos += worldDelta;
			Transform parent = this.target.parent;
			Rigidbody component = this.target.GetComponent<Rigidbody>();
			if (parent != null)
			{
				Vector3 vector = parent.worldToLocalMatrix.MultiplyPoint3x4(this.mTargetPos);
				vector.x = Mathf.Round(vector.x);
				vector.y = Mathf.Round(vector.y);
				if (component != null)
				{
					vector = parent.localToWorldMatrix.MultiplyPoint3x4(vector);
					component.position = vector;
				}
				else
				{
					this.target.localPosition = vector;
				}
			}
			else if (component != null)
			{
				component.position = this.mTargetPos;
			}
			else
			{
				this.target.position = this.mTargetPos;
			}
			UIScrollView component2 = this.panelRegion.GetComponent<UIScrollView>();
			if (component2 != null)
			{
				component2.UpdateScrollbars(true);
				return;
			}
		}
		else
		{
			this.target.position += worldDelta;
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00016BCC File Offset: 0x00014DCC
	private void LateUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		this.mMomentum -= this.mScroll;
		this.mScroll = NGUIMath.SpringLerp(this.mScroll, Vector3.zero, 20f, deltaTime);
		if (this.mMomentum.magnitude < 0.0001f)
		{
			return;
		}
		if (!this.mPressed)
		{
			if (this.panelRegion == null)
			{
				this.FindPanel();
			}
			this.Move(NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime));
			if (this.restrictWithinPanel && this.panelRegion != null)
			{
				this.UpdateBounds();
				if (this.panelRegion.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
				{
					this.CancelMovement();
				}
				else
				{
					this.CancelSpring();
				}
			}
			NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
			if (this.mMomentum.magnitude < 0.0001f)
			{
				this.CancelMovement();
				return;
			}
		}
		else
		{
			NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00016CF4 File Offset: 0x00014EF4
	public void CancelMovement()
	{
		if (this.target != null)
		{
			Vector3 localPosition = this.target.localPosition;
			localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
			localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			this.target.localPosition = localPosition;
		}
		this.mTargetPos = ((this.target != null) ? this.target.position : Vector3.zero);
		this.mMomentum = Vector3.zero;
		this.mScroll = Vector3.zero;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00016D9C File Offset: 0x00014F9C
	public void CancelSpring()
	{
		SpringPosition component = this.target.GetComponent<SpringPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00016DC5 File Offset: 0x00014FC5
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.mScroll -= this.scrollMomentum * (delta * 0.05f);
		}
	}

	// Token: 0x0400034A RID: 842
	public Transform target;

	// Token: 0x0400034B RID: 843
	public UIPanel panelRegion;

	// Token: 0x0400034C RID: 844
	public Vector3 scrollMomentum = Vector3.zero;

	// Token: 0x0400034D RID: 845
	public bool restrictWithinPanel;

	// Token: 0x0400034E RID: 846
	public UIRect contentRect;

	// Token: 0x0400034F RID: 847
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x04000350 RID: 848
	public float momentumAmount = 35f;

	// Token: 0x04000351 RID: 849
	[SerializeField]
	protected Vector3 scale = new Vector3(1f, 1f, 0f);

	// Token: 0x04000352 RID: 850
	[SerializeField]
	[HideInInspector]
	private float scrollWheelFactor;

	// Token: 0x04000353 RID: 851
	private Plane mPlane;

	// Token: 0x04000354 RID: 852
	private Vector3 mTargetPos;

	// Token: 0x04000355 RID: 853
	private Vector3 mLastPos;

	// Token: 0x04000356 RID: 854
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000357 RID: 855
	private Vector3 mScroll = Vector3.zero;

	// Token: 0x04000358 RID: 856
	private Bounds mBounds;

	// Token: 0x04000359 RID: 857
	private int mTouchID;

	// Token: 0x0400035A RID: 858
	private bool mStarted;

	// Token: 0x0400035B RID: 859
	private bool mPressed;

	// Token: 0x02000614 RID: 1556
	[DoNotObfuscateNGUI]
	public enum DragEffect
	{
		// Token: 0x04004490 RID: 17552
		None,
		// Token: 0x04004491 RID: 17553
		Momentum,
		// Token: 0x04004492 RID: 17554
		MomentumAndSpring
	}
}
