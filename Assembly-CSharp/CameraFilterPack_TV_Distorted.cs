using System;
using UnityEngine;

// Token: 0x020001FE RID: 510
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Distorted")]
public class CameraFilterPack_TV_Distorted : MonoBehaviour
{
	// Token: 0x1700031E RID: 798
	// (get) Token: 0x0600112A RID: 4394 RVA: 0x0007BC9A File Offset: 0x00079E9A
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

	// Token: 0x0600112B RID: 4395 RVA: 0x0007BCCE File Offset: 0x00079ECE
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Distorted");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0007BCF0 File Offset: 0x00079EF0
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
			this.material.SetFloat("_RGB", this.RGB);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x0007BD8C File Offset: 0x00079F8C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001437 RID: 5175
	public Shader SCShader;

	// Token: 0x04001438 RID: 5176
	private float TimeX = 1f;

	// Token: 0x04001439 RID: 5177
	[Range(0f, 10f)]
	public float Distortion = 1f;

	// Token: 0x0400143A RID: 5178
	[Range(-0.01f, 0.01f)]
	public float RGB = 0.002f;

	// Token: 0x0400143B RID: 5179
	private Material SCMaterial;
}
