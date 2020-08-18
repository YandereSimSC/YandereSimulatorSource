using System;
using UnityEngine;

// Token: 0x020001BF RID: 447
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glow/Glow_Color")]
public class CameraFilterPack_Glow_Glow_Color : MonoBehaviour
{
	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00074019 File Offset: 0x00072219
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

	// Token: 0x06000F8E RID: 3982 RVA: 0x0007404D File Offset: 0x0007224D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Glow_Glow_Color");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x00074070 File Offset: 0x00072270
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (!(this.SCShader != null))
		{
			Graphics.Blit(sourceTexture, destTexture);
			return;
		}
		int fastFilter = this.FastFilter;
		this.TimeX += Time.deltaTime;
		if (this.TimeX > 100f)
		{
			this.TimeX = 0f;
		}
		this.material.SetFloat("_TimeX", this.TimeX);
		this.material.SetFloat("_Amount", this.Amount);
		this.material.SetFloat("_Value1", this.Threshold);
		this.material.SetFloat("_Value2", this.Intensity);
		this.material.SetFloat("_Value3", this.Precision);
		this.material.SetColor("_GlowColor", this.GlowColor);
		this.material.SetVector("_ScreenResolution", new Vector2((float)(Screen.width / fastFilter), (float)(Screen.height / fastFilter)));
		int width = sourceTexture.width / fastFilter;
		int height = sourceTexture.height / fastFilter;
		if (this.FastFilter > 1)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0);
			temporary.filterMode = FilterMode.Trilinear;
			Graphics.Blit(sourceTexture, temporary, this.material, 3);
			Graphics.Blit(temporary, temporary2, this.material, 2);
			Graphics.Blit(temporary2, temporary, this.material, 0);
			this.material.SetFloat("_Amount", this.Amount * 2f);
			Graphics.Blit(temporary, temporary2, this.material, 2);
			Graphics.Blit(temporary2, temporary, this.material, 0);
			this.material.SetTexture("_MainTex2", temporary);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			Graphics.Blit(sourceTexture, destTexture, this.material, 1);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture, this.material, 0);
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x0007424E File Offset: 0x0007244E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001272 RID: 4722
	public Shader SCShader;

	// Token: 0x04001273 RID: 4723
	private float TimeX = 1f;

	// Token: 0x04001274 RID: 4724
	private Material SCMaterial;

	// Token: 0x04001275 RID: 4725
	[Range(0f, 20f)]
	public float Amount = 4f;

	// Token: 0x04001276 RID: 4726
	[Range(2f, 16f)]
	public int FastFilter = 4;

	// Token: 0x04001277 RID: 4727
	[Range(0f, 1f)]
	public float Threshold = 0.5f;

	// Token: 0x04001278 RID: 4728
	[Range(0f, 3f)]
	public float Intensity = 2.25f;

	// Token: 0x04001279 RID: 4729
	[Range(-1f, 1f)]
	public float Precision = 0.56f;

	// Token: 0x0400127A RID: 4730
	public Color GlowColor = new Color(0f, 0.7f, 1f, 1f);
}
