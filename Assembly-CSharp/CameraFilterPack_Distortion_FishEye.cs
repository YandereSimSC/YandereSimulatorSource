using System;
using UnityEngine;

// Token: 0x0200016A RID: 362
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/FishEye")]
public class CameraFilterPack_Distortion_FishEye : MonoBehaviour
{
	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0006BB1B File Offset: 0x00069D1B
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

	// Token: 0x06000D8F RID: 3471 RVA: 0x0006BB4F File Offset: 0x00069D4F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_FishEye");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0006BB70 File Offset: 0x00069D70
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0006BC26 File Offset: 0x00069E26
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001049 RID: 4169
	public Shader SCShader;

	// Token: 0x0400104A RID: 4170
	private float TimeX = 1f;

	// Token: 0x0400104B RID: 4171
	private Material SCMaterial;

	// Token: 0x0400104C RID: 4172
	[Range(0f, 1f)]
	public float Distortion = 0.35f;
}
