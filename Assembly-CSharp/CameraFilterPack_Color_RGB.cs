using System;
using UnityEngine;

// Token: 0x02000154 RID: 340
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/RGB")]
public class CameraFilterPack_Color_RGB : MonoBehaviour
{
	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00069672 File Offset: 0x00067872
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

	// Token: 0x06000D09 RID: 3337 RVA: 0x000696A6 File Offset: 0x000678A6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_RGB");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x000696C8 File Offset: 0x000678C8
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
			this.material.SetColor("_ColorRGB", this.ColorRGB);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0006977E File Offset: 0x0006797E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FC6 RID: 4038
	public Shader SCShader;

	// Token: 0x04000FC7 RID: 4039
	private float TimeX = 1f;

	// Token: 0x04000FC8 RID: 4040
	private Material SCMaterial;

	// Token: 0x04000FC9 RID: 4041
	public Color ColorRGB = new Color(1f, 1f, 1f);
}
