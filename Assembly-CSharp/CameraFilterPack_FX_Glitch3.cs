using System;
using UnityEngine;

// Token: 0x020001A6 RID: 422
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Glitch3")]
public class CameraFilterPack_FX_Glitch3 : MonoBehaviour
{
	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000714C6 File Offset: 0x0006F6C6
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

	// Token: 0x06000EF8 RID: 3832 RVA: 0x000714FA File Offset: 0x0006F6FA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Glitch3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0007151C File Offset: 0x0006F71C
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
			this.material.SetFloat("_Glitch", this._Glitch);
			this.material.SetFloat("_Noise", this._Noise);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x000715E8 File Offset: 0x0006F7E8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011BB RID: 4539
	public Shader SCShader;

	// Token: 0x040011BC RID: 4540
	private float TimeX = 1f;

	// Token: 0x040011BD RID: 4541
	private Material SCMaterial;

	// Token: 0x040011BE RID: 4542
	[Range(0f, 1f)]
	public float _Glitch = 1f;

	// Token: 0x040011BF RID: 4543
	[Range(0f, 1f)]
	public float _Noise = 1f;
}
