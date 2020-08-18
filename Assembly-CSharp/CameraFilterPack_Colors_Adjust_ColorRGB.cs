using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/ColorsAdjust/ColorRGB")]
public class CameraFilterPack_Colors_Adjust_ColorRGB : MonoBehaviour
{
	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00069BAF File Offset: 0x00067DAF
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

	// Token: 0x06000D21 RID: 3361 RVA: 0x00069BE3 File Offset: 0x00067DE3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_Adjust_ColorRGB");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x00069C04 File Offset: 0x00067E04
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
			this.material.SetFloat("_Value", this.Red);
			this.material.SetFloat("_Value2", this.Green);
			this.material.SetFloat("_Value3", this.Blue);
			this.material.SetFloat("_Value4", this.Brightness);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x00069CFC File Offset: 0x00067EFC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FD8 RID: 4056
	public Shader SCShader;

	// Token: 0x04000FD9 RID: 4057
	private float TimeX = 1f;

	// Token: 0x04000FDA RID: 4058
	private Material SCMaterial;

	// Token: 0x04000FDB RID: 4059
	[Range(-2f, 2f)]
	public float Red;

	// Token: 0x04000FDC RID: 4060
	[Range(-2f, 2f)]
	public float Green;

	// Token: 0x04000FDD RID: 4061
	[Range(-2f, 2f)]
	public float Blue;

	// Token: 0x04000FDE RID: 4062
	[Range(-1f, 1f)]
	public float Brightness;
}
