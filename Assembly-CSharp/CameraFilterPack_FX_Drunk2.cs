using System;
using UnityEngine;

// Token: 0x020001A1 RID: 417
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Drunk2")]
public class CameraFilterPack_FX_Drunk2 : MonoBehaviour
{
	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00070DCD File Offset: 0x0006EFCD
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

	// Token: 0x06000EDA RID: 3802 RVA: 0x00070E01 File Offset: 0x0006F001
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Drunk2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x00070E24 File Offset: 0x0006F024
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x00070F1C File Offset: 0x0006F11C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011A2 RID: 4514
	public Shader SCShader;

	// Token: 0x040011A3 RID: 4515
	private float TimeX = 1f;

	// Token: 0x040011A4 RID: 4516
	private Material SCMaterial;

	// Token: 0x040011A5 RID: 4517
	[Range(0f, 10f)]
	private float Value = 1f;

	// Token: 0x040011A6 RID: 4518
	[Range(0f, 10f)]
	private float Value2 = 1f;

	// Token: 0x040011A7 RID: 4519
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x040011A8 RID: 4520
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
