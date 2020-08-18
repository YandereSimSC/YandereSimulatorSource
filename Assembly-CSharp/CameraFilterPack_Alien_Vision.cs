using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Alien/Vision")]
public class CameraFilterPack_Alien_Vision : MonoBehaviour
{
	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00060FA6 File Offset: 0x0005F1A6
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

	// Token: 0x06000B32 RID: 2866 RVA: 0x00060FDA File Offset: 0x0005F1DA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Alien_Vision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00060FFC File Offset: 0x0005F1FC
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
			this.material.SetFloat("_Value", this.Therma_Variation);
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.Burn);
			this.material.SetFloat("_Value4", this.SceneCut);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x000610F4 File Offset: 0x0005F2F4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DAB RID: 3499
	public Shader SCShader;

	// Token: 0x04000DAC RID: 3500
	private float TimeX = 1f;

	// Token: 0x04000DAD RID: 3501
	private Material SCMaterial;

	// Token: 0x04000DAE RID: 3502
	[Range(0f, 0.5f)]
	public float Therma_Variation = 0.5f;

	// Token: 0x04000DAF RID: 3503
	[Range(0f, 1f)]
	public float Speed = 0.5f;

	// Token: 0x04000DB0 RID: 3504
	[Range(0f, 4f)]
	private float Burn;

	// Token: 0x04000DB1 RID: 3505
	[Range(0f, 16f)]
	private float SceneCut = 1f;
}
