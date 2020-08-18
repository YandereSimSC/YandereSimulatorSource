using System;
using UnityEngine;

// Token: 0x020001FD RID: 509
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Compression FX")]
public class CameraFilterPack_TV_CompressionFX : MonoBehaviour
{
	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06001124 RID: 4388 RVA: 0x0007BB55 File Offset: 0x00079D55
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

	// Token: 0x06001125 RID: 4389 RVA: 0x0007BB89 File Offset: 0x00079D89
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_CompressionFX");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x0007BBAC File Offset: 0x00079DAC
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
			this.material.SetFloat("_Parasite", this.Parasite);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0007BC62 File Offset: 0x00079E62
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001433 RID: 5171
	public Shader SCShader;

	// Token: 0x04001434 RID: 5172
	private float TimeX = 1f;

	// Token: 0x04001435 RID: 5173
	[Range(-10f, 10f)]
	public float Parasite = 1f;

	// Token: 0x04001436 RID: 5174
	private Material SCMaterial;
}
