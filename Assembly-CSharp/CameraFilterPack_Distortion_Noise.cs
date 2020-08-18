using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Noise")]
public class CameraFilterPack_Distortion_Noise : MonoBehaviour
{
	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0006C317 File Offset: 0x0006A517
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

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0006C34B File Offset: 0x0006A54B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Noise");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0006C36C File Offset: 0x0006A56C
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x0006C41B File Offset: 0x0006A61B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001067 RID: 4199
	public Shader SCShader;

	// Token: 0x04001068 RID: 4200
	private float TimeX = 1f;

	// Token: 0x04001069 RID: 4201
	private Material SCMaterial;

	// Token: 0x0400106A RID: 4202
	[Range(0f, 3f)]
	public float Distortion = 1f;
}
