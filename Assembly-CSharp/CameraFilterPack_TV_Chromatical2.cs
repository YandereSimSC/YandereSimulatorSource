using System;
using UnityEngine;

// Token: 0x020001FC RID: 508
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Chromatical2")]
public class CameraFilterPack_TV_Chromatical2 : MonoBehaviour
{
	// Token: 0x1700031C RID: 796
	// (get) Token: 0x0600111E RID: 4382 RVA: 0x0007B9AF File Offset: 0x00079BAF
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

	// Token: 0x0600111F RID: 4383 RVA: 0x0007B9E3 File Offset: 0x00079BE3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Chromatical2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0007BA04 File Offset: 0x00079C04
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
			this.material.SetFloat("_Value", this.Aberration);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("ZoomFade", this.ZoomFade);
			this.material.SetFloat("ZoomSpeed", this.ZoomSpeed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0007BAFC File Offset: 0x00079CFC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400142C RID: 5164
	public Shader SCShader;

	// Token: 0x0400142D RID: 5165
	private float TimeX = 1f;

	// Token: 0x0400142E RID: 5166
	private Material SCMaterial;

	// Token: 0x0400142F RID: 5167
	[Range(0f, 10f)]
	public float Aberration = 2f;

	// Token: 0x04001430 RID: 5168
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001431 RID: 5169
	[Range(0f, 1f)]
	public float ZoomFade = 1f;

	// Token: 0x04001432 RID: 5170
	[Range(0f, 8f)]
	public float ZoomSpeed = 1f;
}
