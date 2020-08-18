using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/EXTRA/Rotation")]
public class CameraFilterPack_EXTRA_Rotation : MonoBehaviour
{
	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0006F2B7 File Offset: 0x0006D4B7
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

	// Token: 0x06000E6D RID: 3693 RVA: 0x0006F2EB File Offset: 0x0006D4EB
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/EXTRA_Rotation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0006F30C File Offset: 0x0006D50C
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
			this.material.SetFloat("_Value", -this.Rotation);
			this.material.SetFloat("_Value2", this.PositionX);
			this.material.SetFloat("_Value3", this.PositionY);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0006F405 File Offset: 0x0006D605
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001132 RID: 4402
	public Shader SCShader;

	// Token: 0x04001133 RID: 4403
	private float TimeX = 1f;

	// Token: 0x04001134 RID: 4404
	private Material SCMaterial;

	// Token: 0x04001135 RID: 4405
	[Range(-360f, 360f)]
	public float Rotation;

	// Token: 0x04001136 RID: 4406
	[Range(-1f, 2f)]
	public float PositionX = 0.5f;

	// Token: 0x04001137 RID: 4407
	[Range(-1f, 2f)]
	public float PositionY = 0.5f;

	// Token: 0x04001138 RID: 4408
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
