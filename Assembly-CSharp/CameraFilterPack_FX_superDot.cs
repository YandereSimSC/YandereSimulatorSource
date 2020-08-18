using System;
using UnityEngine;

// Token: 0x020001B3 RID: 435
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/SuperDot")]
public class CameraFilterPack_FX_superDot : MonoBehaviour
{
	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00072622 File Offset: 0x00070822
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

	// Token: 0x06000F46 RID: 3910 RVA: 0x00072656 File Offset: 0x00070856
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_superDot");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00072678 File Offset: 0x00070878
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

	// Token: 0x06000F48 RID: 3912 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x00072715 File Offset: 0x00070915
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011F9 RID: 4601
	public Shader SCShader;

	// Token: 0x040011FA RID: 4602
	private float TimeX = 1f;

	// Token: 0x040011FB RID: 4603
	private Material SCMaterial;
}
