using System;
using UnityEngine;

// Token: 0x0200016D RID: 365
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Half_Sphere")]
public class CameraFilterPack_Distortion_Half_Sphere : MonoBehaviour
{
	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0006BEDE File Offset: 0x0006A0DE
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

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0006BF12 File Offset: 0x0006A112
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Half_Sphere");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0006BF34 File Offset: 0x0006A134
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
			this.material.SetFloat("_SphereSize", this.SphereSize);
			this.material.SetFloat("_SpherePositionX", this.SpherePositionX);
			this.material.SetFloat("_SpherePositionY", this.SpherePositionY);
			this.material.SetFloat("_Strength", this.Strength);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0006C025 File Offset: 0x0006A225
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001056 RID: 4182
	public Shader SCShader;

	// Token: 0x04001057 RID: 4183
	private float TimeX = 1f;

	// Token: 0x04001058 RID: 4184
	[Range(1f, 6f)]
	private Material SCMaterial;

	// Token: 0x04001059 RID: 4185
	public float SphereSize = 2.5f;

	// Token: 0x0400105A RID: 4186
	[Range(-1f, 1f)]
	public float SpherePositionX;

	// Token: 0x0400105B RID: 4187
	[Range(-1f, 1f)]
	public float SpherePositionY;

	// Token: 0x0400105C RID: 4188
	[Range(1f, 10f)]
	public float Strength = 5f;
}
