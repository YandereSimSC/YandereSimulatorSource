using System;
using UnityEngine;

// Token: 0x02000210 RID: 528
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Vintage")]
public class CameraFilterPack_TV_Vintage : MonoBehaviour
{
	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06001196 RID: 4502 RVA: 0x0007D4E2 File Offset: 0x0007B6E2
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

	// Token: 0x06001197 RID: 4503 RVA: 0x0007D516 File Offset: 0x0007B716
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Vintage");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0007D538 File Offset: 0x0007B738
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
			this.material.SetFloat("_Distortion", this.Distortion);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x0007D5BE File Offset: 0x0007B7BE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001495 RID: 5269
	public Shader SCShader;

	// Token: 0x04001496 RID: 5270
	private float TimeX = 1f;

	// Token: 0x04001497 RID: 5271
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001498 RID: 5272
	private Material SCMaterial;
}
