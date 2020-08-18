using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixel/Snow_8bits")]
public class CameraFilterPack_Atmosphere_Snow_8bits : MonoBehaviour
{
	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00061CC5 File Offset: 0x0005FEC5
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

	// Token: 0x06000B56 RID: 2902 RVA: 0x00061CF9 File Offset: 0x0005FEF9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Snow_8bits");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00061D1C File Offset: 0x0005FF1C
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
			this.material.SetFloat("_Value", this.Threshold);
			this.material.SetFloat("_Value2", this.Size);
			this.material.SetFloat("_Value3", this.DirectionX);
			this.material.SetFloat("_Value4", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00061E14 File Offset: 0x00060014
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DEA RID: 3562
	public Shader SCShader;

	// Token: 0x04000DEB RID: 3563
	private float TimeX = 1f;

	// Token: 0x04000DEC RID: 3564
	private Material SCMaterial;

	// Token: 0x04000DED RID: 3565
	[Range(0.9f, 2f)]
	public float Threshold = 1f;

	// Token: 0x04000DEE RID: 3566
	[Range(8f, 256f)]
	public float Size = 64f;

	// Token: 0x04000DEF RID: 3567
	[Range(-0.5f, 0.5f)]
	public float DirectionX;

	// Token: 0x04000DF0 RID: 3568
	[Range(0f, 1f)]
	public float Fade = 1f;
}
