using System;
using UnityEngine;

// Token: 0x020001C1 RID: 449
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Desert")]
public class CameraFilterPack_Gradients_Desert : MonoBehaviour
{
	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0007444A File Offset: 0x0007264A
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

	// Token: 0x06000F9A RID: 3994 RVA: 0x0007447E File Offset: 0x0007267E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x000744A0 File Offset: 0x000726A0
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

	// Token: 0x06000F9C RID: 3996 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0007456C File Offset: 0x0007276C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001281 RID: 4737
	public Shader SCShader;

	// Token: 0x04001282 RID: 4738
	private string ShaderName = "CameraFilterPack/Gradients_Desert";

	// Token: 0x04001283 RID: 4739
	private float TimeX = 1f;

	// Token: 0x04001284 RID: 4740
	private Material SCMaterial;

	// Token: 0x04001285 RID: 4741
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x04001286 RID: 4742
	[Range(0f, 1f)]
	public float Fade = 1f;
}
