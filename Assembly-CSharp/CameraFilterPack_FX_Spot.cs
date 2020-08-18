using System;
using UnityEngine;

// Token: 0x020001B1 RID: 433
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Spot")]
public class CameraFilterPack_FX_Spot : MonoBehaviour
{
	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0007234F File Offset: 0x0007054F
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

	// Token: 0x06000F3A RID: 3898 RVA: 0x00072383 File Offset: 0x00070583
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Spot");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000723A4 File Offset: 0x000705A4
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
			this.material.SetFloat("_PositionX", this.center.x);
			this.material.SetFloat("_PositionY", this.center.y);
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00072490 File Offset: 0x00070690
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011F0 RID: 4592
	public Shader SCShader;

	// Token: 0x040011F1 RID: 4593
	private float TimeX = 1f;

	// Token: 0x040011F2 RID: 4594
	private Material SCMaterial;

	// Token: 0x040011F3 RID: 4595
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x040011F4 RID: 4596
	public float Radius = 0.2f;
}
