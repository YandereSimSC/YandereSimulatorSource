using System;
using UnityEngine;

// Token: 0x020001E9 RID: 489
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/Deep OilPaint HQ")]
public class CameraFilterPack_Pixelisation_DeepOilPaintHQ : MonoBehaviour
{
	// Token: 0x17000309 RID: 777
	// (get) Token: 0x060010AB RID: 4267 RVA: 0x0007971D File Offset: 0x0007791D
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

	// Token: 0x060010AC RID: 4268 RVA: 0x00079751 File Offset: 0x00077951
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Deep_OilPaintHQ");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00079774 File Offset: 0x00077974
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
			this.material.SetFloat("_LightIntensity", this.Intensity);
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

	// Token: 0x060010AE RID: 4270 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x00079921 File Offset: 0x00077B21
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013A6 RID: 5030
	public Shader SCShader;

	// Token: 0x040013A7 RID: 5031
	private float TimeX = 1f;

	// Token: 0x040013A8 RID: 5032
	public bool _Visualize;

	// Token: 0x040013A9 RID: 5033
	private Material SCMaterial;

	// Token: 0x040013AA RID: 5034
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x040013AB RID: 5035
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x040013AC RID: 5036
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x040013AD RID: 5037
	[Range(0f, 8f)]
	public float Intensity = 1f;

	// Token: 0x040013AE RID: 5038
	public bool AutoAnimatedNear;

	// Token: 0x040013AF RID: 5039
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x040013B0 RID: 5040
	public static Color ChangeColorRGB;
}
