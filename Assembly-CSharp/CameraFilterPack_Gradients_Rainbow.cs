using System;
using UnityEngine;

// Token: 0x020001C6 RID: 454
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Rainbow")]
public class CameraFilterPack_Gradients_Rainbow : MonoBehaviour
{
	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00074B7A File Offset: 0x00072D7A
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

	// Token: 0x06000FB8 RID: 4024 RVA: 0x00074BAE File Offset: 0x00072DAE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00074BD0 File Offset: 0x00072DD0
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

	// Token: 0x06000FBA RID: 4026 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x00074C9C File Offset: 0x00072E9C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400129F RID: 4767
	public Shader SCShader;

	// Token: 0x040012A0 RID: 4768
	private string ShaderName = "CameraFilterPack/Gradients_Rainbow";

	// Token: 0x040012A1 RID: 4769
	private float TimeX = 1f;

	// Token: 0x040012A2 RID: 4770
	private Material SCMaterial;

	// Token: 0x040012A3 RID: 4771
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012A4 RID: 4772
	[Range(0f, 1f)]
	public float Fade = 1f;
}
