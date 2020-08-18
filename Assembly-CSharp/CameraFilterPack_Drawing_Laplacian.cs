using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Laplacian")]
public class CameraFilterPack_Drawing_Laplacian : MonoBehaviour
{
	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0006D9F7 File Offset: 0x0006BBF7
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

	// Token: 0x06000E0D RID: 3597 RVA: 0x0006DA2B File Offset: 0x0006BC2B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Laplacian");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x0006DA4C File Offset: 0x0006BC4C
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

	// Token: 0x06000E0F RID: 3599 RVA: 0x00069F70 File Offset: 0x00068170
	private void Update()
	{
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x0006DAE9 File Offset: 0x0006BCE9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010C6 RID: 4294
	public Shader SCShader;

	// Token: 0x040010C7 RID: 4295
	private float TimeX = 1f;

	// Token: 0x040010C8 RID: 4296
	private Material SCMaterial;
}
