using System;
using UnityEngine;

// Token: 0x02000151 RID: 337
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/GrayScale")]
public class CameraFilterPack_Color_GrayScale : MonoBehaviour
{
	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000692A6 File Offset: 0x000674A6
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

	// Token: 0x06000CF7 RID: 3319 RVA: 0x000692DA File Offset: 0x000674DA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_GrayScale");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x000692FC File Offset: 0x000674FC
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
			this.material.SetFloat("_Fade", this._Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x000693B2 File Offset: 0x000675B2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FBA RID: 4026
	public Shader SCShader;

	// Token: 0x04000FBB RID: 4027
	private float TimeX = 1f;

	// Token: 0x04000FBC RID: 4028
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x04000FBD RID: 4029
	private Material SCMaterial;
}
