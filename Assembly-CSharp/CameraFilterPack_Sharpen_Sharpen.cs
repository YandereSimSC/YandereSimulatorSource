using System;
using UnityEngine;

// Token: 0x020001F1 RID: 497
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Sharpen/Sharpen")]
public class CameraFilterPack_Sharpen_Sharpen : MonoBehaviour
{
	// Token: 0x17000311 RID: 785
	// (get) Token: 0x060010DC RID: 4316 RVA: 0x0007A61A File Offset: 0x0007881A
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

	// Token: 0x060010DD RID: 4317 RVA: 0x0007A64E File Offset: 0x0007884E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Sharpen_Sharpen");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0007A670 File Offset: 0x00078870
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0007A73C File Offset: 0x0007893C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013DF RID: 5087
	public Shader SCShader;

	// Token: 0x040013E0 RID: 5088
	[Range(0.001f, 100f)]
	public float Value = 4f;

	// Token: 0x040013E1 RID: 5089
	[Range(0.001f, 32f)]
	public float Value2 = 1f;

	// Token: 0x040013E2 RID: 5090
	private float TimeX = 1f;

	// Token: 0x040013E3 RID: 5091
	private Material SCMaterial;
}
