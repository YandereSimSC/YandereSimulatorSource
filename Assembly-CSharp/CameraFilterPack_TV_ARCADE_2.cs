using System;
using UnityEngine;

// Token: 0x020001F6 RID: 502
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/ARCADE_2")]
public class CameraFilterPack_TV_ARCADE_2 : MonoBehaviour
{
	// Token: 0x17000316 RID: 790
	// (get) Token: 0x060010FA RID: 4346 RVA: 0x0007ACF2 File Offset: 0x00078EF2
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

	// Token: 0x060010FB RID: 4347 RVA: 0x0007AD26 File Offset: 0x00078F26
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_ARCADE_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0007AD48 File Offset: 0x00078F48
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
			this.material.SetFloat("_Value", this.Interferance_Size);
			this.material.SetFloat("_Value2", this.Interferance_Speed);
			this.material.SetFloat("_Value3", this.Contrast);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0007AE40 File Offset: 0x00079040
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013F7 RID: 5111
	public Shader SCShader;

	// Token: 0x040013F8 RID: 5112
	private float TimeX = 1f;

	// Token: 0x040013F9 RID: 5113
	private Material SCMaterial;

	// Token: 0x040013FA RID: 5114
	[Range(0f, 10f)]
	public float Interferance_Size = 1f;

	// Token: 0x040013FB RID: 5115
	[Range(0f, 10f)]
	public float Interferance_Speed = 0.5f;

	// Token: 0x040013FC RID: 5116
	[Range(0f, 10f)]
	public float Contrast = 1f;

	// Token: 0x040013FD RID: 5117
	[Range(0f, 1f)]
	public float Fade = 1f;
}
