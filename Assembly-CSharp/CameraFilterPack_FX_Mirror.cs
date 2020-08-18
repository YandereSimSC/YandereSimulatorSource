using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Mirror")]
public class CameraFilterPack_FX_Mirror : MonoBehaviour
{
	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00071C5E File Offset: 0x0006FE5E
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

	// Token: 0x06000F1C RID: 3868 RVA: 0x00071C92 File Offset: 0x0006FE92
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Mirror");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x00071CB4 File Offset: 0x0006FEB4
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x00071D51 File Offset: 0x0006FF51
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011D6 RID: 4566
	public Shader SCShader;

	// Token: 0x040011D7 RID: 4567
	private float TimeX = 1f;

	// Token: 0x040011D8 RID: 4568
	private Material SCMaterial;
}
