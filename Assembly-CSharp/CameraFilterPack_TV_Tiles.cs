using System;
using UnityEngine;

// Token: 0x02000209 RID: 521
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Tiles")]
public class CameraFilterPack_TV_Tiles : MonoBehaviour
{
	// Token: 0x17000329 RID: 809
	// (get) Token: 0x0600116C RID: 4460 RVA: 0x0007CB22 File Offset: 0x0007AD22
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

	// Token: 0x0600116D RID: 4461 RVA: 0x0007CB56 File Offset: 0x0007AD56
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Tiles");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0007CB78 File Offset: 0x0007AD78
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0007CC86 File Offset: 0x0007AE86
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400146E RID: 5230
	public Shader SCShader;

	// Token: 0x0400146F RID: 5231
	private float TimeX = 1f;

	// Token: 0x04001470 RID: 5232
	private Material SCMaterial;

	// Token: 0x04001471 RID: 5233
	[Range(0.5f, 2f)]
	public float Size = 1f;

	// Token: 0x04001472 RID: 5234
	[Range(0f, 10f)]
	public float Intensity = 4f;

	// Token: 0x04001473 RID: 5235
	[Range(0f, 1f)]
	public float StretchX = 0.6f;

	// Token: 0x04001474 RID: 5236
	[Range(0f, 1f)]
	public float StretchY = 0.4f;

	// Token: 0x04001475 RID: 5237
	[Range(0f, 1f)]
	public float Fade = 0.6f;
}
