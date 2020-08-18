using System;
using UnityEngine;

// Token: 0x020001B4 RID: 436
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Film/ColorPerfection")]
public class CameraFilterPack_Film_ColorPerfection : MonoBehaviour
{
	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00072742 File Offset: 0x00070942
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

	// Token: 0x06000F4C RID: 3916 RVA: 0x00072776 File Offset: 0x00070976
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Film_ColorPerfection");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00072798 File Offset: 0x00070998
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
			this.material.SetFloat("_Value", this.Gamma);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x00072890 File Offset: 0x00070A90
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011FC RID: 4604
	public Shader SCShader;

	// Token: 0x040011FD RID: 4605
	private float TimeX = 1f;

	// Token: 0x040011FE RID: 4606
	private Material SCMaterial;

	// Token: 0x040011FF RID: 4607
	[Range(0f, 4f)]
	public float Gamma = 0.55f;

	// Token: 0x04001200 RID: 4608
	[Range(0f, 10f)]
	private float Value2 = 1f;

	// Token: 0x04001201 RID: 4609
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001202 RID: 4610
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
