using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_Flash")]
public class CameraFilterPack_Drawing_Manga_Flash : MonoBehaviour
{
	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0006E336 File Offset: 0x0006C536
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

	// Token: 0x06000E3D RID: 3645 RVA: 0x0006E36A File Offset: 0x0006C56A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_Flash");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0006E38C File Offset: 0x0006C58C
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

	// Token: 0x06000E3F RID: 3647 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0006E49B File Offset: 0x0006C69B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010E9 RID: 4329
	public Shader SCShader;

	// Token: 0x040010EA RID: 4330
	private float TimeX = 1f;

	// Token: 0x040010EB RID: 4331
	private Material SCMaterial;

	// Token: 0x040010EC RID: 4332
	[Range(1f, 10f)]
	public float Size = 1f;

	// Token: 0x040010ED RID: 4333
	[Range(0f, 30f)]
	public int Speed = 5;

	// Token: 0x040010EE RID: 4334
	[Range(-1f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x040010EF RID: 4335
	[Range(-1f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x040010F0 RID: 4336
	[Range(0f, 1f)]
	public float Intensity = 1f;
}
