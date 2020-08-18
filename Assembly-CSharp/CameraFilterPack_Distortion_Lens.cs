using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Lens")]
public class CameraFilterPack_Distortion_Lens : MonoBehaviour
{
	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0006C1AE File Offset: 0x0006A3AE
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

	// Token: 0x06000DAD RID: 3501 RVA: 0x0006C1E2 File Offset: 0x0006A3E2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Lens");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0006C204 File Offset: 0x0006A404
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
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0006C2DF File Offset: 0x0006A4DF
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001061 RID: 4193
	public Shader SCShader;

	// Token: 0x04001062 RID: 4194
	private float TimeX = 1f;

	// Token: 0x04001063 RID: 4195
	private Material SCMaterial;

	// Token: 0x04001064 RID: 4196
	[Range(-1f, 1f)]
	public float CenterX;

	// Token: 0x04001065 RID: 4197
	[Range(-1f, 1f)]
	public float CenterY;

	// Token: 0x04001066 RID: 4198
	[Range(0f, 3f)]
	public float Distortion = 1f;
}
