using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glow/Glow")]
public class CameraFilterPack_Glow_Glow : MonoBehaviour
{
	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00073D8D File Offset: 0x00071F8D
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

	// Token: 0x06000F88 RID: 3976 RVA: 0x00073DC1 File Offset: 0x00071FC1
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Glow_Glow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00073DE4 File Offset: 0x00071FE4
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

	// Token: 0x06000F8A RID: 3978 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x00073FAC File Offset: 0x000721AC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400126A RID: 4714
	public Shader SCShader;

	// Token: 0x0400126B RID: 4715
	private float TimeX = 1f;

	// Token: 0x0400126C RID: 4716
	private Material SCMaterial;

	// Token: 0x0400126D RID: 4717
	[Range(0f, 20f)]
	public float Amount = 4f;

	// Token: 0x0400126E RID: 4718
	[Range(2f, 16f)]
	public int FastFilter = 4;

	// Token: 0x0400126F RID: 4719
	[Range(0f, 1f)]
	public float Threshold = 0.5f;

	// Token: 0x04001270 RID: 4720
	[Range(0f, 1f)]
	public float Intensity = 0.75f;

	// Token: 0x04001271 RID: 4721
	[Range(-1f, 1f)]
	public float Precision = 0.56f;
}
