using System;
using UnityEngine;

// Token: 0x020001BC RID: 444
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Spy")]
public class CameraFilterPack_Glasses_On_6 : MonoBehaviour
{
	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0007393D File Offset: 0x00071B3D
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

	// Token: 0x06000F7C RID: 3964 RVA: 0x00073971 File Offset: 0x00071B71
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On7") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x000739A8 File Offset: 0x00071BA8
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

	// Token: 0x06000F7E RID: 3966 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00073B0D File Offset: 0x00071D0D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001255 RID: 4693
	public Shader SCShader;

	// Token: 0x04001256 RID: 4694
	private float TimeX = 1f;

	// Token: 0x04001257 RID: 4695
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x04001258 RID: 4696
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x04001259 RID: 4697
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x0400125A RID: 4698
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.45f, 0.25f);

	// Token: 0x0400125B RID: 4699
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x0400125C RID: 4700
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x0400125D RID: 4701
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x0400125E RID: 4702
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x0400125F RID: 4703
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001260 RID: 4704
	public Color GlassColor = new Color(1f, 0.9f, 0f, 1f);

	// Token: 0x04001261 RID: 4705
	private Material SCMaterial;

	// Token: 0x04001262 RID: 4706
	private Texture2D Texture2;
}
