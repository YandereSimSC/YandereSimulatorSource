using System;
using UnityEngine;

// Token: 0x020001E0 RID: 480
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Noise/TV_3")]
public class CameraFilterPack_Noise_TV_3 : MonoBehaviour
{
	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06001071 RID: 4209 RVA: 0x00078675 File Offset: 0x00076875
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

	// Token: 0x06001072 RID: 4210 RVA: 0x000786A9 File Offset: 0x000768A9
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_Noise3") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Noise_TV_3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x000786E0 File Offset: 0x000768E0
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Fade_Additive);
			this.material.SetFloat("_Value3", this.Fade_Distortion);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000787EE File Offset: 0x000769EE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001369 RID: 4969
	public Shader SCShader;

	// Token: 0x0400136A RID: 4970
	private float TimeX = 1f;

	// Token: 0x0400136B RID: 4971
	private Material SCMaterial;

	// Token: 0x0400136C RID: 4972
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x0400136D RID: 4973
	[Range(0f, 1f)]
	public float Fade_Additive;

	// Token: 0x0400136E RID: 4974
	[Range(0f, 1f)]
	public float Fade_Distortion;

	// Token: 0x0400136F RID: 4975
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x04001370 RID: 4976
	private Texture2D Texture2;
}
