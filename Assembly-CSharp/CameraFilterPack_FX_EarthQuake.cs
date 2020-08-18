using System;
using UnityEngine;

// Token: 0x020001A2 RID: 418
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Earth Quake")]
public class CameraFilterPack_FX_EarthQuake : MonoBehaviour
{
	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00070F75 File Offset: 0x0006F175
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

	// Token: 0x06000EE0 RID: 3808 RVA: 0x00070FA9 File Offset: 0x0006F1A9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_EarthQuake");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00070FCC File Offset: 0x0006F1CC
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.X);
			this.material.SetFloat("_Value3", this.Y);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x000710C4 File Offset: 0x0006F2C4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011A9 RID: 4521
	public Shader SCShader;

	// Token: 0x040011AA RID: 4522
	private float TimeX = 1f;

	// Token: 0x040011AB RID: 4523
	private Material SCMaterial;

	// Token: 0x040011AC RID: 4524
	[Range(0f, 100f)]
	public float Speed = 15f;

	// Token: 0x040011AD RID: 4525
	[Range(0f, 0.2f)]
	public float X = 0.008f;

	// Token: 0x040011AE RID: 4526
	[Range(0f, 0.2f)]
	public float Y = 0.008f;

	// Token: 0x040011AF RID: 4527
	[Range(0f, 0.2f)]
	private float Value4 = 1f;
}
