using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 1")]
public class CameraFilterPack_Oculus_NightVision1 : MonoBehaviour
{
	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06001077 RID: 4215 RVA: 0x00078831 File Offset: 0x00076A31
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

	// Token: 0x06001078 RID: 4216 RVA: 0x00078865 File Offset: 0x00076A65
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Oculus_NightVision1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00078888 File Offset: 0x00076A88
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Vignette", this.Vignette);
			this.material.SetFloat("_Linecount", this.Linecount);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0007896A File Offset: 0x00076B6A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001371 RID: 4977
	public Shader SCShader;

	// Token: 0x04001372 RID: 4978
	private float TimeX = 1f;

	// Token: 0x04001373 RID: 4979
	private float Distortion = 1f;

	// Token: 0x04001374 RID: 4980
	private Material SCMaterial;

	// Token: 0x04001375 RID: 4981
	[Range(0f, 100f)]
	public float Vignette = 1.3f;

	// Token: 0x04001376 RID: 4982
	[Range(1f, 150f)]
	public float Linecount = 90f;
}
