using System;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public class TextureManagerScript : MonoBehaviour
{
	// Token: 0x06001C27 RID: 7207 RVA: 0x0014FBF8 File Offset: 0x0014DDF8
	public Texture2D MergeTextures(Texture2D BackgroundTex, Texture2D TopTex)
	{
		Texture2D texture2D = new Texture2D(1024, 1024);
		Color32[] pixels = BackgroundTex.GetPixels32();
		Color32[] pixels2 = TopTex.GetPixels32();
		for (int i = 0; i < pixels2.Length; i++)
		{
			if (pixels2[i].a != 0)
			{
				pixels[i] = pixels2[i];
			}
		}
		texture2D.SetPixels32(pixels);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x04003498 RID: 13464
	public Texture[] UniformTextures;

	// Token: 0x04003499 RID: 13465
	public Texture[] CasualTextures;

	// Token: 0x0400349A RID: 13466
	public Texture[] SocksTextures;

	// Token: 0x0400349B RID: 13467
	public Texture2D PurpleStockings;

	// Token: 0x0400349C RID: 13468
	public Texture2D GreenStockings;

	// Token: 0x0400349D RID: 13469
	public Texture2D Base2D;

	// Token: 0x0400349E RID: 13470
	public Texture2D Overlay2D;
}
