using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/OilPaint")]
public class CameraFilterPack_Pixelisation_OilPaint : MonoBehaviour
{
	// Token: 0x1700030B RID: 779
	// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00079B39 File Offset: 0x00077D39
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

	// Token: 0x060010B8 RID: 4280 RVA: 0x00079B6D File Offset: 0x00077D6D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_OilPaint");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00079B90 File Offset: 0x00077D90
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00079C46 File Offset: 0x00077E46
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013B8 RID: 5048
	public Shader SCShader;

	// Token: 0x040013B9 RID: 5049
	private float TimeX = 1f;

	// Token: 0x040013BA RID: 5050
	private Material SCMaterial;

	// Token: 0x040013BB RID: 5051
	[Range(0f, 5f)]
	public float Value = 1f;
}
