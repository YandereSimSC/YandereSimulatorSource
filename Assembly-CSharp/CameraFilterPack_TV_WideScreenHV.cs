using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenHV")]
public class CameraFilterPack_TV_WideScreenHV : MonoBehaviour
{
	// Token: 0x17000332 RID: 818
	// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0007D79D File Offset: 0x0007B99D
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

	// Token: 0x060011A3 RID: 4515 RVA: 0x0007D7D1 File Offset: 0x0007B9D1
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenHV");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x0007D7F4 File Offset: 0x0007B9F4
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x0007D8EC File Offset: 0x0007BAEC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014A0 RID: 5280
	public Shader SCShader;

	// Token: 0x040014A1 RID: 5281
	private float TimeX = 1f;

	// Token: 0x040014A2 RID: 5282
	private Material SCMaterial;

	// Token: 0x040014A3 RID: 5283
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x040014A4 RID: 5284
	[Range(0.001f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x040014A5 RID: 5285
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x040014A6 RID: 5286
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
