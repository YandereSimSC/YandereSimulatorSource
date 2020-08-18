using System;
using UnityEngine;

// Token: 0x02000185 RID: 389
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga5")]
public class CameraFilterPack_Drawing_Manga5 : MonoBehaviour
{
	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0006E10E File Offset: 0x0006C30E
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

	// Token: 0x06000E31 RID: 3633 RVA: 0x0006E142 File Offset: 0x0006C342
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga5");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0006E164 File Offset: 0x0006C364
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

	// Token: 0x06000E33 RID: 3635 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x0006E1EA File Offset: 0x0006C3EA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010E0 RID: 4320
	public Shader SCShader;

	// Token: 0x040010E1 RID: 4321
	private float TimeX = 1f;

	// Token: 0x040010E2 RID: 4322
	private Material SCMaterial;

	// Token: 0x040010E3 RID: 4323
	[Range(1f, 8f)]
	public float DotSize = 4.72f;
}
