using System;
using UnityEngine;

// Token: 0x02000147 RID: 327
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Tilt_Shift_V")]
public class CameraFilterPack_Blur_Tilt_Shift_V : MonoBehaviour
{
	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00068248 File Offset: 0x00066448
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

	// Token: 0x06000CBB RID: 3259 RVA: 0x0006827C File Offset: 0x0006647C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/BlurTiltShift_V");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x000682A0 File Offset: 0x000664A0
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (!(this.SCShader != null))
		{
			Graphics.Blit(sourceTexture, destTexture);
			return;
		}
		int fastFilter = this.FastFilter;
		this.TimeX += Time.deltaTime;
		if (this.TimeX > 100f)
		{
			this.TimeX = 0f;
		}
		this.material.SetFloat("_TimeX", this.TimeX);
		this.material.SetFloat("_Amount", this.Amount);
		this.material.SetFloat("_Value1", this.Smooth);
		this.material.SetFloat("_Value2", this.Size);
		this.material.SetFloat("_Value3", this.Position);
		int width = sourceTexture.width / fastFilter;
		int height = sourceTexture.height / fastFilter;
		if (this.FastFilter > 1)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0);
			temporary.filterMode = FilterMode.Trilinear;
			Graphics.Blit(sourceTexture, temporary, this.material, 2);
			Graphics.Blit(temporary, temporary2, this.material, 0);
			this.material.SetFloat("_Amount", this.Amount * 2f);
			Graphics.Blit(temporary2, temporary, this.material, 2);
			Graphics.Blit(temporary, temporary2, this.material, 0);
			this.material.SetTexture("_MainTex2", temporary2);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			Graphics.Blit(sourceTexture, destTexture, this.material, 1);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture, this.material, 0);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00068430 File Offset: 0x00066630
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F78 RID: 3960
	public Shader SCShader;

	// Token: 0x04000F79 RID: 3961
	private float TimeX = 1f;

	// Token: 0x04000F7A RID: 3962
	private Material SCMaterial;

	// Token: 0x04000F7B RID: 3963
	[Range(0f, 20f)]
	public float Amount = 3f;

	// Token: 0x04000F7C RID: 3964
	[Range(2f, 16f)]
	public int FastFilter = 8;

	// Token: 0x04000F7D RID: 3965
	[Range(0f, 1f)]
	public float Smooth = 0.5f;

	// Token: 0x04000F7E RID: 3966
	[Range(0f, 1f)]
	public float Size = 0.5f;

	// Token: 0x04000F7F RID: 3967
	[Range(-1f, 1f)]
	public float Position = 0.5f;
}
