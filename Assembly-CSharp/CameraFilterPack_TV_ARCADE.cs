using System;
using UnityEngine;

// Token: 0x020001F5 RID: 501
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/ARCADE")]
public class CameraFilterPack_TV_ARCADE : MonoBehaviour
{
	// Token: 0x17000315 RID: 789
	// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0007ABAE File Offset: 0x00078DAE
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

	// Token: 0x060010F5 RID: 4341 RVA: 0x0007ABE2 File Offset: 0x00078DE2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_ARCADE");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x0007AC04 File Offset: 0x00078E04
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0007ACBA File Offset: 0x00078EBA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013F3 RID: 5107
	public Shader SCShader;

	// Token: 0x040013F4 RID: 5108
	private float TimeX = 1f;

	// Token: 0x040013F5 RID: 5109
	private Material SCMaterial;

	// Token: 0x040013F6 RID: 5110
	[Range(0f, 1f)]
	public float Fade = 1f;
}
