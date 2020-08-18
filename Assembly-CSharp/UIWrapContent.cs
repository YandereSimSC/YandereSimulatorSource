using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006B RID: 107
[AddComponentMenu("NGUI/Interaction/Wrap Content")]
public class UIWrapContent : MonoBehaviour
{
	// Token: 0x06000331 RID: 817 RVA: 0x0001F4EB File Offset: 0x0001D6EB
	protected virtual void Start()
	{
		this.SortBasedOnScrollMovement();
		this.WrapContent();
		if (this.mScroll != null)
		{
			this.mScroll.GetComponent<UIPanel>().onClipMove = new UIPanel.OnClippingMoved(this.OnMove);
		}
		this.mFirstTime = false;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001F52B File Offset: 0x0001D72B
	protected virtual void OnMove(UIPanel panel)
	{
		this.WrapContent();
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0001F534 File Offset: 0x0001D734
	[ContextMenu("Sort Based on Scroll Movement")]
	public virtual void SortBasedOnScrollMovement()
	{
		if (!this.CacheScrollView())
		{
			return;
		}
		this.mChildren.Clear();
		for (int i = 0; i < this.mTrans.childCount; i++)
		{
			Transform child = this.mTrans.GetChild(i);
			if (!this.hideInactive || child.gameObject.activeInHierarchy)
			{
				this.mChildren.Add(child);
			}
		}
		if (this.mHorizontal)
		{
			this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
		}
		else
		{
			this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortVertical));
		}
		this.ResetChildPositions();
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
	[ContextMenu("Sort Alphabetically")]
	public virtual void SortAlphabetically()
	{
		if (!this.CacheScrollView())
		{
			return;
		}
		this.mChildren.Clear();
		for (int i = 0; i < this.mTrans.childCount; i++)
		{
			Transform child = this.mTrans.GetChild(i);
			if (!this.hideInactive || child.gameObject.activeInHierarchy)
			{
				this.mChildren.Add(child);
			}
		}
		this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortByName));
		this.ResetChildPositions();
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0001F65C File Offset: 0x0001D85C
	protected bool CacheScrollView()
	{
		this.mTrans = base.transform;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
		this.mScroll = this.mPanel.GetComponent<UIScrollView>();
		if (this.mScroll == null)
		{
			return false;
		}
		if (this.mScroll.movement == UIScrollView.Movement.Horizontal)
		{
			this.mHorizontal = true;
		}
		else
		{
			if (this.mScroll.movement != UIScrollView.Movement.Vertical)
			{
				return false;
			}
			this.mHorizontal = false;
		}
		return true;
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
	protected virtual void ResetChildPositions()
	{
		int i = 0;
		int count = this.mChildren.Count;
		while (i < count)
		{
			Transform transform = this.mChildren[i];
			transform.localPosition = (this.mHorizontal ? new Vector3((float)(i * this.itemSize), 0f, 0f) : new Vector3(0f, (float)(-(float)i * this.itemSize), 0f));
			this.UpdateItem(transform, i);
			i++;
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0001F754 File Offset: 0x0001D954
	public virtual void WrapContent()
	{
		float num = (float)(this.itemSize * this.mChildren.Count) * 0.5f;
		Vector3[] worldCorners = this.mPanel.worldCorners;
		for (int i = 0; i < 4; i++)
		{
			Vector3 vector = worldCorners[i];
			vector = this.mTrans.InverseTransformPoint(vector);
			worldCorners[i] = vector;
		}
		Vector3 vector2 = Vector3.Lerp(worldCorners[0], worldCorners[2], 0.5f);
		bool flag = true;
		float num2 = num * 2f;
		if (this.mHorizontal)
		{
			float num3 = worldCorners[0].x - (float)this.itemSize;
			float num4 = worldCorners[2].x + (float)this.itemSize;
			int j = 0;
			int count = this.mChildren.Count;
			while (j < count)
			{
				Transform transform = this.mChildren[j];
				float num5 = transform.localPosition.x - vector2.x;
				if (num5 < -num)
				{
					Vector3 localPosition = transform.localPosition;
					localPosition.x += num2;
					num5 = localPosition.x - vector2.x;
					int num6 = Mathf.RoundToInt(localPosition.x / (float)this.itemSize);
					if (this.minIndex == this.maxIndex || (this.minIndex <= num6 && num6 <= this.maxIndex))
					{
						transform.localPosition = localPosition;
						this.UpdateItem(transform, j);
					}
					else
					{
						flag = false;
					}
				}
				else if (num5 > num)
				{
					Vector3 localPosition2 = transform.localPosition;
					localPosition2.x -= num2;
					num5 = localPosition2.x - vector2.x;
					int num7 = Mathf.RoundToInt(localPosition2.x / (float)this.itemSize);
					if (this.minIndex == this.maxIndex || (this.minIndex <= num7 && num7 <= this.maxIndex))
					{
						transform.localPosition = localPosition2;
						this.UpdateItem(transform, j);
					}
					else
					{
						flag = false;
					}
				}
				else if (this.mFirstTime)
				{
					this.UpdateItem(transform, j);
				}
				if (this.cullContent)
				{
					num5 += this.mPanel.clipOffset.x - this.mTrans.localPosition.x;
					if (!UICamera.IsPressed(transform.gameObject))
					{
						NGUITools.SetActive(transform.gameObject, num5 > num3 && num5 < num4, false);
					}
				}
				j++;
			}
		}
		else
		{
			float num8 = worldCorners[0].y - (float)this.itemSize;
			float num9 = worldCorners[2].y + (float)this.itemSize;
			int k = 0;
			int count2 = this.mChildren.Count;
			while (k < count2)
			{
				Transform transform2 = this.mChildren[k];
				float num10 = transform2.localPosition.y - vector2.y;
				if (num10 < -num)
				{
					Vector3 localPosition3 = transform2.localPosition;
					localPosition3.y += num2;
					num10 = localPosition3.y - vector2.y;
					int num11 = Mathf.RoundToInt(localPosition3.y / (float)this.itemSize);
					if (this.minIndex == this.maxIndex || (this.minIndex <= num11 && num11 <= this.maxIndex))
					{
						transform2.localPosition = localPosition3;
						this.UpdateItem(transform2, k);
					}
					else
					{
						flag = false;
					}
				}
				else if (num10 > num)
				{
					Vector3 localPosition4 = transform2.localPosition;
					localPosition4.y -= num2;
					num10 = localPosition4.y - vector2.y;
					int num12 = Mathf.RoundToInt(localPosition4.y / (float)this.itemSize);
					if (this.minIndex == this.maxIndex || (this.minIndex <= num12 && num12 <= this.maxIndex))
					{
						transform2.localPosition = localPosition4;
						this.UpdateItem(transform2, k);
					}
					else
					{
						flag = false;
					}
				}
				else if (this.mFirstTime)
				{
					this.UpdateItem(transform2, k);
				}
				if (this.cullContent)
				{
					num10 += this.mPanel.clipOffset.y - this.mTrans.localPosition.y;
					if (!UICamera.IsPressed(transform2.gameObject))
					{
						NGUITools.SetActive(transform2.gameObject, num10 > num8 && num10 < num9, false);
					}
				}
				k++;
			}
		}
		this.mScroll.restrictWithinPanel = !flag;
		this.mScroll.InvalidateBounds();
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0001FBC2 File Offset: 0x0001DDC2
	private void OnValidate()
	{
		if (this.maxIndex < this.minIndex)
		{
			this.maxIndex = this.minIndex;
		}
		if (this.minIndex > this.maxIndex)
		{
			this.maxIndex = this.minIndex;
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0001FBF8 File Offset: 0x0001DDF8
	protected virtual void UpdateItem(Transform item, int index)
	{
		if (this.onInitializeItem != null)
		{
			int realIndex = (this.mScroll.movement == UIScrollView.Movement.Vertical) ? Mathf.RoundToInt(item.localPosition.y / (float)this.itemSize) : Mathf.RoundToInt(item.localPosition.x / (float)this.itemSize);
			this.onInitializeItem(item.gameObject, index, realIndex);
		}
	}

	// Token: 0x04000481 RID: 1153
	public int itemSize = 100;

	// Token: 0x04000482 RID: 1154
	public bool cullContent = true;

	// Token: 0x04000483 RID: 1155
	public int minIndex;

	// Token: 0x04000484 RID: 1156
	public int maxIndex;

	// Token: 0x04000485 RID: 1157
	public bool hideInactive;

	// Token: 0x04000486 RID: 1158
	public UIWrapContent.OnInitializeItem onInitializeItem;

	// Token: 0x04000487 RID: 1159
	protected Transform mTrans;

	// Token: 0x04000488 RID: 1160
	protected UIPanel mPanel;

	// Token: 0x04000489 RID: 1161
	protected UIScrollView mScroll;

	// Token: 0x0400048A RID: 1162
	protected bool mHorizontal;

	// Token: 0x0400048B RID: 1163
	protected bool mFirstTime = true;

	// Token: 0x0400048C RID: 1164
	protected List<Transform> mChildren = new List<Transform>();

	// Token: 0x0200062E RID: 1582
	// (Invoke) Token: 0x06002A7F RID: 10879
	public delegate void OnInitializeItem(GameObject go, int wrapIndex, int realIndex);
}
