using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : MonoBehaviour
{
	// Token: 0x060004B8 RID: 1208 RVA: 0x0002CD71 File Offset: 0x0002AF71
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIScrollView>();
		this.mTrans = base.transform;
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0002CD97 File Offset: 0x0002AF97
	private void Update()
	{
		this.AdvanceTowardsPosition();
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
	protected virtual void AdvanceTowardsPosition()
	{
		this.mDelta += RealTime.deltaTime;
		bool flag = false;
		Vector3 localPosition = this.mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(localPosition, this.target, this.strength, this.mDelta);
		if ((localPosition - this.target).sqrMagnitude < 0.01f)
		{
			vector = this.target;
			base.enabled = false;
			flag = true;
			this.mDelta = 0f;
		}
		else
		{
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			vector.z = Mathf.Round(vector.z);
			if ((vector - localPosition).sqrMagnitude < 0.01f)
			{
				return;
			}
			this.mDelta = 0f;
		}
		this.mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector2 clipOffset = this.mPanel.clipOffset;
		clipOffset.x -= vector2.x;
		clipOffset.y -= vector2.y;
		this.mPanel.clipOffset = clipOffset;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (flag && this.onFinished != null)
		{
			SpringPanel.current = this;
			this.onFinished();
			SpringPanel.current = null;
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0002CF04 File Offset: 0x0002B104
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		springPanel.onFinished = null;
		springPanel.enabled = true;
		return springPanel;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0002CF48 File Offset: 0x0002B148
	public static SpringPanel Stop(GameObject go)
	{
		SpringPanel component = go.GetComponent<SpringPanel>();
		if (component != null && component.enabled)
		{
			if (component.onFinished != null)
			{
				component.onFinished();
			}
			component.enabled = false;
		}
		return component;
	}

	// Token: 0x0400050F RID: 1295
	public static SpringPanel current;

	// Token: 0x04000510 RID: 1296
	public Vector3 target = Vector3.zero;

	// Token: 0x04000511 RID: 1297
	public float strength = 10f;

	// Token: 0x04000512 RID: 1298
	public SpringPanel.OnFinished onFinished;

	// Token: 0x04000513 RID: 1299
	[NonSerialized]
	private UIPanel mPanel;

	// Token: 0x04000514 RID: 1300
	[NonSerialized]
	private Transform mTrans;

	// Token: 0x04000515 RID: 1301
	[NonSerialized]
	private UIScrollView mDrag;

	// Token: 0x04000516 RID: 1302
	[NonSerialized]
	private float mDelta;

	// Token: 0x0200063B RID: 1595
	// (Invoke) Token: 0x06002AA4 RID: 10916
	public delegate void OnFinished();
}
