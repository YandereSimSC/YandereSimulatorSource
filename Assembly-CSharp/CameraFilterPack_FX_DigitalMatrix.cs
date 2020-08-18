using System;
using UnityEngine;

// Token: 0x0200019D RID: 413
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/DigitalMatrix")]
public class CameraFilterPack_FX_DigitalMatrix : MonoBehaviour
{
	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x000706D4 File Offset: 0x0006E8D4
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

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00070708 File Offset: 0x0006E908
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_DigitalMatrix");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0007072C File Offset: 0x0006E92C
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.ColorR);
			this.material.SetFloat("_Value3", this.ColorG);
			this.material.SetFloat("_Value4", this.ColorB);
			this.material.SetFloat("_Value5", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0007083A File Offset: 0x0006EA3A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001182 RID: 4482
	public Shader SCShader;

	// Token: 0x04001183 RID: 4483
	private float TimeX = 1f;

	// Token: 0x04001184 RID: 4484
	private Material SCMaterial;

	// Token: 0x04001185 RID: 4485
	[Range(0.4f, 5f)]
	public float Size = 1f;

	// Token: 0x04001186 RID: 4486
	[Range(-10f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001187 RID: 4487
	[Range(-1f, 1f)]
	public float ColorR = -1f;

	// Token: 0x04001188 RID: 4488
	[Range(-1f, 1f)]
	public float ColorG = 1f;

	// Token: 0x04001189 RID: 4489
	[Range(-1f, 1f)]
	public float ColorB = -1f;
}
