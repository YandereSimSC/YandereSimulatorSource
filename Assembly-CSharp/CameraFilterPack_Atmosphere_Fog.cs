using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Fog")]
public class CameraFilterPack_Atmosphere_Fog : MonoBehaviour
{
	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0006125B File Offset: 0x0005F45B
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

	// Token: 0x06000B3E RID: 2878 RVA: 0x0006128F File Offset: 0x0005F48F
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Fog");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x000612C8 File Offset: 0x0005F4C8
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
			this.material.SetFloat("_Near", this._Near);
			this.material.SetFloat("_Far", this._Far);
			this.material.SetColor("_ColorRGB", this.FogColor);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x000613E2 File Offset: 0x0005F5E2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DB5 RID: 3509
	public Shader SCShader;

	// Token: 0x04000DB6 RID: 3510
	private float TimeX = 1f;

	// Token: 0x04000DB7 RID: 3511
	private Material SCMaterial;

	// Token: 0x04000DB8 RID: 3512
	[Range(0f, 1f)]
	public float _Near;

	// Token: 0x04000DB9 RID: 3513
	[Range(0f, 1f)]
	public float _Far = 0.05f;

	// Token: 0x04000DBA RID: 3514
	public Color FogColor = new Color(0.4f, 0.4f, 0.4f, 1f);

	// Token: 0x04000DBB RID: 3515
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000DBC RID: 3516
	public static Color ChangeColorRGB;

	// Token: 0x04000DBD RID: 3517
	private Texture2D Texture2;
}
