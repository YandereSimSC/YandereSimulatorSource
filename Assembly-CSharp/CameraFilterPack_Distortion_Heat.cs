using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Heat")]
public class CameraFilterPack_Distortion_Heat : MonoBehaviour
{
	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0006C068 File Offset: 0x0006A268
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

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0006C09C File Offset: 0x0006A29C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Heat");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0006C0C0 File Offset: 0x0006A2C0
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0006C176 File Offset: 0x0006A376
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400105D RID: 4189
	public Shader SCShader;

	// Token: 0x0400105E RID: 4190
	private float TimeX = 1f;

	// Token: 0x0400105F RID: 4191
	private Material SCMaterial;

	// Token: 0x04001060 RID: 4192
	[Range(1f, 100f)]
	public float Distortion = 35f;
}
