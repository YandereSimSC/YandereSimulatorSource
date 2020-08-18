using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Radial_Fast")]
public class CameraFilterPack_Blur_Radial_Fast : MonoBehaviour
{
	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000C9C RID: 3228 RVA: 0x000678E1 File Offset: 0x00065AE1
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

	// Token: 0x06000C9D RID: 3229 RVA: 0x00067915 File Offset: 0x00065B15
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Radial_Fast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00067938 File Offset: 0x00065B38
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

	// Token: 0x06000C9F RID: 3231 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00067A30 File Offset: 0x00065C30
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F56 RID: 3926
	public Shader SCShader;

	// Token: 0x04000F57 RID: 3927
	private float TimeX = 1f;

	// Token: 0x04000F58 RID: 3928
	private Material SCMaterial;

	// Token: 0x04000F59 RID: 3929
	[Range(-0.5f, 0.5f)]
	public float Intensity = 0.125f;

	// Token: 0x04000F5A RID: 3930
	[Range(-2f, 2f)]
	public float MovX = 0.5f;

	// Token: 0x04000F5B RID: 3931
	[Range(-2f, 2f)]
	public float MovY = 0.5f;

	// Token: 0x04000F5C RID: 3932
	[Range(0f, 10f)]
	private float blurWidth = 1f;
}
