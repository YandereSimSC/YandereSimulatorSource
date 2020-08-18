using System;
using UnityEngine;

// Token: 0x0200020C RID: 524
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/VCR Distortion")]
public class CameraFilterPack_TV_Vcr : MonoBehaviour
{
	// Token: 0x1700032C RID: 812
	// (get) Token: 0x0600117E RID: 4478 RVA: 0x0007D039 File Offset: 0x0007B239
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

	// Token: 0x0600117F RID: 4479 RVA: 0x0007D06D File Offset: 0x0007B26D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Vcr");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0007D090 File Offset: 0x0007B290
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
			this.material.SetFloat("_Distortion", this.Distortion);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0007D116 File Offset: 0x0007B316
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001484 RID: 5252
	public Shader SCShader;

	// Token: 0x04001485 RID: 5253
	private float TimeX = 1f;

	// Token: 0x04001486 RID: 5254
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001487 RID: 5255
	private Material SCMaterial;
}
