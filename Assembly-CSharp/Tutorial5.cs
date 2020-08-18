using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class Tutorial5 : MonoBehaviour
{
	// Token: 0x06000140 RID: 320 RVA: 0x0001349C File Offset: 0x0001169C
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].duration = Mathf.Lerp(2f, 0.5f, UIProgressBar.current.value);
		}
	}
}
