using System;
using UnityEngine;

// Token: 0x02000208 RID: 520
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/RGB Display")]
public class CameraFilterPack_TV_Rgb : MonoBehaviour
{
	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06001166 RID: 4454 RVA: 0x0007C9DF File Offset: 0x0007ABDF
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

	// Token: 0x06001167 RID: 4455 RVA: 0x0007CA13 File Offset: 0x0007AC13
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Rgb");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0007CA34 File Offset: 0x0007AC34
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0007CAEA File Offset: 0x0007ACEA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400146A RID: 5226
	public Shader SCShader;

	// Token: 0x0400146B RID: 5227
	private float TimeX = 1f;

	// Token: 0x0400146C RID: 5228
	[Range(0.01f, 4f)]
	public float Distortion = 1f;

	// Token: 0x0400146D RID: 5229
	private Material SCMaterial;
}
