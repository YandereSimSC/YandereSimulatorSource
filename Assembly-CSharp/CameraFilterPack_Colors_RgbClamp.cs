using System;
using UnityEngine;

// Token: 0x02000161 RID: 353
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/RgbClamp")]
public class CameraFilterPack_Colors_RgbClamp : MonoBehaviour
{
	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0006AE5D File Offset: 0x0006905D
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

	// Token: 0x06000D59 RID: 3417 RVA: 0x0006AE91 File Offset: 0x00069091
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_RgbClamp");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0006AEB4 File Offset: 0x000690B4
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
			this.material.SetFloat("_Value", this.Red_Start);
			this.material.SetFloat("_Value2", this.Red_End);
			this.material.SetFloat("_Value3", this.Green_Start);
			this.material.SetFloat("_Value4", this.Green_End);
			this.material.SetFloat("_Value5", this.Blue_Start);
			this.material.SetFloat("_Value6", this.Blue_End);
			this.material.SetFloat("_Value7", this.RGB_Start);
			this.material.SetFloat("_Value8", this.RGB_End);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0006B004 File Offset: 0x00069204
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001013 RID: 4115
	public Shader SCShader;

	// Token: 0x04001014 RID: 4116
	private float TimeX = 1f;

	// Token: 0x04001015 RID: 4117
	private Material SCMaterial;

	// Token: 0x04001016 RID: 4118
	[Range(0f, 1f)]
	public float Red_Start;

	// Token: 0x04001017 RID: 4119
	[Range(0f, 1f)]
	public float Red_End = 1f;

	// Token: 0x04001018 RID: 4120
	[Range(0f, 1f)]
	public float Green_Start;

	// Token: 0x04001019 RID: 4121
	[Range(0f, 1f)]
	public float Green_End = 1f;

	// Token: 0x0400101A RID: 4122
	[Range(0f, 1f)]
	public float Blue_Start;

	// Token: 0x0400101B RID: 4123
	[Range(0f, 1f)]
	public float Blue_End = 1f;

	// Token: 0x0400101C RID: 4124
	[Range(0f, 1f)]
	public float RGB_Start;

	// Token: 0x0400101D RID: 4125
	[Range(0f, 1f)]
	public float RGB_End = 1f;
}
