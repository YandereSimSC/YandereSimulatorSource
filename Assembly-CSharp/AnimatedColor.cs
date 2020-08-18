using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x0600059D RID: 1437 RVA: 0x0003400E File Offset: 0x0003220E
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00034022 File Offset: 0x00032222
	private void LateUpdate()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x040005A8 RID: 1448
	public Color color = Color.white;

	// Token: 0x040005A9 RID: 1449
	private UIWidget mWidget;
}
