using System;
using UnityEngine;

// Token: 0x020001E7 RID: 487
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Cutting 2")]
public class CameraFilterPack_OldFilm_Cutting2 : MonoBehaviour
{
	// Token: 0x17000307 RID: 775
	// (get) Token: 0x0600109F RID: 4255 RVA: 0x0007946D File Offset: 0x0007766D
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

	// Token: 0x060010A0 RID: 4256 RVA: 0x000794A1 File Offset: 0x000776A1
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_OldFilm2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/OldFilm_Cutting2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x000794D8 File Offset: 0x000776D8
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
			this.material.SetFloat("_Value", 2f - this.Luminosity);
			this.material.SetFloat("_Value2", 1f - this.Vignette);
			this.material.SetFloat("_Value3", this.Negative);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x000795C5 File Offset: 0x000777C5
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001399 RID: 5017
	public Shader SCShader;

	// Token: 0x0400139A RID: 5018
	private float TimeX = 1f;

	// Token: 0x0400139B RID: 5019
	[Range(0f, 10f)]
	public float Speed = 5f;

	// Token: 0x0400139C RID: 5020
	[Range(0f, 2f)]
	public float Luminosity = 1f;

	// Token: 0x0400139D RID: 5021
	[Range(0f, 1f)]
	public float Vignette = 1f;

	// Token: 0x0400139E RID: 5022
	[Range(0f, 1f)]
	public float Negative;

	// Token: 0x0400139F RID: 5023
	private Material SCMaterial;

	// Token: 0x040013A0 RID: 5024
	private Texture2D Texture2;
}
