using System;
using UnityEngine;

// Token: 0x020001AB RID: 427
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/InverChromiLum")]
public class CameraFilterPack_FX_InverChromiLum : MonoBehaviour
{
	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00071B3E File Offset: 0x0006FD3E
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

	// Token: 0x06000F16 RID: 3862 RVA: 0x00071B72 File Offset: 0x0006FD72
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_InverChromiLum");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x00071B94 File Offset: 0x0006FD94
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x00071C31 File Offset: 0x0006FE31
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011D3 RID: 4563
	public Shader SCShader;

	// Token: 0x040011D4 RID: 4564
	private float TimeX = 1f;

	// Token: 0x040011D5 RID: 4565
	private Material SCMaterial;
}
