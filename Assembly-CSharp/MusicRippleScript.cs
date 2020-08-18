using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class MusicRippleScript : MonoBehaviour
{
	// Token: 0x060000A9 RID: 169 RVA: 0x0000A204 File Offset: 0x00008404
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.Timer = 0f;
			this.Frame++;
			if (this.Frame == this.Sprite.Length)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			this.MyRenderer.material.mainTexture = this.Sprite[this.Frame];
		}
	}

	// Token: 0x04000181 RID: 385
	public Renderer MyRenderer;

	// Token: 0x04000182 RID: 386
	public Texture[] Sprite;

	// Token: 0x04000183 RID: 387
	public float Timer;

	// Token: 0x04000184 RID: 388
	public float FPS;

	// Token: 0x04000185 RID: 389
	public int Frame;
}
