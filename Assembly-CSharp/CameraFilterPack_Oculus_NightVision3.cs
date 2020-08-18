using System;
using UnityEngine;

// Token: 0x020001E3 RID: 483
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 3")]
public class CameraFilterPack_Oculus_NightVision3 : MonoBehaviour
{
	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06001085 RID: 4229 RVA: 0x00078C9C File Offset: 0x00076E9C
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

	// Token: 0x06001086 RID: 4230 RVA: 0x00078CD0 File Offset: 0x00076ED0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Oculus_NightVision3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x00078CF4 File Offset: 0x00076EF4
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
			this.material.SetFloat("_Greenness", this.Greenness);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00078DA3 File Offset: 0x00076FA3
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400137D RID: 4989
	public Shader SCShader;

	// Token: 0x0400137E RID: 4990
	private float TimeX = 1f;

	// Token: 0x0400137F RID: 4991
	private Material SCMaterial;

	// Token: 0x04001380 RID: 4992
	[Range(0.2f, 2f)]
	public float Greenness = 1f;
}
