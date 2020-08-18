using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Edge/Sobel")]
public class CameraFilterPack_Edge_Sobel : MonoBehaviour
{
	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0006FC6B File Offset: 0x0006DE6B
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

	// Token: 0x06000E98 RID: 3736 RVA: 0x0006FC9F File Offset: 0x0006DE9F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Edge_Sobel");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0006FCC0 File Offset: 0x0006DEC0
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0006FD5D File Offset: 0x0006DF5D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001157 RID: 4439
	public Shader SCShader;

	// Token: 0x04001158 RID: 4440
	private float TimeX = 1f;

	// Token: 0x04001159 RID: 4441
	private Material SCMaterial;
}
