using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LinearDodge")]
public class CameraFilterPack_Blend2Camera_LinearDodge : MonoBehaviour
{
	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000645DD File Offset: 0x000627DD
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

	// Token: 0x06000BEA RID: 3050 RVA: 0x00064614 File Offset: 0x00062814
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

	// Token: 0x06000BEB RID: 3051 RVA: 0x00064678 File Offset: 0x00062878
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

	// Token: 0x06000BEC RID: 3052 RVA: 0x00064768 File Offset: 0x00062968
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00064768 File Offset: 0x00062968
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x000647A0 File Offset: 0x000629A0
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

	// Token: 0x04000E93 RID: 3731
	private string ShaderName = "CameraFilterPack/Blend2Camera_LinearDodge";

	// Token: 0x04000E94 RID: 3732
	public Shader SCShader;

	// Token: 0x04000E95 RID: 3733
	public Camera Camera2;

	// Token: 0x04000E96 RID: 3734
	private float TimeX = 1f;

	// Token: 0x04000E97 RID: 3735
	private Material SCMaterial;

	// Token: 0x04000E98 RID: 3736
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E99 RID: 3737
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E9A RID: 3738
	private RenderTexture Camera2tex;
}
