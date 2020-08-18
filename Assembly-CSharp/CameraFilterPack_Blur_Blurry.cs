using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Blurry")]
public class CameraFilterPack_Blur_Blurry : MonoBehaviour
{
	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00066CC8 File Offset: 0x00064EC8
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

	// Token: 0x06000C6D RID: 3181 RVA: 0x00066CFC File Offset: 0x00064EFC
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Blurry");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00066D20 File Offset: 0x00064F20
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
		this.material.SetVector("_ScreenResolution", new Vector2((float)(Screen.width / fastFilter), (float)(Screen.height / fastFilter)));
		int width = sourceTexture.width / fastFilter;
		int height = sourceTexture.height / fastFilter;
		if (this.FastFilter > 1)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			temporary.filterMode = FilterMode.Trilinear;
			Graphics.Blit(sourceTexture, temporary, this.material);
			Graphics.Blit(temporary, destTexture);
			RenderTexture.ReleaseTemporary(temporary);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture, this.material);
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x00066E20 File Offset: 0x00065020
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F2A RID: 3882
	public Shader SCShader;

	// Token: 0x04000F2B RID: 3883
	private float TimeX = 1f;

	// Token: 0x04000F2C RID: 3884
	private Material SCMaterial;

	// Token: 0x04000F2D RID: 3885
	[Range(0f, 20f)]
	public float Amount = 2f;

	// Token: 0x04000F2E RID: 3886
	[Range(1f, 8f)]
	public int FastFilter = 2;
}
