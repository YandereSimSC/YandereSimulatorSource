using System;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class ChangeTextureScript : MonoBehaviour
{
	// Token: 0x06001227 RID: 4647 RVA: 0x00080490 File Offset: 0x0007E690
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID == this.Textures.Length)
			{
				this.ID = 1;
			}
			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}
	}

	// Token: 0x04001552 RID: 5458
	public Renderer MyRenderer;

	// Token: 0x04001553 RID: 5459
	public Texture[] Textures;

	// Token: 0x04001554 RID: 5460
	public int ID = 1;
}
