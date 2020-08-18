using System;
using UnityEngine;

// Token: 0x020001AE RID: 430
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Psycho")]
public class CameraFilterPack_FX_Psycho : MonoBehaviour
{
	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00071EC2 File Offset: 0x000700C2
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

	// Token: 0x06000F28 RID: 3880 RVA: 0x00071EF6 File Offset: 0x000700F6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Psycho");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x00071F18 File Offset: 0x00070118
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
			this.material.SetFloat("_Distortion", this.Distortion);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x00071F9E File Offset: 0x0007019E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011DD RID: 4573
	public Shader SCShader;

	// Token: 0x040011DE RID: 4574
	private Material SCMaterial;

	// Token: 0x040011DF RID: 4575
	private float TimeX = 1f;

	// Token: 0x040011E0 RID: 4576
	[Range(0f, 1f)]
	public float Distortion = 1f;
}
