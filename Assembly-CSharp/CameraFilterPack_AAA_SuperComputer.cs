using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Super Computer")]
public class CameraFilterPack_AAA_SuperComputer : MonoBehaviour
{
	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00060756 File Offset: 0x0005E956
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

	// Token: 0x06000B1A RID: 2842 RVA: 0x0006078A File Offset: 0x0005E98A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Super_Computer");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x000607AC File Offset: 0x0005E9AC
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime / 4f;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.ShapeFormula);
			this.material.SetFloat("_Value2", this.Shape);
			this.material.SetFloat("_PositionX", this.center.x);
			this.material.SetFloat("_PositionY", this.center.y);
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetFloat("_BorderSize", this._BorderSize);
			this.material.SetColor("_BorderColor", this._BorderColor);
			this.material.SetFloat("_AlphaHexa", this._AlphaHexa);
			this.material.SetFloat("_SpotSize", this._SpotSize);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00060922 File Offset: 0x0005EB22
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D85 RID: 3461
	public Shader SCShader;

	// Token: 0x04000D86 RID: 3462
	[Range(0f, 1f)]
	public float _AlphaHexa = 1f;

	// Token: 0x04000D87 RID: 3463
	private float TimeX = 1f;

	// Token: 0x04000D88 RID: 3464
	private Material SCMaterial;

	// Token: 0x04000D89 RID: 3465
	[Range(-20f, 20f)]
	public float ShapeFormula = 10f;

	// Token: 0x04000D8A RID: 3466
	[Range(0f, 6f)]
	public float Shape = 1f;

	// Token: 0x04000D8B RID: 3467
	[Range(-4f, 4f)]
	public float _BorderSize = 1f;

	// Token: 0x04000D8C RID: 3468
	public Color _BorderColor = new Color(0f, 0.2f, 1f, 1f);

	// Token: 0x04000D8D RID: 3469
	public float _SpotSize = 2.5f;

	// Token: 0x04000D8E RID: 3470
	public Vector2 center = new Vector2(0f, 0f);

	// Token: 0x04000D8F RID: 3471
	public float Radius = 0.77f;
}
