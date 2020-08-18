using System;
using UnityEngine;

// Token: 0x0200014B RID: 331
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Classic/ThermalVision")]
public class CameraFilterPack_Classic_ThermalVision : MonoBehaviour
{
	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0006895C File Offset: 0x00066B5C
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

	// Token: 0x06000CD3 RID: 3283 RVA: 0x00068990 File Offset: 0x00066B90
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_Classic_ThermalVision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x000689B4 File Offset: 0x00066BB4
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
			this.material.SetFloat("_Speed", this.__Speed);
			this.material.SetFloat("Fade", this._Fade);
			this.material.SetFloat("Crt", this._Crt);
			this.material.SetFloat("Curve", this._Curve);
			this.material.SetFloat("Color1", this._Color1);
			this.material.SetFloat("Color2", this._Color2);
			this.material.SetFloat("Color3", this._Color3);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x00068AEE File Offset: 0x00066CEE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F94 RID: 3988
	public Shader SCShader;

	// Token: 0x04000F95 RID: 3989
	private float TimeX = 1f;

	// Token: 0x04000F96 RID: 3990
	private Material SCMaterial;

	// Token: 0x04000F97 RID: 3991
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04000F98 RID: 3992
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x04000F99 RID: 3993
	[Range(0f, 1f)]
	public float _Crt = 1f;

	// Token: 0x04000F9A RID: 3994
	[Range(0f, 1f)]
	public float _Curve = 1f;

	// Token: 0x04000F9B RID: 3995
	[Range(0f, 1f)]
	public float _Color1 = 1f;

	// Token: 0x04000F9C RID: 3996
	[Range(0f, 1f)]
	public float _Color2 = 1f;

	// Token: 0x04000F9D RID: 3997
	[Range(0f, 1f)]
	public float _Color3 = 1f;
}
