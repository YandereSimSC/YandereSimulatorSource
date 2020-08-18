using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Interaction/Envelop Content")]
public class EnvelopContent : MonoBehaviour
{
	// Token: 0x0600014C RID: 332 RVA: 0x000137B3 File Offset: 0x000119B3
	private void Start()
	{
		this.mStarted = true;
		this.Execute();
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000137C2 File Offset: 0x000119C2
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Execute();
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000137D4 File Offset: 0x000119D4
	[ContextMenu("Execute")]
	public void Execute()
	{
		if (this.targetRoot == base.transform)
		{
			Debug.LogError("Target Root object cannot be the same object that has Envelop Content. Make it a sibling instead.", this);
			return;
		}
		if (NGUITools.IsChild(this.targetRoot, base.transform))
		{
			Debug.LogError("Target Root object should not be a parent of Envelop Content. Make it a sibling instead.", this);
			return;
		}
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.transform.parent, this.targetRoot, !this.ignoreDisabled, true);
		float num = bounds.min.x + (float)this.padLeft;
		float num2 = bounds.min.y + (float)this.padBottom;
		float num3 = bounds.max.x + (float)this.padRight;
		float num4 = bounds.max.y + (float)this.padTop;
		base.GetComponent<UIWidget>().SetRect(num, num2, num3 - num, num4 - num2);
		base.BroadcastMessage("UpdateAnchors", SendMessageOptions.DontRequireReceiver);
		NGUITools.UpdateWidgetCollider(base.gameObject);
	}

	// Token: 0x040002DD RID: 733
	public Transform targetRoot;

	// Token: 0x040002DE RID: 734
	public int padLeft;

	// Token: 0x040002DF RID: 735
	public int padRight;

	// Token: 0x040002E0 RID: 736
	public int padBottom;

	// Token: 0x040002E1 RID: 737
	public int padTop;

	// Token: 0x040002E2 RID: 738
	public bool ignoreDisabled = true;

	// Token: 0x040002E3 RID: 739
	private bool mStarted;
}
