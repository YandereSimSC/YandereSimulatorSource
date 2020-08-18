using System;
using UnityEngine;

// Token: 0x02000182 RID: 386
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga2")]
public class CameraFilterPack_Drawing_Manga2 : MonoBehaviour
{
	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0006DDD2 File Offset: 0x0006BFD2
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

	// Token: 0x06000E1F RID: 3615 RVA: 0x0006DE06 File Offset: 0x0006C006
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0006DE28 File Offset: 0x0006C028
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

	// Token: 0x06000E21 RID: 3617 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x0006DEAE File Offset: 0x0006C0AE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010D4 RID: 4308
	public Shader SCShader;

	// Token: 0x040010D5 RID: 4309
	private float TimeX = 1f;

	// Token: 0x040010D6 RID: 4310
	private Material SCMaterial;

	// Token: 0x040010D7 RID: 4311
	[Range(1f, 8f)]
	public float DotSize = 4.72f;
}
