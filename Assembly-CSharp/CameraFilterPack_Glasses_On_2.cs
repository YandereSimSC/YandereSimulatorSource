using System;
using UnityEngine;

// Token: 0x020001B8 RID: 440
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Vampire")]
public class CameraFilterPack_Glasses_On_2 : MonoBehaviour
{
	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00072E9E File Offset: 0x0007109E
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

	// Token: 0x06000F64 RID: 3940 RVA: 0x00072ED2 File Offset: 0x000710D2
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On3") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_OnX");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x00072F08 File Offset: 0x00071108
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

	// Token: 0x06000F66 RID: 3942 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x0007306D File Offset: 0x0007126D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400121D RID: 4637
	public Shader SCShader;

	// Token: 0x0400121E RID: 4638
	private float TimeX = 1f;

	// Token: 0x0400121F RID: 4639
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x04001220 RID: 4640
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x04001221 RID: 4641
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04001222 RID: 4642
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.25f, 0.25f);

	// Token: 0x04001223 RID: 4643
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x04001224 RID: 4644
	[Range(0f, 1f)]
	public float GlassAberration = 0.5f;

	// Token: 0x04001225 RID: 4645
	[Range(0f, 1f)]
	public float UseFinalGlassColor = 1f;

	// Token: 0x04001226 RID: 4646
	[Range(0f, 1f)]
	public float UseScanLine;

	// Token: 0x04001227 RID: 4647
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001228 RID: 4648
	public Color GlassColor = new Color(1f, 0f, 0f, 1f);

	// Token: 0x04001229 RID: 4649
	private Material SCMaterial;

	// Token: 0x0400122A RID: 4650
	private Texture2D Texture2;
}
