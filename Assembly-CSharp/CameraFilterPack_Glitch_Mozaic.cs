using System;
using UnityEngine;

// Token: 0x020001BD RID: 445
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Mozaic")]
public class CameraFilterPack_Glitch_Mozaic : MonoBehaviour
{
	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00073BE5 File Offset: 0x00071DE5
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

	// Token: 0x06000F82 RID: 3970 RVA: 0x00073C19 File Offset: 0x00071E19
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Glitch_Mozaic");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x00073C3C File Offset: 0x00071E3C
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
			this.material.SetFloat("_Value", this.Intensity);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x00073D34 File Offset: 0x00071F34
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001263 RID: 4707
	public Shader SCShader;

	// Token: 0x04001264 RID: 4708
	private float TimeX = 1f;

	// Token: 0x04001265 RID: 4709
	private Material SCMaterial;

	// Token: 0x04001266 RID: 4710
	[Range(0.001f, 10f)]
	public float Intensity = 1f;

	// Token: 0x04001267 RID: 4711
	[Range(0f, 10f)]
	private float Value2 = 1f;

	// Token: 0x04001268 RID: 4712
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001269 RID: 4713
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
