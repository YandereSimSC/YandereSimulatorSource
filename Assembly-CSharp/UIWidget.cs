﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000084 RID: 132
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Invisible Widget")]
public class UIWidget : UIRect
{
	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600054F RID: 1359 RVA: 0x00032173 File Offset: 0x00030373
	// (set) Token: 0x06000550 RID: 1360 RVA: 0x0003217C File Offset: 0x0003037C
	public UIDrawCall.OnRenderCallback onRender
	{
		get
		{
			return this.mOnRender;
		}
		set
		{
			if (this.mOnRender != value)
			{
				if (this.drawCall != null && this.drawCall.onRender != null && this.mOnRender != null)
				{
					UIDrawCall uidrawCall = this.drawCall;
					uidrawCall.onRender = (UIDrawCall.OnRenderCallback)Delegate.Remove(uidrawCall.onRender, this.mOnRender);
				}
				this.mOnRender = value;
				if (this.drawCall != null)
				{
					UIDrawCall uidrawCall2 = this.drawCall;
					uidrawCall2.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uidrawCall2.onRender, value);
				}
			}
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000551 RID: 1361 RVA: 0x0003220C File Offset: 0x0003040C
	// (set) Token: 0x06000552 RID: 1362 RVA: 0x00032214 File Offset: 0x00030414
	public Vector4 drawRegion
	{
		get
		{
			return this.mDrawRegion;
		}
		set
		{
			if (this.mDrawRegion != value)
			{
				this.mDrawRegion = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000553 RID: 1363 RVA: 0x0003223F File Offset: 0x0003043F
	public Vector2 pivotOffset
	{
		get
		{
			return NGUIMath.GetPivotOffset(this.pivot);
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000554 RID: 1364 RVA: 0x0003224C File Offset: 0x0003044C
	// (set) Token: 0x06000555 RID: 1365 RVA: 0x00032254 File Offset: 0x00030454
	public int width
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			int minWidth = this.minWidth;
			if (value < minWidth)
			{
				value = minWidth;
			}
			if (this.mWidth != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnHeight)
			{
				if (this.isAnchoredHorizontally)
				{
					if (this.leftAnchor.target != null && this.rightAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Left || this.mPivot == UIWidget.Pivot.TopLeft)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
							return;
						}
						if (this.mPivot == UIWidget.Pivot.BottomRight || this.mPivot == UIWidget.Pivot.Right || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
							return;
						}
						int num = value - this.mWidth;
						num -= (num & 1);
						if (num != 0)
						{
							NGUIMath.AdjustWidget(this, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f, 0f);
							return;
						}
					}
					else
					{
						if (this.leftAnchor.target != null)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
							return;
						}
						NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
						return;
					}
				}
				else
				{
					this.SetDimensions(value, this.mHeight);
				}
			}
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000556 RID: 1366 RVA: 0x000323C6 File Offset: 0x000305C6
	// (set) Token: 0x06000557 RID: 1367 RVA: 0x000323D0 File Offset: 0x000305D0
	public int height
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			int minHeight = this.minHeight;
			if (value < minHeight)
			{
				value = minHeight;
			}
			if (this.mHeight != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnWidth)
			{
				if (this.isAnchoredVertically)
				{
					if (this.bottomAnchor.target != null && this.topAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Bottom || this.mPivot == UIWidget.Pivot.BottomRight)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
							return;
						}
						if (this.mPivot == UIWidget.Pivot.TopLeft || this.mPivot == UIWidget.Pivot.Top || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
							return;
						}
						int num = value - this.mHeight;
						num -= (num & 1);
						if (num != 0)
						{
							NGUIMath.AdjustWidget(this, 0f, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f);
							return;
						}
					}
					else
					{
						if (this.bottomAnchor.target != null)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
							return;
						}
						NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
						return;
					}
				}
				else
				{
					this.SetDimensions(this.mWidth, value);
				}
			}
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000558 RID: 1368 RVA: 0x00032542 File Offset: 0x00030742
	// (set) Token: 0x06000559 RID: 1369 RVA: 0x0003254C File Offset: 0x0003074C
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (this.mColor != value)
			{
				bool includeChildren = this.mColor.a != value.a;
				this.mColor = value;
				this.Invalidate(includeChildren);
			}
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0003258C File Offset: 0x0003078C
	public void SetColorNoAlpha(Color c)
	{
		if (this.mColor.r != c.r || this.mColor.g != c.g || this.mColor.b != c.b)
		{
			this.mColor.r = c.r;
			this.mColor.g = c.g;
			this.mColor.b = c.b;
			this.Invalidate(false);
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x0600055B RID: 1371 RVA: 0x0003260C File Offset: 0x0003080C
	// (set) Token: 0x0600055C RID: 1372 RVA: 0x00032619 File Offset: 0x00030819
	public override float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			if (this.mColor.a != value)
			{
				this.mColor.a = value;
				this.Invalidate(true);
			}
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x0600055D RID: 1373 RVA: 0x0003263C File Offset: 0x0003083C
	public bool isVisible
	{
		get
		{
			return this.mIsVisibleByPanel && this.mIsVisibleByAlpha && this.mIsInFront && this.finalAlpha > 0.001f && NGUITools.GetActive(this);
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x0600055E RID: 1374 RVA: 0x0003266B File Offset: 0x0003086B
	public bool hasVertices
	{
		get
		{
			return this.geometry != null && this.geometry.hasVertices;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x0600055F RID: 1375 RVA: 0x00032682 File Offset: 0x00030882
	// (set) Token: 0x06000560 RID: 1376 RVA: 0x0003268A File Offset: 0x0003088A
	public UIWidget.Pivot rawPivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				this.mPivot = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000561 RID: 1377 RVA: 0x00032682 File Offset: 0x00030882
	// (set) Token: 0x06000562 RID: 1378 RVA: 0x000326B0 File Offset: 0x000308B0
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				Vector3 vector = this.worldCorners[0];
				this.mPivot = value;
				this.mChanged = true;
				Vector3 vector2 = this.worldCorners[0];
				Transform cachedTransform = base.cachedTransform;
				Vector3 vector3 = cachedTransform.position;
				float z = cachedTransform.localPosition.z;
				vector3.x += vector.x - vector2.x;
				vector3.y += vector.y - vector2.y;
				base.cachedTransform.position = vector3;
				vector3 = base.cachedTransform.localPosition;
				vector3.x = Mathf.Round(vector3.x);
				vector3.y = Mathf.Round(vector3.y);
				vector3.z = z;
				base.cachedTransform.localPosition = vector3;
			}
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000563 RID: 1379 RVA: 0x00032789 File Offset: 0x00030989
	// (set) Token: 0x06000564 RID: 1380 RVA: 0x00032794 File Offset: 0x00030994
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
				if (this.panel != null)
				{
					this.panel.RemoveWidget(this);
				}
				this.mDepth = value;
				if (this.panel != null)
				{
					this.panel.AddWidget(this);
					if (!Application.isPlaying)
					{
						this.panel.SortWidgets();
						this.panel.RebuildAllDrawCalls();
					}
				}
			}
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000565 RID: 1381 RVA: 0x00032804 File Offset: 0x00030A04
	public int raycastDepth
	{
		get
		{
			if (this.panel == null)
			{
				this.CreatePanel();
			}
			if (!(this.panel != null))
			{
				return this.mDepth;
			}
			return this.mDepth + this.panel.depth * 1000;
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000566 RID: 1382 RVA: 0x00032854 File Offset: 0x00030A54
	public override Vector3[] localCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			this.mCorners[0] = new Vector3(num, num2);
			this.mCorners[1] = new Vector3(num, y);
			this.mCorners[2] = new Vector3(x, y);
			this.mCorners[3] = new Vector3(x, num2);
			return this.mCorners;
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000567 RID: 1383 RVA: 0x000328EC File Offset: 0x00030AEC
	public virtual Vector2 localSize
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return localCorners[2] - localCorners[0];
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000568 RID: 1384 RVA: 0x00032918 File Offset: 0x00030B18
	public Vector3 localCenter
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000569 RID: 1385 RVA: 0x00032944 File Offset: 0x00030B44
	public override Vector3[] worldCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			Transform cachedTransform = base.cachedTransform;
			this.mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
			this.mCorners[1] = cachedTransform.TransformPoint(num, y, 0f);
			this.mCorners[2] = cachedTransform.TransformPoint(x, y, 0f);
			this.mCorners[3] = cachedTransform.TransformPoint(x, num2, 0f);
			return this.mCorners;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x0600056A RID: 1386 RVA: 0x00032A00 File Offset: 0x00030C00
	public Vector3 worldCenter
	{
		get
		{
			return base.cachedTransform.TransformPoint(this.localCenter);
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x0600056B RID: 1387 RVA: 0x00032A14 File Offset: 0x00030C14
	public virtual Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			return new Vector4((this.mDrawRegion.x == 0f) ? num : Mathf.Lerp(num, num3, this.mDrawRegion.x), (this.mDrawRegion.y == 0f) ? num2 : Mathf.Lerp(num2, num4, this.mDrawRegion.y), (this.mDrawRegion.z == 1f) ? num3 : Mathf.Lerp(num, num3, this.mDrawRegion.z), (this.mDrawRegion.w == 1f) ? num4 : Mathf.Lerp(num2, num4, this.mDrawRegion.w));
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600056C RID: 1388 RVA: 0x00032AFB File Offset: 0x00030CFB
	// (set) Token: 0x0600056D RID: 1389 RVA: 0x00032B03 File Offset: 0x00030D03
	public virtual Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				this.RemoveFromPanel();
				this.mMat = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600056E RID: 1390 RVA: 0x00032B28 File Offset: 0x00030D28
	// (set) Token: 0x0600056F RID: 1391 RVA: 0x00032B4D File Offset: 0x00030D4D
	public virtual Texture mainTexture
	{
		get
		{
			Material material = this.material;
			if (!(material != null))
			{
				return null;
			}
			return material.mainTexture;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no mainTexture setter");
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000570 RID: 1392 RVA: 0x00032B64 File Offset: 0x00030D64
	// (set) Token: 0x06000571 RID: 1393 RVA: 0x00032B89 File Offset: 0x00030D89
	public virtual Shader shader
	{
		get
		{
			Material material = this.material;
			if (!(material != null))
			{
				return null;
			}
			return material.shader;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no shader setter");
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000572 RID: 1394 RVA: 0x00032BA0 File Offset: 0x00030DA0
	[Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
	public Vector2 relativeSize
	{
		get
		{
			return Vector2.one;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000573 RID: 1395 RVA: 0x00032BA7 File Offset: 0x00030DA7
	public bool hasBoxCollider
	{
		get
		{
			return base.GetComponent<Collider>() as BoxCollider != null || base.GetComponent<BoxCollider2D>() != null;
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00032BCC File Offset: 0x00030DCC
	public void SetDimensions(int w, int h)
	{
		if (this.mWidth != w || this.mHeight != h)
		{
			this.mWidth = w;
			this.mHeight = h;
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnWidth)
			{
				this.mHeight = Mathf.RoundToInt((float)this.mWidth / this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				this.mWidth = Mathf.RoundToInt((float)this.mHeight * this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.Free)
			{
				this.aspectRatio = (float)this.mWidth / (float)this.mHeight;
			}
			this.mMoved = true;
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00032C7C File Offset: 0x00030E7C
	public override Vector3[] GetSides(Transform relativeTo)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = -pivotOffset.x * (float)this.mWidth;
		float num2 = -pivotOffset.y * (float)this.mHeight;
		float num3 = num + (float)this.mWidth;
		float num4 = num2 + (float)this.mHeight;
		float x = (num + num3) * 0.5f;
		float y = (num2 + num4) * 0.5f;
		Transform cachedTransform = base.cachedTransform;
		this.mCorners[0] = cachedTransform.TransformPoint(num, y, 0f);
		this.mCorners[1] = cachedTransform.TransformPoint(x, num4, 0f);
		this.mCorners[2] = cachedTransform.TransformPoint(num3, y, 0f);
		this.mCorners[3] = cachedTransform.TransformPoint(x, num2, 0f);
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				this.mCorners[i] = relativeTo.InverseTransformPoint(this.mCorners[i]);
			}
		}
		return this.mCorners;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00032D8B File Offset: 0x00030F8B
	public override float CalculateFinalAlpha(int frameID)
	{
		if (this.mAlphaFrameID != frameID)
		{
			this.mAlphaFrameID = frameID;
			this.UpdateFinalAlpha(frameID);
		}
		return this.finalAlpha;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00032DAC File Offset: 0x00030FAC
	protected void UpdateFinalAlpha(int frameID)
	{
		if (!this.mIsVisibleByAlpha || !this.mIsInFront)
		{
			this.finalAlpha = 0f;
			return;
		}
		UIRect parent = base.parent;
		this.finalAlpha = ((parent != null) ? (parent.CalculateFinalAlpha(frameID) * this.mColor.a) : this.mColor.a);
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00032E0C File Offset: 0x0003100C
	public override void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		this.mAlphaFrameID = -1;
		if (this.panel != null)
		{
			bool visibleByPanel = (!this.hideIfOffScreen && !this.panel.hasCumulativeClipping) || this.panel.IsVisible(this);
			this.UpdateVisibility(this.CalculateCumulativeAlpha(Time.frameCount) > 0.001f, visibleByPanel);
			this.UpdateFinalAlpha(Time.frameCount);
			if (includeChildren)
			{
				base.Invalidate(true);
			}
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00032E8C File Offset: 0x0003108C
	public float CalculateCumulativeAlpha(int frameID)
	{
		UIRect parent = base.parent;
		if (!(parent != null))
		{
			return this.mColor.a;
		}
		return parent.CalculateFinalAlpha(frameID) * this.mColor.a;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00032EC8 File Offset: 0x000310C8
	public override void SetRect(float x, float y, float width, float height)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = Mathf.Lerp(x, x + width, pivotOffset.x);
		float num2 = Mathf.Lerp(y, y + height, pivotOffset.y);
		int num3 = Mathf.FloorToInt(width + 0.5f);
		int num4 = Mathf.FloorToInt(height + 0.5f);
		if (pivotOffset.x == 0.5f)
		{
			num3 = num3 >> 1 << 1;
		}
		if (pivotOffset.y == 0.5f)
		{
			num4 = num4 >> 1 << 1;
		}
		Transform transform = base.cachedTransform;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(num + 0.5f);
		localPosition.y = Mathf.Floor(num2 + 0.5f);
		if (num3 < this.minWidth)
		{
			num3 = this.minWidth;
		}
		if (num4 < this.minHeight)
		{
			num4 = this.minHeight;
		}
		transform.localPosition = localPosition;
		this.width = num3;
		this.height = num4;
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

	// Token: 0x0600057B RID: 1403 RVA: 0x0003304C File Offset: 0x0003124C
	public void ResizeCollider()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component != null)
		{
			NGUITools.UpdateWidgetCollider(this, component);
			return;
		}
		NGUITools.UpdateWidgetCollider(this, base.GetComponent<BoxCollider2D>());
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00033080 File Offset: 0x00031280
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int FullCompareFunc(UIWidget left, UIWidget right)
	{
		int num = UIPanel.CompareFunc(left.panel, right.panel);
		if (num != 0)
		{
			return num;
		}
		return UIWidget.PanelCompareFunc(left, right);
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x000330AC File Offset: 0x000312AC
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int PanelCompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		Material material = left.material;
		Material material2 = right.material;
		if (material == material2)
		{
			return 0;
		}
		if (material == null)
		{
			return 1;
		}
		if (material2 == null)
		{
			return -1;
		}
		if (material.GetInstanceID() >= material2.GetInstanceID())
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00033119 File Offset: 0x00031319
	public Bounds CalculateBounds()
	{
		return this.CalculateBounds(null);
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00033124 File Offset: 0x00031324
	public Bounds CalculateBounds(Transform relativeParent)
	{
		if (relativeParent == null)
		{
			Vector3[] localCorners = this.localCorners;
			Bounds result = new Bounds(localCorners[0], Vector3.zero);
			for (int i = 1; i < 4; i++)
			{
				result.Encapsulate(localCorners[i]);
			}
			return result;
		}
		Matrix4x4 worldToLocalMatrix = relativeParent.worldToLocalMatrix;
		Vector3[] worldCorners = this.worldCorners;
		Bounds result2 = new Bounds(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[0]), Vector3.zero);
		for (int j = 1; j < 4; j++)
		{
			result2.Encapsulate(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[j]));
		}
		return result2;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x000331C3 File Offset: 0x000313C3
	public void SetDirty()
	{
		if (this.drawCall != null)
		{
			this.drawCall.isDirty = true;
			return;
		}
		if (this.isVisible && this.hasVertices)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x000331F7 File Offset: 0x000313F7
	public void RemoveFromPanel()
	{
		if (this.panel != null)
		{
			this.panel.RemoveWidget(this);
			this.panel = null;
		}
		this.drawCall = null;
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00033224 File Offset: 0x00031424
	public virtual void MarkAsChanged()
	{
		if (NGUITools.GetActive(this))
		{
			this.mChanged = true;
			if (this.panel != null && base.enabled && NGUITools.GetActive(base.gameObject) && !this.mPlayMode)
			{
				this.SetDirty();
				this.CheckLayer();
			}
		}
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00033278 File Offset: 0x00031478
	public UIPanel CreatePanel()
	{
		if (this.mStarted && this.panel == null && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.panel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != null)
			{
				this.mParentFound = false;
				this.panel.AddWidget(this);
				this.CheckLayer();
				this.Invalidate(true);
			}
		}
		return this.panel;
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00033304 File Offset: 0x00031504
	public void CheckLayer()
	{
		if (this.panel != null && this.panel.gameObject.layer != base.gameObject.layer)
		{
			UnityEngine.Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.panel.gameObject.layer;
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00033364 File Offset: 0x00031564
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		if (this.panel != null)
		{
			UIPanel y = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != y)
			{
				this.RemoveFromPanel();
				this.CreatePanel();
			}
		}
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x000333B8 File Offset: 0x000315B8
	protected override void Awake()
	{
		base.Awake();
		this.mPlayMode = Application.isPlaying;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x000333CB File Offset: 0x000315CB
	protected override void OnInit()
	{
		base.OnInit();
		this.RemoveFromPanel();
		this.mMoved = true;
		base.Update();
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x000333E8 File Offset: 0x000315E8
	protected virtual void UpgradeFrom265()
	{
		Vector3 localScale = base.cachedTransform.localScale;
		this.mWidth = Mathf.Abs(Mathf.RoundToInt(localScale.x));
		this.mHeight = Mathf.Abs(Mathf.RoundToInt(localScale.y));
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00033439 File Offset: 0x00031639
	protected override void OnStart()
	{
		this.CreatePanel();
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00033444 File Offset: 0x00031644
	protected override void OnAnchor()
	{
		Transform cachedTransform = base.cachedTransform;
		Transform parent = cachedTransform.parent;
		Vector3 localPosition = cachedTransform.localPosition;
		Vector2 pivotOffset = this.pivotOffset;
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
				this.mIsInFront = true;
			}
			else
			{
				Vector3 localPos = base.GetLocalPos(this.leftAnchor, parent);
				num = localPos.x + (float)this.leftAnchor.absolute;
				num3 = localPos.y + (float)this.bottomAnchor.absolute;
				num2 = localPos.x + (float)this.rightAnchor.absolute;
				num4 = localPos.y + (float)this.topAnchor.absolute;
				this.mIsInFront = (!this.hideIfOffScreen || localPos.z >= 0f);
			}
		}
		else
		{
			this.mIsInFront = true;
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
				num = localPosition.x - pivotOffset.x * (float)this.mWidth;
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
				num2 = localPosition.x - pivotOffset.x * (float)this.mWidth + (float)this.mWidth;
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
				num3 = localPosition.y - pivotOffset.y * (float)this.mHeight;
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
				num4 = localPosition.y - pivotOffset.y * (float)this.mHeight + (float)this.mHeight;
			}
		}
		Vector3 vector = new Vector3(Mathf.Lerp(num, num2, pivotOffset.x), Mathf.Lerp(num3, num4, pivotOffset.y), localPosition.z);
		vector.x = Mathf.Round(vector.x);
		vector.y = Mathf.Round(vector.y);
		int num5 = Mathf.FloorToInt(num2 - num + 0.5f);
		int num6 = Mathf.FloorToInt(num4 - num3 + 0.5f);
		if (this.keepAspectRatio != UIWidget.AspectRatioSource.Free && this.aspectRatio != 0f)
		{
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				num5 = Mathf.RoundToInt((float)num6 * this.aspectRatio);
			}
			else
			{
				num6 = Mathf.RoundToInt((float)num5 / this.aspectRatio);
			}
		}
		if (num5 < this.minWidth)
		{
			num5 = this.minWidth;
		}
		if (num6 < this.minHeight)
		{
			num6 = this.minHeight;
		}
		if (Vector3.SqrMagnitude(localPosition - vector) > 0.001f)
		{
			base.cachedTransform.localPosition = vector;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
		}
		if (this.mWidth != num5 || this.mHeight != num6)
		{
			this.mWidth = num5;
			this.mHeight = num6;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00033A22 File Offset: 0x00031C22
	protected override void OnUpdate()
	{
		if (this.panel == null)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00033A39 File Offset: 0x00031C39
	private void OnApplicationPause(bool paused)
	{
		if (!paused)
		{
			this.MarkAsChanged();
		}
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00033A44 File Offset: 0x00031C44
	protected override void OnDisable()
	{
		this.RemoveFromPanel();
		base.OnDisable();
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00033A52 File Offset: 0x00031C52
	private void OnDestroy()
	{
		this.RemoveFromPanel();
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00033A5A File Offset: 0x00031C5A
	public bool UpdateVisibility(bool visibleByAlpha, bool visibleByPanel)
	{
		if (this.mIsVisibleByAlpha != visibleByAlpha || this.mIsVisibleByPanel != visibleByPanel)
		{
			this.mChanged = true;
			this.mIsVisibleByAlpha = visibleByAlpha;
			this.mIsVisibleByPanel = visibleByPanel;
			return true;
		}
		return false;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00033A88 File Offset: 0x00031C88
	public bool UpdateTransform(int frame)
	{
		Transform cachedTransform = base.cachedTransform;
		this.mPlayMode = Application.isPlaying;
		if (this.mMoved)
		{
			this.mMoved = true;
			this.mMatrixFrame = -1;
			cachedTransform.hasChanged = false;
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			this.mOldV0 = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num, num2, 0f));
			this.mOldV1 = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(x, y, 0f));
		}
		else if (!this.panel.widgetsAreStatic && cachedTransform.hasChanged)
		{
			this.mMatrixFrame = -1;
			cachedTransform.hasChanged = false;
			Vector2 pivotOffset2 = this.pivotOffset;
			float num3 = -pivotOffset2.x * (float)this.mWidth;
			float num4 = -pivotOffset2.y * (float)this.mHeight;
			float x2 = num3 + (float)this.mWidth;
			float y2 = num4 + (float)this.mHeight;
			Vector3 b = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num3, num4, 0f));
			Vector3 b2 = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(x2, y2, 0f));
			if (Vector3.SqrMagnitude(this.mOldV0 - b) > 1E-06f || Vector3.SqrMagnitude(this.mOldV1 - b2) > 1E-06f)
			{
				this.mMoved = true;
				this.mOldV0 = b;
				this.mOldV1 = b2;
			}
		}
		if (this.mMoved && this.onChange != null)
		{
			this.onChange();
		}
		return this.mMoved || this.mChanged;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00033C6C File Offset: 0x00031E6C
	public bool UpdateGeometry(int frame)
	{
		float num = this.CalculateFinalAlpha(frame);
		if (this.mIsVisibleByAlpha && this.mLastAlpha != num)
		{
			this.mChanged = true;
		}
		this.mLastAlpha = num;
		if (this.mChanged)
		{
			if (this.mIsVisibleByAlpha && num > 0.001f && this.shader != null)
			{
				bool hasVertices = this.geometry.hasVertices;
				if (this.fillGeometry)
				{
					this.geometry.Clear();
					this.OnFill(this.geometry.verts, this.geometry.uvs, this.geometry.cols);
				}
				if (this.geometry.hasVertices)
				{
					if (this.mMatrixFrame != frame)
					{
						this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
						this.mMatrixFrame = frame;
					}
					this.geometry.ApplyTransform(this.mLocalToPanel, this.panel.generateNormals);
					this.mMoved = false;
					this.mChanged = false;
					return true;
				}
				this.mChanged = false;
				return hasVertices;
			}
			else if (this.geometry.hasVertices)
			{
				if (this.fillGeometry)
				{
					this.geometry.Clear();
				}
				this.mMoved = false;
				this.mChanged = false;
				return true;
			}
		}
		else if (this.mMoved && this.geometry.hasVertices)
		{
			if (this.mMatrixFrame != frame)
			{
				this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
				this.mMatrixFrame = frame;
			}
			this.geometry.ApplyTransform(this.mLocalToPanel, this.panel.generateNormals);
			this.mMoved = false;
			this.mChanged = false;
			return true;
		}
		this.mMoved = false;
		this.mChanged = false;
		return false;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00033E3F File Offset: 0x0003203F
	public void WriteToBuffers(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2)
	{
		this.geometry.WriteToBuffers(v, u, c, n, t, u2);
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00033E58 File Offset: 0x00032058
	public virtual void MakePixelPerfect()
	{
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.z = Mathf.Round(localPosition.z);
		localPosition.x = Mathf.Round(localPosition.x);
		localPosition.y = Mathf.Round(localPosition.y);
		base.cachedTransform.localPosition = localPosition;
		Vector3 localScale = base.cachedTransform.localScale;
		base.cachedTransform.localScale = new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f);
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000594 RID: 1428 RVA: 0x00033EEA File Offset: 0x000320EA
	public virtual int minWidth
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000595 RID: 1429 RVA: 0x00033EEA File Offset: 0x000320EA
	public virtual int minHeight
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000596 RID: 1430 RVA: 0x00033EED File Offset: 0x000320ED
	// (set) Token: 0x06000597 RID: 1431 RVA: 0x00002ACE File Offset: 0x00000CCE
	public virtual Vector4 border
	{
		get
		{
			return Vector4.zero;
		}
		set
		{
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00002ACE File Offset: 0x00000CCE
	public virtual void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
	}

	// Token: 0x04000586 RID: 1414
	[HideInInspector]
	[SerializeField]
	protected Color mColor = Color.white;

	// Token: 0x04000587 RID: 1415
	[HideInInspector]
	[SerializeField]
	protected UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x04000588 RID: 1416
	[HideInInspector]
	[SerializeField]
	protected int mWidth = 100;

	// Token: 0x04000589 RID: 1417
	[HideInInspector]
	[SerializeField]
	protected int mHeight = 100;

	// Token: 0x0400058A RID: 1418
	[HideInInspector]
	[SerializeField]
	protected int mDepth;

	// Token: 0x0400058B RID: 1419
	[Tooltip("Custom material, if desired")]
	[HideInInspector]
	[SerializeField]
	protected Material mMat;

	// Token: 0x0400058C RID: 1420
	public UIWidget.OnDimensionsChanged onChange;

	// Token: 0x0400058D RID: 1421
	public UIWidget.OnPostFillCallback onPostFill;

	// Token: 0x0400058E RID: 1422
	public UIDrawCall.OnRenderCallback mOnRender;

	// Token: 0x0400058F RID: 1423
	public bool autoResizeBoxCollider;

	// Token: 0x04000590 RID: 1424
	public bool hideIfOffScreen;

	// Token: 0x04000591 RID: 1425
	public UIWidget.AspectRatioSource keepAspectRatio;

	// Token: 0x04000592 RID: 1426
	public float aspectRatio = 1f;

	// Token: 0x04000593 RID: 1427
	public UIWidget.HitCheck hitCheck;

	// Token: 0x04000594 RID: 1428
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x04000595 RID: 1429
	[NonSerialized]
	public UIGeometry geometry = new UIGeometry();

	// Token: 0x04000596 RID: 1430
	[NonSerialized]
	public bool fillGeometry = true;

	// Token: 0x04000597 RID: 1431
	[NonSerialized]
	protected bool mPlayMode = true;

	// Token: 0x04000598 RID: 1432
	[NonSerialized]
	protected Vector4 mDrawRegion = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x04000599 RID: 1433
	[NonSerialized]
	private Matrix4x4 mLocalToPanel;

	// Token: 0x0400059A RID: 1434
	[NonSerialized]
	private bool mIsVisibleByAlpha = true;

	// Token: 0x0400059B RID: 1435
	[NonSerialized]
	private bool mIsVisibleByPanel = true;

	// Token: 0x0400059C RID: 1436
	[NonSerialized]
	private bool mIsInFront = true;

	// Token: 0x0400059D RID: 1437
	[NonSerialized]
	private float mLastAlpha;

	// Token: 0x0400059E RID: 1438
	[NonSerialized]
	private bool mMoved;

	// Token: 0x0400059F RID: 1439
	[NonSerialized]
	public UIDrawCall drawCall;

	// Token: 0x040005A0 RID: 1440
	[NonSerialized]
	protected Vector3[] mCorners = new Vector3[4];

	// Token: 0x040005A1 RID: 1441
	[NonSerialized]
	private int mAlphaFrameID = -1;

	// Token: 0x040005A2 RID: 1442
	private int mMatrixFrame = -1;

	// Token: 0x040005A3 RID: 1443
	private Vector3 mOldV0;

	// Token: 0x040005A4 RID: 1444
	private Vector3 mOldV1;

	// Token: 0x0200064D RID: 1613
	[DoNotObfuscateNGUI]
	public enum Pivot
	{
		// Token: 0x0400453A RID: 17722
		TopLeft,
		// Token: 0x0400453B RID: 17723
		Top,
		// Token: 0x0400453C RID: 17724
		TopRight,
		// Token: 0x0400453D RID: 17725
		Left,
		// Token: 0x0400453E RID: 17726
		Center,
		// Token: 0x0400453F RID: 17727
		Right,
		// Token: 0x04004540 RID: 17728
		BottomLeft,
		// Token: 0x04004541 RID: 17729
		Bottom,
		// Token: 0x04004542 RID: 17730
		BottomRight
	}

	// Token: 0x0200064E RID: 1614
	// (Invoke) Token: 0x06002AD5 RID: 10965
	public delegate void OnDimensionsChanged();

	// Token: 0x0200064F RID: 1615
	// (Invoke) Token: 0x06002AD9 RID: 10969
	public delegate void OnPostFillCallback(UIWidget widget, int bufferOffset, List<Vector3> verts, List<Vector2> uvs, List<Color> cols);

	// Token: 0x02000650 RID: 1616
	[DoNotObfuscateNGUI]
	public enum AspectRatioSource
	{
		// Token: 0x04004544 RID: 17732
		Free,
		// Token: 0x04004545 RID: 17733
		BasedOnWidth,
		// Token: 0x04004546 RID: 17734
		BasedOnHeight
	}

	// Token: 0x02000651 RID: 1617
	// (Invoke) Token: 0x06002ADD RID: 10973
	public delegate bool HitCheck(Vector3 worldPos);
}
