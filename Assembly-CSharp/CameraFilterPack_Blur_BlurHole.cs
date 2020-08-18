using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Blur Hole")]
public class CameraFilterPack_Blur_BlurHole : MonoBehaviour
{
	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00066AC0 File Offset: 0x00064CC0
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

	// Token: 0x06000C67 RID: 3175 RVA: 0x00066AF4 File Offset: 0x00064CF4
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/BlurHole");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00066B18 File Offset: 0x00064D18
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
			this.material.SetFloat("_Radius", this._Radius);
			this.material.SetFloat("_SpotSize", this._SpotSize);
			this.material.SetFloat("_CenterX", this._CenterX);
			this.material.SetFloat("_CenterY", this._CenterY);
			this.material.SetFloat("_Alpha", this._AlphaBlur);
			this.material.SetFloat("_Alpha2", this._AlphaBlurInside);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00066C4B File Offset: 0x00064E4B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F20 RID: 3872
	public Shader SCShader;

	// Token: 0x04000F21 RID: 3873
	private float TimeX = 1f;

	// Token: 0x04000F22 RID: 3874
	[Range(1f, 16f)]
	public float Size = 10f;

	// Token: 0x04000F23 RID: 3875
	[Range(-1f, 1f)]
	public float _Radius = 0.25f;

	// Token: 0x04000F24 RID: 3876
	[Range(-4f, 4f)]
	public float _SpotSize = 1f;

	// Token: 0x04000F25 RID: 3877
	[Range(0f, 1f)]
	public float _CenterX = 0.5f;

	// Token: 0x04000F26 RID: 3878
	[Range(0f, 1f)]
	public float _CenterY = 0.5f;

	// Token: 0x04000F27 RID: 3879
	[Range(0f, 1f)]
	public float _AlphaBlur = 1f;

	// Token: 0x04000F28 RID: 3880
	[Range(0f, 1f)]
	public float _AlphaBlurInside;

	// Token: 0x04000F29 RID: 3881
	private Material SCMaterial;
}
