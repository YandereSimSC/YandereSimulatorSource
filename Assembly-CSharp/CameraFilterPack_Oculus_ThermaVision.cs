using System;
using UnityEngine;

// Token: 0x020001E5 RID: 485
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/ThermaVision")]
public class CameraFilterPack_Oculus_ThermaVision : MonoBehaviour
{
	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06001093 RID: 4243 RVA: 0x00079131 File Offset: 0x00077331
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

	// Token: 0x06001094 RID: 4244 RVA: 0x00079165 File Offset: 0x00077365
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Oculus_ThermaVision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x00079188 File Offset: 0x00077388
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
			this.material.SetFloat("_Value", this.Therma_Variation);
			this.material.SetFloat("_Value2", this.Contrast);
			this.material.SetFloat("_Value3", this.Burn);
			this.material.SetFloat("_Value4", this.SceneCut);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x00079280 File Offset: 0x00077480
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400138A RID: 5002
	public Shader SCShader;

	// Token: 0x0400138B RID: 5003
	private float TimeX = 1f;

	// Token: 0x0400138C RID: 5004
	private Material SCMaterial;

	// Token: 0x0400138D RID: 5005
	[Range(0f, 1f)]
	public float Therma_Variation = 0.5f;

	// Token: 0x0400138E RID: 5006
	[Range(0f, 8f)]
	private float Contrast = 3f;

	// Token: 0x0400138F RID: 5007
	[Range(0f, 4f)]
	private float Burn;

	// Token: 0x04001390 RID: 5008
	[Range(0f, 16f)]
	private float SceneCut = 1f;
}
