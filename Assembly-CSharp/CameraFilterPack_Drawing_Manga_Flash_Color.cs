using System;
using UnityEngine;

// Token: 0x02000189 RID: 393
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_Flash_Color")]
public class CameraFilterPack_Drawing_Manga_Flash_Color : MonoBehaviour
{
	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0006E6DD File Offset: 0x0006C8DD
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

	// Token: 0x06000E49 RID: 3657 RVA: 0x0006E711 File Offset: 0x0006C911
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_Flash_Color");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0006E734 File Offset: 0x0006C934
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
			this.material.SetColor("Color", this.Color);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x0006E859 File Offset: 0x0006CA59
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010F9 RID: 4345
	public Shader SCShader;

	// Token: 0x040010FA RID: 4346
	private float TimeX = 1f;

	// Token: 0x040010FB RID: 4347
	private Material SCMaterial;

	// Token: 0x040010FC RID: 4348
	[Range(1f, 10f)]
	public float Size = 1f;

	// Token: 0x040010FD RID: 4349
	public Color Color = new Color(0f, 0.7f, 1f, 1f);

	// Token: 0x040010FE RID: 4350
	[Range(0f, 30f)]
	public int Speed = 5;

	// Token: 0x040010FF RID: 4351
	[Range(0f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x04001100 RID: 4352
	[Range(0f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x04001101 RID: 4353
	[Range(0f, 1f)]
	public float Intensity = 1f;
}
