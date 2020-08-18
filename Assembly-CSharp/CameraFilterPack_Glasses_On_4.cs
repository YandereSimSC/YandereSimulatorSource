using System;
using UnityEngine;

// Token: 0x020001BA RID: 442
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Futuristic Desert")]
public class CameraFilterPack_Glasses_On_4 : MonoBehaviour
{
	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000733ED File Offset: 0x000715ED
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

	// Token: 0x06000F70 RID: 3952 RVA: 0x00073421 File Offset: 0x00071621
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On5") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00073458 File Offset: 0x00071658
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
			this.material.SetFloat("UseFinalGlassColor", this.UseFinalGlassColor);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("VisionBlur", this.VisionBlur);
			this.material.SetFloat("GlassDistortion", this.GlassDistortion);
			this.material.SetFloat("GlassAberration", this.GlassAberration);
			this.material.SetColor("GlassesColor", this.GlassesColor);
			this.material.SetColor("GlassesColor2", this.GlassesColor2);
			this.material.SetColor("GlassColor", this.GlassColor);
			this.material.SetFloat("UseScanLineSize", this.UseScanLineSize);
			this.material.SetFloat("UseScanLine", this.UseScanLine);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x000735BD File Offset: 0x000717BD
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001239 RID: 4665
	public Shader SCShader;

	// Token: 0x0400123A RID: 4666
	private float TimeX = 1f;

	// Token: 0x0400123B RID: 4667
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x0400123C RID: 4668
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x0400123D RID: 4669
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x0400123E RID: 4670
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.25f, 0.25f);

	// Token: 0x0400123F RID: 4671
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x04001240 RID: 4672
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x04001241 RID: 4673
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x04001242 RID: 4674
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x04001243 RID: 4675
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001244 RID: 4676
	public Color GlassColor = new Color(1f, 0.4f, 0f, 1f);

	// Token: 0x04001245 RID: 4677
	private Material SCMaterial;

	// Token: 0x04001246 RID: 4678
	private Texture2D Texture2;
}
