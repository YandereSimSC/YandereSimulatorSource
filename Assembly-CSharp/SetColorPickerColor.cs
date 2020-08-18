using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
[RequireComponent(typeof(UIWidget))]
public class SetColorPickerColor : MonoBehaviour
{
	// Token: 0x06000136 RID: 310 RVA: 0x000132AB File Offset: 0x000114AB
	public void SetToCurrent()
	{
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		if (UIColorPicker.current != null)
		{
			this.mWidget.color = UIColorPicker.current.value;
		}
	}

	// Token: 0x040002C8 RID: 712
	[NonSerialized]
	private UIWidget mWidget;
}
