using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Radial")]
public class CameraFilterPack_Blur_Radial : MonoBehaviour
{
	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0006773B File Offset: 0x0006593B
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

	// Token: 0x06000C97 RID: 3223 RVA: 0x0006776F File Offset: 0x0006596F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Radial");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x00067790 File Offset: 0x00065990
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
			this.material.SetFloat("_Value", this.Intensity);
			this.material.SetFloat("_Value2", this.MovX);
			this.material.SetFloat("_Value3", this.MovY);
			this.material.SetFloat("_Value4", this.blurWidth);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x00067888 File Offset: 0x00065A88
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F4F RID: 3919
	public Shader SCShader;

	// Token: 0x04000F50 RID: 3920
	private float TimeX = 1f;

	// Token: 0x04000F51 RID: 3921
	private Material SCMaterial;

	// Token: 0x04000F52 RID: 3922
	[Range(-0.5f, 0.5f)]
	public float Intensity = 0.125f;

	// Token: 0x04000F53 RID: 3923
	[Range(-2f, 2f)]
	public float MovX = 0.5f;

	// Token: 0x04000F54 RID: 3924
	[Range(-2f, 2f)]
	public float MovY = 0.5f;

	// Token: 0x04000F55 RID: 3925
	[Range(0f, 10f)]
	private float blurWidth = 1f;
}
