using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Light/Water2")]
public class CameraFilterPack_Light_Water2 : MonoBehaviour
{
	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0007555A File Offset: 0x0007375A
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

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0007558E File Offset: 0x0007378E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Light_Water2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x000755B0 File Offset: 0x000737B0
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.Speed_X);
			this.material.SetFloat("_Value3", this.Speed_Y);
			this.material.SetFloat("_Value4", this.Intensity);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x000756A8 File Offset: 0x000738A8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012C6 RID: 4806
	public Shader SCShader;

	// Token: 0x040012C7 RID: 4807
	private float TimeX = 1f;

	// Token: 0x040012C8 RID: 4808
	private Material SCMaterial;

	// Token: 0x040012C9 RID: 4809
	[Range(0f, 10f)]
	public float Speed = 0.2f;

	// Token: 0x040012CA RID: 4810
	[Range(0f, 10f)]
	public float Speed_X = 0.2f;

	// Token: 0x040012CB RID: 4811
	[Range(0f, 1f)]
	public float Speed_Y = 0.3f;

	// Token: 0x040012CC RID: 4812
	[Range(0f, 10f)]
	public float Intensity = 2.4f;
}
