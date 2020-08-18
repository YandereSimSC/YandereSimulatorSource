using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Screens")]
public class CameraFilterPack_FX_Screens : MonoBehaviour
{
	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0007217D File Offset: 0x0007037D
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

	// Token: 0x06000F34 RID: 3892 RVA: 0x000721B1 File Offset: 0x000703B1
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Screens");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x000721D4 File Offset: 0x000703D4
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
			this.material.SetFloat("_Value", this.Tiles);
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetColor("_color", this.color);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x000722E2 File Offset: 0x000704E2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011E8 RID: 4584
	public Shader SCShader;

	// Token: 0x040011E9 RID: 4585
	private float TimeX = 1f;

	// Token: 0x040011EA RID: 4586
	private Material SCMaterial;

	// Token: 0x040011EB RID: 4587
	[Range(0f, 256f)]
	public float Tiles = 8f;

	// Token: 0x040011EC RID: 4588
	[Range(0f, 5f)]
	public float Speed = 0.25f;

	// Token: 0x040011ED RID: 4589
	public Color color = new Color(0f, 1f, 1f, 1f);

	// Token: 0x040011EE RID: 4590
	[Range(-1f, 1f)]
	public float PosX;

	// Token: 0x040011EF RID: 4591
	[Range(-1f, 1f)]
	public float PosY;
}
