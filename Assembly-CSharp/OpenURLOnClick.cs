using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class OpenURLOnClick : MonoBehaviour
{
	// Token: 0x0600012C RID: 300 RVA: 0x00012E84 File Offset: 0x00011084
	private void OnClick()
	{
		UILabel component = base.GetComponent<UILabel>();
		if (component != null)
		{
			string urlAtPosition = component.GetUrlAtPosition(UICamera.lastWorldPosition);
			if (!string.IsNullOrEmpty(urlAtPosition))
			{
				Application.OpenURL(urlAtPosition);
			}
		}
	}
}
