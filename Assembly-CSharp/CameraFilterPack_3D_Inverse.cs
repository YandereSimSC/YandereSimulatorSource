using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Inverse")]
public class CameraFilterPack_3D_Inverse : MonoBehaviour
{
	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0005EA83 File Offset: 0x0005CC83
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

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0005EAB7 File Offset: 0x0005CCB7
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Inverse");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0005EAD8 File Offset: 0x0005CCD8
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
			if (this.AutoAnimatedNear)
			{
				this._Distance += Time.deltaTime * this.AutoAnimatedNearSpeed;
				if (this._Distance > 1f)
				{
					this._Distance = -1f;
				}
				if (this._Distance < -1f)
				{
					this._Distance = 1f;
				}
				this.material.SetFloat("_Near", this._Distance);
			}
			else
			{
				this.material.SetFloat("_Near", this._Distance);
			}
			this.material.SetFloat("_Far", this._Size);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_LightIntensity", this.LightIntensity);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			this.material.SetFloat("_FarCamera", 1000f / farClipPlane);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0005EC85 File Offset: 0x0005CE85
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CF5 RID: 3317
	public Shader SCShader;

	// Token: 0x04000CF6 RID: 3318
	private float TimeX = 1f;

	// Token: 0x04000CF7 RID: 3319
	public bool _Visualize;

	// Token: 0x04000CF8 RID: 3320
	private Material SCMaterial;

	// Token: 0x04000CF9 RID: 3321
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x04000CFA RID: 3322
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x04000CFB RID: 3323
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x04000CFC RID: 3324
	[Range(0f, 1f)]
	public float LightIntensity = 1f;

	// Token: 0x04000CFD RID: 3325
	public bool AutoAnimatedNear;

	// Token: 0x04000CFE RID: 3326
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000CFF RID: 3327
	public static Color ChangeColorRGB;
}
