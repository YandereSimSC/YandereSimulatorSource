using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Rain")]
public class CameraFilterPack_Atmosphere_Rain : MonoBehaviour
{
	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0006144F File Offset: 0x0005F64F
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

	// Token: 0x06000B44 RID: 2884 RVA: 0x00061483 File Offset: 0x0005F683
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Rain");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x000614BC File Offset: 0x0005F6BC
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value3", this.DirectionX);
			this.material.SetFloat("_Value4", this.Speed);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Value6", this.Distortion);
			this.material.SetFloat("_Value7", this.StormFlashOnOff);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0006160C File Offset: 0x0005F80C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DBE RID: 3518
	public Shader SCShader;

	// Token: 0x04000DBF RID: 3519
	private float TimeX = 1f;

	// Token: 0x04000DC0 RID: 3520
	private Material SCMaterial;

	// Token: 0x04000DC1 RID: 3521
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000DC2 RID: 3522
	[Range(0f, 2f)]
	public float Intensity = 0.5f;

	// Token: 0x04000DC3 RID: 3523
	[Range(-0.25f, 0.25f)]
	public float DirectionX = 0.12f;

	// Token: 0x04000DC4 RID: 3524
	[Range(0.4f, 2f)]
	public float Size = 1.5f;

	// Token: 0x04000DC5 RID: 3525
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000DC6 RID: 3526
	[Range(0f, 0.5f)]
	public float Distortion = 0.05f;

	// Token: 0x04000DC7 RID: 3527
	[Range(0f, 1f)]
	public float StormFlashOnOff = 1f;

	// Token: 0x04000DC8 RID: 3528
	private Texture2D Texture2;
}
