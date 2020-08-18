using System;
using UnityEngine;

// Token: 0x020001A0 RID: 416
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Drunk")]
public class CameraFilterPack_FX_Drunk : MonoBehaviour
{
	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00070B76 File Offset: 0x0006ED76
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

	// Token: 0x06000ED4 RID: 3796 RVA: 0x00070BAA File Offset: 0x0006EDAA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Drunk");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x00070BCC File Offset: 0x0006EDCC
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_DistortionWave", this.DistortionWave);
			this.material.SetFloat("_Wavy", this.Wavy);
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_ColoredChange", this.ColoredChange);
			this.material.SetFloat("_ChangeRed", this.ChangeRed);
			this.material.SetFloat("_ChangeGreen", this.ChangeGreen);
			this.material.SetFloat("_ChangeBlue", this.ChangeBlue);
			this.material.SetFloat("_Colored", this.ColoredSaturate);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x00070D5E File Offset: 0x0006EF5E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001194 RID: 4500
	public Shader SCShader;

	// Token: 0x04001195 RID: 4501
	private float TimeX = 1f;

	// Token: 0x04001196 RID: 4502
	private Material SCMaterial;

	// Token: 0x04001197 RID: 4503
	[HideInInspector]
	[Range(0f, 20f)]
	public float Value = 6f;

	// Token: 0x04001198 RID: 4504
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001199 RID: 4505
	[Range(0f, 1f)]
	public float Wavy = 1f;

	// Token: 0x0400119A RID: 4506
	[Range(0f, 1f)]
	public float Distortion;

	// Token: 0x0400119B RID: 4507
	[Range(0f, 1f)]
	public float DistortionWave;

	// Token: 0x0400119C RID: 4508
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x0400119D RID: 4509
	[Range(-2f, 2f)]
	public float ColoredSaturate = 1f;

	// Token: 0x0400119E RID: 4510
	[Range(-1f, 2f)]
	public float ColoredChange;

	// Token: 0x0400119F RID: 4511
	[Range(-1f, 1f)]
	public float ChangeRed;

	// Token: 0x040011A0 RID: 4512
	[Range(-1f, 1f)]
	public float ChangeGreen;

	// Token: 0x040011A1 RID: 4513
	[Range(-1f, 1f)]
	public float ChangeBlue;
}
