using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000925 RID: 2341 RVA: 0x0004894D File Offset: 0x00046B4D
	public static bool isVisible
	{
		get
		{
			return UITooltip.mInstance != null && UITooltip.mInstance.mTarget == 1f;
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0004896F File Offset: 0x00046B6F
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00048977 File Offset: 0x00046B77
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00048980 File Offset: 0x00046B80
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x000489E8 File Offset: 0x00046BE8
	protected virtual void Update()
	{
		if (this.mTooltip != UICamera.tooltipObject)
		{
			this.mTooltip = null;
			this.mTarget = 0f;
		}
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, RealTime.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 vector = this.mSize * 0.25f;
				vector.y = -vector.y;
				Vector3 localScale = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(this.mPos - vector, this.mPos, this.mCurrent);
				this.mTrans.localPosition = localPosition;
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00048B00 File Offset: 0x00046D00
	protected virtual void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00048B40 File Offset: 0x00046D40
	protected virtual void SetText(string tooltipText)
	{
		if (!(this.text != null) || string.IsNullOrEmpty(tooltipText))
		{
			this.mTooltip = null;
			this.mTarget = 0f;
			return;
		}
		this.mTarget = 1f;
		this.mTooltip = UICamera.tooltipObject;
		this.text.text = tooltipText;
		this.mPos = UICamera.lastEventPosition;
		Transform transform = this.text.transform;
		Vector3 localPosition = transform.localPosition;
		Vector3 localScale = transform.localScale;
		this.mSize = this.text.printedSize;
		this.mSize.x = this.mSize.x * localScale.x;
		this.mSize.y = this.mSize.y * localScale.y;
		if (this.background != null)
		{
			Vector4 border = this.background.border;
			this.mSize.x = this.mSize.x + (border.x + border.z + (localPosition.x - border.x) * 2f);
			this.mSize.y = this.mSize.y + (border.y + border.w + (-localPosition.y - border.y) * 2f);
			this.background.width = Mathf.RoundToInt(this.mSize.x);
			this.background.height = Mathf.RoundToInt(this.mSize.y);
		}
		if (this.uiCamera != null)
		{
			this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
			this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
			float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
			float num2 = (float)Screen.height * 0.5f / num;
			Vector2 vector = new Vector2(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
			this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
			this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
			this.mPos = this.mTrans.localPosition;
			this.mPos.x = Mathf.Round(this.mPos.x);
			this.mPos.y = Mathf.Round(this.mPos.y);
		}
		else
		{
			if (this.mPos.x + this.mSize.x > (float)Screen.width)
			{
				this.mPos.x = (float)Screen.width - this.mSize.x;
			}
			if (this.mPos.y - this.mSize.y < 0f)
			{
				this.mPos.y = this.mSize.y;
			}
			this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
			this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
		}
		this.mTrans.localPosition = this.mPos;
		if (this.tooltipRoot != null)
		{
			this.tooltipRoot.BroadcastMessage("UpdateAnchors");
			return;
		}
		this.text.BroadcastMessage("UpdateAnchors");
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00048F0A File Offset: 0x0004710A
	[Obsolete("Use UITooltip.Show instead")]
	public static void ShowText(string text)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(text);
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00048F0A File Offset: 0x0004710A
	public static void Show(string text)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(text);
		}
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00048F24 File Offset: 0x00047124
	public static void Hide()
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.mTooltip = null;
			UITooltip.mInstance.mTarget = 0f;
		}
	}

	// Token: 0x040007B2 RID: 1970
	protected static UITooltip mInstance;

	// Token: 0x040007B3 RID: 1971
	public Camera uiCamera;

	// Token: 0x040007B4 RID: 1972
	public UILabel text;

	// Token: 0x040007B5 RID: 1973
	public GameObject tooltipRoot;

	// Token: 0x040007B6 RID: 1974
	public UISprite background;

	// Token: 0x040007B7 RID: 1975
	public float appearSpeed = 10f;

	// Token: 0x040007B8 RID: 1976
	public bool scalingTransitions = true;

	// Token: 0x040007B9 RID: 1977
	protected GameObject mTooltip;

	// Token: 0x040007BA RID: 1978
	protected Transform mTrans;

	// Token: 0x040007BB RID: 1979
	protected float mTarget;

	// Token: 0x040007BC RID: 1980
	protected float mCurrent;

	// Token: 0x040007BD RID: 1981
	protected Vector3 mPos;

	// Token: 0x040007BE RID: 1982
	protected Vector3 mSize = Vector3.zero;

	// Token: 0x040007BF RID: 1983
	protected UIWidget[] mWidgets;
}
