using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Fire")]
public class CameraFilterPack_Gradients_FireGradient : MonoBehaviour
{
	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0007472A File Offset: 0x0007292A
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

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0007475E File Offset: 0x0007295E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x00074780 File Offset: 0x00072980
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

	// Token: 0x06000FA8 RID: 4008 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0007484C File Offset: 0x00072A4C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400128D RID: 4749
	public Shader SCShader;

	// Token: 0x0400128E RID: 4750
	private string ShaderName = "CameraFilterPack/Gradients_FireGradient";

	// Token: 0x0400128F RID: 4751
	private float TimeX = 1f;

	// Token: 0x04001290 RID: 4752
	private Material SCMaterial;

	// Token: 0x04001291 RID: 4753
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x04001292 RID: 4754
	[Range(0f, 1f)]
	public float Fade = 1f;
}
