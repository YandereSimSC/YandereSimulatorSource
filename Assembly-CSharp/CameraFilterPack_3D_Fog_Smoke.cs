using System;
using UnityEngine;

// Token: 0x020000FF RID: 255
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Fog_Smoke")]
public class CameraFilterPack_3D_Fog_Smoke : MonoBehaviour
{
	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0005E50F File Offset: 0x0005C70F
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

	// Token: 0x06000ACC RID: 2764 RVA: 0x0005E543 File Offset: 0x0005C743
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

	// Token: 0x06000ACD RID: 2765 RVA: 0x0005E57C File Offset: 0x0005C77C
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

	// Token: 0x06000ACE RID: 2766 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0005E77D File Offset: 0x0005C97D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CD9 RID: 3289
	public Shader SCShader;

	// Token: 0x04000CDA RID: 3290
	public bool _Visualize;

	// Token: 0x04000CDB RID: 3291
	private float TimeX = 1f;

	// Token: 0x04000CDC RID: 3292
	private Material SCMaterial;

	// Token: 0x04000CDD RID: 3293
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000CDE RID: 3294
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.5f;

	// Token: 0x04000CDF RID: 3295
	[Range(0f, 0.5f)]
	public float _Size = 0.1f;

	// Token: 0x04000CE0 RID: 3296
	[Range(0f, 10f)]
	public float DistortionLevel = 1.2f;

	// Token: 0x04000CE1 RID: 3297
	[Range(0.1f, 10f)]
	public float DistortionSize = 1.4f;

	// Token: 0x04000CE2 RID: 3298
	[Range(-2f, 4f)]
	public float LightIntensity = 0.08f;

	// Token: 0x04000CE3 RID: 3299
	public bool AutoAnimatedNear;

	// Token: 0x04000CE4 RID: 3300
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000CE5 RID: 3301
	private Texture2D Texture2;

	// Token: 0x04000CE6 RID: 3302
	public static Color ChangeColorRGB;
}
