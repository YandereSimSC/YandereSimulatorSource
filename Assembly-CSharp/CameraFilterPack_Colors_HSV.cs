using System;
using UnityEngine;

// Token: 0x0200015E RID: 350
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/HSV")]
public class CameraFilterPack_Colors_HSV : MonoBehaviour
{
	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0006AA7D File Offset: 0x00068C7D
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

	// Token: 0x06000D47 RID: 3399 RVA: 0x0006AAB1 File Offset: 0x00068CB1
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0006AAC4 File Offset: 0x00068CC4
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_HueShift", this._HueShift);
			this.material.SetFloat("_Sat", this._Saturation);
			this.material.SetFloat("_Val", this._ValueBrightness);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0006AB36 File Offset: 0x00068D36
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001003 RID: 4099
	public Shader SCShader;

	// Token: 0x04001004 RID: 4100
	[Range(0f, 360f)]
	public float _HueShift = 180f;

	// Token: 0x04001005 RID: 4101
	[Range(-32f, 32f)]
	public float _Saturation = 1f;

	// Token: 0x04001006 RID: 4102
	[Range(-32f, 32f)]
	public float _ValueBrightness = 1f;

	// Token: 0x04001007 RID: 4103
	private Material SCMaterial;
}
