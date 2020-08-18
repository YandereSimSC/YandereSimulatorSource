using System;
using UnityEngine;

// Token: 0x020001C7 RID: 455
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Stripe")]
public class CameraFilterPack_Gradients_Stripe : MonoBehaviour
{
	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00074CEA File Offset: 0x00072EEA
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

	// Token: 0x06000FBE RID: 4030 RVA: 0x00074D1E File Offset: 0x00072F1E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00074D40 File Offset: 0x00072F40
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

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x00074E0C File Offset: 0x0007300C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012A5 RID: 4773
	public Shader SCShader;

	// Token: 0x040012A6 RID: 4774
	private string ShaderName = "CameraFilterPack/Gradients_Stripe";

	// Token: 0x040012A7 RID: 4775
	private float TimeX = 1f;

	// Token: 0x040012A8 RID: 4776
	private Material SCMaterial;

	// Token: 0x040012A9 RID: 4777
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012AA RID: 4778
	[Range(0f, 1f)]
	public float Fade = 1f;
}
