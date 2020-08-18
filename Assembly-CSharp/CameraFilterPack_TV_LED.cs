using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/LED")]
public class CameraFilterPack_TV_LED : MonoBehaviour
{
	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06001136 RID: 4406 RVA: 0x0007BF61 File Offset: 0x0007A161
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

	// Token: 0x06001137 RID: 4407 RVA: 0x0007BF95 File Offset: 0x0007A195
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_LED");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0007BFB8 File Offset: 0x0007A1B8
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
			this.material.SetFloat("_Size", (float)this.Size);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0007C09B File Offset: 0x0007A29B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001442 RID: 5186
	public Shader SCShader;

	// Token: 0x04001443 RID: 5187
	private float TimeX = 1f;

	// Token: 0x04001444 RID: 5188
	[Range(0f, 1f)]
	public float Fade;

	// Token: 0x04001445 RID: 5189
	[Range(1f, 10f)]
	private float Distortion = 1f;

	// Token: 0x04001446 RID: 5190
	[Range(1f, 15f)]
	public int Size = 5;

	// Token: 0x04001447 RID: 5191
	private Material SCMaterial;
}
