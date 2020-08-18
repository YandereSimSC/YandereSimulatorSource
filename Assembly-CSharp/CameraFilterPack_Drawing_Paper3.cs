﻿using System;
using UnityEngine;

// Token: 0x0200018D RID: 397
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Paper3")]
public class CameraFilterPack_Drawing_Paper3 : MonoBehaviour
{
	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0006EF0E File Offset: 0x0006D10E
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

	// Token: 0x06000E61 RID: 3681 RVA: 0x0006EF42 File Offset: 0x0006D142
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper4") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Paper3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0006EF78 File Offset: 0x0006D178
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
			this.material.SetColor("_PColor", this.Pencil_Color);
			this.material.SetFloat("_Value1", this.Pencil_Size);
			this.material.SetFloat("_Value2", this.Pencil_Correction);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Speed_Animation);
			this.material.SetFloat("_Value5", this.Corner_Lose);
			this.material.SetFloat("_Value6", this.Fade_Paper_to_BackColor);
			this.material.SetFloat("_Value7", this.Fade_With_Original);
			this.material.SetColor("_PColor2", this.Back_Color);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0006F0C7 File Offset: 0x0006D2C7
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001120 RID: 4384
	public Shader SCShader;

	// Token: 0x04001121 RID: 4385
	private float TimeX = 1f;

	// Token: 0x04001122 RID: 4386
	public Color Pencil_Color = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04001123 RID: 4387
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.00125f;

	// Token: 0x04001124 RID: 4388
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.35f;

	// Token: 0x04001125 RID: 4389
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x04001126 RID: 4390
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x04001127 RID: 4391
	[Range(0f, 1f)]
	public float Corner_Lose = 1f;

	// Token: 0x04001128 RID: 4392
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor;

	// Token: 0x04001129 RID: 4393
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x0400112A RID: 4394
	public Color Back_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x0400112B RID: 4395
	private Material SCMaterial;

	// Token: 0x0400112C RID: 4396
	private Texture2D Texture2;
}
