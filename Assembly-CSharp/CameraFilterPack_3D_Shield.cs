using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Shield")]
public class CameraFilterPack_3D_Shield : MonoBehaviour
{
	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0005F7B0 File Offset: 0x0005D9B0
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

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0005F7E4 File Offset: 0x0005D9E4
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Shield");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0005F808 File Offset: 0x0005DA08
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
			this.material.SetFloat("_LightIntensity", this.LightIntensity * 64f);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FadeShield", this._FadeShield);
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.Speed_X);
			this.material.SetFloat("_Value3", this.Speed_Y);
			this.material.SetFloat("_Value4", this.Intensity);
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			this.material.SetFloat("_FarCamera", 1000f / farClipPlane);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0005FA29 File Offset: 0x0005DC29
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D33 RID: 3379
	public Shader SCShader;

	// Token: 0x04000D34 RID: 3380
	public bool _Visualize;

	// Token: 0x04000D35 RID: 3381
	private float TimeX = 1f;

	// Token: 0x04000D36 RID: 3382
	private Material SCMaterial;

	// Token: 0x04000D37 RID: 3383
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x04000D38 RID: 3384
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x04000D39 RID: 3385
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x04000D3A RID: 3386
	[Range(0f, 1f)]
	public float _FadeShield = 0.75f;

	// Token: 0x04000D3B RID: 3387
	[Range(-0.2f, 0.2f)]
	public float LightIntensity = 0.025f;

	// Token: 0x04000D3C RID: 3388
	public bool AutoAnimatedNear;

	// Token: 0x04000D3D RID: 3389
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000D3E RID: 3390
	[Range(0f, 10f)]
	public float Speed = 0.2f;

	// Token: 0x04000D3F RID: 3391
	[Range(0f, 10f)]
	public float Speed_X = 0.2f;

	// Token: 0x04000D40 RID: 3392
	[Range(0f, 1f)]
	public float Speed_Y = 0.3f;

	// Token: 0x04000D41 RID: 3393
	[Range(0f, 10f)]
	public float Intensity = 2.4f;

	// Token: 0x04000D42 RID: 3394
	public static Color ChangeColorRGB;
}
