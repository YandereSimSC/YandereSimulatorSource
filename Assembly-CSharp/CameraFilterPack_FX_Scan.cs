using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Scan")]
public class CameraFilterPack_FX_Scan : MonoBehaviour
{
	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00071FD6 File Offset: 0x000701D6
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

	// Token: 0x06000F2E RID: 3886 RVA: 0x0007200A File Offset: 0x0007020A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Scan");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x0007202C File Offset: 0x0007022C
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
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00072124 File Offset: 0x00070324
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011E1 RID: 4577
	public Shader SCShader;

	// Token: 0x040011E2 RID: 4578
	private float TimeX = 1f;

	// Token: 0x040011E3 RID: 4579
	private Material SCMaterial;

	// Token: 0x040011E4 RID: 4580
	[Range(0.001f, 0.1f)]
	public float Size = 0.025f;

	// Token: 0x040011E5 RID: 4581
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x040011E6 RID: 4582
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x040011E7 RID: 4583
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
