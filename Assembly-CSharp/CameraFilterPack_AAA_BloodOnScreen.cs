using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood On Screen")]
public class CameraFilterPack_AAA_BloodOnScreen : MonoBehaviour
{
	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0005FF06 File Offset: 0x0005E106
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

	// Token: 0x06000B08 RID: 2824 RVA: 0x0005FF3A File Offset: 0x0005E13A
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_BloodOnScreen1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_BloodOnScreen");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0005FF70 File Offset: 0x0005E170
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
			this.material.SetFloat("_Value", Mathf.Clamp(this.Blood_On_Screen, 0.02f, 1.6f));
			this.material.SetFloat("_Value2", Mathf.Clamp(this.Blood_Intensify, 0f, 2f));
			this.material.SetFloat("_Value3", Mathf.Clamp(this.Blood_Darkness, 0f, 2f));
			this.material.SetFloat("_Value4", Mathf.Clamp(this.Blood_Fade, 0f, 1f));
			this.material.SetFloat("_Value5", Mathf.Clamp(this.Blood_Distortion_Speed, 0f, 2f));
			this.material.SetColor("_Color2", this.Blood_Color);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x000600C8 File Offset: 0x0005E2C8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D59 RID: 3417
	public Shader SCShader;

	// Token: 0x04000D5A RID: 3418
	private float TimeX = 1f;

	// Token: 0x04000D5B RID: 3419
	[Range(0.02f, 1.6f)]
	public float Blood_On_Screen = 1f;

	// Token: 0x04000D5C RID: 3420
	public Color Blood_Color = Color.red;

	// Token: 0x04000D5D RID: 3421
	[Range(0f, 2f)]
	public float Blood_Intensify = 0.7f;

	// Token: 0x04000D5E RID: 3422
	[Range(0f, 2f)]
	public float Blood_Darkness = 0.5f;

	// Token: 0x04000D5F RID: 3423
	[Range(0f, 1f)]
	public float Blood_Distortion_Speed = 0.25f;

	// Token: 0x04000D60 RID: 3424
	[Range(0f, 1f)]
	public float Blood_Fade = 1f;

	// Token: 0x04000D61 RID: 3425
	private Material SCMaterial;

	// Token: 0x04000D62 RID: 3426
	private Texture2D Texture2;
}
