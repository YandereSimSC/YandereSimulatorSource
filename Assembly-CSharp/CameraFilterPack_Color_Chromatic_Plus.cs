using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Chromatic_Plus")]
public class CameraFilterPack_Color_Chromatic_Plus : MonoBehaviour
{
	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00068FDA File Offset: 0x000671DA
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

	// Token: 0x06000CEB RID: 3307 RVA: 0x0006900E File Offset: 0x0006720E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Chromatic_Plus");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x00069030 File Offset: 0x00067230
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Distortion", this.Offset);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x00069112 File Offset: 0x00067312
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FB0 RID: 4016
	public Shader SCShader;

	// Token: 0x04000FB1 RID: 4017
	private float TimeX = 1f;

	// Token: 0x04000FB2 RID: 4018
	private Material SCMaterial;

	// Token: 0x04000FB3 RID: 4019
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x04000FB4 RID: 4020
	[Range(0.01f, 0.4f)]
	public float Smooth = 0.26f;

	// Token: 0x04000FB5 RID: 4021
	[Range(-0.02f, 0.02f)]
	public float Offset = 0.005f;
}
