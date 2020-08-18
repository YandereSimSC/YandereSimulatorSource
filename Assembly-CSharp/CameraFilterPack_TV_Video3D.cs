using System;
using UnityEngine;

// Token: 0x0200020D RID: 525
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Video3D")]
public class CameraFilterPack_TV_Video3D : MonoBehaviour
{
	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06001184 RID: 4484 RVA: 0x0007D14E File Offset: 0x0007B34E
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

	// Token: 0x06001185 RID: 4485 RVA: 0x0007D182 File Offset: 0x0007B382
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Video3D");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x0007D1A4 File Offset: 0x0007B3A4
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0007D241 File Offset: 0x0007B441
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001488 RID: 5256
	public Shader SCShader;

	// Token: 0x04001489 RID: 5257
	private float TimeX = 1f;

	// Token: 0x0400148A RID: 5258
	private Material SCMaterial;
}
