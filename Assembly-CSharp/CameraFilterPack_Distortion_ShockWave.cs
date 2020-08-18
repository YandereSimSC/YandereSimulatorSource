using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/ShockWave")]
public class CameraFilterPack_Distortion_ShockWave : MonoBehaviour
{
	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0006C453 File Offset: 0x0006A653
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

	// Token: 0x06000DB9 RID: 3513 RVA: 0x0006C487 File Offset: 0x0006A687
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_ShockWave");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x0006C4A8 File Offset: 0x0006A6A8
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
			this.material.SetFloat("_Value", this.PosX);
			this.material.SetFloat("_Value2", this.PosY);
			this.material.SetFloat("_Value3", this.Speed);
			this.material.SetFloat("_Value4", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0006C5A0 File Offset: 0x0006A7A0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400106B RID: 4203
	public Shader SCShader;

	// Token: 0x0400106C RID: 4204
	private float TimeX = 1f;

	// Token: 0x0400106D RID: 4205
	private Material SCMaterial;

	// Token: 0x0400106E RID: 4206
	[Range(-1.5f, 1.5f)]
	public float PosX = 0.5f;

	// Token: 0x0400106F RID: 4207
	[Range(-1.5f, 1.5f)]
	public float PosY = 0.5f;

	// Token: 0x04001070 RID: 4208
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001071 RID: 4209
	[Range(0f, 10f)]
	private float Size = 1f;
}
