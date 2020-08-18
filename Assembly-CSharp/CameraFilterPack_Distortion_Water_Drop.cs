using System;
using UnityEngine;

// Token: 0x02000175 RID: 373
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Water_Drop")]
public class CameraFilterPack_Distortion_Water_Drop : MonoBehaviour
{
	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0006CAE2 File Offset: 0x0006ACE2
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

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0006CB16 File Offset: 0x0006AD16
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Water_Drop");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0006CB38 File Offset: 0x0006AD38
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			this.material.SetFloat("_WaveIntensity", this.WaveIntensity);
			this.material.SetInt("_NumberOfWaves", this.NumberOfWaves);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0006CC29 File Offset: 0x0006AE29
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001087 RID: 4231
	public Shader SCShader;

	// Token: 0x04001088 RID: 4232
	private float TimeX = 1f;

	// Token: 0x04001089 RID: 4233
	private Material SCMaterial;

	// Token: 0x0400108A RID: 4234
	[Range(-1f, 1f)]
	public float CenterX;

	// Token: 0x0400108B RID: 4235
	[Range(-1f, 1f)]
	public float CenterY;

	// Token: 0x0400108C RID: 4236
	[Range(0f, 10f)]
	public float WaveIntensity = 1f;

	// Token: 0x0400108D RID: 4237
	[Range(0f, 20f)]
	public int NumberOfWaves = 5;
}
