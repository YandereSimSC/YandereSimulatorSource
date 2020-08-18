using System;
using UnityEngine;

// Token: 0x0200019B RID: 411
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Ascii")]
public class CameraFilterPack_FX_Ascii : MonoBehaviour
{
	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0007037B File Offset: 0x0006E57B
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

	// Token: 0x06000EB6 RID: 3766 RVA: 0x000703AF File Offset: 0x0006E5AF
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Ascii");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x000703D0 File Offset: 0x0006E5D0
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
			this.material.SetFloat("Value", this.Value);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0007049C File Offset: 0x0006E69C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001174 RID: 4468
	public Shader SCShader;

	// Token: 0x04001175 RID: 4469
	[Range(0f, 2f)]
	public float Value = 1f;

	// Token: 0x04001176 RID: 4470
	[Range(0.01f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001177 RID: 4471
	private float TimeX = 1f;

	// Token: 0x04001178 RID: 4472
	private Material SCMaterial;
}
