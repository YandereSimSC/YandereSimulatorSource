using System;
using UnityEngine;

// Token: 0x02000146 RID: 326
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Tilt_Shift_Hole")]
public class CameraFilterPack_Blur_Tilt_Shift_Hole : MonoBehaviour
{
	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00067FD5 File Offset: 0x000661D5
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

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00068009 File Offset: 0x00066209
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/BlurTiltShift_Hole");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x0006802C File Offset: 0x0006622C
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
		this.material.SetFloat("_Value3", this.PositionX);
		this.material.SetFloat("_Value4", this.PositionY);
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

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x000681D2 File Offset: 0x000663D2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F6F RID: 3951
	public Shader SCShader;

	// Token: 0x04000F70 RID: 3952
	private float TimeX = 1f;

	// Token: 0x04000F71 RID: 3953
	private Material SCMaterial;

	// Token: 0x04000F72 RID: 3954
	[Range(0f, 20f)]
	public float Amount = 3f;

	// Token: 0x04000F73 RID: 3955
	[Range(2f, 16f)]
	public int FastFilter = 8;

	// Token: 0x04000F74 RID: 3956
	[Range(0f, 1f)]
	public float Smooth = 0.5f;

	// Token: 0x04000F75 RID: 3957
	[Range(0f, 1f)]
	public float Size = 0.2f;

	// Token: 0x04000F76 RID: 3958
	[Range(-1f, 1f)]
	public float PositionX = 0.5f;

	// Token: 0x04000F77 RID: 3959
	[Range(-1f, 1f)]
	public float PositionY = 0.5f;
}
