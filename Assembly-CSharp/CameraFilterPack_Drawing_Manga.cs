using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga")]
public class CameraFilterPack_Drawing_Manga : MonoBehaviour
{
	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0006DCBD File Offset: 0x0006BEBD
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

	// Token: 0x06000E19 RID: 3609 RVA: 0x0006DCF1 File Offset: 0x0006BEF1
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x0006DD14 File Offset: 0x0006BF14
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

	// Token: 0x06000E1B RID: 3611 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0006DD9A File Offset: 0x0006BF9A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010D0 RID: 4304
	public Shader SCShader;

	// Token: 0x040010D1 RID: 4305
	private float TimeX = 1f;

	// Token: 0x040010D2 RID: 4306
	private Material SCMaterial;

	// Token: 0x040010D3 RID: 4307
	[Range(1f, 8f)]
	public float DotSize = 4.72f;
}
