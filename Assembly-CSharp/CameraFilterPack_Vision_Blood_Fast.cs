using System;
using UnityEngine;

// Token: 0x02000219 RID: 537
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Blood_Fast")]
public class CameraFilterPack_Vision_Blood_Fast : MonoBehaviour
{
	// Token: 0x17000339 RID: 825
	// (get) Token: 0x060011CC RID: 4556 RVA: 0x0007E349 File Offset: 0x0007C549
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

	// Token: 0x060011CD RID: 4557 RVA: 0x0007E37D File Offset: 0x0007C57D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Blood_Fast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x0007E3A0 File Offset: 0x0007C5A0
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
			this.material.SetFloat("_Value", this.HoleSize);
			this.material.SetFloat("_Value2", this.HoleSmooth);
			this.material.SetFloat("_Value3", this.Color1);
			this.material.SetFloat("_Value4", this.Color2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x0007E498 File Offset: 0x0007C698
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014D0 RID: 5328
	public Shader SCShader;

	// Token: 0x040014D1 RID: 5329
	private float TimeX = 1f;

	// Token: 0x040014D2 RID: 5330
	private Material SCMaterial;

	// Token: 0x040014D3 RID: 5331
	[Range(0.01f, 1f)]
	public float HoleSize = 0.6f;

	// Token: 0x040014D4 RID: 5332
	[Range(-1f, 1f)]
	public float HoleSmooth = 0.3f;

	// Token: 0x040014D5 RID: 5333
	[Range(-2f, 2f)]
	public float Color1 = 0.2f;

	// Token: 0x040014D6 RID: 5334
	[Range(-2f, 2f)]
	public float Color2 = 0.9f;
}
