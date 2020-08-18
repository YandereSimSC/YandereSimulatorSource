using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/BrightContrastSaturation")]
public class CameraFilterPack_Color_BrightContrastSaturation : MonoBehaviour
{
	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00068D0E File Offset: 0x00066F0E
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

	// Token: 0x06000CDF RID: 3295 RVA: 0x00068D42 File Offset: 0x00066F42
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_BrightContrastSaturation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x00068D64 File Offset: 0x00066F64
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_Brightness", this.Brightness);
			this.material.SetFloat("_Saturation", this.Saturation);
			this.material.SetFloat("_Contrast", this.Contrast);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x00068E46 File Offset: 0x00067046
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FA6 RID: 4006
	public Shader SCShader;

	// Token: 0x04000FA7 RID: 4007
	private float TimeX = 1f;

	// Token: 0x04000FA8 RID: 4008
	private Material SCMaterial;

	// Token: 0x04000FA9 RID: 4009
	[Range(0f, 10f)]
	public float Brightness = 2f;

	// Token: 0x04000FAA RID: 4010
	[Range(0f, 10f)]
	public float Saturation = 1.5f;

	// Token: 0x04000FAB RID: 4011
	[Range(0f, 10f)]
	public float Contrast = 1.5f;
}
