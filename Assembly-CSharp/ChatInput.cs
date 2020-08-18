using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x0001298C File Offset: 0x00010B8C
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		this.mInput.label.maxLineCount = 1;
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 == 0) ? "[FFFFFF]" : "[AAAAAA]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00012A1C File Offset: 0x00010C1C
	public void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUIText.StripSymbols(this.mInput.value);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.value = "";
				this.mInput.isSelected = false;
			}
		}
	}

	// Token: 0x040002A5 RID: 677
	public UITextList textList;

	// Token: 0x040002A6 RID: 678
	public bool fillWithDummyData;

	// Token: 0x040002A7 RID: 679
	private UIInput mInput;
}
