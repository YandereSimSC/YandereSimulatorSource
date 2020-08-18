using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
[ExecuteInEditMode]
public class AnimatedWidget : MonoBehaviour
{
	// Token: 0x060005A0 RID: 1440 RVA: 0x00034048 File Offset: 0x00032248
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0003405C File Offset: 0x0003225C
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.width = Mathf.RoundToInt(this.width);
			this.mWidget.height = Mathf.RoundToInt(this.height);
		}
	}

	// Token: 0x040005AA RID: 1450
	public float width = 1f;

	// Token: 0x040005AB RID: 1451
	public float height = 1f;

	// Token: 0x040005AC RID: 1452
	private UIWidget mWidget;
}
