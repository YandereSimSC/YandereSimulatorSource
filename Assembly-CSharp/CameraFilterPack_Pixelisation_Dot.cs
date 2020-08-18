using System;
using UnityEngine;

// Token: 0x020001EA RID: 490
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/Dot")]
public class CameraFilterPack_Pixelisation_Dot : MonoBehaviour
{
	// Token: 0x1700030A RID: 778
	// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00079991 File Offset: 0x00077B91
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

	// Token: 0x060010B2 RID: 4274 RVA: 0x000799C5 File Offset: 0x00077BC5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_Dot");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x000799E8 File Offset: 0x00077BE8
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
			this.material.SetFloat("_Value2", this.LightBackGround);
			this.material.SetFloat("_Value3", this.Speed);
			this.material.SetFloat("_Value4", this.Size2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x00079AE0 File Offset: 0x00077CE0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013B1 RID: 5041
	public Shader SCShader;

	// Token: 0x040013B2 RID: 5042
	private float TimeX = 1f;

	// Token: 0x040013B3 RID: 5043
	private Material SCMaterial;

	// Token: 0x040013B4 RID: 5044
	[Range(0.0001f, 0.5f)]
	public float Size = 0.005f;

	// Token: 0x040013B5 RID: 5045
	[Range(0f, 1f)]
	public float LightBackGround = 0.3f;

	// Token: 0x040013B6 RID: 5046
	[Range(0f, 10f)]
	private float Speed = 1f;

	// Token: 0x040013B7 RID: 5047
	[Range(0f, 10f)]
	private float Size2 = 1f;
}
