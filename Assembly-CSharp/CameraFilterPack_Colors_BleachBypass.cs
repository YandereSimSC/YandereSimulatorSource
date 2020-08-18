using System;
using UnityEngine;

// Token: 0x0200015B RID: 347
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/BleachBypass")]
public class CameraFilterPack_Colors_BleachBypass : MonoBehaviour
{
	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0006A6D4 File Offset: 0x000688D4
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

	// Token: 0x06000D35 RID: 3381 RVA: 0x0006A708 File Offset: 0x00068908
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_BleachBypass");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0006A72C File Offset: 0x0006892C
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0006A7E2 File Offset: 0x000689E2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FF5 RID: 4085
	public Shader SCShader;

	// Token: 0x04000FF6 RID: 4086
	private float TimeX = 1f;

	// Token: 0x04000FF7 RID: 4087
	private Material SCMaterial;

	// Token: 0x04000FF8 RID: 4088
	[Range(-1f, 2f)]
	public float Value = 1f;
}
