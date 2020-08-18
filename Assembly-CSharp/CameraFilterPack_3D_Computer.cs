using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Computer")]
public class CameraFilterPack_3D_Computer : MonoBehaviour
{
	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0005DFBD File Offset: 0x0005C1BD
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

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0005DFF1 File Offset: 0x0005C1F1
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Computer1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Computer");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0005E028 File Offset: 0x0005C228
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
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_MatrixSize", this.MatrixSize);
			this.material.SetColor("_MatrixColor", this._MatrixColor);
			this.material.SetFloat("_MatrixSpeed", this.MatrixSpeed * 2f);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_LightIntensity", this.LightIntensity);
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

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x0005E1B4 File Offset: 0x0005C3B4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CC0 RID: 3264
	public Shader SCShader;

	// Token: 0x04000CC1 RID: 3265
	private float TimeX = 1f;

	// Token: 0x04000CC2 RID: 3266
	public bool _Visualize;

	// Token: 0x04000CC3 RID: 3267
	private Material SCMaterial;

	// Token: 0x04000CC4 RID: 3268
	[Range(0f, 100f)]
	public float _FixDistance = 2f;

	// Token: 0x04000CC5 RID: 3269
	[Range(-5f, 5f)]
	public float LightIntensity = 1f;

	// Token: 0x04000CC6 RID: 3270
	[Range(0f, 8f)]
	public float MatrixSize = 2f;

	// Token: 0x04000CC7 RID: 3271
	[Range(-4f, 4f)]
	public float MatrixSpeed = 0.1f;

	// Token: 0x04000CC8 RID: 3272
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000CC9 RID: 3273
	public Color _MatrixColor = new Color(0f, 0.5f, 1f, 1f);

	// Token: 0x04000CCA RID: 3274
	public static Color ChangeColorRGB;

	// Token: 0x04000CCB RID: 3275
	private Texture2D Texture2;
}
