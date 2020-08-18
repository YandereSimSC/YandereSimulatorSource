using System;
using UnityEngine;

// Token: 0x02000160 RID: 352
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/NewPosterize")]
public class CameraFilterPack_Colors_NewPosterize : MonoBehaviour
{
	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0006ACB7 File Offset: 0x00068EB7
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

	// Token: 0x06000D53 RID: 3411 RVA: 0x0006ACEB File Offset: 0x00068EEB
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_NewPosterize");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0006AD0C File Offset: 0x00068F0C
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
			this.material.SetFloat("_Value", this.Gamma);
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0006AE04 File Offset: 0x00069004
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400100C RID: 4108
	public Shader SCShader;

	// Token: 0x0400100D RID: 4109
	private float TimeX = 1f;

	// Token: 0x0400100E RID: 4110
	private Material SCMaterial;

	// Token: 0x0400100F RID: 4111
	[Range(0f, 2f)]
	public float Gamma = 1f;

	// Token: 0x04001010 RID: 4112
	[Range(0f, 16f)]
	public float Colors = 11f;

	// Token: 0x04001011 RID: 4113
	[Range(-1f, 1f)]
	public float Green_Mod = 1f;

	// Token: 0x04001012 RID: 4114
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
