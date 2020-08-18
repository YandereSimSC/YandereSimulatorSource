using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/BlackHole")]
public class CameraFilterPack_3D_BlackHole : MonoBehaviour
{
	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0005DD31 File Offset: 0x0005BF31
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

	// Token: 0x06000ABA RID: 2746 RVA: 0x0005DD65 File Offset: 0x0005BF65
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_BlackHole");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0005DD88 File Offset: 0x0005BF88
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
			this.material.SetFloat("_DistortionLevel", this.DistortionLevel);
			this.material.SetFloat("_DistortionSize", this.DistortionSize);
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

	// Token: 0x06000ABC RID: 2748 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0005DF4B File Offset: 0x0005C14B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CB4 RID: 3252
	public Shader SCShader;

	// Token: 0x04000CB5 RID: 3253
	private float TimeX = 1f;

	// Token: 0x04000CB6 RID: 3254
	public bool _Visualize;

	// Token: 0x04000CB7 RID: 3255
	private Material SCMaterial;

	// Token: 0x04000CB8 RID: 3256
	[Range(0f, 100f)]
	public float _FixDistance = 5f;

	// Token: 0x04000CB9 RID: 3257
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.05f;

	// Token: 0x04000CBA RID: 3258
	[Range(0f, 1f)]
	public float _Size = 0.25f;

	// Token: 0x04000CBB RID: 3259
	[Range(-2f, 2f)]
	public float DistortionLevel = 1.2f;

	// Token: 0x04000CBC RID: 3260
	[Range(0f, 1f)]
	public float DistortionSize;

	// Token: 0x04000CBD RID: 3261
	public bool AutoAnimatedNear;

	// Token: 0x04000CBE RID: 3262
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000CBF RID: 3263
	public static Color ChangeColorRGB;
}
