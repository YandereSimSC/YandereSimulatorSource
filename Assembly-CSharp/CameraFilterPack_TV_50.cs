using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/50s")]
public class CameraFilterPack_TV_50 : MonoBehaviour
{
	// Token: 0x17000313 RID: 787
	// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0007A925 File Offset: 0x00078B25
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

	// Token: 0x060010E9 RID: 4329 RVA: 0x0007A959 File Offset: 0x00078B59
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_50");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0007A97C File Offset: 0x00078B7C
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

	// Token: 0x060010EB RID: 4331 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0007AA32 File Offset: 0x00078C32
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013EB RID: 5099
	public Shader SCShader;

	// Token: 0x040013EC RID: 5100
	private float TimeX = 1f;

	// Token: 0x040013ED RID: 5101
	private Material SCMaterial;

	// Token: 0x040013EE RID: 5102
	[Range(0f, 1f)]
	public float Fade = 1f;
}
