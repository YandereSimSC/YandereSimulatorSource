using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06000150 RID: 336 RVA: 0x000138CF File Offset: 0x00011ACF
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x06000151 RID: 337 RVA: 0x000138DD File Offset: 0x00011ADD
	private void Start()
	{
		this.mStarted = true;
		this.Refresh();
		EventDelegate.Add(this.mList.onChange, delegate()
		{
			Localization.language = UIPopupList.current.value;
		});
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0001391C File Offset: 0x00011B1C
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Refresh();
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0001392C File Offset: 0x00011B2C
	public void Refresh()
	{
		if (this.mList != null && Localization.knownLanguages != null)
		{
			this.mList.Clear();
			int i = 0;
			int num = Localization.knownLanguages.Length;
			while (i < num)
			{
				this.mList.items.Add(Localization.knownLanguages[i]);
				i++;
			}
			this.mList.value = Localization.language;
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00013994 File Offset: 0x00011B94
	private void OnLocalize()
	{
		this.Refresh();
	}

	// Token: 0x040002E4 RID: 740
	private UIPopupList mList;

	// Token: 0x040002E5 RID: 741
	private bool mStarted;
}
