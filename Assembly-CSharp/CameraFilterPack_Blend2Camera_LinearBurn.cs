using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LinearBurn")]
public class CameraFilterPack_Blend2Camera_LinearBurn : MonoBehaviour
{
	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x000643BD File Offset: 0x000625BD
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

	// Token: 0x06000BE2 RID: 3042 RVA: 0x000643F4 File Offset: 0x000625F4
	private void Start()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00064458 File Offset: 0x00062658
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.Camera2 != null)
			{
				this.material.SetTexture("_MainTex2", this.Camera2tex);
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00064548 File Offset: 0x00062748
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00064548 File Offset: 0x00062748
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00064580 File Offset: 0x00062780
	private void OnDisable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000E8B RID: 3723
	private string ShaderName = "CameraFilterPack/Blend2Camera_LinearBurn";

	// Token: 0x04000E8C RID: 3724
	public Shader SCShader;

	// Token: 0x04000E8D RID: 3725
	public Camera Camera2;

	// Token: 0x04000E8E RID: 3726
	private float TimeX = 1f;

	// Token: 0x04000E8F RID: 3727
	private Material SCMaterial;

	// Token: 0x04000E90 RID: 3728
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E91 RID: 3729
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E92 RID: 3730
	private RenderTexture Camera2tex;
}
