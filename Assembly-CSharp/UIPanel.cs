using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A7 RID: 167
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Panel")]
public class UIPanel : UIRect
{
	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06000840 RID: 2112 RVA: 0x0004284B File Offset: 0x00040A4B
	// (set) Token: 0x06000841 RID: 2113 RVA: 0x00042853 File Offset: 0x00040A53
	public string sortingLayerName
	{
		get
		{
			return this.mSortingLayerName;
		}
		set
		{
			if (this.mSortingLayerName != value)
			{
				this.mSortingLayerName = value;
				this.UpdateDrawCalls(UIPanel.list.IndexOf(this));
			}
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06000842 RID: 2114 RVA: 0x0004287C File Offset: 0x00040A7C
	public static int nextUnusedDepth
	{
		get
		{
			int num = int.MinValue;
			int i = 0;
			int count = UIPanel.list.Count;
			while (i < count)
			{
				num = Mathf.Max(num, UIPanel.list[i].depth);
				i++;
			}
			if (num != -2147483648)
			{
				return num + 1;
			}
			return 0;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06000843 RID: 2115 RVA: 0x000428CA File Offset: 0x00040ACA
	public override bool canBeAnchored
	{
		get
		{
			return this.mClipping > UIDrawCall.Clipping.None;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06000844 RID: 2116 RVA: 0x000428D5 File Offset: 0x00040AD5
	// (set) Token: 0x06000845 RID: 2117 RVA: 0x000428E0 File Offset: 0x00040AE0
	public override float alpha
	{
		get
		{
			return this.mAlpha;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mAlpha != num)
			{
				bool flag = this.mAlpha > 0.001f;
				this.mAlphaFrameID = -1;
				this.mResized = true;
				this.mAlpha = num;
				int i = 0;
				int count = this.drawCalls.Count;
				while (i < count)
				{
					this.drawCalls[i].isDirty = true;
					i++;
				}
				this.Invalidate(!flag && this.mAlpha > 0.001f);
			}
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000846 RID: 2118 RVA: 0x00042963 File Offset: 0x00040B63
	// (set) Token: 0x06000847 RID: 2119 RVA: 0x0004296B File Offset: 0x00040B6B
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				UIPanel.list.Sort(new Comparison<UIPanel>(UIPanel.CompareFunc));
			}
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000848 RID: 2120 RVA: 0x00042993 File Offset: 0x00040B93
	// (set) Token: 0x06000849 RID: 2121 RVA: 0x0004299B File Offset: 0x00040B9B
	public int sortingOrder
	{
		get
		{
			return this.mSortingOrder;
		}
		set
		{
			if (this.mSortingOrder != value)
			{
				this.mSortingOrder = value;
				this.UpdateDrawCalls(UIPanel.list.IndexOf(this));
			}
		}
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x000429C0 File Offset: 0x00040BC0
	public static int CompareFunc(UIPanel a, UIPanel b)
	{
		if (!(a != b) || !(a != null) || !(b != null))
		{
			return 0;
		}
		if (a.mDepth < b.mDepth)
		{
			return -1;
		}
		if (a.mDepth > b.mDepth)
		{
			return 1;
		}
		if (a.GetInstanceID() >= b.GetInstanceID())
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x0600084B RID: 2123 RVA: 0x00042A1B File Offset: 0x00040C1B
	public float width
	{
		get
		{
			return this.GetViewSize().x;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x0600084C RID: 2124 RVA: 0x00042A28 File Offset: 0x00040C28
	public float height
	{
		get
		{
			return this.GetViewSize().y;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x0600084D RID: 2125 RVA: 0x0002D171 File Offset: 0x0002B371
	public bool halfPixelOffset
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x0600084E RID: 2126 RVA: 0x00042A35 File Offset: 0x00040C35
	public bool usedForUI
	{
		get
		{
			return base.anchorCamera != null && this.mCam.orthographic;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x0600084F RID: 2127 RVA: 0x00042A54 File Offset: 0x00040C54
	public Vector3 drawCallOffset
	{
		get
		{
			if (base.anchorCamera != null && this.mCam.orthographic)
			{
				Vector2 windowSize = this.GetWindowSize();
				float num = ((base.root != null) ? base.root.pixelSizeAdjustment : 1f) / windowSize.y / this.mCam.orthographicSize;
				bool flag = false;
				bool flag2 = false;
				if ((Mathf.RoundToInt(windowSize.x) & 1) == 1)
				{
					flag = !flag;
				}
				if ((Mathf.RoundToInt(windowSize.y) & 1) == 1)
				{
					flag2 = !flag2;
				}
				return new Vector3(flag ? (-num) : 0f, flag2 ? num : 0f);
			}
			return Vector3.zero;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000850 RID: 2128 RVA: 0x00042B0D File Offset: 0x00040D0D
	// (set) Token: 0x06000851 RID: 2129 RVA: 0x00042B15 File Offset: 0x00040D15
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mResized = true;
				this.mClipping = value;
				this.mMatrixFrame = -1;
			}
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000852 RID: 2130 RVA: 0x00042B35 File Offset: 0x00040D35
	public UIPanel parentPanel
	{
		get
		{
			return this.mParentPanel;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06000853 RID: 2131 RVA: 0x00042B40 File Offset: 0x00040D40
	public int clipCount
	{
		get
		{
			int num = 0;
			UIPanel uipanel = this;
			while (uipanel != null)
			{
				if (uipanel.mClipping == UIDrawCall.Clipping.SoftClip || uipanel.mClipping == UIDrawCall.Clipping.TextureMask)
				{
					num++;
				}
				uipanel = uipanel.mParentPanel;
			}
			return num;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06000854 RID: 2132 RVA: 0x00042B7A File Offset: 0x00040D7A
	public bool hasClipping
	{
		get
		{
			return this.mClipping == UIDrawCall.Clipping.SoftClip || this.mClipping == UIDrawCall.Clipping.TextureMask;
		}
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06000855 RID: 2133 RVA: 0x00042B90 File Offset: 0x00040D90
	public bool hasCumulativeClipping
	{
		get
		{
			return this.clipCount != 0;
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000856 RID: 2134 RVA: 0x00042B9B File Offset: 0x00040D9B
	[Obsolete("Use 'hasClipping' or 'hasCumulativeClipping' instead")]
	public bool clipsChildren
	{
		get
		{
			return this.hasCumulativeClipping;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06000857 RID: 2135 RVA: 0x00042BA3 File Offset: 0x00040DA3
	// (set) Token: 0x06000858 RID: 2136 RVA: 0x00042BAC File Offset: 0x00040DAC
	public Vector2 clipOffset
	{
		get
		{
			return this.mClipOffset;
		}
		set
		{
			if (Mathf.Abs(this.mClipOffset.x - value.x) > 0.001f || Mathf.Abs(this.mClipOffset.y - value.y) > 0.001f)
			{
				this.mClipOffset = value;
				this.InvalidateClipping();
				if (this.onClipMove != null)
				{
					this.onClipMove(this);
				}
			}
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00042C18 File Offset: 0x00040E18
	private void InvalidateClipping()
	{
		this.mResized = true;
		this.mMatrixFrame = -1;
		int i = 0;
		int count = UIPanel.list.Count;
		while (i < count)
		{
			UIPanel uipanel = UIPanel.list[i];
			if (uipanel != this && uipanel.parentPanel == this)
			{
				uipanel.InvalidateClipping();
			}
			i++;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x0600085A RID: 2138 RVA: 0x00042C73 File Offset: 0x00040E73
	// (set) Token: 0x0600085B RID: 2139 RVA: 0x00042C7B File Offset: 0x00040E7B
	public Texture2D clipTexture
	{
		get
		{
			return this.mClipTexture;
		}
		set
		{
			if (this.mClipTexture != value)
			{
				this.mClipTexture = value;
			}
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x0600085C RID: 2140 RVA: 0x00042C92 File Offset: 0x00040E92
	// (set) Token: 0x0600085D RID: 2141 RVA: 0x00042C9A File Offset: 0x00040E9A
	[Obsolete("Use 'finalClipRegion' or 'baseClipRegion' instead")]
	public Vector4 clipRange
	{
		get
		{
			return this.baseClipRegion;
		}
		set
		{
			this.baseClipRegion = value;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x0600085E RID: 2142 RVA: 0x00042CA3 File Offset: 0x00040EA3
	// (set) Token: 0x0600085F RID: 2143 RVA: 0x00042CAC File Offset: 0x00040EAC
	public Vector4 baseClipRegion
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (Mathf.Abs(this.mClipRange.x - value.x) > 0.001f || Mathf.Abs(this.mClipRange.y - value.y) > 0.001f || Mathf.Abs(this.mClipRange.z - value.z) > 0.001f || Mathf.Abs(this.mClipRange.w - value.w) > 0.001f)
			{
				this.mResized = true;
				this.mClipRange = value;
				this.mMatrixFrame = -1;
				UIScrollView component = base.GetComponent<UIScrollView>();
				if (component != null)
				{
					component.UpdatePosition();
				}
				if (this.onClipMove != null)
				{
					this.onClipMove(this);
				}
			}
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000860 RID: 2144 RVA: 0x00042D70 File Offset: 0x00040F70
	public Vector4 finalClipRegion
	{
		get
		{
			Vector2 viewSize = this.GetViewSize();
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				return new Vector4(this.mClipRange.x + this.mClipOffset.x, this.mClipRange.y + this.mClipOffset.y, viewSize.x, viewSize.y);
			}
			Vector4 result = new Vector4(0f, 0f, viewSize.x, viewSize.y);
			Vector3 vector = base.anchorCamera.WorldToScreenPoint(base.cachedTransform.position);
			vector.x -= viewSize.x * 0.5f;
			vector.y -= viewSize.y * 0.5f;
			result.x -= vector.x;
			result.y -= vector.y;
			return result;
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06000861 RID: 2145 RVA: 0x00042E51 File Offset: 0x00041051
	// (set) Token: 0x06000862 RID: 2146 RVA: 0x00042E59 File Offset: 0x00041059
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
			}
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06000863 RID: 2147 RVA: 0x00042E70 File Offset: 0x00041070
	public override Vector3[] localCorners
	{
		get
		{
			if (this.mClipping == UIDrawCall.Clipping.None)
			{
				Vector3[] worldCorners = this.worldCorners;
				Transform cachedTransform = base.cachedTransform;
				for (int i = 0; i < 4; i++)
				{
					worldCorners[i] = cachedTransform.InverseTransformPoint(worldCorners[i]);
				}
				return worldCorners;
			}
			float num = this.mClipOffset.x + this.mClipRange.x - 0.5f * this.mClipRange.z;
			float num2 = this.mClipOffset.y + this.mClipRange.y - 0.5f * this.mClipRange.w;
			float x = num + this.mClipRange.z;
			float y = num2 + this.mClipRange.w;
			UIPanel.mCorners[0] = new Vector3(num, num2);
			UIPanel.mCorners[1] = new Vector3(num, y);
			UIPanel.mCorners[2] = new Vector3(x, y);
			UIPanel.mCorners[3] = new Vector3(x, num2);
			return UIPanel.mCorners;
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000864 RID: 2148 RVA: 0x00042F7C File Offset: 0x0004117C
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				float num = this.mClipOffset.x + this.mClipRange.x - 0.5f * this.mClipRange.z;
				float num2 = this.mClipOffset.y + this.mClipRange.y - 0.5f * this.mClipRange.w;
				float x = num + this.mClipRange.z;
				float y = num2 + this.mClipRange.w;
				Transform cachedTransform = base.cachedTransform;
				UIPanel.mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
				UIPanel.mCorners[1] = cachedTransform.TransformPoint(num, y, 0f);
				UIPanel.mCorners[2] = cachedTransform.TransformPoint(x, y, 0f);
				UIPanel.mCorners[3] = cachedTransform.TransformPoint(x, num2, 0f);
			}
			else
			{
				if (base.anchorCamera != null)
				{
					return this.mCam.GetWorldCorners(base.cameraRayDistance);
				}
				Vector2 viewSize = this.GetViewSize();
				float num3 = -0.5f * viewSize.x;
				float num4 = -0.5f * viewSize.y;
				float x2 = num3 + viewSize.x;
				float y2 = num4 + viewSize.y;
				UIPanel.mCorners[0] = new Vector3(num3, num4);
				UIPanel.mCorners[1] = new Vector3(num3, y2);
				UIPanel.mCorners[2] = new Vector3(x2, y2);
				UIPanel.mCorners[3] = new Vector3(x2, num4);
				if (this.anchorOffset && (this.mCam == null || this.mCam.transform.parent != base.cachedTransform))
				{
					Vector3 position = base.cachedTransform.position;
					for (int i = 0; i < 4; i++)
					{
						UIPanel.mCorners[i] += position;
					}
				}
			}
			return UIPanel.mCorners;
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00043198 File Offset: 0x00041398
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			float num = this.mClipOffset.x + this.mClipRange.x - 0.5f * this.mClipRange.z;
			float num2 = this.mClipOffset.y + this.mClipRange.y - 0.5f * this.mClipRange.w;
			float num3 = num + this.mClipRange.z;
			float num4 = num2 + this.mClipRange.w;
			float x = (num + num3) * 0.5f;
			float y = (num2 + num4) * 0.5f;
			Transform cachedTransform = base.cachedTransform;
			UIRect.mSides[0] = cachedTransform.TransformPoint(num, y, 0f);
			UIRect.mSides[1] = cachedTransform.TransformPoint(x, num4, 0f);
			UIRect.mSides[2] = cachedTransform.TransformPoint(num3, y, 0f);
			UIRect.mSides[3] = cachedTransform.TransformPoint(x, num2, 0f);
			if (relativeTo != null)
			{
				for (int i = 0; i < 4; i++)
				{
					UIRect.mSides[i] = relativeTo.InverseTransformPoint(UIRect.mSides[i]);
				}
			}
			return UIRect.mSides;
		}
		if (base.anchorCamera != null && this.anchorOffset)
		{
			Vector3[] sides = this.mCam.GetSides(base.cameraRayDistance);
			Vector3 position = base.cachedTransform.position;
			for (int j = 0; j < 4; j++)
			{
				sides[j] += position;
			}
			if (relativeTo != null)
			{
				for (int k = 0; k < 4; k++)
				{
					sides[k] = relativeTo.InverseTransformPoint(sides[k]);
				}
			}
			return sides;
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x00043382 File Offset: 0x00041582
	public override void Invalidate(bool includeChildren)
	{
		this.mAlphaFrameID = -1;
		base.Invalidate(includeChildren);
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00043394 File Offset: 0x00041594
	public override float CalculateFinalAlpha(int frameID)
	{
		if (this.mAlphaFrameID != frameID)
		{
			this.mAlphaFrameID = frameID;
			UIRect parent = base.parent;
			this.finalAlpha = ((base.parent != null) ? (parent.CalculateFinalAlpha(frameID) * this.mAlpha) : this.mAlpha);
		}
		return this.finalAlpha;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000433E8 File Offset: 0x000415E8
	public override void SetRect(float x, float y, float width, float height)
	{
		int num = Mathf.FloorToInt(width + 0.5f);
		int num2 = Mathf.FloorToInt(height + 0.5f);
		num = num >> 1 << 1;
		num2 = num2 >> 1 << 1;
		Transform transform = base.cachedTransform;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(x + 0.5f);
		localPosition.y = Mathf.Floor(y + 0.5f);
		if (num < 2)
		{
			num = 2;
		}
		if (num2 < 2)
		{
			num2 = 2;
		}
		this.baseClipRegion = new Vector4(localPosition.x, localPosition.y, (float)num, (float)num2);
		if (base.isAnchored)
		{
			transform = transform.parent;
			if (this.leftAnchor.target)
			{
				this.leftAnchor.SetHorizontal(transform, x);
			}
			if (this.rightAnchor.target)
			{
				this.rightAnchor.SetHorizontal(transform, x + width);
			}
			if (this.bottomAnchor.target)
			{
				this.bottomAnchor.SetVertical(transform, y);
			}
			if (this.topAnchor.target)
			{
				this.topAnchor.SetVertical(transform, y + height);
			}
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0004350C File Offset: 0x0004170C
	public bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.worldToLocal.MultiplyPoint3x4(a);
		b = this.worldToLocal.MultiplyPoint3x4(b);
		c = this.worldToLocal.MultiplyPoint3x4(c);
		d = this.worldToLocal.MultiplyPoint3x4(d);
		UIPanel.mTemp[0] = a.x;
		UIPanel.mTemp[1] = b.x;
		UIPanel.mTemp[2] = c.x;
		UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(UIPanel.mTemp);
		float num2 = Mathf.Max(UIPanel.mTemp);
		UIPanel.mTemp[0] = a.y;
		UIPanel.mTemp[1] = b.y;
		UIPanel.mTemp[2] = c.y;
		UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(UIPanel.mTemp);
		float num4 = Mathf.Max(UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00043630 File Offset: 0x00041830
	public bool IsVisible(Vector3 worldPos)
	{
		if (this.mAlpha < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip)
		{
			return true;
		}
		this.UpdateTransformMatrix();
		Vector3 vector = this.worldToLocal.MultiplyPoint3x4(worldPos);
		return vector.x >= this.mMin.x && vector.y >= this.mMin.y && vector.x <= this.mMax.x && vector.y <= this.mMax.y;
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x000436C8 File Offset: 0x000418C8
	public bool IsVisible(UIWidget w)
	{
		UIPanel uipanel = this;
		Vector3[] array = null;
		while (uipanel != null)
		{
			if ((uipanel.mClipping == UIDrawCall.Clipping.None || uipanel.mClipping == UIDrawCall.Clipping.ConstrainButDontClip) && !w.hideIfOffScreen)
			{
				uipanel = uipanel.mParentPanel;
			}
			else
			{
				if (array == null)
				{
					array = w.worldCorners;
				}
				if (!uipanel.IsVisible(array[0], array[1], array[2], array[3]))
				{
					return false;
				}
				uipanel = uipanel.mParentPanel;
			}
		}
		return true;
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00043740 File Offset: 0x00041940
	public bool Affects(UIWidget w)
	{
		if (w == null)
		{
			return false;
		}
		UIPanel panel = w.panel;
		if (panel == null)
		{
			return false;
		}
		UIPanel uipanel = this;
		while (uipanel != null)
		{
			if (uipanel == panel)
			{
				return true;
			}
			if (!uipanel.hasCumulativeClipping)
			{
				return false;
			}
			uipanel = uipanel.mParentPanel;
		}
		return false;
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00043794 File Offset: 0x00041994
	[ContextMenu("Force Refresh")]
	public void RebuildAllDrawCalls()
	{
		this.mRebuild = true;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x000437A0 File Offset: 0x000419A0
	public void SetDirty()
	{
		int i = 0;
		int count = this.drawCalls.Count;
		while (i < count)
		{
			this.drawCalls[i].isDirty = true;
			i++;
		}
		this.Invalidate(true);
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x000437DE File Offset: 0x000419DE
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x000437E8 File Offset: 0x000419E8
	private void FindParent()
	{
		Transform parent = base.cachedTransform.parent;
		this.mParentPanel = ((parent != null) ? NGUITools.FindInParents<UIPanel>(parent.gameObject) : null);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0004381E File Offset: 0x00041A1E
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		this.FindParent();
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0004382C File Offset: 0x00041A2C
	protected override void OnStart()
	{
		this.mLayer = base.cachedGameObject.layer;
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0004383F File Offset: 0x00041A3F
	protected override void OnEnable()
	{
		this.mRebuild = true;
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		this.OnStart();
		base.OnEnable();
		this.mMatrixFrame = -1;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0004386C File Offset: 0x00041A6C
	protected override void OnInit()
	{
		if (UIPanel.list.Contains(this))
		{
			return;
		}
		base.OnInit();
		this.FindParent();
		if (base.GetComponent<Rigidbody>() == null && this.mParentPanel == null)
		{
			UICamera uicamera = (base.anchorCamera != null) ? this.mCam.GetComponent<UICamera>() : null;
			if (uicamera != null && (uicamera.eventType == UICamera.EventType.UI_3D || uicamera.eventType == UICamera.EventType.World_3D))
			{
				Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
				rigidbody.isKinematic = true;
				rigidbody.useGravity = false;
			}
		}
		this.mRebuild = true;
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		UIPanel.list.Add(this);
		UIPanel.list.Sort(new Comparison<UIPanel>(UIPanel.CompareFunc));
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00043934 File Offset: 0x00041B34
	protected override void OnDisable()
	{
		int i = 0;
		int count = this.drawCalls.Count;
		while (i < count)
		{
			UIDrawCall uidrawCall = this.drawCalls[i];
			if (uidrawCall != null)
			{
				UIDrawCall.Destroy(uidrawCall);
			}
			i++;
		}
		this.drawCalls.Clear();
		UIPanel.list.Remove(this);
		this.mAlphaFrameID = -1;
		this.mMatrixFrame = -1;
		if (UIPanel.list.Count == 0)
		{
			UIDrawCall.ReleaseAll();
			UIPanel.mUpdateFrame = -1;
		}
		base.OnDisable();
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x000439B8 File Offset: 0x00041BB8
	private void UpdateTransformMatrix()
	{
		int frameCount = Time.frameCount;
		if (this.mHasMoved || this.mMatrixFrame != frameCount)
		{
			this.mMatrixFrame = frameCount;
			this.worldToLocal = base.cachedTransform.worldToLocalMatrix;
			Vector2 vector = this.GetViewSize() * 0.5f;
			float num = this.mClipOffset.x + this.mClipRange.x;
			float num2 = this.mClipOffset.y + this.mClipRange.y;
			this.mMin.x = num - vector.x;
			this.mMin.y = num2 - vector.y;
			this.mMax.x = num + vector.x;
			this.mMax.y = num2 + vector.y;
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x00043A84 File Offset: 0x00041C84
	protected override void OnAnchor()
	{
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return;
		}
		Transform cachedTransform = base.cachedTransform;
		Transform parent = cachedTransform.parent;
		Vector2 viewSize = this.GetViewSize();
		Vector2 vector = cachedTransform.localPosition;
		float num;
		float num2;
		float num3;
		float num4;
		if (this.leftAnchor.target == this.bottomAnchor.target && this.leftAnchor.target == this.rightAnchor.target && this.leftAnchor.target == this.topAnchor.target)
		{
			Vector3[] sides = this.leftAnchor.GetSides(parent);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
			}
			else
			{
				Vector2 vector2 = base.GetLocalPos(this.leftAnchor, parent);
				num = vector2.x + (float)this.leftAnchor.absolute;
				num3 = vector2.y + (float)this.bottomAnchor.absolute;
				num2 = vector2.x + (float)this.rightAnchor.absolute;
				num4 = vector2.y + (float)this.topAnchor.absolute;
			}
		}
		else
		{
			if (this.leftAnchor.target)
			{
				Vector3[] sides2 = this.leftAnchor.GetSides(parent);
				if (sides2 != null)
				{
					num = NGUIMath.Lerp(sides2[0].x, sides2[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				}
				else
				{
					num = base.GetLocalPos(this.leftAnchor, parent).x + (float)this.leftAnchor.absolute;
				}
			}
			else
			{
				num = this.mClipRange.x - 0.5f * viewSize.x;
			}
			if (this.rightAnchor.target)
			{
				Vector3[] sides3 = this.rightAnchor.GetSides(parent);
				if (sides3 != null)
				{
					num2 = NGUIMath.Lerp(sides3[0].x, sides3[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				}
				else
				{
					num2 = base.GetLocalPos(this.rightAnchor, parent).x + (float)this.rightAnchor.absolute;
				}
			}
			else
			{
				num2 = this.mClipRange.x + 0.5f * viewSize.x;
			}
			if (this.bottomAnchor.target)
			{
				Vector3[] sides4 = this.bottomAnchor.GetSides(parent);
				if (sides4 != null)
				{
					num3 = NGUIMath.Lerp(sides4[3].y, sides4[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				}
				else
				{
					num3 = base.GetLocalPos(this.bottomAnchor, parent).y + (float)this.bottomAnchor.absolute;
				}
			}
			else
			{
				num3 = this.mClipRange.y - 0.5f * viewSize.y;
			}
			if (this.topAnchor.target)
			{
				Vector3[] sides5 = this.topAnchor.GetSides(parent);
				if (sides5 != null)
				{
					num4 = NGUIMath.Lerp(sides5[3].y, sides5[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
				}
				else
				{
					num4 = base.GetLocalPos(this.topAnchor, parent).y + (float)this.topAnchor.absolute;
				}
			}
			else
			{
				num4 = this.mClipRange.y + 0.5f * viewSize.y;
			}
		}
		num -= vector.x + this.mClipOffset.x;
		num2 -= vector.x + this.mClipOffset.x;
		num3 -= vector.y + this.mClipOffset.y;
		num4 -= vector.y + this.mClipOffset.y;
		float x = Mathf.Lerp(num, num2, 0.5f);
		float y = Mathf.Lerp(num3, num4, 0.5f);
		float num5 = num2 - num;
		float num6 = num4 - num3;
		float num7 = Mathf.Max(2f, this.mClipSoftness.x);
		float num8 = Mathf.Max(2f, this.mClipSoftness.y);
		if (num5 < num7)
		{
			num5 = num7;
		}
		if (num6 < num8)
		{
			num6 = num8;
		}
		this.baseClipRegion = new Vector4(x, y, num5, num6);
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00043FC8 File Offset: 0x000421C8
	private void LateUpdate()
	{
		if (UIPanel.mUpdateFrame != Time.frameCount)
		{
			UIPanel.mUpdateFrame = Time.frameCount;
			int i = 0;
			int count = UIPanel.list.Count;
			while (i < count)
			{
				UIPanel.list[i].UpdateSelf();
				i++;
			}
			int num = 3000;
			int j = 0;
			int count2 = UIPanel.list.Count;
			while (j < count2)
			{
				UIPanel uipanel = UIPanel.list[j];
				if (uipanel.renderQueue == UIPanel.RenderQueue.Automatic)
				{
					uipanel.startingRenderQueue = num;
					uipanel.UpdateDrawCalls(j);
					num += uipanel.drawCalls.Count;
				}
				else if (uipanel.renderQueue == UIPanel.RenderQueue.StartAt)
				{
					uipanel.UpdateDrawCalls(j);
					if (uipanel.drawCalls.Count != 0)
					{
						num = Mathf.Max(num, uipanel.startingRenderQueue + uipanel.drawCalls.Count);
					}
				}
				else
				{
					uipanel.UpdateDrawCalls(j);
					if (uipanel.drawCalls.Count != 0)
					{
						num = Mathf.Max(num, uipanel.startingRenderQueue + 1);
					}
				}
				j++;
			}
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000440D4 File Offset: 0x000422D4
	private void UpdateSelf()
	{
		this.mHasMoved = base.cachedTransform.hasChanged;
		this.UpdateTransformMatrix();
		this.UpdateLayers();
		this.UpdateWidgets();
		if (this.mRebuild)
		{
			this.mRebuild = false;
			this.FillAllDrawCalls();
		}
		else
		{
			bool needsCulling = this.mCam == null || this.mCam.useOcclusionCulling;
			int i = 0;
			while (i < this.drawCalls.Count)
			{
				UIDrawCall uidrawCall = this.drawCalls[i];
				if (uidrawCall.isDirty && !this.FillDrawCall(uidrawCall, needsCulling))
				{
					UIDrawCall.Destroy(uidrawCall);
					this.drawCalls.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}
		if (this.mUpdateScroll)
		{
			this.mUpdateScroll = false;
			UIScrollView component = base.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars();
			}
		}
		if (this.mHasMoved)
		{
			this.mHasMoved = false;
			this.mTrans.hasChanged = false;
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000441C1 File Offset: 0x000423C1
	public void SortWidgets()
	{
		this.mSortWidgets = false;
		this.widgets.Sort(new Comparison<UIWidget>(UIWidget.PanelCompareFunc));
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x000441E4 File Offset: 0x000423E4
	private void FillAllDrawCalls()
	{
		for (int i = 0; i < this.drawCalls.Count; i++)
		{
			UIDrawCall.Destroy(this.drawCalls[i]);
		}
		this.drawCalls.Clear();
		Material material = null;
		Texture texture = null;
		Shader shader = null;
		UIDrawCall uidrawCall = null;
		int num = 0;
		bool needsBounds = this.mCam == null || this.mCam.useOcclusionCulling;
		if (this.mSortWidgets)
		{
			this.SortWidgets();
		}
		for (int j = 0; j < this.widgets.Count; j++)
		{
			UIWidget uiwidget = this.widgets[j];
			if (uiwidget.isVisible && uiwidget.hasVertices)
			{
				Material material2 = uiwidget.material;
				if (this.onCreateMaterial != null)
				{
					material2 = this.onCreateMaterial(uiwidget, material2);
				}
				Texture mainTexture = uiwidget.mainTexture;
				Shader shader2 = uiwidget.shader;
				if (material != material2 || texture != mainTexture || shader != shader2)
				{
					if (uidrawCall != null && uidrawCall.verts.Count != 0)
					{
						this.drawCalls.Add(uidrawCall);
						uidrawCall.UpdateGeometry(num, needsBounds);
						uidrawCall.onRender = this.mOnRender;
						this.mOnRender = null;
						num = 0;
						uidrawCall = null;
					}
					material = material2;
					texture = mainTexture;
					shader = shader2;
				}
				if (material != null || shader != null || texture != null)
				{
					if (uidrawCall == null)
					{
						if (true)
						{
							uidrawCall = UIDrawCall.Create(this, material, texture, shader);
							uidrawCall.depthStart = uiwidget.depth;
							uidrawCall.depthEnd = uidrawCall.depthStart;
							uidrawCall.panel = this;
							uidrawCall.onCreateDrawCall = this.onCreateDrawCall;
						}
					}
					else
					{
						int depth = uiwidget.depth;
						if (depth < uidrawCall.depthStart)
						{
							uidrawCall.depthStart = depth;
						}
						if (depth > uidrawCall.depthEnd)
						{
							uidrawCall.depthEnd = depth;
						}
					}
					uiwidget.drawCall = uidrawCall;
					if (uidrawCall != null)
					{
						num++;
						if (this.generateNormals)
						{
							uiwidget.WriteToBuffers(uidrawCall.verts, uidrawCall.uvs, uidrawCall.cols, uidrawCall.norms, uidrawCall.tans, this.generateUV2 ? uidrawCall.uv2 : null);
						}
						else
						{
							uiwidget.WriteToBuffers(uidrawCall.verts, uidrawCall.uvs, uidrawCall.cols, null, null, this.generateUV2 ? uidrawCall.uv2 : null);
						}
						if (uiwidget.mOnRender != null)
						{
							if (this.mOnRender == null)
							{
								this.mOnRender = uiwidget.mOnRender;
							}
							else
							{
								this.mOnRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(this.mOnRender, uiwidget.mOnRender);
							}
						}
					}
				}
			}
			else
			{
				uiwidget.drawCall = null;
			}
		}
		if (uidrawCall != null && uidrawCall.verts.Count != 0)
		{
			this.drawCalls.Add(uidrawCall);
			uidrawCall.UpdateGeometry(num, needsBounds);
			uidrawCall.onRender = this.mOnRender;
			this.mOnRender = null;
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x000444E0 File Offset: 0x000426E0
	public bool FillDrawCall(UIDrawCall dc)
	{
		bool needsCulling = this.mCam == null || this.mCam.useOcclusionCulling;
		return this.FillDrawCall(dc, needsCulling);
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00044514 File Offset: 0x00042714
	public bool FillDrawCall(UIDrawCall dc, bool needsCulling)
	{
		if (dc != null)
		{
			dc.isDirty = false;
			int num = 0;
			int i = 0;
			while (i < this.widgets.Count)
			{
				UIWidget uiwidget = this.widgets[i];
				if (uiwidget == null)
				{
					this.widgets.RemoveAt(i);
				}
				else
				{
					if (uiwidget.drawCall == dc)
					{
						if (uiwidget.isVisible && uiwidget.hasVertices)
						{
							num++;
							if (this.generateNormals)
							{
								uiwidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, dc.norms, dc.tans, this.generateUV2 ? dc.uv2 : null);
							}
							else
							{
								uiwidget.WriteToBuffers(dc.verts, dc.uvs, dc.cols, null, null, this.generateUV2 ? dc.uv2 : null);
							}
							if (uiwidget.mOnRender != null)
							{
								if (this.mOnRender == null)
								{
									this.mOnRender = uiwidget.mOnRender;
								}
								else
								{
									this.mOnRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(this.mOnRender, uiwidget.mOnRender);
								}
							}
						}
						else
						{
							uiwidget.drawCall = null;
						}
					}
					i++;
				}
			}
			if (dc.verts.Count != 0)
			{
				dc.UpdateGeometry(num, needsCulling);
				dc.onRender = this.mOnRender;
				this.mOnRender = null;
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0004467C File Offset: 0x0004287C
	private void UpdateDrawCalls(int sortOrder)
	{
		Transform cachedTransform = base.cachedTransform;
		bool usedForUI = this.usedForUI;
		if (this.clipping != UIDrawCall.Clipping.None)
		{
			this.drawCallClipRange = this.finalClipRegion;
			this.drawCallClipRange.z = this.drawCallClipRange.z * 0.5f;
			this.drawCallClipRange.w = this.drawCallClipRange.w * 0.5f;
		}
		else
		{
			this.drawCallClipRange = Vector4.zero;
		}
		int width = Screen.width;
		int height = Screen.height;
		if (this.drawCallClipRange.z == 0f)
		{
			this.drawCallClipRange.z = (float)width * 0.5f;
		}
		if (this.drawCallClipRange.w == 0f)
		{
			this.drawCallClipRange.w = (float)height * 0.5f;
		}
		if (this.halfPixelOffset)
		{
			this.drawCallClipRange.x = this.drawCallClipRange.x - 0.5f;
			this.drawCallClipRange.y = this.drawCallClipRange.y + 0.5f;
		}
		Vector3 vector;
		if (usedForUI)
		{
			Transform parent = base.cachedTransform.parent;
			vector = base.cachedTransform.localPosition;
			if (this.clipping != UIDrawCall.Clipping.None)
			{
				vector.x = (float)Mathf.RoundToInt(vector.x);
				vector.y = (float)Mathf.RoundToInt(vector.y);
			}
			if (parent != null)
			{
				vector = parent.TransformPoint(vector);
			}
			vector += this.drawCallOffset;
		}
		else
		{
			vector = cachedTransform.position;
		}
		Quaternion rotation = cachedTransform.rotation;
		Vector3 lossyScale = cachedTransform.lossyScale;
		for (int i = 0; i < this.drawCalls.Count; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls[i];
			Transform cachedTransform2 = uidrawCall.cachedTransform;
			cachedTransform2.position = vector;
			cachedTransform2.rotation = rotation;
			cachedTransform2.localScale = lossyScale;
			uidrawCall.renderQueue = ((this.renderQueue == UIPanel.RenderQueue.Explicit) ? this.startingRenderQueue : (this.startingRenderQueue + i));
			uidrawCall.alwaysOnScreen = (this.alwaysOnScreen && (this.mClipping == UIDrawCall.Clipping.None || this.mClipping == UIDrawCall.Clipping.ConstrainButDontClip));
			uidrawCall.sortingOrder = (this.useSortingOrder ? ((this.mSortingOrder == 0 && this.renderQueue == UIPanel.RenderQueue.Automatic) ? sortOrder : this.mSortingOrder) : 0);
			uidrawCall.sortingLayerName = (this.useSortingOrder ? this.mSortingLayerName : null);
			uidrawCall.clipTexture = this.mClipTexture;
			uidrawCall.shadowMode = this.shadowMode;
		}
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x000448D0 File Offset: 0x00042AD0
	private void UpdateLayers()
	{
		if (this.mLayer != base.cachedGameObject.layer)
		{
			this.mLayer = this.mGo.layer;
			int i = 0;
			int count = this.widgets.Count;
			while (i < count)
			{
				UIWidget uiwidget = this.widgets[i];
				if (uiwidget && uiwidget.parent == this)
				{
					uiwidget.gameObject.layer = this.mLayer;
				}
				i++;
			}
			base.ResetAnchors();
			for (int j = 0; j < this.drawCalls.Count; j++)
			{
				this.drawCalls[j].gameObject.layer = this.mLayer;
			}
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00044988 File Offset: 0x00042B88
	private void UpdateWidgets()
	{
		bool flag = false;
		bool flag2 = false;
		bool hasCumulativeClipping = this.hasCumulativeClipping;
		if (!this.cullWhileDragging)
		{
			for (int i = 0; i < UIScrollView.list.size; i++)
			{
				UIScrollView uiscrollView = UIScrollView.list.buffer[i];
				if (uiscrollView.panel == this && uiscrollView.isDragging)
				{
					flag2 = true;
				}
			}
		}
		if (this.mForced != flag2)
		{
			this.mForced = flag2;
			this.mResized = true;
		}
		int frameCount = Time.frameCount;
		int j = 0;
		int count = this.widgets.Count;
		while (j < count)
		{
			UIWidget uiwidget = this.widgets[j];
			if (uiwidget.panel == this && uiwidget.enabled)
			{
				if (uiwidget.UpdateTransform(frameCount) || this.mResized || (this.mHasMoved && !this.alwaysOnScreen))
				{
					bool visibleByAlpha = flag2 || uiwidget.CalculateCumulativeAlpha(frameCount) > 0.001f;
					uiwidget.UpdateVisibility(visibleByAlpha, flag2 || this.alwaysOnScreen || (!hasCumulativeClipping && !uiwidget.hideIfOffScreen) || this.IsVisible(uiwidget));
				}
				if (uiwidget.UpdateGeometry(frameCount))
				{
					flag = true;
					if (!this.mRebuild)
					{
						if (uiwidget.drawCall != null)
						{
							uiwidget.drawCall.isDirty = true;
						}
						else
						{
							this.FindDrawCall(uiwidget);
						}
					}
				}
			}
			j++;
		}
		if (flag && this.onGeometryUpdated != null)
		{
			this.onGeometryUpdated();
		}
		this.mResized = false;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00044B18 File Offset: 0x00042D18
	public UIDrawCall FindDrawCall(UIWidget w)
	{
		Material material = w.material;
		Texture mainTexture = w.mainTexture;
		Shader shader = w.shader;
		int depth = w.depth;
		for (int i = 0; i < this.drawCalls.Count; i++)
		{
			UIDrawCall uidrawCall = this.drawCalls[i];
			int num = (i == 0) ? int.MinValue : (this.drawCalls[i - 1].depthEnd + 1);
			int num2 = (i + 1 == this.drawCalls.Count) ? int.MaxValue : (this.drawCalls[i + 1].depthStart - 1);
			if (num <= depth && num2 >= depth)
			{
				if (uidrawCall.baseMaterial == material && uidrawCall.shader == shader && uidrawCall.mainTexture == mainTexture)
				{
					if (w.isVisible)
					{
						w.drawCall = uidrawCall;
						if (w.hasVertices)
						{
							uidrawCall.isDirty = true;
						}
						return uidrawCall;
					}
				}
				else
				{
					this.mRebuild = true;
				}
				return null;
			}
		}
		this.mRebuild = true;
		return null;
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00044C2C File Offset: 0x00042E2C
	public void AddWidget(UIWidget w)
	{
		this.mUpdateScroll = true;
		if (this.widgets.Count == 0)
		{
			this.widgets.Add(w);
		}
		else if (this.mSortWidgets)
		{
			this.widgets.Add(w);
			this.SortWidgets();
		}
		else if (UIWidget.PanelCompareFunc(w, this.widgets[0]) == -1)
		{
			this.widgets.Insert(0, w);
		}
		else
		{
			int i = this.widgets.Count;
			while (i > 0)
			{
				if (UIWidget.PanelCompareFunc(w, this.widgets[--i]) != -1)
				{
					this.widgets.Insert(i + 1, w);
					break;
				}
			}
		}
		this.FindDrawCall(w);
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00044CE0 File Offset: 0x00042EE0
	public void RemoveWidget(UIWidget w)
	{
		if (this.widgets.Remove(w) && w.drawCall != null)
		{
			int depth = w.depth;
			if (depth == w.drawCall.depthStart || depth == w.drawCall.depthEnd)
			{
				this.mRebuild = true;
			}
			w.drawCall.isDirty = true;
			w.drawCall = null;
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00044D46 File Offset: 0x00042F46
	public void Refresh()
	{
		this.mRebuild = true;
		UIPanel.mUpdateFrame = -1;
		if (UIPanel.list.Count > 0)
		{
			UIPanel.list[0].LateUpdate();
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00044D74 File Offset: 0x00042F74
	public virtual Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		Vector4 finalClipRegion = this.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		Vector2 minRect = new Vector2(min.x, min.y);
		Vector2 maxRect = new Vector2(max.x, max.y);
		Vector2 minArea = new Vector2(finalClipRegion.x - num, finalClipRegion.y - num2);
		Vector2 maxArea = new Vector2(finalClipRegion.x + num, finalClipRegion.y + num2);
		if (this.softBorderPadding && this.clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += this.mClipSoftness.x;
			minArea.y += this.mClipSoftness.y;
			maxArea.x -= this.mClipSoftness.x;
			maxArea.y -= this.mClipSoftness.y;
		}
		return NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00044E6C File Offset: 0x0004306C
	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		Vector3 vector = targetBounds.min;
		Vector3 vector2 = targetBounds.max;
		float num = 1f;
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				num = root.pixelSizeAdjustment;
			}
		}
		if (num != 1f)
		{
			vector /= num;
			vector2 /= num;
		}
		Vector3 b = this.CalculateConstrainOffset(vector, vector2) * num;
		if (b.sqrMagnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += b;
				targetBounds.center += b;
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.gameObject, target.localPosition + b, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00044F60 File Offset: 0x00043160
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref bounds, immediate);
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00044F84 File Offset: 0x00043184
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, false, -1);
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00044F8E File Offset: 0x0004318E
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		return UIPanel.Find(trans, createIfMissing, -1);
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00044F98 File Offset: 0x00043198
	public static UIPanel Find(Transform trans, bool createIfMissing, int layer)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(trans);
		if (uipanel != null)
		{
			return uipanel;
		}
		while (trans.parent != null)
		{
			trans = trans.parent;
		}
		if (!createIfMissing)
		{
			return null;
		}
		return NGUITools.CreateUI(trans, false, layer);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00044FDC File Offset: 0x000431DC
	public Vector2 GetWindowSize()
	{
		UIRoot root = base.root;
		Vector2 vector = NGUITools.screenSize;
		if (root != null)
		{
			vector *= root.GetPixelSizeAdjustment(Mathf.RoundToInt(vector.y));
		}
		return vector;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00045018 File Offset: 0x00043218
	public Vector2 GetViewSize()
	{
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			return new Vector2(this.mClipRange.z, this.mClipRange.w);
		}
		return NGUITools.screenSize;
	}

	// Token: 0x04000731 RID: 1841
	public static List<UIPanel> list = new List<UIPanel>();

	// Token: 0x04000732 RID: 1842
	public UIPanel.OnGeometryUpdated onGeometryUpdated;

	// Token: 0x04000733 RID: 1843
	public bool showInPanelTool = true;

	// Token: 0x04000734 RID: 1844
	public bool generateNormals;

	// Token: 0x04000735 RID: 1845
	public bool generateUV2;

	// Token: 0x04000736 RID: 1846
	public UIDrawCall.ShadowMode shadowMode;

	// Token: 0x04000737 RID: 1847
	public bool widgetsAreStatic;

	// Token: 0x04000738 RID: 1848
	public bool cullWhileDragging = true;

	// Token: 0x04000739 RID: 1849
	public bool alwaysOnScreen;

	// Token: 0x0400073A RID: 1850
	public bool anchorOffset;

	// Token: 0x0400073B RID: 1851
	public bool softBorderPadding = true;

	// Token: 0x0400073C RID: 1852
	public UIPanel.RenderQueue renderQueue;

	// Token: 0x0400073D RID: 1853
	public int startingRenderQueue = 3000;

	// Token: 0x0400073E RID: 1854
	[NonSerialized]
	public List<UIWidget> widgets = new List<UIWidget>();

	// Token: 0x0400073F RID: 1855
	[NonSerialized]
	public List<UIDrawCall> drawCalls = new List<UIDrawCall>();

	// Token: 0x04000740 RID: 1856
	[NonSerialized]
	public Matrix4x4 worldToLocal = Matrix4x4.identity;

	// Token: 0x04000741 RID: 1857
	[NonSerialized]
	public Vector4 drawCallClipRange = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x04000742 RID: 1858
	public UIPanel.OnClippingMoved onClipMove;

	// Token: 0x04000743 RID: 1859
	public UIPanel.OnCreateMaterial onCreateMaterial;

	// Token: 0x04000744 RID: 1860
	public UIDrawCall.OnCreateDrawCall onCreateDrawCall;

	// Token: 0x04000745 RID: 1861
	[HideInInspector]
	[SerializeField]
	private Texture2D mClipTexture;

	// Token: 0x04000746 RID: 1862
	[HideInInspector]
	[SerializeField]
	private float mAlpha = 1f;

	// Token: 0x04000747 RID: 1863
	[HideInInspector]
	[SerializeField]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x04000748 RID: 1864
	[HideInInspector]
	[SerializeField]
	private Vector4 mClipRange = new Vector4(0f, 0f, 300f, 200f);

	// Token: 0x04000749 RID: 1865
	[HideInInspector]
	[SerializeField]
	private Vector2 mClipSoftness = new Vector2(4f, 4f);

	// Token: 0x0400074A RID: 1866
	[HideInInspector]
	[SerializeField]
	private int mDepth;

	// Token: 0x0400074B RID: 1867
	[HideInInspector]
	[SerializeField]
	private int mSortingOrder;

	// Token: 0x0400074C RID: 1868
	[HideInInspector]
	[SerializeField]
	private string mSortingLayerName;

	// Token: 0x0400074D RID: 1869
	private bool mRebuild;

	// Token: 0x0400074E RID: 1870
	private bool mResized;

	// Token: 0x0400074F RID: 1871
	[SerializeField]
	private Vector2 mClipOffset = Vector2.zero;

	// Token: 0x04000750 RID: 1872
	private int mMatrixFrame = -1;

	// Token: 0x04000751 RID: 1873
	private int mAlphaFrameID;

	// Token: 0x04000752 RID: 1874
	private int mLayer = -1;

	// Token: 0x04000753 RID: 1875
	private static float[] mTemp = new float[4];

	// Token: 0x04000754 RID: 1876
	private Vector2 mMin = Vector2.zero;

	// Token: 0x04000755 RID: 1877
	private Vector2 mMax = Vector2.zero;

	// Token: 0x04000756 RID: 1878
	private bool mSortWidgets;

	// Token: 0x04000757 RID: 1879
	private bool mUpdateScroll;

	// Token: 0x04000758 RID: 1880
	public bool useSortingOrder;

	// Token: 0x04000759 RID: 1881
	private UIPanel mParentPanel;

	// Token: 0x0400075A RID: 1882
	private static Vector3[] mCorners = new Vector3[4];

	// Token: 0x0400075B RID: 1883
	private static int mUpdateFrame = -1;

	// Token: 0x0400075C RID: 1884
	[NonSerialized]
	private bool mHasMoved;

	// Token: 0x0400075D RID: 1885
	private UIDrawCall.OnRenderCallback mOnRender;

	// Token: 0x0400075E RID: 1886
	private bool mForced;

	// Token: 0x02000682 RID: 1666
	[DoNotObfuscateNGUI]
	public enum RenderQueue
	{
		// Token: 0x040045D8 RID: 17880
		Automatic,
		// Token: 0x040045D9 RID: 17881
		StartAt,
		// Token: 0x040045DA RID: 17882
		Explicit
	}

	// Token: 0x02000683 RID: 1667
	// (Invoke) Token: 0x06002B4E RID: 11086
	public delegate void OnGeometryUpdated();

	// Token: 0x02000684 RID: 1668
	// (Invoke) Token: 0x06002B52 RID: 11090
	public delegate void OnClippingMoved(UIPanel panel);

	// Token: 0x02000685 RID: 1669
	// (Invoke) Token: 0x06002B56 RID: 11094
	public delegate Material OnCreateMaterial(UIWidget widget, Material mat);
}
