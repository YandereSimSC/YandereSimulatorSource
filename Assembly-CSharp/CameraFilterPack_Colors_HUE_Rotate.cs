using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/HUE_Rotate")]
public class CameraFilterPack_Colors_HUE_Rotate : MonoBehaviour
{
	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0006AB79 File Offset: 0x00068D79
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

	// Token: 0x06000D4D RID: 3405 RVA: 0x0006ABAD File Offset: 0x00068DAD
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_HUE_Rotate");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0006ABD0 File Offset: 0x00068DD0
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
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0006AC7F File Offset: 0x00068E7F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001008 RID: 4104
	public Shader SCShader;

	// Token: 0x04001009 RID: 4105
	private float TimeX = 1f;

	// Token: 0x0400100A RID: 4106
	private Material SCMaterial;

	// Token: 0x0400100B RID: 4107
	[Range(1f, 20f)]
	public float Speed = 10f;
}
