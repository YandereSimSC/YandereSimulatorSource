using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Film/Grain")]
public class CameraFilterPack_Film_Grain : MonoBehaviour
{
	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06000F51 RID: 3921 RVA: 0x000728E9 File Offset: 0x00070AE9
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

	// Token: 0x06000F52 RID: 3922 RVA: 0x0007291D File Offset: 0x00070B1D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Film_Grain");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x00072940 File Offset: 0x00070B40
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

	// Token: 0x06000F54 RID: 3924 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x000729F6 File Offset: 0x00070BF6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001203 RID: 4611
	public Shader SCShader;

	// Token: 0x04001204 RID: 4612
	private float TimeX = 1f;

	// Token: 0x04001205 RID: 4613
	private Material SCMaterial;

	// Token: 0x04001206 RID: 4614
	[Range(-64f, 64f)]
	public float Value = 32f;
}
