﻿using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Super Hexagon")]
public class CameraFilterPack_AAA_SuperHexagon : MonoBehaviour
{
	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000B1F RID: 2847 RVA: 0x000609D0 File Offset: 0x0005EBD0
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

	// Token: 0x06000B20 RID: 2848 RVA: 0x00060A04 File Offset: 0x0005EC04
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Super_Hexagon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00060A28 File Offset: 0x0005EC28
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
			this.material.SetFloat("_Value", this.HexaSize);
			this.material.SetFloat("_PositionX", this.center.x);
			this.material.SetFloat("_PositionY", this.center.y);
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetFloat("_BorderSize", this._BorderSize);
			this.material.SetColor("_BorderColor", this._BorderColor);
			this.material.SetColor("_HexaColor", this._HexaColor);
			this.material.SetFloat("_AlphaHexa", this._AlphaHexa);
			this.material.SetFloat("_SpotSize", this._SpotSize);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00060B98 File Offset: 0x0005ED98
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D90 RID: 3472
	public Shader SCShader;

	// Token: 0x04000D91 RID: 3473
	[Range(0f, 1f)]
	public float _AlphaHexa = 1f;

	// Token: 0x04000D92 RID: 3474
	private float TimeX = 1f;

	// Token: 0x04000D93 RID: 3475
	private Material SCMaterial;

	// Token: 0x04000D94 RID: 3476
	[Range(0.2f, 10f)]
	public float HexaSize = 2.5f;

	// Token: 0x04000D95 RID: 3477
	public float _BorderSize = 1f;

	// Token: 0x04000D96 RID: 3478
	public Color _BorderColor = new Color(0.75f, 0.75f, 1f, 1f);

	// Token: 0x04000D97 RID: 3479
	public Color _HexaColor = new Color(0f, 0.5f, 1f, 1f);

	// Token: 0x04000D98 RID: 3480
	public float _SpotSize = 2.5f;

	// Token: 0x04000D99 RID: 3481
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x04000D9A RID: 3482
	public float Radius = 0.25f;
}
