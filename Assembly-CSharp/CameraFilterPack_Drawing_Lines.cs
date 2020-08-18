using System;
using UnityEngine;

// Token: 0x02000180 RID: 384
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Lines")]
public class CameraFilterPack_Drawing_Lines : MonoBehaviour
{
	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0006DB16 File Offset: 0x0006BD16
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

	// Token: 0x06000E13 RID: 3603 RVA: 0x0006DB4A File Offset: 0x0006BD4A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Lines");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0006DB6C File Offset: 0x0006BD6C
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
			this.material.SetFloat("_Value", this.Number);
			this.material.SetFloat("_Value2", this.Random);
			this.material.SetFloat("_Value3", this.PositionY);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0006DC64 File Offset: 0x0006BE64
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010C9 RID: 4297
	public Shader SCShader;

	// Token: 0x040010CA RID: 4298
	private float TimeX = 1f;

	// Token: 0x040010CB RID: 4299
	private Material SCMaterial;

	// Token: 0x040010CC RID: 4300
	[Range(0.1f, 10f)]
	public float Number = 1f;

	// Token: 0x040010CD RID: 4301
	[Range(0f, 1f)]
	public float Random = 0.5f;

	// Token: 0x040010CE RID: 4302
	[Range(0f, 10f)]
	private float PositionY = 1f;

	// Token: 0x040010CF RID: 4303
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
