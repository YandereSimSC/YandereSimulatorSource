using System;
using UnityEngine;

// Token: 0x0200019F RID: 415
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Dot_Circle")]
public class CameraFilterPack_FX_Dot_Circle : MonoBehaviour
{
	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00070A30 File Offset: 0x0006EC30
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

	// Token: 0x06000ECE RID: 3790 RVA: 0x00070A64 File Offset: 0x0006EC64
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Dot_Circle");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x00070A88 File Offset: 0x0006EC88
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Value", this.Value);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x00070B3E File Offset: 0x0006ED3E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001190 RID: 4496
	public Shader SCShader;

	// Token: 0x04001191 RID: 4497
	private float TimeX = 1f;

	// Token: 0x04001192 RID: 4498
	private Material SCMaterial;

	// Token: 0x04001193 RID: 4499
	[Range(4f, 32f)]
	public float Value = 7f;
}
