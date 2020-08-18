using System;
using UnityEngine;

// Token: 0x020001D8 RID: 472
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch4")]
public class CameraFilterPack_NewGlitch4 : MonoBehaviour
{
	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x0600103F RID: 4159 RVA: 0x0007747F File Offset: 0x0007567F
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

	// Token: 0x06001040 RID: 4160 RVA: 0x000774B3 File Offset: 0x000756B3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch4");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x000774D4 File Offset: 0x000756D4
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
			this.material.SetFloat("Fade", this._Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x000775A0 File Offset: 0x000757A0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001323 RID: 4899
	public Shader SCShader;

	// Token: 0x04001324 RID: 4900
	private float TimeX = 1f;

	// Token: 0x04001325 RID: 4901
	private Material SCMaterial;

	// Token: 0x04001326 RID: 4902
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04001327 RID: 4903
	[Range(0f, 1f)]
	public float _Fade = 1f;
}
