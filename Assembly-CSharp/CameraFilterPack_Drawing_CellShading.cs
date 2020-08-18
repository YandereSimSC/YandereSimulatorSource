using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/CellShading")]
public class CameraFilterPack_Drawing_CellShading : MonoBehaviour
{
	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0006D029 File Offset: 0x0006B229
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

	// Token: 0x06000DE3 RID: 3555 RVA: 0x0006D05D File Offset: 0x0006B25D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_CellShading");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0006D080 File Offset: 0x0006B280
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0006D145 File Offset: 0x0006B345
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400109F RID: 4255
	public Shader SCShader;

	// Token: 0x040010A0 RID: 4256
	private float TimeX = 1f;

	// Token: 0x040010A1 RID: 4257
	private Material SCMaterial;

	// Token: 0x040010A2 RID: 4258
	[Range(0f, 1f)]
	public float EdgeSize = 0.1f;

	// Token: 0x040010A3 RID: 4259
	[Range(0f, 10f)]
	public float ColorLevel = 4f;
}
