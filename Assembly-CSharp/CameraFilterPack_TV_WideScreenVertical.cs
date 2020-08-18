using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenVertical")]
public class CameraFilterPack_TV_WideScreenVertical : MonoBehaviour
{
	// Token: 0x17000334 RID: 820
	// (get) Token: 0x060011AE RID: 4526 RVA: 0x0007DAED File Offset: 0x0007BCED
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

	// Token: 0x060011AF RID: 4527 RVA: 0x0007DB21 File Offset: 0x0007BD21
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenVertical");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x0007DB44 File Offset: 0x0007BD44
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x0007DC3C File Offset: 0x0007BE3C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014AE RID: 5294
	public Shader SCShader;

	// Token: 0x040014AF RID: 5295
	private float TimeX = 1f;

	// Token: 0x040014B0 RID: 5296
	private Material SCMaterial;

	// Token: 0x040014B1 RID: 5297
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x040014B2 RID: 5298
	[Range(0.001f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x040014B3 RID: 5299
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x040014B4 RID: 5300
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
