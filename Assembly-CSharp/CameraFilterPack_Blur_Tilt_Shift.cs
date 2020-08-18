using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Tilt_Shift")]
public class CameraFilterPack_Blur_Tilt_Shift : MonoBehaviour
{
	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00067D58 File Offset: 0x00065F58
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

	// Token: 0x06000CAF RID: 3247 RVA: 0x00067D8C File Offset: 0x00065F8C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/BlurTiltShift");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00067DB0 File Offset: 0x00065FB0
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
		this.material.SetVector("_ScreenResolution", new Vector2((float)(Screen.width / fastFilter), (float)(Screen.height / fastFilter)));
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

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00067F6A File Offset: 0x0006616A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F67 RID: 3943
	public Shader SCShader;

	// Token: 0x04000F68 RID: 3944
	private float TimeX = 1f;

	// Token: 0x04000F69 RID: 3945
	private Material SCMaterial;

	// Token: 0x04000F6A RID: 3946
	[Range(0f, 20f)]
	public float Amount = 3f;

	// Token: 0x04000F6B RID: 3947
	[Range(2f, 16f)]
	public int FastFilter = 8;

	// Token: 0x04000F6C RID: 3948
	[Range(0f, 1f)]
	public float Smooth = 0.5f;

	// Token: 0x04000F6D RID: 3949
	[Range(0f, 1f)]
	public float Size = 0.5f;

	// Token: 0x04000F6E RID: 3950
	[Range(-1f, 1f)]
	public float Position = 0.5f;
}
