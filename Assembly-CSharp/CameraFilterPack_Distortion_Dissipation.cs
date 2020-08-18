using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Dissipation")]
public class CameraFilterPack_Distortion_Dissipation : MonoBehaviour
{
	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0006B6F8 File Offset: 0x000698F8
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

	// Token: 0x06000D7D RID: 3453 RVA: 0x0006B72C File Offset: 0x0006992C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Dissipation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0006B750 File Offset: 0x00069950
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
			this.material.SetFloat("_Value", this.Dissipation);
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0006B848 File Offset: 0x00069A48
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001039 RID: 4153
	public Shader SCShader;

	// Token: 0x0400103A RID: 4154
	private float TimeX = 1f;

	// Token: 0x0400103B RID: 4155
	private Material SCMaterial;

	// Token: 0x0400103C RID: 4156
	[Range(0f, 2.99f)]
	public float Dissipation = 1f;

	// Token: 0x0400103D RID: 4157
	[Range(0f, 16f)]
	private float Colors = 11f;

	// Token: 0x0400103E RID: 4158
	[Range(-1f, 1f)]
	private float Green_Mod = 1f;

	// Token: 0x0400103F RID: 4159
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
