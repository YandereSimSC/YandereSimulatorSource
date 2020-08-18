using System;
using UnityEngine;

// Token: 0x020001F2 RID: 498
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Special/Bubble")]
public class CameraFilterPack_Special_Bubble : MonoBehaviour
{
	// Token: 0x17000312 RID: 786
	// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0007A77F File Offset: 0x0007897F
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

	// Token: 0x060010E3 RID: 4323 RVA: 0x0007A7B3 File Offset: 0x000789B3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Special_Bubble");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0007A7D4 File Offset: 0x000789D4
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
			this.material.SetFloat("_Value", this.X);
			this.material.SetFloat("_Value2", this.Y);
			this.material.SetFloat("_Value3", this.Rate);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0007A8CC File Offset: 0x00078ACC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013E4 RID: 5092
	public Shader SCShader;

	// Token: 0x040013E5 RID: 5093
	private float TimeX = 1f;

	// Token: 0x040013E6 RID: 5094
	private Material SCMaterial;

	// Token: 0x040013E7 RID: 5095
	[Range(-4f, 4f)]
	public float X = 0.5f;

	// Token: 0x040013E8 RID: 5096
	[Range(-4f, 4f)]
	public float Y = 0.5f;

	// Token: 0x040013E9 RID: 5097
	[Range(0f, 5f)]
	public float Rate = 1f;

	// Token: 0x040013EA RID: 5098
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
