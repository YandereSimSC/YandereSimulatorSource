using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/WaterDropPro")]
public class CameraFilterPack_AAA_WaterDropPro : MonoBehaviour
{
	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00060E02 File Offset: 0x0005F002
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

	// Token: 0x06000B2C RID: 2860 RVA: 0x00060E36 File Offset: 0x0005F036
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_WaterDrop") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_WaterDropPro");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00060E6C File Offset: 0x0005F06C
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
			this.material.SetFloat("_SizeX", this.SizeX);
			this.material.SetFloat("_SizeY", this.SizeY);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x00060F4D File Offset: 0x0005F14D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DA3 RID: 3491
	public Shader SCShader;

	// Token: 0x04000DA4 RID: 3492
	private float TimeX = 1f;

	// Token: 0x04000DA5 RID: 3493
	[Range(8f, 64f)]
	public float Distortion = 8f;

	// Token: 0x04000DA6 RID: 3494
	[Range(0f, 7f)]
	public float SizeX = 1f;

	// Token: 0x04000DA7 RID: 3495
	[Range(0f, 7f)]
	public float SizeY = 0.5f;

	// Token: 0x04000DA8 RID: 3496
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04000DA9 RID: 3497
	private Material SCMaterial;

	// Token: 0x04000DAA RID: 3498
	private Texture2D Texture2;
}
