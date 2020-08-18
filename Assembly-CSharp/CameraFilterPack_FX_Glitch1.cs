using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Glitch1")]
public class CameraFilterPack_FX_Glitch1 : MonoBehaviour
{
	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0007123E File Offset: 0x0006F43E
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

	// Token: 0x06000EEC RID: 3820 RVA: 0x00071272 File Offset: 0x0006F472
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Glitch1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x00071294 File Offset: 0x0006F494
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
			this.material.SetFloat("_Glitch", this.Glitch);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x0007134A File Offset: 0x0006F54A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011B3 RID: 4531
	public Shader SCShader;

	// Token: 0x040011B4 RID: 4532
	private float TimeX = 1f;

	// Token: 0x040011B5 RID: 4533
	private Material SCMaterial;

	// Token: 0x040011B6 RID: 4534
	[Range(0f, 1f)]
	public float Glitch = 1f;
}
