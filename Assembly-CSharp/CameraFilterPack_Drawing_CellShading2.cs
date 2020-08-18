using System;
using UnityEngine;

// Token: 0x02000179 RID: 377
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/CellShading2")]
public class CameraFilterPack_Drawing_CellShading2 : MonoBehaviour
{
	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0006D188 File Offset: 0x0006B388
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

	// Token: 0x06000DE9 RID: 3561 RVA: 0x0006D1BC File Offset: 0x0006B3BC
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_CellShading2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0006D1E0 File Offset: 0x0006B3E0
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
			this.material.SetFloat("_EdgeSize", this.EdgeSize);
			this.material.SetFloat("_ColorLevel", this.ColorLevel);
			this.material.SetFloat("_Distortion", this.Blur);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x0006D2BB File Offset: 0x0006B4BB
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010A4 RID: 4260
	public Shader SCShader;

	// Token: 0x040010A5 RID: 4261
	private float TimeX = 1f;

	// Token: 0x040010A6 RID: 4262
	private Material SCMaterial;

	// Token: 0x040010A7 RID: 4263
	[Range(0f, 1f)]
	public float EdgeSize = 0.1f;

	// Token: 0x040010A8 RID: 4264
	[Range(0f, 10f)]
	public float ColorLevel = 4f;

	// Token: 0x040010A9 RID: 4265
	[Range(0f, 1f)]
	public float Blur = 1f;
}
