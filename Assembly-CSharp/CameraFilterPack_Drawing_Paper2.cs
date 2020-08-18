using System;
using UnityEngine;

// Token: 0x0200018C RID: 396
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Paper2")]
public class CameraFilterPack_Drawing_Paper2 : MonoBehaviour
{
	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0006EC9A File Offset: 0x0006CE9A
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

	// Token: 0x06000E5B RID: 3675 RVA: 0x0006ECCE File Offset: 0x0006CECE
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper3") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Paper2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0006ED04 File Offset: 0x0006CF04
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

	// Token: 0x06000E5D RID: 3677 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0006EE53 File Offset: 0x0006D053
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001113 RID: 4371
	public Shader SCShader;

	// Token: 0x04001114 RID: 4372
	private float TimeX = 1f;

	// Token: 0x04001115 RID: 4373
	public Color Pencil_Color = new Color(0f, 0.371f, 0.78f, 1f);

	// Token: 0x04001116 RID: 4374
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.0008f;

	// Token: 0x04001117 RID: 4375
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.76f;

	// Token: 0x04001118 RID: 4376
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x04001119 RID: 4377
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x0400111A RID: 4378
	[Range(0f, 1f)]
	public float Corner_Lose = 0.85f;

	// Token: 0x0400111B RID: 4379
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor;

	// Token: 0x0400111C RID: 4380
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x0400111D RID: 4381
	public Color Back_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x0400111E RID: 4382
	private Material SCMaterial;

	// Token: 0x0400111F RID: 4383
	private Texture2D Texture2;
}
