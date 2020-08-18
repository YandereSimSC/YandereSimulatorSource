using System;
using UnityEngine;

// Token: 0x020001F8 RID: 504
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Artefact")]
public class CameraFilterPack_TV_Artefact : MonoBehaviour
{
	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06001106 RID: 4358 RVA: 0x0007B06B File Offset: 0x0007926B
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

	// Token: 0x06001107 RID: 4359 RVA: 0x0007B09F File Offset: 0x0007929F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Artefact");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0007B0C0 File Offset: 0x000792C0
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
			this.material.SetFloat("_Colorisation", this.Colorisation);
			this.material.SetFloat("_Parasite", this.Parasite);
			this.material.SetFloat("_Noise", this.Noise);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0007B1B8 File Offset: 0x000793B8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001406 RID: 5126
	public Shader SCShader;

	// Token: 0x04001407 RID: 5127
	private float TimeX = 1f;

	// Token: 0x04001408 RID: 5128
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001409 RID: 5129
	[Range(-10f, 10f)]
	public float Colorisation = 1f;

	// Token: 0x0400140A RID: 5130
	[Range(-10f, 10f)]
	public float Parasite = 1f;

	// Token: 0x0400140B RID: 5131
	[Range(-10f, 10f)]
	public float Noise = 1f;

	// Token: 0x0400140C RID: 5132
	private Material SCMaterial;
}
