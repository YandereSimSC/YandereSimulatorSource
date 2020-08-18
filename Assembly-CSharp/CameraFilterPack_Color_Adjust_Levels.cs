using System;
using UnityEngine;

// Token: 0x0200014C RID: 332
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Levels")]
public class CameraFilterPack_Color_Adjust_Levels : MonoBehaviour
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00068B73 File Offset: 0x00066D73
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

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00068BA7 File Offset: 0x00066DA7
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Levels");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00068BC8 File Offset: 0x00066DC8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("levelMinimum", this.levelMinimum);
			this.material.SetFloat("levelMiddle", this.levelMiddle);
			this.material.SetFloat("levelMaximum", this.levelMaximum);
			this.material.SetFloat("minOutput", this.minOutput);
			this.material.SetFloat("maxOutput", this.maxOutput);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00068CC0 File Offset: 0x00066EC0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F9E RID: 3998
	public Shader SCShader;

	// Token: 0x04000F9F RID: 3999
	private float TimeX = 1f;

	// Token: 0x04000FA0 RID: 4000
	private Material SCMaterial;

	// Token: 0x04000FA1 RID: 4001
	[Range(0f, 1f)]
	public float levelMinimum;

	// Token: 0x04000FA2 RID: 4002
	[Range(0f, 1f)]
	public float levelMiddle = 0.5f;

	// Token: 0x04000FA3 RID: 4003
	[Range(0f, 1f)]
	public float levelMaximum = 1f;

	// Token: 0x04000FA4 RID: 4004
	[Range(0f, 1f)]
	public float minOutput;

	// Token: 0x04000FA5 RID: 4005
	[Range(0f, 1f)]
	public float maxOutput = 1f;
}
