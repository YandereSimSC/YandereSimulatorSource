using System;
using UnityEngine;

// Token: 0x02000203 RID: 515
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Old")]
public class CameraFilterPack_TV_Old : MonoBehaviour
{
	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06001148 RID: 4424 RVA: 0x0007C362 File Offset: 0x0007A562
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

	// Token: 0x06001149 RID: 4425 RVA: 0x0007C396 File Offset: 0x0007A596
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Old");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x0007C3B8 File Offset: 0x0007A5B8
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

	// Token: 0x0600114B RID: 4427 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0007C43E File Offset: 0x0007A63E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001450 RID: 5200
	public Shader SCShader;

	// Token: 0x04001451 RID: 5201
	private float TimeX = 1f;

	// Token: 0x04001452 RID: 5202
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001453 RID: 5203
	private Material SCMaterial;
}
