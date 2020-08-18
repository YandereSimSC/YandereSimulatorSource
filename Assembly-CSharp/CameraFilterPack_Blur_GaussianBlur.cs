using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/GaussianBlur")]
public class CameraFilterPack_Blur_GaussianBlur : MonoBehaviour
{
	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000672DB File Offset: 0x000654DB
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

	// Token: 0x06000C85 RID: 3205 RVA: 0x0006730F File Offset: 0x0006550F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_GaussianBlur");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00067330 File Offset: 0x00065530
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
			this.material.SetFloat("_Distortion", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000673DF File Offset: 0x000655DF
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F40 RID: 3904
	public Shader SCShader;

	// Token: 0x04000F41 RID: 3905
	private float TimeX = 1f;

	// Token: 0x04000F42 RID: 3906
	[Range(1f, 16f)]
	public float Size = 10f;

	// Token: 0x04000F43 RID: 3907
	private Material SCMaterial;
}
