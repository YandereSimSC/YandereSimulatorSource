using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/EnhancedComics")]
public class CameraFilterPack_Drawing_EnhancedComics : MonoBehaviour
{
	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0006D6A6 File Offset: 0x0006B8A6
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

	// Token: 0x06000E01 RID: 3585 RVA: 0x0006D6DA File Offset: 0x0006B8DA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_EnhancedComics");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x0006D6FC File Offset: 0x0006B8FC
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
			this.material.SetFloat("_DotSize", this.DotSize);
			this.material.SetFloat("_ColorR", this._ColorR);
			this.material.SetFloat("_ColorG", this._ColorG);
			this.material.SetFloat("_ColorB", this._ColorB);
			this.material.SetFloat("_Blood", this._Blood);
			this.material.SetColor("_ColorRGB", this.ColorRGB);
			this.material.SetFloat("_SmoothStart", this._SmoothStart);
			this.material.SetFloat("_SmoothEnd", this._SmoothEnd);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0006D81F File Offset: 0x0006BA1F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010B6 RID: 4278
	public Shader SCShader;

	// Token: 0x040010B7 RID: 4279
	private float TimeX = 1f;

	// Token: 0x040010B8 RID: 4280
	private Material SCMaterial;

	// Token: 0x040010B9 RID: 4281
	[Range(0f, 1f)]
	public float DotSize = 0.15f;

	// Token: 0x040010BA RID: 4282
	[Range(0f, 1f)]
	public float _ColorR = 0.9f;

	// Token: 0x040010BB RID: 4283
	[Range(0f, 1f)]
	public float _ColorG = 0.4f;

	// Token: 0x040010BC RID: 4284
	[Range(0f, 1f)]
	public float _ColorB = 0.4f;

	// Token: 0x040010BD RID: 4285
	[Range(0f, 1f)]
	public float _Blood = 0.5f;

	// Token: 0x040010BE RID: 4286
	[Range(0f, 1f)]
	public float _SmoothStart = 0.02f;

	// Token: 0x040010BF RID: 4287
	[Range(0f, 1f)]
	public float _SmoothEnd = 0.1f;

	// Token: 0x040010C0 RID: 4288
	public Color ColorRGB = new Color(1f, 0f, 0f);
}
