using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x0200002E RID: 46
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x06000114 RID: 276 RVA: 0x00012A78 File Offset: 0x00010C78
	private IEnumerator Start()
	{
		UnityWebRequest www = UnityWebRequest.Get(this.url);
		yield return www.SendWebRequest();
		this.mTex = DownloadHandlerTexture.GetContent(www);
		if (this.mTex != null)
		{
			UITexture component = base.GetComponent<UITexture>();
			component.mainTexture = this.mTex;
			if (this.pixelPerfect)
			{
				component.MakePixelPerfect();
			}
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00012A87 File Offset: 0x00010C87
	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			UnityEngine.Object.Destroy(this.mTex);
		}
	}

	// Token: 0x040002A8 RID: 680
	public string url = "http://www.yourwebsite.com/logo.png";

	// Token: 0x040002A9 RID: 681
	public bool pixelPerfect = true;

	// Token: 0x040002AA RID: 682
	private Texture2D mTex;
}
