using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class ScrollingImage : MonoBehaviour
{
	// Token: 0x06000077 RID: 119 RVA: 0x0000465C File Offset: 0x0000285C
	private void Update()
	{
		this.scroll += Time.deltaTime * this.scrollSpeed;
		if (this.image != null)
		{
			this.image.uvRect = new Rect(this.scroll, this.scroll, 1f, 1f);
		}
	}

	// Token: 0x04000091 RID: 145
	[SerializeField]
	private RawImage image;

	// Token: 0x04000092 RID: 146
	[SerializeField]
	private float scrollSpeed;

	// Token: 0x04000093 RID: 147
	private float scroll;
}
