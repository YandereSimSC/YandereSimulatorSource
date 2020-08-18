using System;
using UnityEngine;

// Token: 0x02000220 RID: 544
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/SniperScore")]
public class CameraFilterPack_Vision_SniperScore : MonoBehaviour
{
	// Token: 0x17000340 RID: 832
	// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0007EFA1 File Offset: 0x0007D1A1
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

	// Token: 0x060011F7 RID: 4599 RVA: 0x0007EFD5 File Offset: 0x0007D1D5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_SniperScore");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x0007EFF8 File Offset: 0x0007D1F8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetFloat("_Cible", this._Cible);
			this.material.SetFloat("_ExtraColor", this._ExtraColor);
			this.material.SetFloat("_Distortion", this._Distortion);
			this.material.SetFloat("_PosX", this._PosX);
			this.material.SetFloat("_PosY", this._PosY);
			this.material.SetColor("_Tint", this._Tint);
			this.material.SetFloat("_ExtraLight", this._ExtraLight);
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			this.material.SetVector("_ScreenResolution", new Vector4(vector.x, vector.y, vector.y / vector.x, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x0007F1B9 File Offset: 0x0007D3B9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001506 RID: 5382
	public Shader SCShader;

	// Token: 0x04001507 RID: 5383
	private float TimeX = 1f;

	// Token: 0x04001508 RID: 5384
	private Material SCMaterial;

	// Token: 0x04001509 RID: 5385
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x0400150A RID: 5386
	[Range(0f, 1f)]
	public float Size = 0.45f;

	// Token: 0x0400150B RID: 5387
	[Range(0.01f, 0.4f)]
	public float Smooth = 0.045f;

	// Token: 0x0400150C RID: 5388
	[Range(0f, 1f)]
	public float _Cible = 0.5f;

	// Token: 0x0400150D RID: 5389
	[Range(0f, 1f)]
	public float _Distortion = 0.5f;

	// Token: 0x0400150E RID: 5390
	[Range(0f, 1f)]
	public float _ExtraColor = 0.5f;

	// Token: 0x0400150F RID: 5391
	[Range(0f, 1f)]
	public float _ExtraLight = 0.35f;

	// Token: 0x04001510 RID: 5392
	public Color _Tint = new Color(0f, 0.6f, 0f, 0.25f);

	// Token: 0x04001511 RID: 5393
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x04001512 RID: 5394
	[Range(0f, 10f)]
	private float StretchY = 1f;

	// Token: 0x04001513 RID: 5395
	[Range(-1f, 1f)]
	public float _PosX;

	// Token: 0x04001514 RID: 5396
	[Range(-1f, 1f)]
	public float _PosY;
}
