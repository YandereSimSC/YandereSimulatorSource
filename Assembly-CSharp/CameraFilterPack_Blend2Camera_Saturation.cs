using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Saturation")]
public class CameraFilterPack_Blend2Camera_Saturation : MonoBehaviour
{
	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00065789 File Offset: 0x00063989
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

	// Token: 0x06000C23 RID: 3107 RVA: 0x000657C0 File Offset: 0x000639C0
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

	// Token: 0x06000C24 RID: 3108 RVA: 0x00065824 File Offset: 0x00063A24
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

	// Token: 0x06000C25 RID: 3109 RVA: 0x00065914 File Offset: 0x00063B14
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x00065914 File Offset: 0x00063B14
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0006594C File Offset: 0x00063B4C
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

	// Token: 0x04000ECD RID: 3789
	private string ShaderName = "CameraFilterPack/Blend2Camera_Saturation";

	// Token: 0x04000ECE RID: 3790
	public Shader SCShader;

	// Token: 0x04000ECF RID: 3791
	public Camera Camera2;

	// Token: 0x04000ED0 RID: 3792
	private float TimeX = 1f;

	// Token: 0x04000ED1 RID: 3793
	private Material SCMaterial;

	// Token: 0x04000ED2 RID: 3794
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000ED3 RID: 3795
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000ED4 RID: 3796
	private RenderTexture Camera2tex;
}
