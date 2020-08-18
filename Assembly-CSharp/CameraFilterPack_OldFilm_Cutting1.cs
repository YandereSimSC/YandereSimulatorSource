using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Cutting 1")]
public class CameraFilterPack_OldFilm_Cutting1 : MonoBehaviour
{
	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06001099 RID: 4249 RVA: 0x000792CE File Offset: 0x000774CE
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

	// Token: 0x0600109A RID: 4250 RVA: 0x00079302 File Offset: 0x00077502
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_OldFilm1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/OldFilm_Cutting1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x00079338 File Offset: 0x00077538
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
			this.material.SetFloat("_Value", this.Luminosity);
			this.material.SetFloat("_Value2", 1f - this.Vignette);
			this.material.SetFloat("_Value3", this.Negative);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0007941F File Offset: 0x0007761F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001391 RID: 5009
	public Shader SCShader;

	// Token: 0x04001392 RID: 5010
	private float TimeX = 1f;

	// Token: 0x04001393 RID: 5011
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001394 RID: 5012
	[Range(0f, 2f)]
	public float Luminosity = 1.5f;

	// Token: 0x04001395 RID: 5013
	[Range(0f, 1f)]
	public float Vignette = 1f;

	// Token: 0x04001396 RID: 5014
	[Range(0f, 2f)]
	public float Negative;

	// Token: 0x04001397 RID: 5015
	private Material SCMaterial;

	// Token: 0x04001398 RID: 5016
	private Texture2D Texture2;
}
