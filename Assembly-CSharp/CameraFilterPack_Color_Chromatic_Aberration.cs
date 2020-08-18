using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Chromatic_Aberration")]
public class CameraFilterPack_Color_Chromatic_Aberration : MonoBehaviour
{
	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00068E94 File Offset: 0x00067094
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

	// Token: 0x06000CE5 RID: 3301 RVA: 0x00068EC8 File Offset: 0x000670C8
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Chromatic_Aberration");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x00068EEC File Offset: 0x000670EC
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
			this.material.SetFloat("_Distortion", this.Offset);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x00068FA2 File Offset: 0x000671A2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FAC RID: 4012
	public Shader SCShader;

	// Token: 0x04000FAD RID: 4013
	private float TimeX = 1f;

	// Token: 0x04000FAE RID: 4014
	private Material SCMaterial;

	// Token: 0x04000FAF RID: 4015
	[Range(-0.02f, 0.02f)]
	public float Offset = 0.02f;
}
