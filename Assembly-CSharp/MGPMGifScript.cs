using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class MGPMGifScript : MonoBehaviour
{
	// Token: 0x06000080 RID: 128 RVA: 0x00005BA0 File Offset: 0x00003DA0
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.ID++;
			if (this.ID == this.Frames.Length)
			{
				this.ID = 0;
			}
			this.MyRenderer.material.mainTexture = this.Frames[this.ID];
			this.Timer = 0f;
		}
	}

	// Token: 0x040000BE RID: 190
	public Texture[] Frames;

	// Token: 0x040000BF RID: 191
	public Renderer MyRenderer;

	// Token: 0x040000C0 RID: 192
	public float Timer;

	// Token: 0x040000C1 RID: 193
	public float FPS;

	// Token: 0x040000C2 RID: 194
	public int ID;
}
