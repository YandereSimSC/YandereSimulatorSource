using System;
using UnityEngine;

// Token: 0x0200015D RID: 349
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/DarkColor")]
public class CameraFilterPack_Colors_DarkColor : MonoBehaviour
{
	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0006A8D7 File Offset: 0x00068AD7
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

	// Token: 0x06000D41 RID: 3393 RVA: 0x0006A90B File Offset: 0x00068B0B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_DarkColor");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0006A92C File Offset: 0x00068B2C
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
			this.material.SetFloat("_Value", this.Alpha);
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0006AA24 File Offset: 0x00068C24
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FFC RID: 4092
	public Shader SCShader;

	// Token: 0x04000FFD RID: 4093
	private float TimeX = 1f;

	// Token: 0x04000FFE RID: 4094
	private Material SCMaterial;

	// Token: 0x04000FFF RID: 4095
	[Range(-5f, 5f)]
	public float Alpha = 1f;

	// Token: 0x04001000 RID: 4096
	[Range(0f, 16f)]
	private float Colors = 11f;

	// Token: 0x04001001 RID: 4097
	[Range(-1f, 1f)]
	private float Green_Mod = 1f;

	// Token: 0x04001002 RID: 4098
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
