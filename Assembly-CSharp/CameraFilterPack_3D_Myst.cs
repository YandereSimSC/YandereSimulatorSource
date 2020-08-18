using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Myst")]
public class CameraFilterPack_3D_Myst : MonoBehaviour
{
	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0005F214 File Offset: 0x0005D414
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

	// Token: 0x06000AEA RID: 2794 RVA: 0x0005F248 File Offset: 0x0005D448
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Myst1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Myst");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0005F280 File Offset: 0x0005D480
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
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_DistortionLevel", this.DistortionLevel * 28f);
			this.material.SetFloat("_DistortionSize", this.DistortionSize * 16f);
			this.material.SetFloat("_LightIntensity", this.LightIntensity * 64f);
			this.material.SetTexture("_MainTex2", this.Texture2);
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			this.material.SetFloat("_FarCamera", 1000f / farClipPlane);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0005F481 File Offset: 0x0005D681
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D18 RID: 3352
	public Shader SCShader;

	// Token: 0x04000D19 RID: 3353
	public bool _Visualize;

	// Token: 0x04000D1A RID: 3354
	private float TimeX = 1f;

	// Token: 0x04000D1B RID: 3355
	private Material SCMaterial;

	// Token: 0x04000D1C RID: 3356
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000D1D RID: 3357
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.5f;

	// Token: 0x04000D1E RID: 3358
	[Range(0f, 0.5f)]
	public float _Size = 0.1f;

	// Token: 0x04000D1F RID: 3359
	[Range(0f, 10f)]
	public float DistortionLevel = 1.2f;

	// Token: 0x04000D20 RID: 3360
	[Range(0.1f, 10f)]
	public float DistortionSize = 1.4f;

	// Token: 0x04000D21 RID: 3361
	[Range(-2f, 4f)]
	public float LightIntensity = 0.08f;

	// Token: 0x04000D22 RID: 3362
	public bool AutoAnimatedNear;

	// Token: 0x04000D23 RID: 3363
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000D24 RID: 3364
	private Texture2D Texture2;

	// Token: 0x04000D25 RID: 3365
	public static Color ChangeColorRGB;
}
