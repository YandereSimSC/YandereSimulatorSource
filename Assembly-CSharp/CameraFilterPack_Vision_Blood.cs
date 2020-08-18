using System;
using UnityEngine;

// Token: 0x02000218 RID: 536
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Blood")]
public class CameraFilterPack_Vision_Blood : MonoBehaviour
{
	// Token: 0x17000338 RID: 824
	// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0007E1A0 File Offset: 0x0007C3A0
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

	// Token: 0x060011C7 RID: 4551 RVA: 0x0007E1D4 File Offset: 0x0007C3D4
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Blood");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0007E1F8 File Offset: 0x0007C3F8
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
			this.material.SetFloat("_Value", this.HoleSize);
			this.material.SetFloat("_Value2", this.HoleSmooth);
			this.material.SetFloat("_Value3", this.Color1);
			this.material.SetFloat("_Value4", this.Color2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x0007E2F0 File Offset: 0x0007C4F0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014C9 RID: 5321
	public Shader SCShader;

	// Token: 0x040014CA RID: 5322
	private float TimeX = 1f;

	// Token: 0x040014CB RID: 5323
	private Material SCMaterial;

	// Token: 0x040014CC RID: 5324
	[Range(0.01f, 1f)]
	public float HoleSize = 0.6f;

	// Token: 0x040014CD RID: 5325
	[Range(-1f, 1f)]
	public float HoleSmooth = 0.3f;

	// Token: 0x040014CE RID: 5326
	[Range(-2f, 2f)]
	public float Color1 = 0.2f;

	// Token: 0x040014CF RID: 5327
	[Range(-2f, 2f)]
	public float Color2 = 0.9f;
}
