using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	// Token: 0x060005A3 RID: 1443 RVA: 0x000340B6 File Offset: 0x000322B6
	private void Start()
	{
		this.mTrans = base.transform;
		if (this.updateScrollView)
		{
			this.mSv = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x000340E0 File Offset: 0x000322E0
	private void Update()
	{
		float deltaTime = this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).sqrMagnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).sqrMagnitude)
			{
				this.mTrans.position = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).sqrMagnitude * 1E-05f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).sqrMagnitude)
			{
				this.mTrans.localPosition = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		if (this.mSv != null)
		{
			this.mSv.UpdateScrollbars(true);
		}
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00034278 File Offset: 0x00032478
	private void NotifyListeners()
	{
		SpringPosition.current = this;
		if (this.onFinished != null)
		{
			this.onFinished();
		}
		if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
		{
			this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
		}
		SpringPosition.current = null;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x000342D4 File Offset: 0x000324D4
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x040005AD RID: 1453
	public static SpringPosition current;

	// Token: 0x040005AE RID: 1454
	public Vector3 target = Vector3.zero;

	// Token: 0x040005AF RID: 1455
	public float strength = 10f;

	// Token: 0x040005B0 RID: 1456
	public bool worldSpace;

	// Token: 0x040005B1 RID: 1457
	public bool ignoreTimeScale;

	// Token: 0x040005B2 RID: 1458
	public bool updateScrollView;

	// Token: 0x040005B3 RID: 1459
	public SpringPosition.OnFinished onFinished;

	// Token: 0x040005B4 RID: 1460
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x040005B5 RID: 1461
	[SerializeField]
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x040005B6 RID: 1462
	private Transform mTrans;

	// Token: 0x040005B7 RID: 1463
	private float mThreshold;

	// Token: 0x040005B8 RID: 1464
	private UIScrollView mSv;

	// Token: 0x02000652 RID: 1618
	// (Invoke) Token: 0x06002AE1 RID: 10977
	public delegate void OnFinished();
}
