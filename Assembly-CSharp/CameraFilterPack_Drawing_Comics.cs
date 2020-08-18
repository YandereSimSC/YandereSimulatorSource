using System;
using UnityEngine;

// Token: 0x0200017A RID: 378
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Comics")]
public class CameraFilterPack_Drawing_Comics : MonoBehaviour
{
	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0006D309 File Offset: 0x0006B509
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

	// Token: 0x06000DEF RID: 3567 RVA: 0x0006D33D File Offset: 0x0006B53D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Comics");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0006D360 File Offset: 0x0006B560
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
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x0006D3E6 File Offset: 0x0006B5E6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010AA RID: 4266
	public Shader SCShader;

	// Token: 0x040010AB RID: 4267
	private float TimeX = 1f;

	// Token: 0x040010AC RID: 4268
	private Material SCMaterial;

	// Token: 0x040010AD RID: 4269
	[Range(0f, 1f)]
	public float DotSize = 0.5f;
}
