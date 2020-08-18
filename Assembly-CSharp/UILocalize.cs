using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
	// Token: 0x1700019C RID: 412
	// (set) Token: 0x06000838 RID: 2104 RVA: 0x00042688 File Offset: 0x00040888
	public string value
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				UIWidget component = base.GetComponent<UIWidget>();
				UILabel uilabel = component as UILabel;
				UISprite uisprite = component as UISprite;
				if (uilabel != null)
				{
					UIInput uiinput = NGUITools.FindInParents<UIInput>(uilabel.gameObject);
					if (uiinput != null && uiinput.label == uilabel)
					{
						uiinput.defaultText = value;
						return;
					}
					uilabel.text = value;
					return;
				}
				else if (uisprite != null)
				{
					UIButton uibutton = NGUITools.FindInParents<UIButton>(uisprite.gameObject);
					if (uibutton != null && uibutton.tweenTarget == uisprite.gameObject)
					{
						uibutton.normalSprite = value;
					}
					uisprite.spriteName = value;
					uisprite.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00042734 File Offset: 0x00040934
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnLocalize();
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00042744 File Offset: 0x00040944
	private void Start()
	{
		this.mStarted = true;
		this.OnLocalize();
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00042754 File Offset: 0x00040954
	private void OnLocalize()
	{
		if (string.IsNullOrEmpty(this.key))
		{
			UILabel component = base.GetComponent<UILabel>();
			if (component != null)
			{
				this.key = component.text;
			}
		}
		if (!string.IsNullOrEmpty(this.key))
		{
			this.value = Localization.Get(this.key, true);
		}
	}

	// Token: 0x0400072D RID: 1837
	public string key;

	// Token: 0x0400072E RID: 1838
	private bool mStarted;
}
