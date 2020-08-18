using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class MGPMWaterScript : MonoBehaviour
{
	// Token: 0x06000099 RID: 153 RVA: 0x00007EB8 File Offset: 0x000060B8
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.Timer = 0f;
			this.Frame++;
			if (this.Frame == this.Sprite.Length)
			{
				this.Frame = 0;
			}
			this.MyRenderer.material.mainTexture = this.Sprite[this.Frame];
		}
		base.transform.localPosition = new Vector3(0f, base.transform.localPosition.y - this.Speed * Time.deltaTime, 3f);
		if (base.transform.localPosition.y < -640f)
		{
			base.transform.localPosition = new Vector3(0f, base.transform.localPosition.y + 1280f, 3f);
		}
	}

	// Token: 0x04000129 RID: 297
	public Renderer MyRenderer;

	// Token: 0x0400012A RID: 298
	public Texture[] Sprite;

	// Token: 0x0400012B RID: 299
	public float Speed;

	// Token: 0x0400012C RID: 300
	public float Timer;

	// Token: 0x0400012D RID: 301
	public float FPS;

	// Token: 0x0400012E RID: 302
	public int Frame;
}
