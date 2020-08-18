using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
	// Token: 0x060008FA RID: 2298 RVA: 0x000473A8 File Offset: 0x000455A8
	private void Awake()
	{
		this.mAnim = base.GetComponent<Animation>();
		this.mRect = default(Rect);
		this.mTrans = base.transform;
		this.mWidget = base.GetComponent<UIWidget>();
		this.mSprite = base.GetComponent<UISprite>();
		this.mPanel = base.GetComponent<UIPanel>();
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0004741D File Offset: 0x0004561D
	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0004743F File Offset: 0x0004563F
	private void ScreenSizeChanged()
	{
		if (this.mStarted && this.runOnlyOnce)
		{
			this.Update();
		}
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00047458 File Offset: 0x00045658
	private void Start()
	{
		if (this.container == null && this.widgetContainer != null)
		{
			this.container = this.widgetContainer.gameObject;
			this.widgetContainer = null;
		}
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		this.Update();
		this.mStarted = true;
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x000474DC File Offset: 0x000456DC
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.isPlaying)
		{
			return;
		}
		if (this.style != UIStretch.Style.None)
		{
			UIWidget uiwidget = (this.container == null) ? null : this.container.GetComponent<UIWidget>();
			UIPanel uipanel = (this.container == null && uiwidget == null) ? null : this.container.GetComponent<UIPanel>();
			float num = 1f;
			if (uiwidget != null)
			{
				Bounds bounds = uiwidget.CalculateBounds(base.transform.parent);
				this.mRect.x = bounds.min.x;
				this.mRect.y = bounds.min.y;
				this.mRect.width = bounds.size.x;
				this.mRect.height = bounds.size.y;
			}
			else if (uipanel != null)
			{
				if (uipanel.clipping == UIDrawCall.Clipping.None)
				{
					float num2 = (this.mRoot != null) ? ((float)this.mRoot.activeHeight / (float)Screen.height * 0.5f) : 0.5f;
					this.mRect.xMin = (float)(-(float)Screen.width) * num2;
					this.mRect.yMin = (float)(-(float)Screen.height) * num2;
					this.mRect.xMax = -this.mRect.xMin;
					this.mRect.yMax = -this.mRect.yMin;
				}
				else
				{
					Vector4 finalClipRegion = uipanel.finalClipRegion;
					this.mRect.x = finalClipRegion.x - finalClipRegion.z * 0.5f;
					this.mRect.y = finalClipRegion.y - finalClipRegion.w * 0.5f;
					this.mRect.width = finalClipRegion.z;
					this.mRect.height = finalClipRegion.w;
				}
			}
			else if (this.container != null)
			{
				Transform parent = base.transform.parent;
				Bounds bounds2 = (parent != null) ? NGUIMath.CalculateRelativeWidgetBounds(parent, this.container.transform) : NGUIMath.CalculateRelativeWidgetBounds(this.container.transform);
				this.mRect.x = bounds2.min.x;
				this.mRect.y = bounds2.min.y;
				this.mRect.width = bounds2.size.x;
				this.mRect.height = bounds2.size.y;
			}
			else
			{
				if (!(this.uiCamera != null))
				{
					return;
				}
				this.mRect = this.uiCamera.pixelRect;
				if (this.mRoot != null)
				{
					num = this.mRoot.pixelSizeAdjustment;
				}
			}
			float num3 = this.mRect.width;
			float num4 = this.mRect.height;
			if (num != 1f && num4 > 1f)
			{
				float num5 = (float)this.mRoot.activeHeight / num4;
				num3 *= num5;
				num4 *= num5;
			}
			Vector3 vector = (this.mWidget != null) ? new Vector3((float)this.mWidget.width, (float)this.mWidget.height) : this.mTrans.localScale;
			if (this.style == UIStretch.Style.BasedOnHeight)
			{
				vector.x = this.relativeSize.x * num4;
				vector.y = this.relativeSize.y * num4;
			}
			else if (this.style == UIStretch.Style.FillKeepingRatio)
			{
				float num6 = num3 / num4;
				if (this.initialSize.x / this.initialSize.y < num6)
				{
					float num7 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num7;
				}
				else
				{
					float num8 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num8;
					vector.y = num4;
				}
			}
			else if (this.style == UIStretch.Style.FitInternalKeepingRatio)
			{
				float num9 = num3 / num4;
				if (this.initialSize.x / this.initialSize.y > num9)
				{
					float num10 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num10;
				}
				else
				{
					float num11 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num11;
					vector.y = num4;
				}
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x = this.relativeSize.x * num3;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y = this.relativeSize.y * num4;
				}
			}
			if (this.mSprite != null)
			{
				float num12 = (this.mSprite.atlas != null) ? this.mSprite.pixelSize : 1f;
				vector.x -= this.borderPadding.x * num12;
				vector.y -= this.borderPadding.y * num12;
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mSprite.width = Mathf.RoundToInt(vector.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mSprite.height = Mathf.RoundToInt(vector.y);
				}
				vector = Vector3.one;
			}
			else if (this.mWidget != null)
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mWidget.width = Mathf.RoundToInt(vector.x - this.borderPadding.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mWidget.height = Mathf.RoundToInt(vector.y - this.borderPadding.y);
				}
				vector = Vector3.one;
			}
			else if (this.mPanel != null)
			{
				Vector4 baseClipRegion = this.mPanel.baseClipRegion;
				if (this.style != UIStretch.Style.Vertical)
				{
					baseClipRegion.z = vector.x - this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					baseClipRegion.w = vector.y - this.borderPadding.y;
				}
				this.mPanel.baseClipRegion = baseClipRegion;
				vector = Vector3.one;
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x -= this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y -= this.borderPadding.y;
				}
			}
			if (this.mTrans.localScale != vector)
			{
				this.mTrans.localScale = vector;
			}
			if (this.runOnlyOnce && Application.isPlaying)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x04000791 RID: 1937
	public Camera uiCamera;

	// Token: 0x04000792 RID: 1938
	public GameObject container;

	// Token: 0x04000793 RID: 1939
	public UIStretch.Style style;

	// Token: 0x04000794 RID: 1940
	public bool runOnlyOnce = true;

	// Token: 0x04000795 RID: 1941
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x04000796 RID: 1942
	public Vector2 initialSize = Vector2.one;

	// Token: 0x04000797 RID: 1943
	public Vector2 borderPadding = Vector2.zero;

	// Token: 0x04000798 RID: 1944
	[HideInInspector]
	[SerializeField]
	private UIWidget widgetContainer;

	// Token: 0x04000799 RID: 1945
	private Transform mTrans;

	// Token: 0x0400079A RID: 1946
	private UIWidget mWidget;

	// Token: 0x0400079B RID: 1947
	private UISprite mSprite;

	// Token: 0x0400079C RID: 1948
	private UIPanel mPanel;

	// Token: 0x0400079D RID: 1949
	private UIRoot mRoot;

	// Token: 0x0400079E RID: 1950
	private Animation mAnim;

	// Token: 0x0400079F RID: 1951
	private Rect mRect;

	// Token: 0x040007A0 RID: 1952
	private bool mStarted;

	// Token: 0x0200068E RID: 1678
	[DoNotObfuscateNGUI]
	public enum Style
	{
		// Token: 0x040045EF RID: 17903
		None,
		// Token: 0x040045F0 RID: 17904
		Horizontal,
		// Token: 0x040045F1 RID: 17905
		Vertical,
		// Token: 0x040045F2 RID: 17906
		Both,
		// Token: 0x040045F3 RID: 17907
		BasedOnHeight,
		// Token: 0x040045F4 RID: 17908
		FillKeepingRatio,
		// Token: 0x040045F5 RID: 17909
		FitInternalKeepingRatio
	}
}
