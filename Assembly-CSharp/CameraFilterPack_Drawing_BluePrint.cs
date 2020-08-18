using System;
using UnityEngine;

// Token: 0x02000177 RID: 375
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/BluePrint")]
public class CameraFilterPack_Drawing_BluePrint : MonoBehaviour
{
	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0006CDA7 File Offset: 0x0006AFA7
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

	// Token: 0x06000DDD RID: 3549 RVA: 0x0006CDDB File Offset: 0x0006AFDB
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_BluePrint");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0006CE14 File Offset: 0x0006B014
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

	// Token: 0x06000DDF RID: 3551 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0006CF63 File Offset: 0x0006B163
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001092 RID: 4242
	public Shader SCShader;

	// Token: 0x04001093 RID: 4243
	private float TimeX = 1f;

	// Token: 0x04001094 RID: 4244
	public Color Pencil_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04001095 RID: 4245
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.0008f;

	// Token: 0x04001096 RID: 4246
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.76f;

	// Token: 0x04001097 RID: 4247
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x04001098 RID: 4248
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x04001099 RID: 4249
	[Range(0f, 1f)]
	public float Corner_Lose = 0.5f;

	// Token: 0x0400109A RID: 4250
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor = 0.2f;

	// Token: 0x0400109B RID: 4251
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x0400109C RID: 4252
	public Color Back_Color = new Color(0.175f, 0.402f, 0.687f, 1f);

	// Token: 0x0400109D RID: 4253
	private Material SCMaterial;

	// Token: 0x0400109E RID: 4254
	private Texture2D Texture2;
}
