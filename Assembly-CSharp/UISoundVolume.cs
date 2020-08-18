using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x0600030F RID: 783 RVA: 0x0001E5F2 File Offset: 0x0001C7F2
	private void Awake()
	{
		UISlider component = base.GetComponent<UISlider>();
		component.value = NGUITools.soundVolume;
		EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0001E61C File Offset: 0x0001C81C
	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}
}
