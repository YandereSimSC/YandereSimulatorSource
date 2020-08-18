﻿using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Scroll View")]
public class UIScrollView : MonoBehaviour
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060002DB RID: 731 RVA: 0x0001C8D7 File Offset: 0x0001AAD7
	public UIPanel panel
	{
		get
		{
			return this.mPanel;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060002DC RID: 732 RVA: 0x0001C8DF File Offset: 0x0001AADF
	public bool isDragging
	{
		get
		{
			return this.mPressed && this.mDragStarted;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060002DD RID: 733 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
	public virtual Bounds bounds
	{
		get
		{
			if (!this.mCalculatedBounds)
			{
				this.mCalculatedBounds = true;
				this.mTrans = base.transform;
				this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mTrans, this.mTrans);
			}
			return this.mBounds;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060002DE RID: 734 RVA: 0x0001C92B File Offset: 0x0001AB2B
	public bool canMoveHorizontally
	{
		get
		{
			return this.movement == UIScrollView.Movement.Horizontal || this.movement == UIScrollView.Movement.Unrestricted || (this.movement == UIScrollView.Movement.Custom && this.customMovement.x != 0f);
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060002DF RID: 735 RVA: 0x0001C960 File Offset: 0x0001AB60
	public bool canMoveVertically
	{
		get
		{
			return this.movement == UIScrollView.Movement.Vertical || this.movement == UIScrollView.Movement.Unrestricted || (this.movement == UIScrollView.Movement.Custom && this.customMovement.y != 0f);
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060002E0 RID: 736 RVA: 0x0001C998 File Offset: 0x0001AB98
	public virtual bool shouldMoveHorizontally
	{
		get
		{
			float num = this.bounds.size.x;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.x * 2f;
			}
			return Mathf.RoundToInt(num - this.mPanel.width) > 0;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
	public virtual bool shouldMoveVertically
	{
		get
		{
			float num = this.bounds.size.y;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.y * 2f;
			}
			return Mathf.RoundToInt(num - this.mPanel.height) > 0;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001CA58 File Offset: 0x0001AC58
	protected virtual bool shouldMove
	{
		get
		{
			if (!this.disableDragIfFits)
			{
				return true;
			}
			if (this.mPanel == null)
			{
				this.mPanel = base.GetComponent<UIPanel>();
			}
			Vector4 finalClipRegion = this.mPanel.finalClipRegion;
			Bounds bounds = this.bounds;
			float num = (finalClipRegion.z == 0f) ? ((float)Screen.width) : (finalClipRegion.z * 0.5f);
			float num2 = (finalClipRegion.w == 0f) ? ((float)Screen.height) : (finalClipRegion.w * 0.5f);
			if (this.canMoveHorizontally)
			{
				if (bounds.min.x + 0.001f < finalClipRegion.x - num)
				{
					return true;
				}
				if (bounds.max.x - 0.001f > finalClipRegion.x + num)
				{
					return true;
				}
			}
			if (this.canMoveVertically)
			{
				if (bounds.min.y + 0.001f < finalClipRegion.y - num2)
				{
					return true;
				}
				if (bounds.max.y - 0.001f > finalClipRegion.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060002E3 RID: 739 RVA: 0x0001CB69 File Offset: 0x0001AD69
	// (set) Token: 0x060002E4 RID: 740 RVA: 0x0001CB71 File Offset: 0x0001AD71
	public Vector3 currentMomentum
	{
		get
		{
			return this.mMomentum;
		}
		set
		{
			this.mMomentum = value;
			this.mShouldMove = true;
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0001CB84 File Offset: 0x0001AD84
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mPanel = base.GetComponent<UIPanel>();
		if (this.mPanel.clipping == UIDrawCall.Clipping.None)
		{
			this.mPanel.clipping = UIDrawCall.Clipping.ConstrainButDontClip;
		}
		if (this.movement != UIScrollView.Movement.Custom && this.scale.sqrMagnitude > 0.001f)
		{
			if (this.scale.x == 1f && this.scale.y == 0f)
			{
				this.movement = UIScrollView.Movement.Horizontal;
			}
			else if (this.scale.x == 0f && this.scale.y == 1f)
			{
				this.movement = UIScrollView.Movement.Vertical;
			}
			else if (this.scale.x == 1f && this.scale.y == 1f)
			{
				this.movement = UIScrollView.Movement.Unrestricted;
			}
			else
			{
				this.movement = UIScrollView.Movement.Custom;
				this.customMovement.x = this.scale.x;
				this.customMovement.y = this.scale.y;
			}
			this.scale = Vector3.zero;
		}
		if (this.contentPivot == UIWidget.Pivot.TopLeft && this.relativePositionOnReset != Vector2.zero)
		{
			this.contentPivot = NGUIMath.GetPivot(new Vector2(this.relativePositionOnReset.x, 1f - this.relativePositionOnReset.y));
			this.relativePositionOnReset = Vector2.zero;
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0001CCFC File Offset: 0x0001AEFC
	private void OnEnable()
	{
		UIScrollView.list.Add(this);
		if (this.mStarted && Application.isPlaying)
		{
			this.CheckScrollbars();
		}
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0001CD1E File Offset: 0x0001AF1E
	private void Start()
	{
		this.mStarted = true;
		if (Application.isPlaying)
		{
			this.CheckScrollbars();
		}
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0001CD34 File Offset: 0x0001AF34
	private void CheckScrollbars()
	{
		if (this.horizontalScrollBar != null)
		{
			EventDelegate.Add(this.horizontalScrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
			this.horizontalScrollBar.BroadcastMessage("CacheDefaultColor", SendMessageOptions.DontRequireReceiver);
			this.horizontalScrollBar.alpha = ((this.showScrollBars == UIScrollView.ShowCondition.Always || this.shouldMoveHorizontally) ? 1f : 0f);
		}
		if (this.verticalScrollBar != null)
		{
			EventDelegate.Add(this.verticalScrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
			this.verticalScrollBar.BroadcastMessage("CacheDefaultColor", SendMessageOptions.DontRequireReceiver);
			this.verticalScrollBar.alpha = ((this.showScrollBars == UIScrollView.ShowCondition.Always || this.shouldMoveVertically) ? 1f : 0f);
		}
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0001CE07 File Offset: 0x0001B007
	private void OnDisable()
	{
		UIScrollView.list.Remove(this);
		this.mPressed = false;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0001CE1C File Offset: 0x0001B01C
	public bool RestrictWithinBounds(bool instant)
	{
		return this.RestrictWithinBounds(instant, true, true);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0001CE28 File Offset: 0x0001B028
	public bool RestrictWithinBounds(bool instant, bool horizontal, bool vertical)
	{
		if (this.mPanel == null)
		{
			return false;
		}
		Bounds bounds = this.bounds;
		Vector3 vector = this.mPanel.CalculateConstrainOffset(bounds.min, bounds.max);
		if (!horizontal)
		{
			vector.x = 0f;
		}
		if (!vertical)
		{
			vector.y = 0f;
		}
		if (vector.sqrMagnitude > 0.1f)
		{
			if (!instant && this.dragEffect == UIScrollView.DragEffect.MomentumAndSpring)
			{
				Vector3 vector2 = this.mTrans.localPosition + vector;
				vector2.x = Mathf.Round(vector2.x);
				vector2.y = Mathf.Round(vector2.y);
				SpringPanel.Begin(this.mPanel.gameObject, vector2, 8f);
			}
			else
			{
				this.MoveRelative(vector);
				if (Mathf.Abs(vector.x) > 0.01f)
				{
					this.mMomentum.x = 0f;
				}
				if (Mathf.Abs(vector.y) > 0.01f)
				{
					this.mMomentum.y = 0f;
				}
				if (Mathf.Abs(vector.z) > 0.01f)
				{
					this.mMomentum.z = 0f;
				}
				this.mScroll = 0f;
			}
			return true;
		}
		return false;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0001CF78 File Offset: 0x0001B178
	public void DisableSpring()
	{
		SpringPanel component = base.GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0001CF9C File Offset: 0x0001B19C
	public void UpdateScrollbars()
	{
		this.UpdateScrollbars(true);
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
	public virtual void UpdateScrollbars(bool recalculateBounds)
	{
		if (this.mPanel == null)
		{
			return;
		}
		if (this.horizontalScrollBar != null || this.verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				this.mCalculatedBounds = false;
				this.mShouldMove = this.shouldMove;
			}
			Bounds bounds = this.bounds;
			Vector2 vector = bounds.min;
			Vector2 vector2 = bounds.max;
			if (this.horizontalScrollBar != null && vector2.x > vector.x)
			{
				Vector4 finalClipRegion = this.mPanel.finalClipRegion;
				int num = Mathf.RoundToInt(finalClipRegion.z);
				if ((num & 1) != 0)
				{
					num--;
				}
				float num2 = (float)num * 0.5f;
				num2 = Mathf.Round(num2);
				if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					num2 -= this.mPanel.clipSoftness.x;
				}
				float contentSize = vector2.x - vector.x;
				float viewSize = num2 * 2f;
				float num3 = vector.x;
				float num4 = vector2.x;
				float num5 = finalClipRegion.x - num2;
				float num6 = finalClipRegion.x + num2;
				num3 = num5 - num3;
				num4 -= num6;
				this.UpdateScrollbars(this.horizontalScrollBar, num3, num4, contentSize, viewSize, false);
			}
			if (this.verticalScrollBar != null && vector2.y > vector.y)
			{
				Vector4 finalClipRegion2 = this.mPanel.finalClipRegion;
				int num7 = Mathf.RoundToInt(finalClipRegion2.w);
				if ((num7 & 1) != 0)
				{
					num7--;
				}
				float num8 = (float)num7 * 0.5f;
				num8 = Mathf.Round(num8);
				if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					num8 -= this.mPanel.clipSoftness.y;
				}
				float contentSize2 = vector2.y - vector.y;
				float viewSize2 = num8 * 2f;
				float num9 = vector.y;
				float num10 = vector2.y;
				float num11 = finalClipRegion2.y - num8;
				float num12 = finalClipRegion2.y + num8;
				num9 = num11 - num9;
				num10 -= num12;
				this.UpdateScrollbars(this.verticalScrollBar, num9, num10, contentSize2, viewSize2, true);
				return;
			}
		}
		else if (recalculateBounds)
		{
			this.mCalculatedBounds = false;
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0001D1DC File Offset: 0x0001B3DC
	protected void UpdateScrollbars(UIProgressBar slider, float contentMin, float contentMax, float contentSize, float viewSize, bool inverted)
	{
		if (slider == null)
		{
			return;
		}
		this.mIgnoreCallbacks = true;
		float num;
		if (viewSize < contentSize)
		{
			contentMin = Mathf.Clamp01(contentMin / contentSize);
			contentMax = Mathf.Clamp01(contentMax / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((num > 0.001f) ? (1f - contentMin / num) : 0f) : ((num > 0.001f) ? (contentMin / num) : 1f));
		}
		else
		{
			contentMin = Mathf.Clamp01(-contentMin / contentSize);
			contentMax = Mathf.Clamp01(-contentMax / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((num > 0.001f) ? (1f - contentMin / num) : 0f) : ((num > 0.001f) ? (contentMin / num) : 1f));
			if (contentSize > 0f)
			{
				contentMin = Mathf.Clamp01(contentMin / contentSize);
				contentMax = Mathf.Clamp01(contentMax / contentSize);
				num = contentMin + contentMax;
			}
		}
		UIScrollBar uiscrollBar = slider as UIScrollBar;
		if (uiscrollBar != null)
		{
			uiscrollBar.barSize = 1f - num;
		}
		this.mIgnoreCallbacks = false;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0001D2EC File Offset: 0x0001B4EC
	public virtual void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		if (this.mPanel == null)
		{
			this.mPanel = base.GetComponent<UIPanel>();
		}
		this.DisableSpring();
		Bounds bounds = this.bounds;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.y)
		{
			return;
		}
		Vector4 finalClipRegion = this.mPanel.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= this.mPanel.clipSoftness.x;
			num4 += this.mPanel.clipSoftness.x;
			num5 -= this.mPanel.clipSoftness.y;
			num6 += this.mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = this.mTrans.localPosition;
			if (this.canMoveHorizontally)
			{
				localPosition.x += finalClipRegion.x - num7;
			}
			if (this.canMoveVertically)
			{
				localPosition.y += finalClipRegion.y - num8;
			}
			this.mTrans.localPosition = localPosition;
		}
		if (this.canMoveHorizontally)
		{
			finalClipRegion.x = num7;
		}
		if (this.canMoveVertically)
		{
			finalClipRegion.y = num8;
		}
		Vector4 baseClipRegion = this.mPanel.baseClipRegion;
		this.mPanel.clipOffset = new Vector2(finalClipRegion.x - baseClipRegion.x, finalClipRegion.y - baseClipRegion.y);
		if (updateScrollbars)
		{
			this.UpdateScrollbars(this.mDragID == -10);
		}
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
	public void InvalidateBounds()
	{
		this.mCalculatedBounds = false;
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0001D504 File Offset: 0x0001B704
	[ContextMenu("Reset Clipping Position")]
	public void ResetPosition()
	{
		if (NGUITools.GetActive(this))
		{
			this.mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.contentPivot);
			this.SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, false);
			this.SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, true);
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0001D560 File Offset: 0x0001B760
	public void UpdatePosition()
	{
		if (!this.mIgnoreCallbacks && (this.horizontalScrollBar != null || this.verticalScrollBar != null))
		{
			this.mIgnoreCallbacks = true;
			this.mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.contentPivot);
			float x = (this.horizontalScrollBar != null) ? this.horizontalScrollBar.value : pivotOffset.x;
			float y = (this.verticalScrollBar != null) ? this.verticalScrollBar.value : (1f - pivotOffset.y);
			this.SetDragAmount(x, y, false);
			this.UpdateScrollbars(true);
			this.mIgnoreCallbacks = false;
		}
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0001D610 File Offset: 0x0001B810
	public void OnScrollBar()
	{
		if (!this.mIgnoreCallbacks)
		{
			this.mIgnoreCallbacks = true;
			float x = (this.horizontalScrollBar != null) ? this.horizontalScrollBar.value : 0f;
			float y = (this.verticalScrollBar != null) ? this.verticalScrollBar.value : 0f;
			this.SetDragAmount(x, y, false);
			this.mIgnoreCallbacks = false;
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0001D680 File Offset: 0x0001B880
	public virtual void MoveRelative(Vector3 relative)
	{
		this.mTrans.localPosition += relative;
		Vector2 clipOffset = this.mPanel.clipOffset;
		clipOffset.x -= relative.x;
		clipOffset.y -= relative.y;
		this.mPanel.clipOffset = clipOffset;
		this.UpdateScrollbars(false);
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
	public void MoveAbsolute(Vector3 absolute)
	{
		Vector3 a = this.mTrans.InverseTransformPoint(absolute);
		Vector3 b = this.mTrans.InverseTransformPoint(Vector3.zero);
		this.MoveRelative(a - b);
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0001D720 File Offset: 0x0001B920
	public void Press(bool pressed)
	{
		if (this.mPressed == pressed || UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		if (this.smoothDragStart && pressed)
		{
			this.mDragStarted = false;
			this.mDragStartOffset = Vector2.zero;
		}
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (!pressed && this.mDragID == UICamera.currentTouchID)
			{
				this.mDragID = -10;
			}
			this.mCalculatedBounds = false;
			this.mShouldMove = this.shouldMove;
			if (!this.mShouldMove)
			{
				return;
			}
			this.mPressed = pressed;
			if (pressed)
			{
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				this.DisableSpring();
				this.mLastPos = UICamera.lastWorldPosition;
				this.mPlane = new Plane(this.mTrans.rotation * Vector3.back, this.mLastPos);
				Vector2 clipOffset = this.mPanel.clipOffset;
				clipOffset.x = Mathf.Round(clipOffset.x);
				clipOffset.y = Mathf.Round(clipOffset.y);
				this.mPanel.clipOffset = clipOffset;
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				this.mTrans.localPosition = localPosition;
				if (!this.smoothDragStart)
				{
					this.mDragStarted = true;
					this.mDragStartOffset = Vector2.zero;
					if (this.onDragStarted != null)
					{
						this.onDragStarted();
						return;
					}
				}
			}
			else
			{
				if (this.centerOnChild)
				{
					if (this.mDragStarted && this.onDragFinished != null)
					{
						this.onDragFinished();
					}
					this.centerOnChild.Recenter();
					return;
				}
				if (this.mDragStarted && this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
				{
					this.RestrictWithinBounds(this.dragEffect == UIScrollView.DragEffect.None, this.canMoveHorizontally, this.canMoveVertically);
				}
				if (this.mDragStarted && this.onDragFinished != null)
				{
					this.onDragFinished();
				}
				if (!this.mShouldMove && this.onStoppedMoving != null)
				{
					this.onStoppedMoving();
				}
			}
		}
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0001D954 File Offset: 0x0001BB54
	public void Drag()
	{
		if (!this.mPressed || UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.mShouldMove)
		{
			if (this.mDragID == -10)
			{
				this.mDragID = UICamera.currentTouchID;
			}
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			if (this.smoothDragStart && !this.mDragStarted)
			{
				this.mDragStarted = true;
				this.mDragStartOffset = UICamera.currentTouch.totalDelta;
				if (this.onDragStarted != null)
				{
					this.onDragStarted();
				}
			}
			Ray ray = this.smoothDragStart ? UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos - this.mDragStartOffset) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float distance = 0f;
			if (this.mPlane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (vector.x != 0f || vector.y != 0f || vector.z != 0f)
				{
					vector = this.mTrans.InverseTransformDirection(vector);
					if (this.movement == UIScrollView.Movement.Horizontal)
					{
						vector.y = 0f;
						vector.z = 0f;
					}
					else if (this.movement == UIScrollView.Movement.Vertical)
					{
						vector.x = 0f;
						vector.z = 0f;
					}
					else if (this.movement == UIScrollView.Movement.Unrestricted)
					{
						vector.z = 0f;
					}
					else
					{
						vector.Scale(this.customMovement);
					}
					vector = this.mTrans.TransformDirection(vector);
				}
				if (this.dragEffect == UIScrollView.DragEffect.None)
				{
					this.mMomentum = Vector3.zero;
				}
				else
				{
					this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				}
				if (!this.iOSDragEmulation || this.dragEffect != UIScrollView.DragEffect.MomentumAndSpring)
				{
					this.MoveAbsolute(vector);
				}
				else
				{
					Vector3 vector2 = this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max);
					if (this.movement == UIScrollView.Movement.Horizontal)
					{
						vector2.y = 0f;
					}
					else if (this.movement == UIScrollView.Movement.Vertical)
					{
						vector2.x = 0f;
					}
					else if (this.movement == UIScrollView.Movement.Custom)
					{
						vector2.x *= this.customMovement.x;
						vector2.y *= this.customMovement.y;
					}
					if (vector2.magnitude > 1f)
					{
						this.MoveAbsolute(vector * 0.5f);
						this.mMomentum *= 0.5f;
					}
					else
					{
						this.MoveAbsolute(vector);
					}
				}
				if (this.constrainOnDrag && this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect != UIScrollView.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(true, this.canMoveHorizontally, this.canMoveVertically);
				}
			}
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0001DC98 File Offset: 0x0001BE98
	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.scrollWheelFactor != 0f)
		{
			this.DisableSpring();
			this.mShouldMove |= this.shouldMove;
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0001DD14 File Offset: 0x0001BF14
	private void LateUpdate()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		if (this.showScrollBars != UIScrollView.ShowCondition.Always && (this.verticalScrollBar || this.horizontalScrollBar))
		{
			bool flag = false;
			bool flag2 = false;
			if (this.showScrollBars != UIScrollView.ShowCondition.WhenDragging || this.mDragID != -10 || this.mMomentum.magnitude > 0.01f)
			{
				flag = this.shouldMoveVertically;
				flag2 = this.shouldMoveHorizontally;
			}
			if (this.verticalScrollBar)
			{
				float num = this.verticalScrollBar.alpha;
				num += (flag ? (deltaTime * 6f) : (-deltaTime * 3f));
				num = Mathf.Clamp01(num);
				if (this.verticalScrollBar.alpha != num)
				{
					this.verticalScrollBar.alpha = num;
				}
			}
			if (this.horizontalScrollBar)
			{
				float num2 = this.horizontalScrollBar.alpha;
				num2 += (flag2 ? (deltaTime * 6f) : (-deltaTime * 3f));
				num2 = Mathf.Clamp01(num2);
				if (this.horizontalScrollBar.alpha != num2)
				{
					this.horizontalScrollBar.alpha = num2;
				}
			}
		}
		if (!this.mShouldMove)
		{
			return;
		}
		if (!this.mPressed)
		{
			if (this.mMomentum.magnitude > 0.0001f || Mathf.Abs(this.mScroll) > 0.0001f)
			{
				if (this.movement == UIScrollView.Movement.Horizontal)
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(this.mScroll * 0.05f, 0f, 0f));
				}
				else if (this.movement == UIScrollView.Movement.Vertical)
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(0f, this.mScroll * 0.05f, 0f));
				}
				else if (this.movement == UIScrollView.Movement.Unrestricted)
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(this.mScroll * 0.05f, this.mScroll * 0.05f, 0f));
				}
				else
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(this.mScroll * this.customMovement.x * 0.05f, this.mScroll * this.customMovement.y * 0.05f, 0f));
				}
				this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
				Vector3 absolute = NGUIMath.SpringDampen(ref this.mMomentum, this.dampenStrength, deltaTime);
				this.MoveAbsolute(absolute);
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
				{
					if (NGUITools.GetActive(this.centerOnChild))
					{
						if (this.centerOnChild.nextPageThreshold != 0f)
						{
							this.mMomentum = Vector3.zero;
							this.mScroll = 0f;
						}
						else
						{
							this.centerOnChild.Recenter();
						}
					}
					else
					{
						this.RestrictWithinBounds(false, this.canMoveHorizontally, this.canMoveVertically);
					}
				}
				if (this.onMomentumMove != null)
				{
					this.onMomentumMove();
					return;
				}
			}
			else
			{
				this.mScroll = 0f;
				this.mMomentum = Vector3.zero;
				SpringPanel component = base.GetComponent<SpringPanel>();
				if (component != null && component.enabled)
				{
					return;
				}
				this.mShouldMove = false;
				if (this.onStoppedMoving != null)
				{
					this.onStoppedMoving();
					return;
				}
			}
		}
		else
		{
			this.mScroll = 0f;
			NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
	public void OnPan(Vector2 delta)
	{
		if (this.horizontalScrollBar != null)
		{
			this.horizontalScrollBar.OnPan(delta);
		}
		if (this.verticalScrollBar != null)
		{
			this.verticalScrollBar.OnPan(delta);
		}
		if (this.horizontalScrollBar == null && this.verticalScrollBar == null)
		{
			if (this.canMoveHorizontally)
			{
				this.Scroll(delta.x);
				return;
			}
			if (this.canMoveVertically)
			{
				this.Scroll(delta.y);
			}
		}
	}

	// Token: 0x0400042A RID: 1066
	public static BetterList<UIScrollView> list = new BetterList<UIScrollView>();

	// Token: 0x0400042B RID: 1067
	public UIScrollView.Movement movement;

	// Token: 0x0400042C RID: 1068
	public UIScrollView.DragEffect dragEffect = UIScrollView.DragEffect.MomentumAndSpring;

	// Token: 0x0400042D RID: 1069
	public bool restrictWithinPanel = true;

	// Token: 0x0400042E RID: 1070
	[Tooltip("Whether the scroll view will execute its constrain within bounds logic on every drag operation")]
	public bool constrainOnDrag;

	// Token: 0x0400042F RID: 1071
	public bool disableDragIfFits;

	// Token: 0x04000430 RID: 1072
	public bool smoothDragStart = true;

	// Token: 0x04000431 RID: 1073
	public bool iOSDragEmulation = true;

	// Token: 0x04000432 RID: 1074
	public float scrollWheelFactor = 0.25f;

	// Token: 0x04000433 RID: 1075
	public float momentumAmount = 35f;

	// Token: 0x04000434 RID: 1076
	public float dampenStrength = 9f;

	// Token: 0x04000435 RID: 1077
	public UIProgressBar horizontalScrollBar;

	// Token: 0x04000436 RID: 1078
	public UIProgressBar verticalScrollBar;

	// Token: 0x04000437 RID: 1079
	public UIScrollView.ShowCondition showScrollBars = UIScrollView.ShowCondition.OnlyIfNeeded;

	// Token: 0x04000438 RID: 1080
	public Vector2 customMovement = new Vector2(1f, 0f);

	// Token: 0x04000439 RID: 1081
	public UIWidget.Pivot contentPivot;

	// Token: 0x0400043A RID: 1082
	public UIScrollView.OnDragNotification onDragStarted;

	// Token: 0x0400043B RID: 1083
	public UIScrollView.OnDragNotification onDragFinished;

	// Token: 0x0400043C RID: 1084
	public UIScrollView.OnDragNotification onMomentumMove;

	// Token: 0x0400043D RID: 1085
	public UIScrollView.OnDragNotification onStoppedMoving;

	// Token: 0x0400043E RID: 1086
	[HideInInspector]
	[SerializeField]
	private Vector3 scale = new Vector3(1f, 0f, 0f);

	// Token: 0x0400043F RID: 1087
	[SerializeField]
	[HideInInspector]
	private Vector2 relativePositionOnReset = Vector2.zero;

	// Token: 0x04000440 RID: 1088
	protected Transform mTrans;

	// Token: 0x04000441 RID: 1089
	protected UIPanel mPanel;

	// Token: 0x04000442 RID: 1090
	protected Plane mPlane;

	// Token: 0x04000443 RID: 1091
	protected Vector3 mLastPos;

	// Token: 0x04000444 RID: 1092
	protected bool mPressed;

	// Token: 0x04000445 RID: 1093
	protected Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000446 RID: 1094
	protected float mScroll;

	// Token: 0x04000447 RID: 1095
	protected Bounds mBounds;

	// Token: 0x04000448 RID: 1096
	protected bool mCalculatedBounds;

	// Token: 0x04000449 RID: 1097
	protected bool mShouldMove;

	// Token: 0x0400044A RID: 1098
	protected bool mIgnoreCallbacks;

	// Token: 0x0400044B RID: 1099
	protected int mDragID = -10;

	// Token: 0x0400044C RID: 1100
	protected Vector2 mDragStartOffset = Vector2.zero;

	// Token: 0x0400044D RID: 1101
	protected bool mDragStarted;

	// Token: 0x0400044E RID: 1102
	[NonSerialized]
	private bool mStarted;

	// Token: 0x0400044F RID: 1103
	[HideInInspector]
	public UICenterOnChild centerOnChild;

	// Token: 0x02000625 RID: 1573
	[DoNotObfuscateNGUI]
	public enum Movement
	{
		// Token: 0x040044D2 RID: 17618
		Horizontal,
		// Token: 0x040044D3 RID: 17619
		Vertical,
		// Token: 0x040044D4 RID: 17620
		Unrestricted,
		// Token: 0x040044D5 RID: 17621
		Custom
	}

	// Token: 0x02000626 RID: 1574
	[DoNotObfuscateNGUI]
	public enum DragEffect
	{
		// Token: 0x040044D7 RID: 17623
		None,
		// Token: 0x040044D8 RID: 17624
		Momentum,
		// Token: 0x040044D9 RID: 17625
		MomentumAndSpring
	}

	// Token: 0x02000627 RID: 1575
	[DoNotObfuscateNGUI]
	public enum ShowCondition
	{
		// Token: 0x040044DB RID: 17627
		Always,
		// Token: 0x040044DC RID: 17628
		OnlyIfNeeded,
		// Token: 0x040044DD RID: 17629
		WhenDragging
	}

	// Token: 0x02000628 RID: 1576
	// (Invoke) Token: 0x06002A73 RID: 10867
	public delegate void OnDragNotification();
}
