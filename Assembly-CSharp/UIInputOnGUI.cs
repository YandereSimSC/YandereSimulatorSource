using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
[RequireComponent(typeof(UIInput))]
public class UIInputOnGUI : MonoBehaviour
{
	// Token: 0x060007B6 RID: 1974 RVA: 0x000403E9 File Offset: 0x0003E5E9
	private void Awake()
	{
		this.mInput = base.GetComponent<UIInput>();
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x000403F7 File Offset: 0x0003E5F7
	private void OnGUI()
	{
		if (Event.current.rawType == EventType.KeyDown)
		{
			this.mInput.ProcessEvent(Event.current);
		}
	}

	// Token: 0x040006FC RID: 1788
	[NonSerialized]
	private UIInput mInput;
}
