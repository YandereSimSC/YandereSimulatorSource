using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x0600059A RID: 1434 RVA: 0x00033F9B File Offset: 0x0003219B
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.LateUpdate();
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00033FBB File Offset: 0x000321BB
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x040005A5 RID: 1445
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x040005A6 RID: 1446
	private UIWidget mWidget;

	// Token: 0x040005A7 RID: 1447
	private UIPanel mPanel;
}
