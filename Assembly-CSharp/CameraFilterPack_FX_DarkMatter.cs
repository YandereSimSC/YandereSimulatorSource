using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/DarkMatter")]
public class CameraFilterPack_FX_DarkMatter : MonoBehaviour
{
	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000704DF File Offset: 0x0006E6DF
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

	// Token: 0x06000EBC RID: 3772 RVA: 0x00070513 File Offset: 0x0006E713
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_DarkMatter");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x00070534 File Offset: 0x0006E734
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Value5", this.Zoom);
			this.material.SetFloat("_Value6", this.DarkIntensity);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00070658 File Offset: 0x0006E858
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001179 RID: 4473
	public Shader SCShader;

	// Token: 0x0400117A RID: 4474
	private float TimeX = 1f;

	// Token: 0x0400117B RID: 4475
	private Material SCMaterial;

	// Token: 0x0400117C RID: 4476
	[Range(-10f, 10f)]
	public float Speed = 0.8f;

	// Token: 0x0400117D RID: 4477
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x0400117E RID: 4478
	[Range(-1f, 2f)]
	public float PosX = 0.5f;

	// Token: 0x0400117F RID: 4479
	[Range(-1f, 2f)]
	public float PosY = 0.5f;

	// Token: 0x04001180 RID: 4480
	[Range(-2f, 2f)]
	public float Zoom = 0.33f;

	// Token: 0x04001181 RID: 4481
	[Range(0f, 5f)]
	public float DarkIntensity = 2f;
}
