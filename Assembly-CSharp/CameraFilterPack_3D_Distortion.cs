using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Distortion")]
public class CameraFilterPack_3D_Distortion : MonoBehaviour
{
	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0005E244 File Offset: 0x0005C444
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

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0005E278 File Offset: 0x0005C478
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Distortion");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0005E29C File Offset: 0x0005C49C
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
			this.material.SetFloat("_DistortionLevel", this.DistortionLevel * 28f);
			this.material.SetFloat("_DistortionSize", this.DistortionSize * 16f);
			this.material.SetFloat("_LightIntensity", this.LightIntensity * 64f);
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

	// Token: 0x06000AC8 RID: 2760 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0005E487 File Offset: 0x0005C687
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CCC RID: 3276
	public Shader SCShader;

	// Token: 0x04000CCD RID: 3277
	private float TimeX = 1f;

	// Token: 0x04000CCE RID: 3278
	public bool _Visualize;

	// Token: 0x04000CCF RID: 3279
	private Material SCMaterial;

	// Token: 0x04000CD0 RID: 3280
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000CD1 RID: 3281
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.5f;

	// Token: 0x04000CD2 RID: 3282
	[Range(0f, 0.5f)]
	public float _Size = 0.1f;

	// Token: 0x04000CD3 RID: 3283
	[Range(0f, 10f)]
	public float DistortionLevel = 1.2f;

	// Token: 0x04000CD4 RID: 3284
	[Range(0.1f, 10f)]
	public float DistortionSize = 1.4f;

	// Token: 0x04000CD5 RID: 3285
	[Range(-2f, 4f)]
	public float LightIntensity = 0.08f;

	// Token: 0x04000CD6 RID: 3286
	public bool AutoAnimatedNear;

	// Token: 0x04000CD7 RID: 3287
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000CD8 RID: 3288
	public static Color ChangeColorRGB;
}
