using System;
using UnityEngine;

// Token: 0x020001F4 RID: 500
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/80s")]
public class CameraFilterPack_TV_80 : MonoBehaviour
{
	// Token: 0x17000314 RID: 788
	// (get) Token: 0x060010EE RID: 4334 RVA: 0x0007AA6A File Offset: 0x00078C6A
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

	// Token: 0x060010EF RID: 4335 RVA: 0x0007AA9E File Offset: 0x00078C9E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_80");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0007AAC0 File Offset: 0x00078CC0
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
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0007AB76 File Offset: 0x00078D76
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013EF RID: 5103
	public Shader SCShader;

	// Token: 0x040013F0 RID: 5104
	private float TimeX = 1f;

	// Token: 0x040013F1 RID: 5105
	private Material SCMaterial;

	// Token: 0x040013F2 RID: 5106
	[Range(0f, 1f)]
	public float Fade = 1f;
}
