using System;
using UnityEngine;

// Token: 0x020001AD RID: 429
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Plasma")]
public class CameraFilterPack_FX_Plasma : MonoBehaviour
{
	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00071D7E File Offset: 0x0006FF7E
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

	// Token: 0x06000F22 RID: 3874 RVA: 0x00071DB2 File Offset: 0x0006FFB2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Plasma");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x00071DD4 File Offset: 0x0006FFD4
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

	// Token: 0x06000F24 RID: 3876 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x00071E8A File Offset: 0x0007008A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011D9 RID: 4569
	public Shader SCShader;

	// Token: 0x040011DA RID: 4570
	private float TimeX = 1f;

	// Token: 0x040011DB RID: 4571
	private Material SCMaterial;

	// Token: 0x040011DC RID: 4572
	[Range(0f, 20f)]
	private float Value = 6f;
}
