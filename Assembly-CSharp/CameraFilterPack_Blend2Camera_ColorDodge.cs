using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/ColorDodge")]
public class CameraFilterPack_Blend2Camera_ColorDodge : MonoBehaviour
{
	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00062729 File Offset: 0x00060929
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

	// Token: 0x06000B7C RID: 2940 RVA: 0x00062760 File Offset: 0x00060960
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

	// Token: 0x06000B7D RID: 2941 RVA: 0x000627C4 File Offset: 0x000609C4
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

	// Token: 0x06000B7E RID: 2942 RVA: 0x000628B4 File Offset: 0x00060AB4
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x000628B4 File Offset: 0x00060AB4
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x000628EC File Offset: 0x00060AEC
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

	// Token: 0x04000E16 RID: 3606
	private string ShaderName = "CameraFilterPack/Blend2Camera_ColorDodge";

	// Token: 0x04000E17 RID: 3607
	public Shader SCShader;

	// Token: 0x04000E18 RID: 3608
	public Camera Camera2;

	// Token: 0x04000E19 RID: 3609
	private float TimeX = 1f;

	// Token: 0x04000E1A RID: 3610
	private Material SCMaterial;

	// Token: 0x04000E1B RID: 3611
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E1C RID: 3612
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E1D RID: 3613
	private RenderTexture Camera2tex;
}
