using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Wave_Horizontal")]
public class CameraFilterPack_Distortion_Wave_Horizontal : MonoBehaviour
{
	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0006CC68 File Offset: 0x0006AE68
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

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0006CC9C File Offset: 0x0006AE9C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Wave_Horizontal");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0006CCC0 File Offset: 0x0006AEC0
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_WaveIntensity", this.WaveIntensity);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0006CD6F File Offset: 0x0006AF6F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400108E RID: 4238
	public Shader SCShader;

	// Token: 0x0400108F RID: 4239
	private float TimeX = 1f;

	// Token: 0x04001090 RID: 4240
	private Material SCMaterial;

	// Token: 0x04001091 RID: 4241
	[Range(1f, 100f)]
	public float WaveIntensity = 32f;
}
