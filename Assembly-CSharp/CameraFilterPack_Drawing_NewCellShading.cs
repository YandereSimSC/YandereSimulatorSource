using System;
using UnityEngine;

// Token: 0x0200018A RID: 394
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/NewCellShading")]
public class CameraFilterPack_Drawing_NewCellShading : MonoBehaviour
{
	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0006E8E4 File Offset: 0x0006CAE4
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

	// Token: 0x06000E4F RID: 3663 RVA: 0x0006E918 File Offset: 0x0006CB18
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_NewCellShading");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0006E93C File Offset: 0x0006CB3C
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
			this.material.SetFloat("_Threshold", this.Threshold);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0006E9EB File Offset: 0x0006CBEB
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001102 RID: 4354
	public Shader SCShader;

	// Token: 0x04001103 RID: 4355
	private float TimeX = 1f;

	// Token: 0x04001104 RID: 4356
	private Material SCMaterial;

	// Token: 0x04001105 RID: 4357
	[Range(0f, 1f)]
	public float Threshold = 0.2f;
}
