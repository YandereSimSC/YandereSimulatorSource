using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch6")]
public class CameraFilterPack_NewGlitch6 : MonoBehaviour
{
	// Token: 0x170002FA RID: 762
	// (get) Token: 0x0600104B RID: 4171 RVA: 0x000777F7 File Offset: 0x000759F7
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

	// Token: 0x0600104C RID: 4172 RVA: 0x0007782B File Offset: 0x00075A2B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch6");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x0007784C File Offset: 0x00075A4C
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
			this.material.SetFloat("_Speed", this.__Speed);
			this.material.SetFloat("FadeLight", this._FadeLight);
			this.material.SetFloat("FadeDark", this._FadeDark);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0007792E File Offset: 0x00075B2E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001332 RID: 4914
	public Shader SCShader;

	// Token: 0x04001333 RID: 4915
	private float TimeX = 1f;

	// Token: 0x04001334 RID: 4916
	private Material SCMaterial;

	// Token: 0x04001335 RID: 4917
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04001336 RID: 4918
	[Range(0f, 1f)]
	public float _FadeLight = 1f;

	// Token: 0x04001337 RID: 4919
	[Range(0f, 1f)]
	public float _FadeDark = 1f;
}
