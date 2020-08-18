using System;
using UnityEngine;

// Token: 0x02000201 RID: 513
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Movie Noise")]
public class CameraFilterPack_TV_MovieNoise : MonoBehaviour
{
	// Token: 0x17000321 RID: 801
	// (get) Token: 0x0600113C RID: 4412 RVA: 0x0007C0DA File Offset: 0x0007A2DA
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

	// Token: 0x0600113D RID: 4413 RVA: 0x0007C10E File Offset: 0x0007A30E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_MovieNoise");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0007C130 File Offset: 0x0007A330
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
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x0007C1E6 File Offset: 0x0007A3E6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001448 RID: 5192
	public Shader SCShader;

	// Token: 0x04001449 RID: 5193
	private float TimeX = 1f;

	// Token: 0x0400144A RID: 5194
	private Material SCMaterial;

	// Token: 0x0400144B RID: 5195
	[Range(0.0001f, 1f)]
	public float Fade = 0.01f;
}
