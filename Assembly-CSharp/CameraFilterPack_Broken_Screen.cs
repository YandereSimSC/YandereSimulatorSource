using System;
using UnityEngine;

// Token: 0x02000148 RID: 328
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Broken/Broken_Screen")]
public class CameraFilterPack_Broken_Screen : MonoBehaviour
{
	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0006849D File Offset: 0x0006669D
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

	// Token: 0x06000CC1 RID: 3265 RVA: 0x000684D1 File Offset: 0x000666D1
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Broken_Screen1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Broken_Screen");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00068508 File Offset: 0x00066708
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
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_Shadow", this.Shadow);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x000685BD File Offset: 0x000667BD
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F80 RID: 3968
	public Shader SCShader;

	// Token: 0x04000F81 RID: 3969
	private float TimeX = 1f;

	// Token: 0x04000F82 RID: 3970
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000F83 RID: 3971
	[Range(-1f, 1f)]
	public float Shadow = 1f;

	// Token: 0x04000F84 RID: 3972
	private Material SCMaterial;

	// Token: 0x04000F85 RID: 3973
	private Texture2D Texture2;
}
