using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Scan_Scene")]
public class CameraFilterPack_3D_Scan_Scene : MonoBehaviour
{
	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0005F507 File Offset: 0x0005D707
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

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0005F53B File Offset: 0x0005D73B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Scan_Scene");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0005F55C File Offset: 0x0005D75C
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
			this.material.SetFloat("_DepthLevel", this.Fade);
			if (this.AutoAnimatedNear)
			{
				this._Distance += Time.deltaTime * this.AutoAnimatedNearSpeed;
				if (this._Distance > 1f)
				{
					this._Distance = 0f;
				}
				if (this._Distance < 0f)
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
			this.material.SetColor("_ColorRGB", this.ScanColor);
			this.material.SetFloat("_FixDistance", this._FixDistance);
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

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0005F71F File Offset: 0x0005D91F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D26 RID: 3366
	public Shader SCShader;

	// Token: 0x04000D27 RID: 3367
	public bool _Visualize;

	// Token: 0x04000D28 RID: 3368
	private float TimeX = 1f;

	// Token: 0x04000D29 RID: 3369
	private Material SCMaterial;

	// Token: 0x04000D2A RID: 3370
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000D2B RID: 3371
	[Range(0f, 0.99f)]
	public float _Distance = 1f;

	// Token: 0x04000D2C RID: 3372
	[Range(0f, 0.1f)]
	public float _Size = 0.01f;

	// Token: 0x04000D2D RID: 3373
	public bool AutoAnimatedNear;

	// Token: 0x04000D2E RID: 3374
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 1f;

	// Token: 0x04000D2F RID: 3375
	public Color ScanColor = new Color(2f, 0f, 0f, 1f);

	// Token: 0x04000D30 RID: 3376
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000D31 RID: 3377
	public static Color ChangeColorRGB;

	// Token: 0x04000D32 RID: 3378
	private Texture2D Texture2;
}
