using System;
using UnityEngine;

// Token: 0x02000202 RID: 514
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Noise")]
public class CameraFilterPack_TV_Noise : MonoBehaviour
{
	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06001142 RID: 4418 RVA: 0x0007C21E File Offset: 0x0007A41E
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

	// Token: 0x06001143 RID: 4419 RVA: 0x0007C252 File Offset: 0x0007A452
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Noise");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x0007C274 File Offset: 0x0007A474
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x0007C32A File Offset: 0x0007A52A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400144C RID: 5196
	public Shader SCShader;

	// Token: 0x0400144D RID: 5197
	private float TimeX = 1f;

	// Token: 0x0400144E RID: 5198
	private Material SCMaterial;

	// Token: 0x0400144F RID: 5199
	[Range(0.0001f, 1f)]
	public float Fade = 0.01f;
}
