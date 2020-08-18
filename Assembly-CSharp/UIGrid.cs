using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000057 RID: 87
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	// Token: 0x17000026 RID: 38
	// (set) Token: 0x0600020E RID: 526 RVA: 0x00017C32 File Offset: 0x00015E32
	public bool repositionNow
	{
		set
		{
			if (value)
			{
				this.mReposition = true;
				base.enabled = true;
			}
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00017C48 File Offset: 0x00015E48
	public List<Transform> GetChildList()
	{
		Transform transform = base.transform;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if ((!this.hideInactive || (child && child.gameObject.activeSelf)) && !UIDragDropItem.IsDragged(child.gameObject))
			{
				list.Add(child);
			}
		}
		if (this.sorting != UIGrid.Sorting.None && this.arrangement != UIGrid.Arrangement.CellSnap)
		{
			if (this.sorting == UIGrid.Sorting.Alphabetic)
			{
				list.Sort(new Comparison<Transform>(UIGrid.SortByName));
			}
			else if (this.sorting == UIGrid.Sorting.Horizontal)
			{
				list.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
			}
			else if (this.sorting == UIGrid.Sorting.Vertical)
			{
				list.Sort(new Comparison<Transform>(UIGrid.SortVertical));
			}
			else if (this.onCustomSort != null)
			{
				list.Sort(this.onCustomSort);
			}
			else
			{
				this.Sort(list);
			}
		}
		return list;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00017D34 File Offset: 0x00015F34
	public Transform GetChild(int index)
	{
		List<Transform> childList = this.GetChildList();
		if (index >= childList.Count)
		{
			return null;
		}
		return childList[index];
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00017D5A File Offset: 0x00015F5A
	public int GetIndex(Transform trans)
	{
		return this.GetChildList().IndexOf(trans);
	}

	// Token: 0x06000212 RID: 530 RVA: 0x00017D68 File Offset: 0x00015F68
	[Obsolete("Use gameObject.AddChild or transform.parent = gridTransform")]
	public void AddChild(Transform trans)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00017D68 File Offset: 0x00015F68
	[Obsolete("Use gameObject.AddChild or transform.parent = gridTransform")]
	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00017D8C File Offset: 0x00015F8C
	public bool RemoveChild(Transform t)
	{
		List<Transform> childList = this.GetChildList();
		if (childList.Remove(t))
		{
			this.ResetPosition(childList);
			return true;
		}
		return false;
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00017DB3 File Offset: 0x00015FB3
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06000216 RID: 534 RVA: 0x00017DD0 File Offset: 0x00015FD0
	protected virtual void Start()
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		bool flag = this.animateSmoothly;
		this.animateSmoothly = false;
		this.Reposition();
		this.animateSmoothly = flag;
		base.enabled = false;
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00017E0D File Offset: 0x0001600D
	protected virtual void Update()
	{
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00017E1C File Offset: 0x0001601C
	private void OnValidate()
	{
		if (!Application.isPlaying && NGUITools.GetActive(this))
		{
			this.Reposition();
		}
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00017E33 File Offset: 0x00016033
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00017E48 File Offset: 0x00016048
	public static int SortHorizontal(Transform a, Transform b)
	{
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	// Token: 0x0600021B RID: 539 RVA: 0x00017E74 File Offset: 0x00016074
	public static int SortVertical(Transform a, Transform b)
	{
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00002ACE File Offset: 0x00000CCE
	protected virtual void Sort(List<Transform> list)
	{
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00017EA0 File Offset: 0x000160A0
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(base.gameObject))
		{
			this.Init();
		}
		if (this.sorted)
		{
			this.sorted = false;
			if (this.sorting == UIGrid.Sorting.None)
			{
				this.sorting = UIGrid.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this, "last change");
		}
		List<Transform> childList = this.GetChildList();
		this.ResetPosition(childList);
		if (this.keepWithinPanel)
		{
			this.ConstrainWithinPanel();
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00017F28 File Offset: 0x00016128
	public void ConstrainWithinPanel()
	{
		if (this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(base.transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00017F74 File Offset: 0x00016174
	protected virtual void ResetPosition(List<Transform> list)
	{
		this.mReposition = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			Transform transform = list[i];
			Vector3 vector = transform.localPosition;
			float z = vector.z;
			if (this.arrangement == UIGrid.Arrangement.CellSnap)
			{
				if (this.cellWidth > 0f)
				{
					vector.x = Mathf.Round(vector.x / this.cellWidth) * this.cellWidth;
				}
				if (this.cellHeight > 0f)
				{
					vector.y = Mathf.Round(vector.y / this.cellHeight) * this.cellHeight;
				}
			}
			else
			{
				vector = ((this.arrangement == UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z) : new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z));
			}
			if (this.animateSmoothly && Application.isPlaying && (this.pivot != UIWidget.Pivot.TopLeft || Vector3.SqrMagnitude(transform.localPosition - vector) >= 0.0001f))
			{
				SpringPosition springPosition = SpringPosition.Begin(transform.gameObject, vector, 15f);
				springPosition.updateScrollView = true;
				springPosition.ignoreTimeScale = true;
			}
			else
			{
				transform.localPosition = vector;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= this.maxPerLine && this.maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
			i++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			float num5;
			float num6;
			if (this.arrangement == UIGrid.Arrangement.Horizontal)
			{
				num5 = Mathf.Lerp(0f, (float)num3 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num4) * this.cellHeight, 0f, pivotOffset.y);
			}
			else
			{
				num5 = Mathf.Lerp(0f, (float)num4 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num3) * this.cellHeight, 0f, pivotOffset.y);
			}
			foreach (Transform transform2 in list)
			{
				SpringPosition component = transform2.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
					SpringPosition springPosition2 = component;
					springPosition2.target.x = springPosition2.target.x - num5;
					SpringPosition springPosition3 = component;
					springPosition3.target.y = springPosition3.target.y - num6;
					component.enabled = true;
				}
				else
				{
					Vector3 localPosition = transform2.localPosition;
					localPosition.x -= num5;
					localPosition.y -= num6;
					transform2.localPosition = localPosition;
				}
			}
		}
	}

	// Token: 0x04000396 RID: 918
	public UIGrid.Arrangement arrangement;

	// Token: 0x04000397 RID: 919
	public UIGrid.Sorting sorting;

	// Token: 0x04000398 RID: 920
	public UIWidget.Pivot pivot;

	// Token: 0x04000399 RID: 921
	public int maxPerLine;

	// Token: 0x0400039A RID: 922
	public float cellWidth = 200f;

	// Token: 0x0400039B RID: 923
	public float cellHeight = 200f;

	// Token: 0x0400039C RID: 924
	public bool animateSmoothly;

	// Token: 0x0400039D RID: 925
	public bool hideInactive;

	// Token: 0x0400039E RID: 926
	public bool keepWithinPanel;

	// Token: 0x0400039F RID: 927
	public UIGrid.OnReposition onReposition;

	// Token: 0x040003A0 RID: 928
	public Comparison<Transform> onCustomSort;

	// Token: 0x040003A1 RID: 929
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x040003A2 RID: 930
	protected bool mReposition;

	// Token: 0x040003A3 RID: 931
	protected UIPanel mPanel;

	// Token: 0x040003A4 RID: 932
	protected bool mInitDone;

	// Token: 0x02000615 RID: 1557
	// (Invoke) Token: 0x06002A5B RID: 10843
	public delegate void OnReposition();

	// Token: 0x02000616 RID: 1558
	[DoNotObfuscateNGUI]
	public enum Arrangement
	{
		// Token: 0x04004494 RID: 17556
		Horizontal,
		// Token: 0x04004495 RID: 17557
		Vertical,
		// Token: 0x04004496 RID: 17558
		CellSnap
	}

	// Token: 0x02000617 RID: 1559
	[DoNotObfuscateNGUI]
	public enum Sorting
	{
		// Token: 0x04004498 RID: 17560
		None,
		// Token: 0x04004499 RID: 17561
		Alphabetic,
		// Token: 0x0400449A RID: 17562
		Horizontal,
		// Token: 0x0400449B RID: 17563
		Vertical,
		// Token: 0x0400449C RID: 17564
		Custom
	}
}
