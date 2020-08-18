using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood_Hit")]
public class CameraFilterPack_AAA_Blood_Hit : MonoBehaviour
{
	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00060144 File Offset: 0x0005E344
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

	// Token: 0x06000B0E RID: 2830 RVA: 0x00060178 File Offset: 0x0005E378
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_Blood_Hit1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Blood_Hit");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x000601B0 File Offset: 0x0005E3B0
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
			this.material.SetFloat("_Value2", Mathf.Clamp(this.Hit_Left, 0f, 1f));
			this.material.SetFloat("_Value3", Mathf.Clamp(this.Hit_Up, 0f, 1f));
			this.material.SetFloat("_Value4", Mathf.Clamp(this.Hit_Right, 0f, 1f));
			this.material.SetFloat("_Value5", Mathf.Clamp(this.Hit_Down, 0f, 1f));
			this.material.SetFloat("_Value6", Mathf.Clamp(this.Blood_Hit_Left, 0f, 1f));
			this.material.SetFloat("_Value7", Mathf.Clamp(this.Blood_Hit_Up, 0f, 1f));
			this.material.SetFloat("_Value8", Mathf.Clamp(this.Blood_Hit_Right, 0f, 1f));
			this.material.SetFloat("_Value9", Mathf.Clamp(this.Blood_Hit_Down, 0f, 1f));
			this.material.SetFloat("_Value10", Mathf.Clamp(this.Hit_Full, 0f, 1f));
			this.material.SetFloat("_Value11", Mathf.Clamp(this.Blood_Hit_Full_1, 0f, 1f));
			this.material.SetFloat("_Value12", Mathf.Clamp(this.Blood_Hit_Full_2, 0f, 1f));
			this.material.SetFloat("_Value13", Mathf.Clamp(this.Blood_Hit_Full_3, 0f, 1f));
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0006040B File Offset: 0x0005E60B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D63 RID: 3427
	public Shader SCShader;

	// Token: 0x04000D64 RID: 3428
	private float TimeX = 1f;

	// Token: 0x04000D65 RID: 3429
	[Range(0f, 1f)]
	public float Hit_Left = 1f;

	// Token: 0x04000D66 RID: 3430
	[Range(0f, 1f)]
	public float Hit_Up;

	// Token: 0x04000D67 RID: 3431
	[Range(0f, 1f)]
	public float Hit_Right;

	// Token: 0x04000D68 RID: 3432
	[Range(0f, 1f)]
	public float Hit_Down;

	// Token: 0x04000D69 RID: 3433
	[Range(0f, 1f)]
	public float Blood_Hit_Left;

	// Token: 0x04000D6A RID: 3434
	[Range(0f, 1f)]
	public float Blood_Hit_Up;

	// Token: 0x04000D6B RID: 3435
	[Range(0f, 1f)]
	public float Blood_Hit_Right;

	// Token: 0x04000D6C RID: 3436
	[Range(0f, 1f)]
	public float Blood_Hit_Down;

	// Token: 0x04000D6D RID: 3437
	[Range(0f, 1f)]
	public float Hit_Full;

	// Token: 0x04000D6E RID: 3438
	[Range(0f, 1f)]
	public float Blood_Hit_Full_1;

	// Token: 0x04000D6F RID: 3439
	[Range(0f, 1f)]
	public float Blood_Hit_Full_2;

	// Token: 0x04000D70 RID: 3440
	[Range(0f, 1f)]
	public float Blood_Hit_Full_3;

	// Token: 0x04000D71 RID: 3441
	[Range(0f, 1f)]
	public float LightReflect = 0.5f;

	// Token: 0x04000D72 RID: 3442
	private Material SCMaterial;

	// Token: 0x04000D73 RID: 3443
	private Texture2D Texture2;
}
