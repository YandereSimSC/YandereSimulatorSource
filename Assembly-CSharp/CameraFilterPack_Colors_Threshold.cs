using System;
using UnityEngine;

// Token: 0x02000162 RID: 354
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Threshold")]
public class CameraFilterPack_Colors_Threshold : MonoBehaviour
{
	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0006B05D File Offset: 0x0006925D
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

	// Token: 0x06000D5F RID: 3423 RVA: 0x0006B091 File Offset: 0x00069291
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_Threshold");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0006B0B4 File Offset: 0x000692B4
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
			this.material.SetFloat("_Distortion", this.Threshold);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0006B13A File Offset: 0x0006933A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400101E RID: 4126
	public Shader SCShader;

	// Token: 0x0400101F RID: 4127
	private float TimeX = 1f;

	// Token: 0x04001020 RID: 4128
	[Range(0f, 1f)]
	public float Threshold = 0.3f;

	// Token: 0x04001021 RID: 4129
	private Material SCMaterial;
}
