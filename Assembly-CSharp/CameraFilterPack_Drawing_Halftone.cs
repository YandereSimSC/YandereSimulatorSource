using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Halftone")]
public class CameraFilterPack_Drawing_Halftone : MonoBehaviour
{
	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0006D8C1 File Offset: 0x0006BAC1
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

	// Token: 0x06000E07 RID: 3591 RVA: 0x0006D8F5 File Offset: 0x0006BAF5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Halftone");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0006D918 File Offset: 0x0006BB18
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
			this.material.SetFloat("_Distortion", this.Threshold);
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0006D9B4 File Offset: 0x0006BBB4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010C1 RID: 4289
	public Shader SCShader;

	// Token: 0x040010C2 RID: 4290
	private float TimeX = 1f;

	// Token: 0x040010C3 RID: 4291
	private Material SCMaterial;

	// Token: 0x040010C4 RID: 4292
	[Range(0f, 1f)]
	public float Threshold = 0.6f;

	// Token: 0x040010C5 RID: 4293
	[Range(1f, 16f)]
	public float DotSize = 4f;
}
