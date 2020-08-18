using System;
using UnityEngine;

// Token: 0x0200020F RID: 527
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Vignetting")]
public class CameraFilterPack_TV_Vignetting : MonoBehaviour
{
	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06001190 RID: 4496 RVA: 0x0007D38E File Offset: 0x0007B58E
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

	// Token: 0x06001191 RID: 4497 RVA: 0x0007D3C2 File Offset: 0x0007B5C2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Vignetting");
		this.Vignette = (Resources.Load("CameraFilterPack_TV_Vignetting1") as Texture2D);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x0007D3F8 File Offset: 0x0007B5F8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetTexture("Vignette", this.Vignette);
			this.material.SetFloat("_Vignetting", this.Vignetting);
			this.material.SetFloat("_Vignetting2", this.VignettingFull);
			this.material.SetColor("_VignettingColor", this.VignettingColor);
			this.material.SetFloat("_VignettingDirt", this.VignettingDirt);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x0007D496 File Offset: 0x0007B696
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400148E RID: 5262
	public Shader SCShader;

	// Token: 0x0400148F RID: 5263
	private Material SCMaterial;

	// Token: 0x04001490 RID: 5264
	private Texture2D Vignette;

	// Token: 0x04001491 RID: 5265
	[Range(0f, 1f)]
	public float Vignetting = 1f;

	// Token: 0x04001492 RID: 5266
	[Range(0f, 1f)]
	public float VignettingFull;

	// Token: 0x04001493 RID: 5267
	[Range(0f, 1f)]
	public float VignettingDirt;

	// Token: 0x04001494 RID: 5268
	public Color VignettingColor = new Color(0f, 0f, 0f, 1f);
}
