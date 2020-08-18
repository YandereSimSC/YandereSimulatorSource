using System;
using UnityEngine;

// Token: 0x020001A5 RID: 421
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Glitch2")]
public class CameraFilterPack_FX_Glitch2 : MonoBehaviour
{
	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00071382 File Offset: 0x0006F582
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

	// Token: 0x06000EF2 RID: 3826 RVA: 0x000713B6 File Offset: 0x0006F5B6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Glitch2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x000713D8 File Offset: 0x0006F5D8
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
			this.material.SetFloat("_Glitch", this.Glitch);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x0007148E File Offset: 0x0006F68E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011B7 RID: 4535
	public Shader SCShader;

	// Token: 0x040011B8 RID: 4536
	private float TimeX = 1f;

	// Token: 0x040011B9 RID: 4537
	private Material SCMaterial;

	// Token: 0x040011BA RID: 4538
	[Range(0f, 1f)]
	public float Glitch = 1f;
}
