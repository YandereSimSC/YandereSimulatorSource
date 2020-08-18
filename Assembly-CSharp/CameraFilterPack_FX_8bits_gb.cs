using System;
using UnityEngine;

// Token: 0x0200019A RID: 410
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixel/8bits_gb")]
public class CameraFilterPack_FX_8bits_gb : MonoBehaviour
{
	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00070233 File Offset: 0x0006E433
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

	// Token: 0x06000EB0 RID: 3760 RVA: 0x00070267 File Offset: 0x0006E467
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_8bits_gb");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x00070288 File Offset: 0x0006E488
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
			if (this.Brightness == 0f)
			{
				this.Brightness = 0.001f;
			}
			this.material.SetFloat("_Distortion", this.Brightness);
			RenderTexture temporary = RenderTexture.GetTemporary(160, 144, 0);
			Graphics.Blit(sourceTexture, temporary, this.material);
			temporary.filterMode = FilterMode.Point;
			Graphics.Blit(temporary, destTexture);
			RenderTexture.ReleaseTemporary(temporary);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0007034E File Offset: 0x0006E54E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001170 RID: 4464
	public Shader SCShader;

	// Token: 0x04001171 RID: 4465
	private float TimeX = 1f;

	// Token: 0x04001172 RID: 4466
	private Material SCMaterial;

	// Token: 0x04001173 RID: 4467
	[Range(-1f, 1f)]
	public float Brightness;
}
