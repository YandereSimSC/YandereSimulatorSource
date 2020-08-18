using System;
using UnityEngine;

// Token: 0x020001BB RID: 443
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Futuristic Montain")]
public class CameraFilterPack_Glasses_On_5 : MonoBehaviour
{
	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00073695 File Offset: 0x00071895
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

	// Token: 0x06000F76 RID: 3958 RVA: 0x000736C9 File Offset: 0x000718C9
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On6") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00073700 File Offset: 0x00071900
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

	// Token: 0x06000F78 RID: 3960 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00073865 File Offset: 0x00071A65
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001247 RID: 4679
	public Shader SCShader;

	// Token: 0x04001248 RID: 4680
	private float TimeX = 1f;

	// Token: 0x04001249 RID: 4681
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x0400124A RID: 4682
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x0400124B RID: 4683
	public Color GlassesColor = new Color(0.1f, 0.1f, 0.1f, 1f);

	// Token: 0x0400124C RID: 4684
	public Color GlassesColor2 = new Color(0.45f, 0.45f, 0.45f, 0.25f);

	// Token: 0x0400124D RID: 4685
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x0400124E RID: 4686
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x0400124F RID: 4687
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x04001250 RID: 4688
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x04001251 RID: 4689
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001252 RID: 4690
	public Color GlassColor = new Color(0.1f, 0.3f, 1f, 1f);

	// Token: 0x04001253 RID: 4691
	private Material SCMaterial;

	// Token: 0x04001254 RID: 4692
	private Texture2D Texture2;
}
