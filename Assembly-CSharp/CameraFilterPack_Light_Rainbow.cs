using System;
using UnityEngine;

// Token: 0x020001CA RID: 458
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Light/Rainbow")]
public class CameraFilterPack_Light_Rainbow : MonoBehaviour
{
	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0007513A File Offset: 0x0007333A
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

	// Token: 0x06000FD0 RID: 4048 RVA: 0x0007516E File Offset: 0x0007336E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Light_Rainbow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00075190 File Offset: 0x00073390
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00075246 File Offset: 0x00073446
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012B7 RID: 4791
	public Shader SCShader;

	// Token: 0x040012B8 RID: 4792
	private float TimeX = 1f;

	// Token: 0x040012B9 RID: 4793
	private Material SCMaterial;

	// Token: 0x040012BA RID: 4794
	[Range(0.01f, 5f)]
	public float Value = 1.5f;
}
