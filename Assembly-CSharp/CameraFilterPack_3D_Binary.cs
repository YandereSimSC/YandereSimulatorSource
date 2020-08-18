using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Binary")]
public class CameraFilterPack_3D_Binary : MonoBehaviour
{
	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0005DAA2 File Offset: 0x0005BCA2
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

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0005DAD6 File Offset: 0x0005BCD6
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Binary1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Binary");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0005DB0C File Offset: 0x0005BD0C
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
			this.material.SetFloat("_FadeFromBinary", this.FadeFromBinary);
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

	// Token: 0x06000AB6 RID: 2742 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0005DCAE File Offset: 0x0005BEAE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CA7 RID: 3239
	public Shader SCShader;

	// Token: 0x04000CA8 RID: 3240
	private float TimeX = 1f;

	// Token: 0x04000CA9 RID: 3241
	public bool _Visualize;

	// Token: 0x04000CAA RID: 3242
	private Material SCMaterial;

	// Token: 0x04000CAB RID: 3243
	[Range(0f, 100f)]
	public float _FixDistance = 2f;

	// Token: 0x04000CAC RID: 3244
	[Range(-5f, 5f)]
	public float LightIntensity;

	// Token: 0x04000CAD RID: 3245
	[Range(0f, 8f)]
	public float MatrixSize = 2f;

	// Token: 0x04000CAE RID: 3246
	[Range(-4f, 4f)]
	public float MatrixSpeed = 1f;

	// Token: 0x04000CAF RID: 3247
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000CB0 RID: 3248
	[Range(0f, 1f)]
	public float FadeFromBinary;

	// Token: 0x04000CB1 RID: 3249
	public Color _MatrixColor = new Color(1f, 0.3f, 0.3f, 1f);

	// Token: 0x04000CB2 RID: 3250
	public static Color ChangeColorRGB;

	// Token: 0x04000CB3 RID: 3251
	private Texture2D Texture2;
}
