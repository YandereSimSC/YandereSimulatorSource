using System;
using UnityEngine;

// Token: 0x02000194 RID: 404
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Edge/Neon")]
public class CameraFilterPack_Edge_Neon : MonoBehaviour
{
	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0006F9F3 File Offset: 0x0006DBF3
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

	// Token: 0x06000E8C RID: 3724 RVA: 0x0006FA27 File Offset: 0x0006DC27
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Edge_Neon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0006FA48 File Offset: 0x0006DC48
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
			this.material.SetFloat("_EdgeWeight", this.EdgeWeight);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x0006FAF7 File Offset: 0x0006DCF7
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400114F RID: 4431
	public Shader SCShader;

	// Token: 0x04001150 RID: 4432
	private float TimeX = 1f;

	// Token: 0x04001151 RID: 4433
	private Material SCMaterial;

	// Token: 0x04001152 RID: 4434
	[Range(1f, 10f)]
	public float EdgeWeight = 1f;
}
