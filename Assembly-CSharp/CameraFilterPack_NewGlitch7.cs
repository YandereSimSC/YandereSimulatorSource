using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Glitch Drawing")]
public class CameraFilterPack_NewGlitch7 : MonoBehaviour
{
	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06001051 RID: 4177 RVA: 0x0007797C File Offset: 0x00075B7C
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

	// Token: 0x06001052 RID: 4178 RVA: 0x000779B0 File Offset: 0x00075BB0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch7");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x000779D4 File Offset: 0x00075BD4
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
			this.material.SetFloat("LightMin", this._LightMin);
			this.material.SetFloat("LightMax", this._LightMax);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x00077AB6 File Offset: 0x00075CB6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001338 RID: 4920
	public Shader SCShader;

	// Token: 0x04001339 RID: 4921
	private float TimeX = 1f;

	// Token: 0x0400133A RID: 4922
	private Material SCMaterial;

	// Token: 0x0400133B RID: 4923
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x0400133C RID: 4924
	[Range(0f, 1f)]
	public float _LightMin;

	// Token: 0x0400133D RID: 4925
	[Range(0f, 1f)]
	public float _LightMax = 1f;
}
