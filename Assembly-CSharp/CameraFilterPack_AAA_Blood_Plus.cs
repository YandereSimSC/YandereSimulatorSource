using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood_Plus")]
public class CameraFilterPack_AAA_Blood_Plus : MonoBehaviour
{
	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0006044E File Offset: 0x0005E64E
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

	// Token: 0x06000B14 RID: 2836 RVA: 0x00060482 File Offset: 0x0005E682
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_Blood2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Blood_Plus");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x000604B8 File Offset: 0x0005E6B8
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
			this.material.SetFloat("_Value", this.LightReflect);
			this.material.SetFloat("_Value2", Mathf.Clamp(this.Blood_1, 0f, 1f));
			this.material.SetFloat("_Value3", Mathf.Clamp(this.Blood_2, 0f, 1f));
			this.material.SetFloat("_Value4", Mathf.Clamp(this.Blood_3, 0f, 1f));
			this.material.SetFloat("_Value5", Mathf.Clamp(this.Blood_4, 0f, 1f));
			this.material.SetFloat("_Value6", Mathf.Clamp(this.Blood_5, 0f, 1f));
			this.material.SetFloat("_Value7", Mathf.Clamp(this.Blood_6, 0f, 1f));
			this.material.SetFloat("_Value8", Mathf.Clamp(this.Blood_7, 0f, 1f));
			this.material.SetFloat("_Value9", Mathf.Clamp(this.Blood_8, 0f, 1f));
			this.material.SetFloat("_Value10", Mathf.Clamp(this.Blood_9, 0f, 1f));
			this.material.SetFloat("_Value11", Mathf.Clamp(this.Blood_10, 0f, 1f));
			this.material.SetFloat("_Value12", Mathf.Clamp(this.Blood_11, 0f, 1f));
			this.material.SetFloat("_Value13", Mathf.Clamp(this.Blood_12, 0f, 1f));
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00060713 File Offset: 0x0005E913
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D74 RID: 3444
	public Shader SCShader;

	// Token: 0x04000D75 RID: 3445
	private float TimeX = 1f;

	// Token: 0x04000D76 RID: 3446
	[Range(0f, 1f)]
	public float Blood_1 = 1f;

	// Token: 0x04000D77 RID: 3447
	[Range(0f, 1f)]
	public float Blood_2;

	// Token: 0x04000D78 RID: 3448
	[Range(0f, 1f)]
	public float Blood_3;

	// Token: 0x04000D79 RID: 3449
	[Range(0f, 1f)]
	public float Blood_4;

	// Token: 0x04000D7A RID: 3450
	[Range(0f, 1f)]
	public float Blood_5;

	// Token: 0x04000D7B RID: 3451
	[Range(0f, 1f)]
	public float Blood_6;

	// Token: 0x04000D7C RID: 3452
	[Range(0f, 1f)]
	public float Blood_7;

	// Token: 0x04000D7D RID: 3453
	[Range(0f, 1f)]
	public float Blood_8;

	// Token: 0x04000D7E RID: 3454
	[Range(0f, 1f)]
	public float Blood_9;

	// Token: 0x04000D7F RID: 3455
	[Range(0f, 1f)]
	public float Blood_10;

	// Token: 0x04000D80 RID: 3456
	[Range(0f, 1f)]
	public float Blood_11;

	// Token: 0x04000D81 RID: 3457
	[Range(0f, 1f)]
	public float Blood_12;

	// Token: 0x04000D82 RID: 3458
	[Range(0f, 1f)]
	public float LightReflect = 0.5f;

	// Token: 0x04000D83 RID: 3459
	private Material SCMaterial;

	// Token: 0x04000D84 RID: 3460
	private Texture2D Texture2;
}
