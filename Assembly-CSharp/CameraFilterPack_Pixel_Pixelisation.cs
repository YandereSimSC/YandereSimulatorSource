using System;
using UnityEngine;

// Token: 0x020001E8 RID: 488
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixel/Pixelisation")]
public class CameraFilterPack_Pixel_Pixelisation : MonoBehaviour
{
	// Token: 0x17000308 RID: 776
	// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00079613 File Offset: 0x00077813
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

	// Token: 0x060010A6 RID: 4262 RVA: 0x00079647 File Offset: 0x00077847
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixel_Pixelisation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x00079668 File Offset: 0x00077868
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_Val", this._Pixelisation);
			this.material.SetFloat("_Val2", this._SizeX);
			this.material.SetFloat("_Val3", this._SizeY);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x000796DA File Offset: 0x000778DA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013A1 RID: 5025
	public Shader SCShader;

	// Token: 0x040013A2 RID: 5026
	[Range(0.6f, 120f)]
	public float _Pixelisation = 8f;

	// Token: 0x040013A3 RID: 5027
	[Range(0.6f, 120f)]
	public float _SizeX = 1f;

	// Token: 0x040013A4 RID: 5028
	[Range(0.6f, 120f)]
	public float _SizeY = 1f;

	// Token: 0x040013A5 RID: 5029
	private Material SCMaterial;
}
