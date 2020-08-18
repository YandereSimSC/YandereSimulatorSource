using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Drost")]
public class CameraFilterPack_Vision_Drost : MonoBehaviour
{
	// Token: 0x1700033B RID: 827
	// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0007E699 File Offset: 0x0007C899
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

	// Token: 0x060011D9 RID: 4569 RVA: 0x0007E6CD File Offset: 0x0007C8CD
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Drost");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x0007E6F0 File Offset: 0x0007C8F0
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
			this.material.SetFloat("_Value", this.Intensity);
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x0007E7E8 File Offset: 0x0007C9E8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014DE RID: 5342
	public Shader SCShader;

	// Token: 0x040014DF RID: 5343
	private float TimeX = 1f;

	// Token: 0x040014E0 RID: 5344
	private Material SCMaterial;

	// Token: 0x040014E1 RID: 5345
	[Range(0f, 0.4f)]
	public float Intensity = 0.4f;

	// Token: 0x040014E2 RID: 5346
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x040014E3 RID: 5347
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x040014E4 RID: 5348
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
