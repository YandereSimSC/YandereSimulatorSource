﻿using System;
using UnityEngine;

// Token: 0x0200011E RID: 286
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/DarkerColor")]
public class CameraFilterPack_Blend2Camera_DarkerColor : MonoBehaviour
{
	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00062E0D File Offset: 0x0006100D
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

	// Token: 0x06000B93 RID: 2963 RVA: 0x00062E44 File Offset: 0x00061044
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

	// Token: 0x06000B94 RID: 2964 RVA: 0x00062EA8 File Offset: 0x000610A8
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

	// Token: 0x06000B95 RID: 2965 RVA: 0x00062F98 File Offset: 0x00061198
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00062F98 File Offset: 0x00061198
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00062FD0 File Offset: 0x000611D0
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

	// Token: 0x04000E35 RID: 3637
	private string ShaderName = "CameraFilterPack/Blend2Camera_DarkerColor";

	// Token: 0x04000E36 RID: 3638
	public Shader SCShader;

	// Token: 0x04000E37 RID: 3639
	public Camera Camera2;

	// Token: 0x04000E38 RID: 3640
	private float TimeX = 1f;

	// Token: 0x04000E39 RID: 3641
	private Material SCMaterial;

	// Token: 0x04000E3A RID: 3642
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E3B RID: 3643
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E3C RID: 3644
	private RenderTexture Camera2tex;
}
