using System;
using UnityEngine;

// Token: 0x020001C9 RID: 457
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Thermal")]
public class CameraFilterPack_Gradients_Therma : MonoBehaviour
{
	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00074FCA File Offset: 0x000731CA
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

	// Token: 0x06000FCA RID: 4042 RVA: 0x00074FFE File Offset: 0x000731FE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00075020 File Offset: 0x00073220
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

	// Token: 0x06000FCC RID: 4044 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x000750EC File Offset: 0x000732EC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012B1 RID: 4785
	public Shader SCShader;

	// Token: 0x040012B2 RID: 4786
	private string ShaderName = "CameraFilterPack/Gradients_Therma";

	// Token: 0x040012B3 RID: 4787
	private float TimeX = 1f;

	// Token: 0x040012B4 RID: 4788
	private Material SCMaterial;

	// Token: 0x040012B5 RID: 4789
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012B6 RID: 4790
	[Range(0f, 1f)]
	public float Fade = 1f;
}
