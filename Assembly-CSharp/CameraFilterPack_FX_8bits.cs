using System;
using UnityEngine;

// Token: 0x02000199 RID: 409
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixel/8bits")]
public class CameraFilterPack_FX_8bits : MonoBehaviour
{
	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x000700D2 File Offset: 0x0006E2D2
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

	// Token: 0x06000EAA RID: 3754 RVA: 0x00070106 File Offset: 0x0006E306
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_8bits");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x00070128 File Offset: 0x0006E328
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
			RenderTexture temporary = RenderTexture.GetTemporary(this.ResolutionX, this.ResolutionY, 0);
			Graphics.Blit(sourceTexture, temporary, this.material);
			temporary.filterMode = FilterMode.Point;
			Graphics.Blit(temporary, destTexture);
			RenderTexture.ReleaseTemporary(temporary);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x000701F0 File Offset: 0x0006E3F0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400116A RID: 4458
	public Shader SCShader;

	// Token: 0x0400116B RID: 4459
	private float TimeX = 1f;

	// Token: 0x0400116C RID: 4460
	private Material SCMaterial;

	// Token: 0x0400116D RID: 4461
	[Range(-1f, 1f)]
	public float Brightness;

	// Token: 0x0400116E RID: 4462
	[Range(80f, 640f)]
	public int ResolutionX = 160;

	// Token: 0x0400116F RID: 4463
	[Range(60f, 480f)]
	public int ResolutionY = 240;
}
