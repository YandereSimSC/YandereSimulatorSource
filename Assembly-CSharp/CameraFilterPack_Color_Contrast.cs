using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Contrast")]
public class CameraFilterPack_Color_Contrast : MonoBehaviour
{
	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00069160 File Offset: 0x00067360
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

	// Token: 0x06000CF1 RID: 3313 RVA: 0x00069194 File Offset: 0x00067394
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Contrast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x000691B8 File Offset: 0x000673B8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_Contrast", this.Contrast);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0006926E File Offset: 0x0006746E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FB6 RID: 4022
	public Shader SCShader;

	// Token: 0x04000FB7 RID: 4023
	private float TimeX = 1f;

	// Token: 0x04000FB8 RID: 4024
	private Material SCMaterial;

	// Token: 0x04000FB9 RID: 4025
	[Range(0f, 10f)]
	public float Contrast = 4.5f;
}
