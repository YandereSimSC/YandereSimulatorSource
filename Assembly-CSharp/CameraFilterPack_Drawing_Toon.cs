using System;
using UnityEngine;

// Token: 0x0200018E RID: 398
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Toon")]
public class CameraFilterPack_Drawing_Toon : MonoBehaviour
{
	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0006F182 File Offset: 0x0006D382
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

	// Token: 0x06000E67 RID: 3687 RVA: 0x0006F1B6 File Offset: 0x0006D3B6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Toon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0006F1D8 File Offset: 0x0006D3D8
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

	// Token: 0x06000E69 RID: 3689 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x0006F274 File Offset: 0x0006D474
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400112D RID: 4397
	public Shader SCShader;

	// Token: 0x0400112E RID: 4398
	private Material SCMaterial;

	// Token: 0x0400112F RID: 4399
	private float TimeX = 1f;

	// Token: 0x04001130 RID: 4400
	[Range(0f, 2f)]
	public float Threshold = 1f;

	// Token: 0x04001131 RID: 4401
	[Range(0f, 8f)]
	public float DotSize = 1f;
}
