using System;
using UnityEngine;

// Token: 0x02000205 RID: 517
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Old_Movie_2")]
public class CameraFilterPack_TV_Old_Movie_2 : MonoBehaviour
{
	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06001154 RID: 4436 RVA: 0x0007C58A File Offset: 0x0007A78A
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

	// Token: 0x06001155 RID: 4437 RVA: 0x0007C5BE File Offset: 0x0007A7BE
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Old_Movie_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x0007C5E0 File Offset: 0x0007A7E0
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
			this.material.SetFloat("_Value", this.FramePerSecond);
			this.material.SetFloat("_Value2", this.Contrast);
			this.material.SetFloat("_Value3", this.Burn);
			this.material.SetFloat("_Value4", this.SceneCut);
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0007C6EE File Offset: 0x0007A8EE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001458 RID: 5208
	public Shader SCShader;

	// Token: 0x04001459 RID: 5209
	private float TimeX = 1f;

	// Token: 0x0400145A RID: 5210
	private Material SCMaterial;

	// Token: 0x0400145B RID: 5211
	[Range(1f, 60f)]
	public float FramePerSecond = 15f;

	// Token: 0x0400145C RID: 5212
	[Range(0f, 5f)]
	public float Contrast = 1f;

	// Token: 0x0400145D RID: 5213
	[Range(0f, 4f)]
	public float Burn;

	// Token: 0x0400145E RID: 5214
	[Range(0f, 16f)]
	public float SceneCut = 1f;

	// Token: 0x0400145F RID: 5215
	[Range(0f, 1f)]
	public float Fade = 1f;
}
