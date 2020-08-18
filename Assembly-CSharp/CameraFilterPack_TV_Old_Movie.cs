using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Old_Movie")]
public class CameraFilterPack_TV_Old_Movie : MonoBehaviour
{
	// Token: 0x17000324 RID: 804
	// (get) Token: 0x0600114E RID: 4430 RVA: 0x0007C476 File Offset: 0x0007A676
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

	// Token: 0x0600114F RID: 4431 RVA: 0x0007C4AA File Offset: 0x0007A6AA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Old_Movie");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0007C4CC File Offset: 0x0007A6CC
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

	// Token: 0x06001151 RID: 4433 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0007C552 File Offset: 0x0007A752
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001454 RID: 5204
	public Shader SCShader;

	// Token: 0x04001455 RID: 5205
	private float TimeX = 1f;

	// Token: 0x04001456 RID: 5206
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001457 RID: 5207
	private Material SCMaterial;
}
