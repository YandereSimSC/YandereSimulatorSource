using System;
using UnityEngine;

// Token: 0x0200019E RID: 414
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/DigitalMatrixDistortion")]
public class CameraFilterPack_FX_DigitalMatrixDistortion : MonoBehaviour
{
	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x000708A9 File Offset: 0x0006EAA9
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

	// Token: 0x06000EC8 RID: 3784 RVA: 0x000708DD File Offset: 0x0006EADD
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_DigitalMatrixDistortion");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00070900 File Offset: 0x0006EB00
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Distortion);
			this.material.SetFloat("_Value5", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x000709E2 File Offset: 0x0006EBE2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400118A RID: 4490
	public Shader SCShader;

	// Token: 0x0400118B RID: 4491
	private float TimeX = 1f;

	// Token: 0x0400118C RID: 4492
	private Material SCMaterial;

	// Token: 0x0400118D RID: 4493
	[Range(0.4f, 5f)]
	public float Size = 1.4f;

	// Token: 0x0400118E RID: 4494
	[Range(-2f, 2f)]
	public float Speed = 0.5f;

	// Token: 0x0400118F RID: 4495
	[Range(-5f, 5f)]
	public float Distortion = 2.3f;
}
