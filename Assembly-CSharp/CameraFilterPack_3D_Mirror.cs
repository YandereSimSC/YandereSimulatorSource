﻿using System;
using UnityEngine;

// Token: 0x02000103 RID: 259
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Mirror")]
public class CameraFilterPack_3D_Mirror : MonoBehaviour
{
	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0005EF7C File Offset: 0x0005D17C
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

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0005EFB0 File Offset: 0x0005D1B0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Mirror");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0005EFD4 File Offset: 0x0005D1D4
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("Lightning", this.Lightning);
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

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0005F197 File Offset: 0x0005D397
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D0C RID: 3340
	public Shader SCShader;

	// Token: 0x04000D0D RID: 3341
	public bool _Visualize;

	// Token: 0x04000D0E RID: 3342
	private float TimeX = 1f;

	// Token: 0x04000D0F RID: 3343
	private Material SCMaterial;

	// Token: 0x04000D10 RID: 3344
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x04000D11 RID: 3345
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x04000D12 RID: 3346
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x04000D13 RID: 3347
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000D14 RID: 3348
	[Range(0f, 2f)]
	public float Lightning = 2f;

	// Token: 0x04000D15 RID: 3349
	public bool AutoAnimatedNear;

	// Token: 0x04000D16 RID: 3350
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000D17 RID: 3351
	public static Color ChangeColorRGB;
}
