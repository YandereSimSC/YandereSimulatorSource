using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Hexagon_Black")]
public class CameraFilterPack_FX_Hexagon_Black : MonoBehaviour
{
	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0007185E File Offset: 0x0006FA5E
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

	// Token: 0x06000F0A RID: 3850 RVA: 0x00071892 File Offset: 0x0006FA92
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Hexagon_Black");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x000718B4 File Offset: 0x0006FAB4
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

	// Token: 0x06000F0C RID: 3852 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0007196A File Offset: 0x0006FB6A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011C8 RID: 4552
	public Shader SCShader;

	// Token: 0x040011C9 RID: 4553
	private float TimeX = 1f;

	// Token: 0x040011CA RID: 4554
	private Material SCMaterial;

	// Token: 0x040011CB RID: 4555
	[Range(0.2f, 10f)]
	public float Value = 1f;
}
