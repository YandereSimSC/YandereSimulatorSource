using System;
using UnityEngine;

// Token: 0x020001C5 RID: 453
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Neon")]
public class CameraFilterPack_Gradients_NeonGradient : MonoBehaviour
{
	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00074A0A File Offset: 0x00072C0A
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

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00074A3E File Offset: 0x00072C3E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00074A60 File Offset: 0x00072C60
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
			this.material.SetFloat("_Value", this.Switch);
			this.material.SetFloat("_Value2", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x00074B2C File Offset: 0x00072D2C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001299 RID: 4761
	public Shader SCShader;

	// Token: 0x0400129A RID: 4762
	private string ShaderName = "CameraFilterPack/Gradients_NeonGradient";

	// Token: 0x0400129B RID: 4763
	private float TimeX = 1f;

	// Token: 0x0400129C RID: 4764
	private Material SCMaterial;

	// Token: 0x0400129D RID: 4765
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x0400129E RID: 4766
	[Range(0f, 1f)]
	public float Fade = 1f;
}
