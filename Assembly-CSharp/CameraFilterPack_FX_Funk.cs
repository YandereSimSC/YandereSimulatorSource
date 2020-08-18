using System;
using UnityEngine;

// Token: 0x020001A3 RID: 419
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Funk")]
public class CameraFilterPack_FX_Funk : MonoBehaviour
{
	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0007111D File Offset: 0x0006F31D
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

	// Token: 0x06000EE6 RID: 3814 RVA: 0x00071151 File Offset: 0x0006F351
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Funk");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00071174 File Offset: 0x0006F374
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

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x00071211 File Offset: 0x0006F411
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011B0 RID: 4528
	public Shader SCShader;

	// Token: 0x040011B1 RID: 4529
	private float TimeX = 1f;

	// Token: 0x040011B2 RID: 4530
	private Material SCMaterial;
}
