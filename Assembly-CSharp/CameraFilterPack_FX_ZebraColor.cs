using System;
using UnityEngine;

// Token: 0x020001B2 RID: 434
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/ZebraColor")]
public class CameraFilterPack_FX_ZebraColor : MonoBehaviour
{
	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000F3F RID: 3903 RVA: 0x000724DD File Offset: 0x000706DD
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

	// Token: 0x06000F40 RID: 3904 RVA: 0x00072511 File Offset: 0x00070711
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_ZebraColor");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00072534 File Offset: 0x00070734
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

	// Token: 0x06000F42 RID: 3906 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x000725EA File Offset: 0x000707EA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011F5 RID: 4597
	public Shader SCShader;

	// Token: 0x040011F6 RID: 4598
	private float TimeX = 1f;

	// Token: 0x040011F7 RID: 4599
	private Material SCMaterial;

	// Token: 0x040011F8 RID: 4600
	[Range(1f, 10f)]
	public float Value = 3f;
}
