using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Warp")]
public class CameraFilterPack_Vision_Warp : MonoBehaviour
{
	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06001202 RID: 4610 RVA: 0x0007F41D File Offset: 0x0007D61D
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

	// Token: 0x06001203 RID: 4611 RVA: 0x0007F451 File Offset: 0x0007D651
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Warp");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x0007F474 File Offset: 0x0007D674
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x0007F56C File Offset: 0x0007D76C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400151C RID: 5404
	public Shader SCShader;

	// Token: 0x0400151D RID: 5405
	private float TimeX = 1f;

	// Token: 0x0400151E RID: 5406
	private Material SCMaterial;

	// Token: 0x0400151F RID: 5407
	[Range(0f, 1f)]
	public float Value = 0.6f;

	// Token: 0x04001520 RID: 5408
	[Range(0f, 1f)]
	public float Value2 = 0.6f;

	// Token: 0x04001521 RID: 5409
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001522 RID: 5410
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
