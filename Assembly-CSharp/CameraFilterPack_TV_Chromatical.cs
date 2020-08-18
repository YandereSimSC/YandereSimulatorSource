using System;
using UnityEngine;

// Token: 0x020001FB RID: 507
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Chromatical")]
public class CameraFilterPack_TV_Chromatical : MonoBehaviour
{
	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06001118 RID: 4376 RVA: 0x0007B829 File Offset: 0x00079A29
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

	// Token: 0x06001119 RID: 4377 RVA: 0x0007B85D File Offset: 0x00079A5D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Chromatical");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0007B880 File Offset: 0x00079A80
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime * 2f;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("Intensity", this.Intensity);
			this.material.SetFloat("Speed", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x0007B961 File Offset: 0x00079B61
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001426 RID: 5158
	public Shader SCShader;

	// Token: 0x04001427 RID: 5159
	private float TimeX = 1f;

	// Token: 0x04001428 RID: 5160
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001429 RID: 5161
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x0400142A RID: 5162
	[Range(0f, 3f)]
	public float Speed = 1f;

	// Token: 0x0400142B RID: 5163
	private Material SCMaterial;
}
