using System;
using UnityEngine;

// Token: 0x0200017B RID: 379
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Crosshatch")]
public class CameraFilterPack_Drawing_Crosshatch : MonoBehaviour
{
	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0006D41E File Offset: 0x0006B61E
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

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0006D452 File Offset: 0x0006B652
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Crosshatch");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0006D474 File Offset: 0x0006B674
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
			this.material.SetFloat("_Distortion", this.Width);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x0006D52A File Offset: 0x0006B72A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010AE RID: 4270
	public Shader SCShader;

	// Token: 0x040010AF RID: 4271
	private float TimeX = 1f;

	// Token: 0x040010B0 RID: 4272
	private Material SCMaterial;

	// Token: 0x040010B1 RID: 4273
	[Range(1f, 10f)]
	public float Width = 2f;
}
