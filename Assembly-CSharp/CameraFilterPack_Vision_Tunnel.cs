using System;
using UnityEngine;

// Token: 0x02000221 RID: 545
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Tunnel")]
public class CameraFilterPack_Vision_Tunnel : MonoBehaviour
{
	// Token: 0x17000341 RID: 833
	// (get) Token: 0x060011FC RID: 4604 RVA: 0x0007F274 File Offset: 0x0007D474
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

	// Token: 0x060011FD RID: 4605 RVA: 0x0007F2A8 File Offset: 0x0007D4A8
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Tunnel");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x0007F2CC File Offset: 0x0007D4CC
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
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x0007F3C4 File Offset: 0x0007D5C4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001515 RID: 5397
	public Shader SCShader;

	// Token: 0x04001516 RID: 5398
	private float TimeX = 1f;

	// Token: 0x04001517 RID: 5399
	private Material SCMaterial;

	// Token: 0x04001518 RID: 5400
	[Range(0f, 1f)]
	public float Value = 0.6f;

	// Token: 0x04001519 RID: 5401
	[Range(0f, 1f)]
	public float Value2 = 0.4f;

	// Token: 0x0400151A RID: 5402
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x0400151B RID: 5403
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
