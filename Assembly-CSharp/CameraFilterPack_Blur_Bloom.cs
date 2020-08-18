using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Bloom")]
public class CameraFilterPack_Blur_Bloom : MonoBehaviour
{
	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00066961 File Offset: 0x00064B61
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

	// Token: 0x06000C61 RID: 3169 RVA: 0x00066995 File Offset: 0x00064B95
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Bloom");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x000669B8 File Offset: 0x00064BB8
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
			this.material.SetFloat("_Amount", this.Amount);
			this.material.SetFloat("_Glow", this.Glow);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00066A7D File Offset: 0x00064C7D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F1B RID: 3867
	public Shader SCShader;

	// Token: 0x04000F1C RID: 3868
	private float TimeX = 1f;

	// Token: 0x04000F1D RID: 3869
	private Material SCMaterial;

	// Token: 0x04000F1E RID: 3870
	[Range(0f, 10f)]
	public float Amount = 4.5f;

	// Token: 0x04000F1F RID: 3871
	[Range(0f, 1f)]
	public float Glow = 0.5f;
}
