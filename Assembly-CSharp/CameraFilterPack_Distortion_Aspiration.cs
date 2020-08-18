using System;
using UnityEngine;

// Token: 0x02000164 RID: 356
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Aspiration")]
public class CameraFilterPack_Distortion_Aspiration : MonoBehaviour
{
	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0006B25C File Offset: 0x0006945C
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

	// Token: 0x06000D6B RID: 3435 RVA: 0x0006B290 File Offset: 0x00069490
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Aspiration");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0006B2B4 File Offset: 0x000694B4
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
			this.material.SetFloat("_Value", 1f - this.Value);
			this.material.SetFloat("_Value2", this.PosX);
			this.material.SetFloat("_Value3", this.PosY);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0006B3B2 File Offset: 0x000695B2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001026 RID: 4134
	public Shader SCShader;

	// Token: 0x04001027 RID: 4135
	private float TimeX = 1f;

	// Token: 0x04001028 RID: 4136
	private Material SCMaterial;

	// Token: 0x04001029 RID: 4137
	[Range(0f, 1f)]
	public float Value = 0.8f;

	// Token: 0x0400102A RID: 4138
	[Range(-1f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x0400102B RID: 4139
	[Range(-1f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x0400102C RID: 4140
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
