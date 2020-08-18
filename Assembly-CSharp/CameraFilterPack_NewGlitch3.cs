using System;
using UnityEngine;

// Token: 0x020001D7 RID: 471
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch3")]
public class CameraFilterPack_NewGlitch3 : MonoBehaviour
{
	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06001039 RID: 4153 RVA: 0x0007731B File Offset: 0x0007551B
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

	// Token: 0x0600103A RID: 4154 RVA: 0x0007734F File Offset: 0x0007554F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x00077370 File Offset: 0x00075570
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
			this.material.SetFloat("RedFade", this._RedFade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0007743C File Offset: 0x0007563C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400131E RID: 4894
	public Shader SCShader;

	// Token: 0x0400131F RID: 4895
	private float TimeX = 1f;

	// Token: 0x04001320 RID: 4896
	private Material SCMaterial;

	// Token: 0x04001321 RID: 4897
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04001322 RID: 4898
	[Range(0f, 1f)]
	public float _RedFade = 1f;
}
