using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Ansi")]
public class CameraFilterPack_Gradients_Ansi : MonoBehaviour
{
	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000742D8 File Offset: 0x000724D8
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

	// Token: 0x06000F94 RID: 3988 RVA: 0x0007430C File Offset: 0x0007250C
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x00074330 File Offset: 0x00072530
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

	// Token: 0x06000F96 RID: 3990 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x000743FC File Offset: 0x000725FC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400127B RID: 4731
	public Shader SCShader;

	// Token: 0x0400127C RID: 4732
	private string ShaderName = "CameraFilterPack/Gradients_Ansi";

	// Token: 0x0400127D RID: 4733
	private float TimeX = 1f;

	// Token: 0x0400127E RID: 4734
	private Material SCMaterial;

	// Token: 0x0400127F RID: 4735
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x04001280 RID: 4736
	[Range(0f, 1f)]
	public float Fade = 1f;
}
