using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Antialiasing/FXAA")]
public class CameraFilterPack_Antialiasing_FXAA : MonoBehaviour
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00061142 File Offset: 0x0005F342
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

	// Token: 0x06000B38 RID: 2872 RVA: 0x00061176 File Offset: 0x0005F376
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Antialiasing_FXAA");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00061198 File Offset: 0x0005F398
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x0006122E File Offset: 0x0005F42E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DB2 RID: 3506
	public Shader SCShader;

	// Token: 0x04000DB3 RID: 3507
	private float TimeX = 1f;

	// Token: 0x04000DB4 RID: 3508
	private Material SCMaterial;
}
