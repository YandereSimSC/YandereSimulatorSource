using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Dithering2x2")]
public class CameraFilterPack_Blur_Dithering2x2 : MonoBehaviour
{
	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00066FCF File Offset: 0x000651CF
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

	// Token: 0x06000C79 RID: 3193 RVA: 0x00067003 File Offset: 0x00065203
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Dithering2x2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00067024 File Offset: 0x00065224
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
			this.material.SetFloat("_Level", (float)this.Level);
			this.material.SetVector("_Distance", this.Distance);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x000670F6 File Offset: 0x000652F6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F34 RID: 3892
	public Shader SCShader;

	// Token: 0x04000F35 RID: 3893
	private float TimeX = 1f;

	// Token: 0x04000F36 RID: 3894
	private Material SCMaterial;

	// Token: 0x04000F37 RID: 3895
	[Range(2f, 16f)]
	public int Level = 4;

	// Token: 0x04000F38 RID: 3896
	public Vector2 Distance = new Vector2(30f, 0f);
}
