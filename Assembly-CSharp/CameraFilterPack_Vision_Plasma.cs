using System;
using UnityEngine;

// Token: 0x0200021D RID: 541
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Plasma")]
public class CameraFilterPack_Vision_Plasma : MonoBehaviour
{
	// Token: 0x1700033D RID: 829
	// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0007EA7C File Offset: 0x0007CC7C
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

	// Token: 0x060011E5 RID: 4581 RVA: 0x0007EAB0 File Offset: 0x0007CCB0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Plasma");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x0007EAD4 File Offset: 0x0007CCD4
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
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x0007EBCC File Offset: 0x0007CDCC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014F0 RID: 5360
	public Shader SCShader;

	// Token: 0x040014F1 RID: 5361
	private float TimeX = 1f;

	// Token: 0x040014F2 RID: 5362
	private Material SCMaterial;

	// Token: 0x040014F3 RID: 5363
	[Range(-2f, 2f)]
	public float Value = 0.6f;

	// Token: 0x040014F4 RID: 5364
	[Range(-2f, 2f)]
	public float Value2 = 0.2f;

	// Token: 0x040014F5 RID: 5365
	[Range(0f, 60f)]
	public float Intensity = 15f;

	// Token: 0x040014F6 RID: 5366
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
