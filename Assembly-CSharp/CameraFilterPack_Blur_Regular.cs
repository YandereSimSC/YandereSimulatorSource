using System;
using UnityEngine;

// Token: 0x02000143 RID: 323
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Regular")]
public class CameraFilterPack_Blur_Regular : MonoBehaviour
{
	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00067A89 File Offset: 0x00065C89
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

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00067ABD File Offset: 0x00065CBD
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Regular");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00067AE0 File Offset: 0x00065CE0
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

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00067BB2 File Offset: 0x00065DB2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F5D RID: 3933
	public Shader SCShader;

	// Token: 0x04000F5E RID: 3934
	private float TimeX = 1f;

	// Token: 0x04000F5F RID: 3935
	private Material SCMaterial;

	// Token: 0x04000F60 RID: 3936
	[Range(1f, 16f)]
	public int Level = 4;

	// Token: 0x04000F61 RID: 3937
	public Vector2 Distance = new Vector2(30f, 0f);
}
