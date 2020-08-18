using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Matrix")]
public class CameraFilterPack_3D_Matrix : MonoBehaviour
{
	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0005ECF5 File Offset: 0x0005CEF5
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

	// Token: 0x06000ADE RID: 2782 RVA: 0x0005ED29 File Offset: 0x0005CF29
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Matrix1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Matrix");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0005ED60 File Offset: 0x0005CF60
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

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0005EEEC File Offset: 0x0005D0EC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D00 RID: 3328
	public Shader SCShader;

	// Token: 0x04000D01 RID: 3329
	private float TimeX = 1f;

	// Token: 0x04000D02 RID: 3330
	private Material SCMaterial;

	// Token: 0x04000D03 RID: 3331
	public bool _Visualize;

	// Token: 0x04000D04 RID: 3332
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000D05 RID: 3333
	[Range(-5f, 5f)]
	public float LightIntensity = 1f;

	// Token: 0x04000D06 RID: 3334
	[Range(0f, 6f)]
	public float MatrixSize = 1f;

	// Token: 0x04000D07 RID: 3335
	[Range(-4f, 4f)]
	public float MatrixSpeed = 1f;

	// Token: 0x04000D08 RID: 3336
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000D09 RID: 3337
	public Color _MatrixColor = new Color(0f, 1f, 0f, 1f);

	// Token: 0x04000D0A RID: 3338
	public static Color ChangeColorRGB;

	// Token: 0x04000D0B RID: 3339
	private Texture2D Texture2;
}
