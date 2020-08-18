using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class CrackScript : MonoBehaviour
{
	// Token: 0x060012BA RID: 4794 RVA: 0x00095B82 File Offset: 0x00093D82
	private void Update()
	{
		this.Texture.fillAmount += Time.deltaTime * 10f;
	}

	// Token: 0x04001851 RID: 6225
	public UITexture Texture;
}
