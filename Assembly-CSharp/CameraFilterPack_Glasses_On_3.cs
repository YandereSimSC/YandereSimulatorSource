using System;
using UnityEngine;

// Token: 0x020001B9 RID: 441
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Night Glasses")]
public class CameraFilterPack_Glasses_On_3 : MonoBehaviour
{
	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00073145 File Offset: 0x00071345
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

	// Token: 0x06000F6A RID: 3946 RVA: 0x00073179 File Offset: 0x00071379
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On4") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x000731B0 File Offset: 0x000713B0
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

	// Token: 0x06000F6C RID: 3948 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00073315 File Offset: 0x00071515
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400122B RID: 4651
	public Shader SCShader;

	// Token: 0x0400122C RID: 4652
	private float TimeX = 1f;

	// Token: 0x0400122D RID: 4653
	[Range(0f, 1f)]
	public float Fade = 0.3f;

	// Token: 0x0400122E RID: 4654
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x0400122F RID: 4655
	public Color GlassesColor = new Color(0.7f, 0.7f, 0.7f, 1f);

	// Token: 0x04001230 RID: 4656
	public Color GlassesColor2 = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04001231 RID: 4657
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x04001232 RID: 4658
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x04001233 RID: 4659
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x04001234 RID: 4660
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x04001235 RID: 4661
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001236 RID: 4662
	public Color GlassColor = new Color(0f, 0.5f, 0f, 1f);

	// Token: 0x04001237 RID: 4663
	private Material SCMaterial;

	// Token: 0x04001238 RID: 4664
	private Texture2D Texture2;
}
