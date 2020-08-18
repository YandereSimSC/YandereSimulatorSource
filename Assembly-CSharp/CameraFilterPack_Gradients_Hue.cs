using System;
using UnityEngine;

// Token: 0x020001C4 RID: 452
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Hue")]
public class CameraFilterPack_Gradients_Hue : MonoBehaviour
{
	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0007489A File Offset: 0x00072A9A
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

	// Token: 0x06000FAC RID: 4012 RVA: 0x000748CE File Offset: 0x00072ACE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x000748F0 File Offset: 0x00072AF0
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

	// Token: 0x06000FAE RID: 4014 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x000749BC File Offset: 0x00072BBC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001293 RID: 4755
	public Shader SCShader;

	// Token: 0x04001294 RID: 4756
	private string ShaderName = "CameraFilterPack/Gradients_Hue";

	// Token: 0x04001295 RID: 4757
	private float TimeX = 1f;

	// Token: 0x04001296 RID: 4758
	private Material SCMaterial;

	// Token: 0x04001297 RID: 4759
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x04001298 RID: 4760
	[Range(0f, 1f)]
	public float Fade = 1f;
}
