using System;
using UnityEngine;

// Token: 0x020001CB RID: 459
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Light/Rainbow2")]
public class CameraFilterPack_Light_Rainbow2 : MonoBehaviour
{
	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0007527E File Offset: 0x0007347E
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

	// Token: 0x06000FD6 RID: 4054 RVA: 0x000752B2 File Offset: 0x000734B2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Light_Rainbow2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x000752D4 File Offset: 0x000734D4
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

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0007538A File Offset: 0x0007358A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012BB RID: 4795
	public Shader SCShader;

	// Token: 0x040012BC RID: 4796
	private float TimeX = 1f;

	// Token: 0x040012BD RID: 4797
	private Material SCMaterial;

	// Token: 0x040012BE RID: 4798
	[Range(0.01f, 5f)]
	public float Value = 1.5f;
}
