using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Videoflip")]
public class CameraFilterPack_TV_Videoflip : MonoBehaviour
{
	// Token: 0x1700032E RID: 814
	// (get) Token: 0x0600118A RID: 4490 RVA: 0x0007D26E File Offset: 0x0007B46E
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

	// Token: 0x0600118B RID: 4491 RVA: 0x0007D2A2 File Offset: 0x0007B4A2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Videoflip");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x0007D2C4 File Offset: 0x0007B4C4
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

	// Token: 0x0600118D RID: 4493 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x0007D361 File Offset: 0x0007B561
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400148B RID: 5259
	public Shader SCShader;

	// Token: 0x0400148C RID: 5260
	private float TimeX = 1f;

	// Token: 0x0400148D RID: 5261
	private Material SCMaterial;
}
