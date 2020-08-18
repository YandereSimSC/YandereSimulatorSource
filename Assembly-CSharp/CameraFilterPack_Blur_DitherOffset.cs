using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/DitherOffset")]
public class CameraFilterPack_Blur_DitherOffset : MonoBehaviour
{
	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00066E5F File Offset: 0x0006505F
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

	// Token: 0x06000C73 RID: 3187 RVA: 0x00066E93 File Offset: 0x00065093
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_DitherOffset");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x00066EB4 File Offset: 0x000650B4
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
			this.material.SetFloat("_Level", (float)this.Level);
			this.material.SetVector("_Distance", this.Distance);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x00066F86 File Offset: 0x00065186
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F2F RID: 3887
	public Shader SCShader;

	// Token: 0x04000F30 RID: 3888
	private float TimeX = 1f;

	// Token: 0x04000F31 RID: 3889
	private Material SCMaterial;

	// Token: 0x04000F32 RID: 3890
	[Range(1f, 16f)]
	public int Level = 4;

	// Token: 0x04000F33 RID: 3891
	public Vector2 Distance = new Vector2(30f, 0f);
}
