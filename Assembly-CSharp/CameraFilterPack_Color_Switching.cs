using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Switching")]
public class CameraFilterPack_Color_Switching : MonoBehaviour
{
	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0006990A File Offset: 0x00067B0A
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

	// Token: 0x06000D15 RID: 3349 RVA: 0x0006993E File Offset: 0x00067B3E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Switching");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x00069960 File Offset: 0x00067B60
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
			this.material.SetFloat("_Distortion", (float)this.Color);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x00069A17 File Offset: 0x00067C17
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FCE RID: 4046
	public Shader SCShader;

	// Token: 0x04000FCF RID: 4047
	private float TimeX = 1f;

	// Token: 0x04000FD0 RID: 4048
	private Material SCMaterial;

	// Token: 0x04000FD1 RID: 4049
	[Range(0f, 5f)]
	public int Color = 1;
}
