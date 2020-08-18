using System;
using UnityEngine;

// Token: 0x02000188 RID: 392
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_FlashWhite")]
public class CameraFilterPack_Drawing_Manga_FlashWhite : MonoBehaviour
{
	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0006E509 File Offset: 0x0006C709
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

	// Token: 0x06000E43 RID: 3651 RVA: 0x0006E53D File Offset: 0x0006C73D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_FlashWhite");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0006E560 File Offset: 0x0006C760
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
			this.material.SetFloat("_Value2", (float)this.Speed);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Intensity", this.Intensity);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0006E66F File Offset: 0x0006C86F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010F1 RID: 4337
	public Shader SCShader;

	// Token: 0x040010F2 RID: 4338
	private float TimeX = 1f;

	// Token: 0x040010F3 RID: 4339
	private Material SCMaterial;

	// Token: 0x040010F4 RID: 4340
	[Range(1f, 10f)]
	public float Size = 1f;

	// Token: 0x040010F5 RID: 4341
	[Range(0f, 30f)]
	public int Speed = 5;

	// Token: 0x040010F6 RID: 4342
	[Range(-1f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x040010F7 RID: 4343
	[Range(-1f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x040010F8 RID: 4344
	[Range(0f, 1f)]
	public float Intensity = 1f;
}
