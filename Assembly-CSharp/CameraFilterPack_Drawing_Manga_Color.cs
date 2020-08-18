using System;
using UnityEngine;

// Token: 0x02000186 RID: 390
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_Color")]
public class CameraFilterPack_Drawing_Manga_Color : MonoBehaviour
{
	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0006E222 File Offset: 0x0006C422
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

	// Token: 0x06000E37 RID: 3639 RVA: 0x0006E256 File Offset: 0x0006C456
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_Color");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0006E278 File Offset: 0x0006C478
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
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0006E2FE File Offset: 0x0006C4FE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010E4 RID: 4324
	public Shader SCShader;

	// Token: 0x040010E5 RID: 4325
	private float TimeX = 1f;

	// Token: 0x040010E6 RID: 4326
	private Material SCMaterial;

	// Token: 0x040010E7 RID: 4327
	[Range(1f, 8f)]
	public float DotSize = 1.6f;

	// Token: 0x040010E8 RID: 4328
	public static float ChangeDotSize;
}
