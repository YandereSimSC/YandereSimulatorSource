using System;
using UnityEngine;

// Token: 0x020001AA RID: 426
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Hypno")]
public class CameraFilterPack_FX_Hypno : MonoBehaviour
{
	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06000F0F RID: 3855 RVA: 0x000719A2 File Offset: 0x0006FBA2
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

	// Token: 0x06000F10 RID: 3856 RVA: 0x000719D6 File Offset: 0x0006FBD6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Hypno");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x000719F8 File Offset: 0x0006FBF8
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
			this.material.SetFloat("_Value2", this.Red);
			this.material.SetFloat("_Value3", this.Green);
			this.material.SetFloat("_Value4", this.Blue);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x00071AF0 File Offset: 0x0006FCF0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011CC RID: 4556
	public Shader SCShader;

	// Token: 0x040011CD RID: 4557
	private float TimeX = 1f;

	// Token: 0x040011CE RID: 4558
	private Material SCMaterial;

	// Token: 0x040011CF RID: 4559
	[Range(0f, 1f)]
	public float Speed = 1f;

	// Token: 0x040011D0 RID: 4560
	[Range(-2f, 2f)]
	public float Red;

	// Token: 0x040011D1 RID: 4561
	[Range(-2f, 2f)]
	public float Green = 1f;

	// Token: 0x040011D2 RID: 4562
	[Range(-2f, 2f)]
	public float Blue = 1f;
}
