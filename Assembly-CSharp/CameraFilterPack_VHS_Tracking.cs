using System;
using UnityEngine;

// Token: 0x02000215 RID: 533
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/Tracking")]
public class CameraFilterPack_VHS_Tracking : MonoBehaviour
{
	// Token: 0x17000335 RID: 821
	// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0007DC95 File Offset: 0x0007BE95
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

	// Token: 0x060011B5 RID: 4533 RVA: 0x0007DCC9 File Offset: 0x0007BEC9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/VHS_Tracking");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x0007DCEC File Offset: 0x0007BEEC
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
			this.material.SetFloat("_Value", this.Tracking);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x0007DDA2 File Offset: 0x0007BFA2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014B5 RID: 5301
	public Shader SCShader;

	// Token: 0x040014B6 RID: 5302
	private float TimeX = 1f;

	// Token: 0x040014B7 RID: 5303
	private Material SCMaterial;

	// Token: 0x040014B8 RID: 5304
	[Range(0f, 2f)]
	public float Tracking = 1f;
}
