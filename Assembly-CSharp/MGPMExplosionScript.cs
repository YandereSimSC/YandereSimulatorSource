using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class MGPMExplosionScript : MonoBehaviour
{
	// Token: 0x0600007E RID: 126 RVA: 0x00005B20 File Offset: 0x00003D20
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

	// Token: 0x040000B9 RID: 185
	public Renderer MyRenderer;

	// Token: 0x040000BA RID: 186
	public Texture[] Sprite;

	// Token: 0x040000BB RID: 187
	public float Timer;

	// Token: 0x040000BC RID: 188
	public float FPS;

	// Token: 0x040000BD RID: 189
	public int Frame;
}
