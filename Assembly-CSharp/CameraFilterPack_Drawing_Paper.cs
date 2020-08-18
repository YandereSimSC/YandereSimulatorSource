using System;
using UnityEngine;

// Token: 0x0200018B RID: 395
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Paper")]
public class CameraFilterPack_Drawing_Paper : MonoBehaviour
{
	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0006EA23 File Offset: 0x0006CC23
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

	// Token: 0x06000E55 RID: 3669 RVA: 0x0006EA57 File Offset: 0x0006CC57
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Paper");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x0006EA90 File Offset: 0x0006CC90
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

	// Token: 0x06000E57 RID: 3671 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0006EBDF File Offset: 0x0006CDDF
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001106 RID: 4358
	public Shader SCShader;

	// Token: 0x04001107 RID: 4359
	private float TimeX = 1f;

	// Token: 0x04001108 RID: 4360
	public Color Pencil_Color = new Color(0.156f, 0.3f, 0.738f, 1f);

	// Token: 0x04001109 RID: 4361
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.0008f;

	// Token: 0x0400110A RID: 4362
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.76f;

	// Token: 0x0400110B RID: 4363
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x0400110C RID: 4364
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x0400110D RID: 4365
	[Range(0f, 1f)]
	public float Corner_Lose = 0.5f;

	// Token: 0x0400110E RID: 4366
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor;

	// Token: 0x0400110F RID: 4367
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x04001110 RID: 4368
	public Color Back_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04001111 RID: 4369
	private Material SCMaterial;

	// Token: 0x04001112 RID: 4370
	private Texture2D Texture2;
}
