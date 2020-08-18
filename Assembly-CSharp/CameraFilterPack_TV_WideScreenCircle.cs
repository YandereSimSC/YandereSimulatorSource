using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenCircle")]
public class CameraFilterPack_TV_WideScreenCircle : MonoBehaviour
{
	// Token: 0x17000331 RID: 817
	// (get) Token: 0x0600119C RID: 4508 RVA: 0x0007D5F6 File Offset: 0x0007B7F6
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

	// Token: 0x0600119D RID: 4509 RVA: 0x0007D62A File Offset: 0x0007B82A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenCircle");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0007D64C File Offset: 0x0007B84C
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

	// Token: 0x0600119F RID: 4511 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0007D744 File Offset: 0x0007B944
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001499 RID: 5273
	public Shader SCShader;

	// Token: 0x0400149A RID: 5274
	private float TimeX = 1f;

	// Token: 0x0400149B RID: 5275
	private Material SCMaterial;

	// Token: 0x0400149C RID: 5276
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x0400149D RID: 5277
	[Range(0.01f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x0400149E RID: 5278
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x0400149F RID: 5279
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
