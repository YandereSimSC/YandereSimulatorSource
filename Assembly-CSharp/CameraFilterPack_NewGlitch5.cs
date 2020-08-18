using System;
using UnityEngine;

// Token: 0x020001D9 RID: 473
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch5")]
public class CameraFilterPack_NewGlitch5 : MonoBehaviour
{
	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06001045 RID: 4165 RVA: 0x000775E3 File Offset: 0x000757E3
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

	// Token: 0x06001046 RID: 4166 RVA: 0x00077617 File Offset: 0x00075817
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch5");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x00077638 File Offset: 0x00075838
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
			this.material.SetFloat("Parasite", this._Parasite);
			this.material.SetFloat("ZoomX", this._ZoomX);
			this.material.SetFloat("ZoomY", this._ZoomY);
			this.material.SetFloat("PosX", this._PosX);
			this.material.SetFloat("PosY", this._PosY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x00077772 File Offset: 0x00075972
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001328 RID: 4904
	public Shader SCShader;

	// Token: 0x04001329 RID: 4905
	private float TimeX = 1f;

	// Token: 0x0400132A RID: 4906
	private Material SCMaterial;

	// Token: 0x0400132B RID: 4907
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x0400132C RID: 4908
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x0400132D RID: 4909
	[Range(0f, 1f)]
	public float _Parasite = 1f;

	// Token: 0x0400132E RID: 4910
	[Range(0f, 0f)]
	public float _ZoomX = 1f;

	// Token: 0x0400132F RID: 4911
	[Range(0f, 0f)]
	public float _ZoomY = 1f;

	// Token: 0x04001330 RID: 4912
	[Range(0f, 0f)]
	public float _PosX = 1f;

	// Token: 0x04001331 RID: 4913
	[Range(0f, 0f)]
	public float _PosY = 1f;
}
