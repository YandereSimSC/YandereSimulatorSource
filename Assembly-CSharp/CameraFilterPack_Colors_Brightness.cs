using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Brightness")]
public class CameraFilterPack_Colors_Brightness : MonoBehaviour
{
	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0006A81A File Offset: 0x00068A1A
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

	// Token: 0x06000D3B RID: 3387 RVA: 0x0006A84E File Offset: 0x00068A4E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_Brightness");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0006A86F File Offset: 0x00068A6F
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_Val", this._Brightness);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0006A8AA File Offset: 0x00068AAA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FF9 RID: 4089
	public Shader SCShader;

	// Token: 0x04000FFA RID: 4090
	[Range(0f, 2f)]
	public float _Brightness = 1.5f;

	// Token: 0x04000FFB RID: 4091
	private Material SCMaterial;
}
