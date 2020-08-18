using System;
using UnityEngine;

// Token: 0x020001F0 RID: 496
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Retro/Loading")]
public class CameraFilterPack_Retro_Loading : MonoBehaviour
{
	// Token: 0x17000310 RID: 784
	// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0007A4D5 File Offset: 0x000786D5
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

	// Token: 0x060010D7 RID: 4311 RVA: 0x0007A509 File Offset: 0x00078709
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Retro_Loading");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x0007A52C File Offset: 0x0007872C
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x0007A5E2 File Offset: 0x000787E2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013DB RID: 5083
	public Shader SCShader;

	// Token: 0x040013DC RID: 5084
	private float TimeX = 1f;

	// Token: 0x040013DD RID: 5085
	private Material SCMaterial;

	// Token: 0x040013DE RID: 5086
	[Range(0.1f, 10f)]
	public float Speed = 1f;
}
