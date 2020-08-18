using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Edge/Golden")]
public class CameraFilterPack_Edge_Golden : MonoBehaviour
{
	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0006F8DB File Offset: 0x0006DADB
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

	// Token: 0x06000E86 RID: 3718 RVA: 0x0006F90F File Offset: 0x0006DB0F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Edge_Golden");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x0006F930 File Offset: 0x0006DB30
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0006F9C6 File Offset: 0x0006DBC6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400114C RID: 4428
	public Shader SCShader;

	// Token: 0x0400114D RID: 4429
	private float TimeX = 1f;

	// Token: 0x0400114E RID: 4430
	private Material SCMaterial;
}
