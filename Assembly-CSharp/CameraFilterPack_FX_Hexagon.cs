using System;
using UnityEngine;

// Token: 0x020001A8 RID: 424
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Hexagon")]
public class CameraFilterPack_FX_Hexagon : MonoBehaviour
{
	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0007173E File Offset: 0x0006F93E
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

	// Token: 0x06000F04 RID: 3844 RVA: 0x00071772 File Offset: 0x0006F972
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Hexagon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00071794 File Offset: 0x0006F994
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x00071831 File Offset: 0x0006FA31
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011C5 RID: 4549
	public Shader SCShader;

	// Token: 0x040011C6 RID: 4550
	private float TimeX = 1f;

	// Token: 0x040011C7 RID: 4551
	private Material SCMaterial;
}
