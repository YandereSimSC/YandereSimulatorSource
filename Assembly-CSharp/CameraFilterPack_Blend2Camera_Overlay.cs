using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Overlay")]
public class CameraFilterPack_Blend2Camera_Overlay : MonoBehaviour
{
	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00064E5D File Offset: 0x0006305D
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

	// Token: 0x06000C0A RID: 3082 RVA: 0x00064E94 File Offset: 0x00063094
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

	// Token: 0x06000C0B RID: 3083 RVA: 0x00064EF8 File Offset: 0x000630F8
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

	// Token: 0x06000C0C RID: 3084 RVA: 0x00064FE8 File Offset: 0x000631E8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00064FE8 File Offset: 0x000631E8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x00065020 File Offset: 0x00063220
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

	// Token: 0x04000EB3 RID: 3763
	private string ShaderName = "CameraFilterPack/Blend2Camera_Overlay";

	// Token: 0x04000EB4 RID: 3764
	public Shader SCShader;

	// Token: 0x04000EB5 RID: 3765
	public Camera Camera2;

	// Token: 0x04000EB6 RID: 3766
	private float TimeX = 1f;

	// Token: 0x04000EB7 RID: 3767
	private Material SCMaterial;

	// Token: 0x04000EB8 RID: 3768
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EB9 RID: 3769
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EBA RID: 3770
	private RenderTexture Camera2tex;
}
