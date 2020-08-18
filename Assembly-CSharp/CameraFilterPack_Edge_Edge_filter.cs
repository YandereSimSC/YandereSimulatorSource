using System;
using UnityEngine;

// Token: 0x02000192 RID: 402
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Edge/Edge_filter")]
public class CameraFilterPack_Edge_Edge_filter : MonoBehaviour
{
	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0006F773 File Offset: 0x0006D973
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

	// Token: 0x06000E80 RID: 3712 RVA: 0x0006F7A7 File Offset: 0x0006D9A7
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Edge_Edge_filter");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0006F7C8 File Offset: 0x0006D9C8
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
			this.material.SetFloat("_RedAmplifier", this.RedAmplifier);
			this.material.SetFloat("_GreenAmplifier", this.GreenAmplifier);
			this.material.SetFloat("_BlueAmplifier", this.BlueAmplifier);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0006F8A3 File Offset: 0x0006DAA3
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001146 RID: 4422
	public Shader SCShader;

	// Token: 0x04001147 RID: 4423
	private float TimeX = 1f;

	// Token: 0x04001148 RID: 4424
	private Material SCMaterial;

	// Token: 0x04001149 RID: 4425
	[Range(0f, 10f)]
	public float RedAmplifier;

	// Token: 0x0400114A RID: 4426
	[Range(0f, 10f)]
	public float GreenAmplifier = 2f;

	// Token: 0x0400114B RID: 4427
	[Range(0f, 10f)]
	public float BlueAmplifier;
}
