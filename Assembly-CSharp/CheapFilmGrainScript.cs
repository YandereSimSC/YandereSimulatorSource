using System;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class CheapFilmGrainScript : MonoBehaviour
{
	// Token: 0x0600122F RID: 4655 RVA: 0x00080C5A File Offset: 0x0007EE5A
	private void Update()
	{
		this.MyRenderer.material.mainTextureScale = new Vector2(UnityEngine.Random.Range(this.Floor, this.Ceiling), UnityEngine.Random.Range(this.Floor, this.Ceiling));
	}

	// Token: 0x04001571 RID: 5489
	public Renderer MyRenderer;

	// Token: 0x04001572 RID: 5490
	public float Floor = 100f;

	// Token: 0x04001573 RID: 5491
	public float Ceiling = 200f;
}
