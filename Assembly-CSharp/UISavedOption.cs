using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x060002CA RID: 714 RVA: 0x0001C1F6 File Offset: 0x0001A3F6
	private string key
	{
		get
		{
			if (!string.IsNullOrEmpty(this.keyName))
			{
				return this.keyName;
			}
			return "NGUI State: " + base.name;
		}
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0001C21C File Offset: 0x0001A41C
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.mCheck = base.GetComponent<UIToggle>();
		this.mSlider = base.GetComponent<UIProgressBar>();
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0001C244 File Offset: 0x0001A444
	private void OnEnable()
	{
		if (this.mList != null)
		{
			EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
			string @string = PlayerPrefs.GetString(this.key);
			if (!string.IsNullOrEmpty(@string))
			{
				this.mList.value = @string;
				return;
			}
		}
		else
		{
			if (this.mCheck != null)
			{
				EventDelegate.Add(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
				this.mCheck.value = (PlayerPrefs.GetInt(this.key, this.mCheck.startsActive ? 1 : 0) != 0);
				return;
			}
			if (this.mSlider != null)
			{
				EventDelegate.Add(this.mSlider.onChange, new EventDelegate.Callback(this.SaveProgress));
				this.mSlider.value = PlayerPrefs.GetFloat(this.key, this.mSlider.value);
				return;
			}
			string string2 = PlayerPrefs.GetString(this.key);
			UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UIToggle uitoggle = componentsInChildren[i];
				uitoggle.value = (uitoggle.name == string2);
				i++;
			}
		}
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0001C380 File Offset: 0x0001A580
	private void OnDisable()
	{
		if (this.mCheck != null)
		{
			EventDelegate.Remove(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
			return;
		}
		if (this.mList != null)
		{
			EventDelegate.Remove(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
			return;
		}
		if (this.mSlider != null)
		{
			EventDelegate.Remove(this.mSlider.onChange, new EventDelegate.Callback(this.SaveProgress));
			return;
		}
		UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			UIToggle uitoggle = componentsInChildren[i];
			if (uitoggle.value)
			{
				PlayerPrefs.SetString(this.key, uitoggle.name);
				return;
			}
			i++;
		}
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0001C447 File Offset: 0x0001A647
	public void SaveSelection()
	{
		PlayerPrefs.SetString(this.key, UIPopupList.current.value);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0001C45E File Offset: 0x0001A65E
	public void SaveState()
	{
		PlayerPrefs.SetInt(this.key, UIToggle.current.value ? 1 : 0);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0001C47B File Offset: 0x0001A67B
	public void SaveProgress()
	{
		PlayerPrefs.SetFloat(this.key, UIProgressBar.current.value);
	}

	// Token: 0x04000423 RID: 1059
	public string keyName;

	// Token: 0x04000424 RID: 1060
	private UIPopupList mList;

	// Token: 0x04000425 RID: 1061
	private UIToggle mCheck;

	// Token: 0x04000426 RID: 1062
	private UIProgressBar mSlider;
}
