using System;
using UnityEngine;

// Token: 0x0200013F RID: 319
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Movie")]
public class CameraFilterPack_Blur_Movie : MonoBehaviour
{
	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00067417 File Offset: 0x00065617
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

	// Token: 0x06000C8B RID: 3211 RVA: 0x0006744B File Offset: 0x0006564B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Movie");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0006746C File Offset: 0x0006566C
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
		this.material.SetFloat("_Radius", this.Radius / (float)fastFilter);
		this.material.SetFloat("_Factor", this.Factor);
		this.material.SetVector("_ScreenResolution", new Vector2((float)(Screen.width / fastFilter), (float)(Screen.height / fastFilter)));
		int width = sourceTexture.width / fastFilter;
		int height = sourceTexture.height / fastFilter;
		if (this.FastFilter > 1)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			Graphics.Blit(sourceTexture, temporary, this.material);
			Graphics.Blit(temporary, destTexture);
			RenderTexture.ReleaseTemporary(temporary);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture, this.material);
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x0006757E File Offset: 0x0006577E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F44 RID: 3908
	public Shader SCShader;

	// Token: 0x04000F45 RID: 3909
	private float TimeX = 1f;

	// Token: 0x04000F46 RID: 3910
	private Material SCMaterial;

	// Token: 0x04000F47 RID: 3911
	[Range(0f, 1000f)]
	public float Radius = 150f;

	// Token: 0x04000F48 RID: 3912
	[Range(0f, 1000f)]
	public float Factor = 200f;

	// Token: 0x04000F49 RID: 3913
	[Range(1f, 8f)]
	public int FastFilter = 2;
}
