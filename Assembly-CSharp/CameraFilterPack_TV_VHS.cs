using System;
using UnityEngine;

// Token: 0x0200020A RID: 522
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/VHS")]
public class CameraFilterPack_TV_VHS : MonoBehaviour
{
	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06001172 RID: 4466 RVA: 0x0007CCF5 File Offset: 0x0007AEF5
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

	// Token: 0x06001173 RID: 4467 RVA: 0x0007CD29 File Offset: 0x0007AF29
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_VHS");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x0007CD4C File Offset: 0x0007AF4C
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
			this.material.SetFloat("_Value3", this.Calibrage);
			this.material.SetFloat("_Value4", this.WhiteParasite);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x0007CE44 File Offset: 0x0007B044
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001476 RID: 5238
	public Shader SCShader;

	// Token: 0x04001477 RID: 5239
	private float TimeX = 1f;

	// Token: 0x04001478 RID: 5240
	private Material SCMaterial;

	// Token: 0x04001479 RID: 5241
	[Range(1f, 256f)]
	public float Cryptage = 64f;

	// Token: 0x0400147A RID: 5242
	[Range(1f, 100f)]
	public float Parasite = 32f;

	// Token: 0x0400147B RID: 5243
	[Range(0f, 3f)]
	public float Calibrage;

	// Token: 0x0400147C RID: 5244
	[Range(0f, 1f)]
	public float WhiteParasite = 1f;
}
