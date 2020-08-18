using System;
using UnityEngine;

// Token: 0x020001DF RID: 479
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Noise/TV_2")]
public class CameraFilterPack_Noise_TV_2 : MonoBehaviour
{
	// Token: 0x170002FF RID: 767
	// (get) Token: 0x0600106B RID: 4203 RVA: 0x000784B7 File Offset: 0x000766B7
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

	// Token: 0x0600106C RID: 4204 RVA: 0x000784EB File Offset: 0x000766EB
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_Noise2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Noise_TV_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x00078524 File Offset: 0x00076724
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Fade_Additive);
			this.material.SetFloat("_Value3", this.Fade_Distortion);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x00078632 File Offset: 0x00076832
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001361 RID: 4961
	public Shader SCShader;

	// Token: 0x04001362 RID: 4962
	private float TimeX = 1f;

	// Token: 0x04001363 RID: 4963
	private Material SCMaterial;

	// Token: 0x04001364 RID: 4964
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001365 RID: 4965
	[Range(0f, 1f)]
	public float Fade_Additive;

	// Token: 0x04001366 RID: 4966
	[Range(0f, 1f)]
	public float Fade_Distortion;

	// Token: 0x04001367 RID: 4967
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x04001368 RID: 4968
	private Texture2D Texture2;
}
