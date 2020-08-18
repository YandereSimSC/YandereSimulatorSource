using System;
using UnityEngine;

// Token: 0x02000173 RID: 371
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Twist")]
public class CameraFilterPack_Distortion_Twist : MonoBehaviour
{
	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0006C7A1 File Offset: 0x0006A9A1
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

	// Token: 0x06000DC5 RID: 3525 RVA: 0x0006C7D5 File Offset: 0x0006A9D5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Twist");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0006C7F8 File Offset: 0x0006A9F8
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
			this.material.SetFloat("_Size", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0006C8E9 File Offset: 0x0006AAE9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001079 RID: 4217
	public Shader SCShader;

	// Token: 0x0400107A RID: 4218
	private float TimeX = 1f;

	// Token: 0x0400107B RID: 4219
	private Material SCMaterial;

	// Token: 0x0400107C RID: 4220
	[Range(-2f, 2f)]
	public float CenterX = 0.5f;

	// Token: 0x0400107D RID: 4221
	[Range(-2f, 2f)]
	public float CenterY = 0.5f;

	// Token: 0x0400107E RID: 4222
	[Range(-3.14f, 3.14f)]
	public float Distortion = 1f;

	// Token: 0x0400107F RID: 4223
	[Range(-2f, 2f)]
	public float Size = 1f;
}
