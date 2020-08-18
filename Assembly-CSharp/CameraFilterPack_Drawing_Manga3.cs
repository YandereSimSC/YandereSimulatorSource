using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga3")]
public class CameraFilterPack_Drawing_Manga3 : MonoBehaviour
{
	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0006DEE6 File Offset: 0x0006C0E6
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

	// Token: 0x06000E25 RID: 3621 RVA: 0x0006DF1A File Offset: 0x0006C11A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0006DF3C File Offset: 0x0006C13C
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
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0006DFC2 File Offset: 0x0006C1C2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010D8 RID: 4312
	public Shader SCShader;

	// Token: 0x040010D9 RID: 4313
	private float TimeX = 1f;

	// Token: 0x040010DA RID: 4314
	private Material SCMaterial;

	// Token: 0x040010DB RID: 4315
	[Range(1f, 8f)]
	public float DotSize = 4.72f;
}
