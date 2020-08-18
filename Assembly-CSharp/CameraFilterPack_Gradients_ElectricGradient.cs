using System;
using UnityEngine;

// Token: 0x020001C2 RID: 450
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Electric")]
public class CameraFilterPack_Gradients_ElectricGradient : MonoBehaviour
{
	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000745BA File Offset: 0x000727BA
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

	// Token: 0x06000FA0 RID: 4000 RVA: 0x000745EE File Offset: 0x000727EE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x00074610 File Offset: 0x00072810
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

	// Token: 0x06000FA2 RID: 4002 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x000746DC File Offset: 0x000728DC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001287 RID: 4743
	public Shader SCShader;

	// Token: 0x04001288 RID: 4744
	private string ShaderName = "CameraFilterPack/Gradients_ElectricGradient";

	// Token: 0x04001289 RID: 4745
	private float TimeX = 1f;

	// Token: 0x0400128A RID: 4746
	private Material SCMaterial;

	// Token: 0x0400128B RID: 4747
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x0400128C RID: 4748
	[Range(0f, 1f)]
	public float Fade = 1f;
}
