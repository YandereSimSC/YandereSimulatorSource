using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Classic Glasses")]
public class CameraFilterPack_Glasses_On : MonoBehaviour
{
	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00072BFF File Offset: 0x00070DFF
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

	// Token: 0x06000F5E RID: 3934 RVA: 0x00072C33 File Offset: 0x00070E33
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00072C6C File Offset: 0x00070E6C
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

	// Token: 0x06000F60 RID: 3936 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00072DD1 File Offset: 0x00070FD1
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400120F RID: 4623
	public Shader SCShader;

	// Token: 0x04001210 RID: 4624
	private float TimeX = 1f;

	// Token: 0x04001211 RID: 4625
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x04001212 RID: 4626
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.0095f;

	// Token: 0x04001213 RID: 4627
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04001214 RID: 4628
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.25f, 0.25f);

	// Token: 0x04001215 RID: 4629
	[Range(0f, 1f)]
	public float GlassDistortion = 0.45f;

	// Token: 0x04001216 RID: 4630
	[Range(0f, 1f)]
	public float GlassAberration = 0.5f;

	// Token: 0x04001217 RID: 4631
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x04001218 RID: 4632
	[Range(0f, 1f)]
	public float UseScanLine;

	// Token: 0x04001219 RID: 4633
	[Range(1f, 512f)]
	public float UseScanLineSize = 1f;

	// Token: 0x0400121A RID: 4634
	public Color GlassColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x0400121B RID: 4635
	private Material SCMaterial;

	// Token: 0x0400121C RID: 4636
	private Texture2D Texture2;
}
