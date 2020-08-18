using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Flush")]
public class CameraFilterPack_Distortion_Flush : MonoBehaviour
{
	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0006BD9B File Offset: 0x00069F9B
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

	// Token: 0x06000D9B RID: 3483 RVA: 0x0006BDCF File Offset: 0x00069FCF
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Flush");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0006BDF0 File Offset: 0x00069FF0
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
			this.material.SetFloat("Value", this.Value);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0006BEA6 File Offset: 0x0006A0A6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001052 RID: 4178
	public Shader SCShader;

	// Token: 0x04001053 RID: 4179
	private float TimeX = 1f;

	// Token: 0x04001054 RID: 4180
	private Material SCMaterial;

	// Token: 0x04001055 RID: 4181
	[Range(-10f, 50f)]
	public float Value = 5f;
}
