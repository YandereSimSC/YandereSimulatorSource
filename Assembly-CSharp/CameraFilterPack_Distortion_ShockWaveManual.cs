using System;
using UnityEngine;

// Token: 0x02000172 RID: 370
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/ShockWave Manual")]
public class CameraFilterPack_Distortion_ShockWaveManual : MonoBehaviour
{
	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0006C5F9 File Offset: 0x0006A7F9
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

	// Token: 0x06000DBF RID: 3519 RVA: 0x0006C62D File Offset: 0x0006A82D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_ShockWaveManual");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x0006C650 File Offset: 0x0006A850
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
			this.material.SetFloat("_Value3", this.Value);
			this.material.SetFloat("_Value4", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x0006C748 File Offset: 0x0006A948
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001072 RID: 4210
	public Shader SCShader;

	// Token: 0x04001073 RID: 4211
	private float TimeX = 1f;

	// Token: 0x04001074 RID: 4212
	private Material SCMaterial;

	// Token: 0x04001075 RID: 4213
	[Range(-1.5f, 1.5f)]
	public float PosX = 0.5f;

	// Token: 0x04001076 RID: 4214
	[Range(-1.5f, 1.5f)]
	public float PosY = 0.5f;

	// Token: 0x04001077 RID: 4215
	[Range(-0.1f, 2f)]
	public float Value = 0.5f;

	// Token: 0x04001078 RID: 4216
	[Range(0f, 10f)]
	public float Size = 1f;
}
