using System;
using UnityEngine;

// Token: 0x0200021E RID: 542
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Psycho")]
public class CameraFilterPack_Vision_Psycho : MonoBehaviour
{
	// Token: 0x1700033E RID: 830
	// (get) Token: 0x060011EA RID: 4586 RVA: 0x0007EC25 File Offset: 0x0007CE25
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

	// Token: 0x060011EB RID: 4587 RVA: 0x0007EC59 File Offset: 0x0007CE59
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Psycho");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x0007EC7C File Offset: 0x0007CE7C
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
			this.material.SetFloat("_Value", this.HoleSize);
			this.material.SetFloat("_Value2", this.HoleSmooth);
			this.material.SetFloat("_Value3", this.Color1);
			this.material.SetFloat("_Value4", this.Color2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x0007ED74 File Offset: 0x0007CF74
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014F7 RID: 5367
	public Shader SCShader;

	// Token: 0x040014F8 RID: 5368
	private float TimeX = 1f;

	// Token: 0x040014F9 RID: 5369
	private Material SCMaterial;

	// Token: 0x040014FA RID: 5370
	[Range(0.01f, 1f)]
	public float HoleSize = 0.6f;

	// Token: 0x040014FB RID: 5371
	[Range(-1f, 1f)]
	public float HoleSmooth = 0.3f;

	// Token: 0x040014FC RID: 5372
	[Range(-2f, 2f)]
	public float Color1 = 0.2f;

	// Token: 0x040014FD RID: 5373
	[Range(-2f, 2f)]
	public float Color2 = 0.9f;
}
