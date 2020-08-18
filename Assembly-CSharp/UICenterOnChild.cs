using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004B RID: 75
[AddComponentMenu("NGUI/Interaction/Center Scroll View on Child")]
public class UICenterOnChild : MonoBehaviour
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060001A4 RID: 420 RVA: 0x00015256 File Offset: 0x00013456
	public GameObject centeredObject
	{
		get
		{
			return this.mCenteredObject;
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0001525E File Offset: 0x0001345E
	private void Start()
	{
		this.Recenter();
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00015266 File Offset: 0x00013466
	private void OnEnable()
	{
		if (this.mScrollView)
		{
			this.mScrollView.centerOnChild = this;
			this.Recenter();
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00015287 File Offset: 0x00013487
	private void OnDisable()
	{
		if (this.mScrollView)
		{
			this.mScrollView.centerOnChild = null;
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x000152A2 File Offset: 0x000134A2
	private void OnDragFinished()
	{
		if (base.enabled)
		{
			this.Recenter();
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x000152B2 File Offset: 0x000134B2
	private void OnValidate()
	{
		this.nextPageThreshold = Mathf.Abs(this.nextPageThreshold);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x000152C8 File Offset: 0x000134C8
	[ContextMenu("Execute")]
	public void Recenter()
	{
		if (this.mScrollView == null)
		{
			this.mScrollView = NGUITools.FindInParents<UIScrollView>(base.gameObject);
			if (this.mScrollView == null)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					base.GetType(),
					" requires ",
					typeof(UIScrollView),
					" on a parent object in order to work"
				}), this);
				base.enabled = false;
				return;
			}
			if (this.mScrollView)
			{
				this.mScrollView.centerOnChild = this;
			}
			if (this.mScrollView.horizontalScrollBar != null)
			{
				UIProgressBar horizontalScrollBar = this.mScrollView.horizontalScrollBar;
				horizontalScrollBar.onDragFinished = (UIProgressBar.OnDragFinished)Delegate.Combine(horizontalScrollBar.onDragFinished, new UIProgressBar.OnDragFinished(this.OnDragFinished));
			}
			if (this.mScrollView.verticalScrollBar != null)
			{
				UIProgressBar verticalScrollBar = this.mScrollView.verticalScrollBar;
				verticalScrollBar.onDragFinished = (UIProgressBar.OnDragFinished)Delegate.Combine(verticalScrollBar.onDragFinished, new UIProgressBar.OnDragFinished(this.OnDragFinished));
			}
		}
		if (this.mScrollView.panel == null)
		{
			return;
		}
		Transform transform = base.transform;
		if (transform.childCount == 0)
		{
			return;
		}
		Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
		Vector3 vector = (worldCorners[2] + worldCorners[0]) * 0.5f;
		Vector3 vector2 = this.mScrollView.currentMomentum * this.mScrollView.momentumAmount;
		Vector3 a = NGUIMath.SpringDampen(ref vector2, 9f, 2f);
		Vector3 b = vector - a * 0.01f;
		float num = float.MaxValue;
		Transform target = null;
		int index = 0;
		int num2 = 0;
		UIGrid component = base.GetComponent<UIGrid>();
		List<Transform> list = null;
		if (component != null)
		{
			list = component.GetChildList();
			int i = 0;
			int count = list.Count;
			int num3 = 0;
			while (i < count)
			{
				Transform transform2 = list[i];
				if (transform2.gameObject.activeInHierarchy)
				{
					float num4 = Vector3.SqrMagnitude(transform2.position - b);
					if (num4 < num)
					{
						num = num4;
						target = transform2;
						index = i;
						num2 = num3;
					}
					num3++;
				}
				i++;
			}
		}
		else
		{
			int j = 0;
			int childCount = transform.childCount;
			int num5 = 0;
			while (j < childCount)
			{
				Transform child = transform.GetChild(j);
				if (child.gameObject.activeInHierarchy)
				{
					float num6 = Vector3.SqrMagnitude(child.position - b);
					if (num6 < num)
					{
						num = num6;
						target = child;
						index = j;
						num2 = num5;
					}
					num5++;
				}
				j++;
			}
		}
		if (this.nextPageThreshold > 0f && UICamera.currentTouch != null && this.mCenteredObject != null && this.mCenteredObject.transform == ((list != null) ? list[index] : transform.GetChild(index)))
		{
			Vector3 vector3 = UICamera.currentTouch.totalDelta;
			vector3 = base.transform.rotation * vector3;
			UIScrollView.Movement movement = this.mScrollView.movement;
			float num7;
			if (movement != UIScrollView.Movement.Horizontal)
			{
				if (movement != UIScrollView.Movement.Vertical)
				{
					num7 = vector3.magnitude;
				}
				else
				{
					num7 = -vector3.y;
				}
			}
			else
			{
				num7 = vector3.x;
			}
			if (Mathf.Abs(num7) > this.nextPageThreshold)
			{
				if (num7 > this.nextPageThreshold)
				{
					if (list != null)
					{
						if (num2 > 0)
						{
							target = list[num2 - 1];
						}
						else
						{
							target = ((base.GetComponent<UIWrapContent>() == null) ? list[0] : list[list.Count - 1]);
						}
					}
					else if (num2 > 0)
					{
						target = transform.GetChild(num2 - 1);
					}
					else
					{
						target = ((base.GetComponent<UIWrapContent>() == null) ? transform.GetChild(0) : transform.GetChild(transform.childCount - 1));
					}
				}
				else if (num7 < -this.nextPageThreshold)
				{
					if (list != null)
					{
						if (num2 < list.Count - 1)
						{
							target = list[num2 + 1];
						}
						else
						{
							target = ((base.GetComponent<UIWrapContent>() == null) ? list[list.Count - 1] : list[0]);
						}
					}
					else if (num2 < transform.childCount - 1)
					{
						target = transform.GetChild(num2 + 1);
					}
					else
					{
						target = ((base.GetComponent<UIWrapContent>() == null) ? transform.GetChild(transform.childCount - 1) : transform.GetChild(0));
					}
				}
			}
		}
		this.CenterOn(target, vector);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00015778 File Offset: 0x00013978
	private void CenterOn(Transform target, Vector3 panelCenter)
	{
		if (target != null && this.mScrollView != null && this.mScrollView.panel != null)
		{
			Transform cachedTransform = this.mScrollView.panel.cachedTransform;
			this.mCenteredObject = target.gameObject;
			Vector3 a = cachedTransform.InverseTransformPoint(target.position);
			Vector3 b = cachedTransform.InverseTransformPoint(panelCenter);
			Vector3 b2 = a - b;
			if (!this.mScrollView.canMoveHorizontally)
			{
				b2.x = 0f;
			}
			if (!this.mScrollView.canMoveVertically)
			{
				b2.y = 0f;
			}
			b2.z = 0f;
			Vector3 vector = cachedTransform.localPosition - b2;
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			vector.z = Mathf.Round(vector.z);
			SpringPanel.Begin(this.mScrollView.panel.cachedGameObject, vector, this.springStrength).onFinished = this.onFinished;
		}
		else
		{
			this.mCenteredObject = null;
		}
		if (this.onCenter != null)
		{
			this.onCenter(this.mCenteredObject);
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x000158BC File Offset: 0x00013ABC
	public void CenterOn(Transform target)
	{
		if (this.mScrollView != null && this.mScrollView.panel != null)
		{
			Vector3[] worldCorners = this.mScrollView.panel.worldCorners;
			Vector3 panelCenter = (worldCorners[2] + worldCorners[0]) * 0.5f;
			this.CenterOn(target, panelCenter);
		}
	}

	// Token: 0x0400032D RID: 813
	public float springStrength = 8f;

	// Token: 0x0400032E RID: 814
	public float nextPageThreshold;

	// Token: 0x0400032F RID: 815
	public SpringPanel.OnFinished onFinished;

	// Token: 0x04000330 RID: 816
	public UICenterOnChild.OnCenterCallback onCenter;

	// Token: 0x04000331 RID: 817
	private UIScrollView mScrollView;

	// Token: 0x04000332 RID: 818
	private GameObject mCenteredObject;

	// Token: 0x02000612 RID: 1554
	// (Invoke) Token: 0x06002A57 RID: 10839
	public delegate void OnCenterCallback(GameObject centeredObject);
}
