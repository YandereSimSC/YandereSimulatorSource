using System;
using UnityEngine;

// Token: 0x02000207 RID: 519
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Posterize")]
public class CameraFilterPack_TV_Posterize : MonoBehaviour
{
	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06001160 RID: 4448 RVA: 0x0007C8AB File Offset: 0x0007AAAB
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

	// Token: 0x06001161 RID: 4449 RVA: 0x0007C8DF File Offset: 0x0007AADF
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Posterize");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0007C900 File Offset: 0x0007AB00
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("_Distortion", this.Posterize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x0007C99C File Offset: 0x0007AB9C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001465 RID: 5221
	public Shader SCShader;

	// Token: 0x04001466 RID: 5222
	private float TimeX = 1f;

	// Token: 0x04001467 RID: 5223
	[Range(1f, 256f)]
	public float Posterize = 64f;

	// Token: 0x04001468 RID: 5224
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001469 RID: 5225
	private Material SCMaterial;
}
