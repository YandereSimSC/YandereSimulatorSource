using System;
using UnityEngine;

// Token: 0x02000184 RID: 388
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga4")]
public class CameraFilterPack_Drawing_Manga4 : MonoBehaviour
{
	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0006DFFA File Offset: 0x0006C1FA
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

	// Token: 0x06000E2B RID: 3627 RVA: 0x0006E02E File Offset: 0x0006C22E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga4");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0006E050 File Offset: 0x0006C250
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

	// Token: 0x06000E2D RID: 3629 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x0006E0D6 File Offset: 0x0006C2D6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010DC RID: 4316
	public Shader SCShader;

	// Token: 0x040010DD RID: 4317
	private float TimeX = 1f;

	// Token: 0x040010DE RID: 4318
	private Material SCMaterial;

	// Token: 0x040010DF RID: 4319
	[Range(1f, 8f)]
	public float DotSize = 4.72f;
}
