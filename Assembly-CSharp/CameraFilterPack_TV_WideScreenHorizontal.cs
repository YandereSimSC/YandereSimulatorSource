using System;
using UnityEngine;

// Token: 0x02000213 RID: 531
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenHorizontal")]
public class CameraFilterPack_TV_WideScreenHorizontal : MonoBehaviour
{
	// Token: 0x17000333 RID: 819
	// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0007D945 File Offset: 0x0007BB45
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

	// Token: 0x060011A9 RID: 4521 RVA: 0x0007D979 File Offset: 0x0007BB79
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenHorizontal");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x0007D99C File Offset: 0x0007BB9C
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x0007DA94 File Offset: 0x0007BC94
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014A7 RID: 5287
	public Shader SCShader;

	// Token: 0x040014A8 RID: 5288
	private float TimeX = 1f;

	// Token: 0x040014A9 RID: 5289
	private Material SCMaterial;

	// Token: 0x040014AA RID: 5290
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x040014AB RID: 5291
	[Range(0.001f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x040014AC RID: 5292
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x040014AD RID: 5293
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
