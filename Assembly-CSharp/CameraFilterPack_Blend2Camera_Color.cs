using System;
using UnityEngine;

// Token: 0x02000119 RID: 281
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Color")]
public class CameraFilterPack_Blend2Camera_Color : MonoBehaviour
{
	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06000B6B RID: 2923 RVA: 0x000622EC File Offset: 0x000604EC
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

	// Token: 0x06000B6C RID: 2924 RVA: 0x00062320 File Offset: 0x00060520
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

	// Token: 0x06000B6D RID: 2925 RVA: 0x00062384 File Offset: 0x00060584
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

	// Token: 0x06000B6E RID: 2926 RVA: 0x00062474 File Offset: 0x00060674
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00062474 File Offset: 0x00060674
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x000624AC File Offset: 0x000606AC
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

	// Token: 0x04000E06 RID: 3590
	private string ShaderName = "CameraFilterPack/Blend2Camera_Color";

	// Token: 0x04000E07 RID: 3591
	public Shader SCShader;

	// Token: 0x04000E08 RID: 3592
	public Camera Camera2;

	// Token: 0x04000E09 RID: 3593
	private float TimeX = 1f;

	// Token: 0x04000E0A RID: 3594
	private Material SCMaterial;

	// Token: 0x04000E0B RID: 3595
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E0C RID: 3596
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E0D RID: 3597
	private RenderTexture Camera2tex;
}
