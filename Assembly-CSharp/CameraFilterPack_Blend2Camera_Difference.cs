using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Difference")]
public class CameraFilterPack_Blend2Camera_Difference : MonoBehaviour
{
	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0006302D File Offset: 0x0006122D
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

	// Token: 0x06000B9B RID: 2971 RVA: 0x00063064 File Offset: 0x00061264
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

	// Token: 0x06000B9C RID: 2972 RVA: 0x000630C8 File Offset: 0x000612C8
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

	// Token: 0x06000B9D RID: 2973 RVA: 0x000631B8 File Offset: 0x000613B8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x000631B8 File Offset: 0x000613B8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x000631F0 File Offset: 0x000613F0
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

	// Token: 0x04000E3D RID: 3645
	private string ShaderName = "CameraFilterPack/Blend2Camera_Difference";

	// Token: 0x04000E3E RID: 3646
	public Shader SCShader;

	// Token: 0x04000E3F RID: 3647
	public Camera Camera2;

	// Token: 0x04000E40 RID: 3648
	private float TimeX = 1f;

	// Token: 0x04000E41 RID: 3649
	private Material SCMaterial;

	// Token: 0x04000E42 RID: 3650
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E43 RID: 3651
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E44 RID: 3652
	private RenderTexture Camera2tex;
}
