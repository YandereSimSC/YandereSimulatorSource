using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/VHS_Rewind")]
public class CameraFilterPack_TV_VHS_Rewind : MonoBehaviour
{
	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06001178 RID: 4472 RVA: 0x0007CE92 File Offset: 0x0007B092
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0007CEC6 File Offset: 0x0007B0C6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_VHS_Rewind");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x0007CEE8 File Offset: 0x0007B0E8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.Cryptage);
			this.material.SetFloat("_Value2", this.Parasite);
			this.material.SetFloat("_Value3", this.Parasite2);
			this.material.SetFloat("_Value4", this.WhiteParasite);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0007CFE0 File Offset: 0x0007B1E0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400147D RID: 5245
	public Shader SCShader;

	// Token: 0x0400147E RID: 5246
	private float TimeX = 1f;

	// Token: 0x0400147F RID: 5247
	private Material SCMaterial;

	// Token: 0x04001480 RID: 5248
	[Range(0f, 1f)]
	public float Cryptage = 1f;

	// Token: 0x04001481 RID: 5249
	[Range(-20f, 20f)]
	public float Parasite = 9f;

	// Token: 0x04001482 RID: 5250
	[Range(-20f, 20f)]
	public float Parasite2 = 12f;

	// Token: 0x04001483 RID: 5251
	[Range(0f, 1f)]
	private float WhiteParasite = 1f;
}
