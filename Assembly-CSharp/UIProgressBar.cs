using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005F RID: 95
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Progress Bar")]
public class UIProgressBar : UIWidgetContainer
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060002B0 RID: 688 RVA: 0x0001B6C9 File Offset: 0x000198C9
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060002B1 RID: 689 RVA: 0x0001B6EB File Offset: 0x000198EB
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060002B2 RID: 690 RVA: 0x0001B717 File Offset: 0x00019917
	// (set) Token: 0x060002B3 RID: 691 RVA: 0x0001B71F File Offset: 0x0001991F
	public UIWidget foregroundWidget
	{
		get
		{
			return this.mFG;
		}
		set
		{
			if (this.mFG != value)
			{
				this.mFG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060002B4 RID: 692 RVA: 0x0001B73D File Offset: 0x0001993D
	// (set) Token: 0x060002B5 RID: 693 RVA: 0x0001B745 File Offset: 0x00019945
	public UIWidget backgroundWidget
	{
		get
		{
			return this.mBG;
		}
		set
		{
			if (this.mBG != value)
			{
				this.mBG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001B763 File Offset: 0x00019963
	// (set) Token: 0x060002B7 RID: 695 RVA: 0x0001B76B File Offset: 0x0001996B
	public UIProgressBar.FillDirection fillDirection
	{
		get
		{
			return this.mFill;
		}
		set
		{
			if (this.mFill != value)
			{
				this.mFill = value;
				if (this.mStarted)
				{
					this.ForceUpdate();
				}
			}
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001B78B File Offset: 0x0001998B
	// (set) Token: 0x060002B9 RID: 697 RVA: 0x0001B7BC File Offset: 0x000199BC
	public float value
	{
		get
		{
			if (this.numberOfSteps > 1)
			{
				return Mathf.Round(this.mValue * (float)(this.numberOfSteps - 1)) / (float)(this.numberOfSteps - 1);
			}
			return this.mValue;
		}
		set
		{
			this.Set(value, true);
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060002BA RID: 698 RVA: 0x0001B7C6 File Offset: 0x000199C6
	// (set) Token: 0x060002BB RID: 699 RVA: 0x0001B804 File Offset: 0x00019A04
	public float alpha
	{
		get
		{
			if (this.mFG != null)
			{
				return this.mFG.alpha;
			}
			if (this.mBG != null)
			{
				return this.mBG.alpha;
			}
			return 1f;
		}
		set
		{
			if (this.mFG != null)
			{
				this.mFG.alpha = value;
				if (this.mFG.GetComponent<Collider>() != null)
				{
					this.mFG.GetComponent<Collider>().enabled = (this.mFG.alpha > 0.001f);
				}
				else if (this.mFG.GetComponent<Collider2D>() != null)
				{
					this.mFG.GetComponent<Collider2D>().enabled = (this.mFG.alpha > 0.001f);
				}
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				if (this.mBG.GetComponent<Collider>() != null)
				{
					this.mBG.GetComponent<Collider>().enabled = (this.mBG.alpha > 0.001f);
				}
				else if (this.mBG.GetComponent<Collider2D>() != null)
				{
					this.mBG.GetComponent<Collider2D>().enabled = (this.mBG.alpha > 0.001f);
				}
			}
			if (this.thumb != null)
			{
				UIWidget component = this.thumb.GetComponent<UIWidget>();
				if (component != null)
				{
					component.alpha = value;
					if (component.GetComponent<Collider>() != null)
					{
						component.GetComponent<Collider>().enabled = (component.alpha > 0.001f);
						return;
					}
					if (component.GetComponent<Collider2D>() != null)
					{
						component.GetComponent<Collider2D>().enabled = (component.alpha > 0.001f);
					}
				}
			}
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060002BC RID: 700 RVA: 0x0001B994 File Offset: 0x00019B94
	protected bool isHorizontal
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.LeftToRight || this.mFill == UIProgressBar.FillDirection.RightToLeft;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060002BD RID: 701 RVA: 0x0001B9A9 File Offset: 0x00019BA9
	protected bool isInverted
	{
		get
		{
			return this.mFill == UIProgressBar.FillDirection.RightToLeft || this.mFill == UIProgressBar.FillDirection.TopToBottom;
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0001B9C0 File Offset: 0x00019BC0
	public void Set(float val, bool notify = true)
	{
		val = Mathf.Clamp01(val);
		if (this.mValue != val)
		{
			float value = this.value;
			this.mValue = val;
			if (this.mStarted && value != this.value)
			{
				if (notify && NGUITools.GetActive(this) && EventDelegate.IsValid(this.onChange))
				{
					UIProgressBar.current = this;
					EventDelegate.Execute(this.onChange);
					UIProgressBar.current = null;
				}
				this.ForceUpdate();
			}
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0001BA34 File Offset: 0x00019C34
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		this.mStarted = true;
		this.Upgrade();
		if (Application.isPlaying)
		{
			if (this.mBG != null)
			{
				this.mBG.autoResizeBoxCollider = true;
			}
			this.OnStart();
			if (UIProgressBar.current == null && this.onChange != null)
			{
				UIProgressBar.current = this;
				EventDelegate.Execute(this.onChange);
				UIProgressBar.current = null;
			}
		}
		this.ForceUpdate();
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00002ACE File Offset: 0x00000CCE
	protected virtual void Upgrade()
	{
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00002ACE File Offset: 0x00000CCE
	protected virtual void OnStart()
	{
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0001BAB0 File Offset: 0x00019CB0
	protected void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0001BAC0 File Offset: 0x00019CC0
	protected void OnValidate()
	{
		if (NGUITools.GetActive(this))
		{
			this.Upgrade();
			this.mIsDirty = true;
			float num = Mathf.Clamp01(this.mValue);
			if (this.mValue != num)
			{
				this.mValue = num;
			}
			if (this.numberOfSteps < 0)
			{
				this.numberOfSteps = 0;
			}
			else if (this.numberOfSteps > 21)
			{
				this.numberOfSteps = 21;
			}
			this.ForceUpdate();
			return;
		}
		float num2 = Mathf.Clamp01(this.mValue);
		if (this.mValue != num2)
		{
			this.mValue = num2;
		}
		if (this.numberOfSteps < 0)
		{
			this.numberOfSteps = 0;
			return;
		}
		if (this.numberOfSteps > 21)
		{
			this.numberOfSteps = 21;
		}
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0001BB68 File Offset: 0x00019D68
	protected float ScreenToValue(Vector2 screenPos)
	{
		Transform cachedTransform = this.cachedTransform;
		Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = this.cachedCamera.ScreenPointToRay(screenPos);
		float distance;
		if (!plane.Raycast(ray, out distance))
		{
			return this.value;
		}
		return this.LocalToValue(cachedTransform.InverseTransformPoint(ray.GetPoint(distance)));
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0001BBD8 File Offset: 0x00019DD8
	protected virtual float LocalToValue(Vector2 localPos)
	{
		if (!(this.mFG != null))
		{
			return this.value;
		}
		Vector3[] localCorners = this.mFG.localCorners;
		Vector3 vector = localCorners[2] - localCorners[0];
		if (this.isHorizontal)
		{
			float num = (localPos.x - localCorners[0].x) / vector.x;
			if (!this.isInverted)
			{
				return num;
			}
			return 1f - num;
		}
		else
		{
			float num2 = (localPos.y - localCorners[0].y) / vector.y;
			if (!this.isInverted)
			{
				return num2;
			}
			return 1f - num2;
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0001BC80 File Offset: 0x00019E80
	public virtual void ForceUpdate()
	{
		this.mIsDirty = false;
		bool flag = false;
		if (this.mFG != null)
		{
			UIBasicSprite uibasicSprite = this.mFG as UIBasicSprite;
			if (this.isHorizontal)
			{
				if (uibasicSprite != null && uibasicSprite.type == UIBasicSprite.Type.Filled)
				{
					if (uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
					{
						uibasicSprite.fillDirection = UIBasicSprite.FillDirection.Horizontal;
						uibasicSprite.invert = this.isInverted;
					}
					uibasicSprite.fillAmount = this.value;
				}
				else
				{
					this.mFG.drawRegion = (this.isInverted ? new Vector4(1f - this.value, 0f, 1f, 1f) : new Vector4(0f, 0f, this.value, 1f));
					this.mFG.enabled = true;
					flag = (this.value < 0.001f);
				}
			}
			else if (uibasicSprite != null && uibasicSprite.type == UIBasicSprite.Type.Filled)
			{
				if (uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Horizontal || uibasicSprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
				{
					uibasicSprite.fillDirection = UIBasicSprite.FillDirection.Vertical;
					uibasicSprite.invert = this.isInverted;
				}
				uibasicSprite.fillAmount = this.value;
			}
			else
			{
				this.mFG.drawRegion = (this.isInverted ? new Vector4(0f, 1f - this.value, 1f, 1f) : new Vector4(0f, 0f, 1f, this.value));
				this.mFG.enabled = true;
				flag = (this.value < 0.001f);
			}
		}
		if (this.thumb != null && (this.mFG != null || this.mBG != null))
		{
			Vector3[] array = (this.mFG != null) ? this.mFG.localCorners : this.mBG.localCorners;
			Vector4 vector = (this.mFG != null) ? this.mFG.border : this.mBG.border;
			Vector3[] array2 = array;
			int num = 0;
			array2[num].x = array2[num].x + vector.x;
			Vector3[] array3 = array;
			int num2 = 1;
			array3[num2].x = array3[num2].x + vector.x;
			Vector3[] array4 = array;
			int num3 = 2;
			array4[num3].x = array4[num3].x - vector.z;
			Vector3[] array5 = array;
			int num4 = 3;
			array5[num4].x = array5[num4].x - vector.z;
			Vector3[] array6 = array;
			int num5 = 0;
			array6[num5].y = array6[num5].y + vector.y;
			Vector3[] array7 = array;
			int num6 = 1;
			array7[num6].y = array7[num6].y - vector.w;
			Vector3[] array8 = array;
			int num7 = 2;
			array8[num7].y = array8[num7].y - vector.w;
			Vector3[] array9 = array;
			int num8 = 3;
			array9[num8].y = array9[num8].y + vector.y;
			Transform transform = (this.mFG != null) ? this.mFG.cachedTransform : this.mBG.cachedTransform;
			for (int i = 0; i < 4; i++)
			{
				array[i] = transform.TransformPoint(array[i]);
			}
			if (this.isHorizontal)
			{
				Vector3 a = Vector3.Lerp(array[0], array[1], 0.5f);
				Vector3 b = Vector3.Lerp(array[2], array[3], 0.5f);
				this.SetThumbPosition(Vector3.Lerp(a, b, this.isInverted ? (1f - this.value) : this.value));
			}
			else
			{
				Vector3 a2 = Vector3.Lerp(array[0], array[3], 0.5f);
				Vector3 b2 = Vector3.Lerp(array[1], array[2], 0.5f);
				this.SetThumbPosition(Vector3.Lerp(a2, b2, this.isInverted ? (1f - this.value) : this.value));
			}
		}
		if (flag)
		{
			this.mFG.enabled = false;
		}
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0001C074 File Offset: 0x0001A274
	protected void SetThumbPosition(Vector3 worldPos)
	{
		Transform parent = this.thumb.parent;
		if (parent != null)
		{
			worldPos = parent.InverseTransformPoint(worldPos);
			worldPos.x = Mathf.Round(worldPos.x);
			worldPos.y = Mathf.Round(worldPos.y);
			worldPos.z = 0f;
			if (Vector3.Distance(this.thumb.localPosition, worldPos) > 0.001f)
			{
				this.thumb.localPosition = worldPos;
				return;
			}
		}
		else if (Vector3.Distance(this.thumb.position, worldPos) > 1E-05f)
		{
			this.thumb.position = worldPos;
		}
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0001C118 File Offset: 0x0001A318
	public virtual void OnPan(Vector2 delta)
	{
		if (base.enabled)
		{
			switch (this.mFill)
			{
			case UIProgressBar.FillDirection.LeftToRight:
			{
				float value = Mathf.Clamp01(this.mValue + delta.x);
				this.value = value;
				this.mValue = value;
				return;
			}
			case UIProgressBar.FillDirection.RightToLeft:
			{
				float value2 = Mathf.Clamp01(this.mValue - delta.x);
				this.value = value2;
				this.mValue = value2;
				return;
			}
			case UIProgressBar.FillDirection.BottomToTop:
			{
				float value3 = Mathf.Clamp01(this.mValue + delta.y);
				this.value = value3;
				this.mValue = value3;
				return;
			}
			case UIProgressBar.FillDirection.TopToBottom:
			{
				float value4 = Mathf.Clamp01(this.mValue - delta.y);
				this.value = value4;
				this.mValue = value4;
				break;
			}
			default:
				return;
			}
		}
	}

	// Token: 0x04000415 RID: 1045
	public static UIProgressBar current;

	// Token: 0x04000416 RID: 1046
	public UIProgressBar.OnDragFinished onDragFinished;

	// Token: 0x04000417 RID: 1047
	public Transform thumb;

	// Token: 0x04000418 RID: 1048
	[HideInInspector]
	[SerializeField]
	protected UIWidget mBG;

	// Token: 0x04000419 RID: 1049
	[HideInInspector]
	[SerializeField]
	protected UIWidget mFG;

	// Token: 0x0400041A RID: 1050
	[HideInInspector]
	[SerializeField]
	protected float mValue = 1f;

	// Token: 0x0400041B RID: 1051
	[HideInInspector]
	[SerializeField]
	protected UIProgressBar.FillDirection mFill;

	// Token: 0x0400041C RID: 1052
	[NonSerialized]
	protected bool mStarted;

	// Token: 0x0400041D RID: 1053
	[NonSerialized]
	protected Transform mTrans;

	// Token: 0x0400041E RID: 1054
	[NonSerialized]
	protected bool mIsDirty;

	// Token: 0x0400041F RID: 1055
	[NonSerialized]
	protected Camera mCam;

	// Token: 0x04000420 RID: 1056
	[NonSerialized]
	protected float mOffset;

	// Token: 0x04000421 RID: 1057
	public int numberOfSteps;

	// Token: 0x04000422 RID: 1058
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x02000622 RID: 1570
	[DoNotObfuscateNGUI]
	public enum FillDirection
	{
		// Token: 0x040044C9 RID: 17609
		LeftToRight,
		// Token: 0x040044CA RID: 17610
		RightToLeft,
		// Token: 0x040044CB RID: 17611
		BottomToTop,
		// Token: 0x040044CC RID: 17612
		TopToBottom
	}

	// Token: 0x02000623 RID: 1571
	// (Invoke) Token: 0x06002A6F RID: 10863
	public delegate void OnDragFinished();
}
