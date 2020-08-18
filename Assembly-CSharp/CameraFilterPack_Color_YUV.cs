using System;
using UnityEngine;

// Token: 0x02000157 RID: 343
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Color_YUV")]
public class CameraFilterPack_Color_YUV : MonoBehaviour
{
	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00069A4B File Offset: 0x00067C4B
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

	// Token: 0x06000D1B RID: 3355 RVA: 0x00069A7F File Offset: 0x00067C7F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_YUV");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x00069AA0 File Offset: 0x00067CA0
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
			this.material.SetFloat("_Y", this._Y);
			this.material.SetFloat("_U", this._U);
			this.material.SetFloat("_V", this._V);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x00069B82 File Offset: 0x00067D82
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FD2 RID: 4050
	public Shader SCShader;

	// Token: 0x04000FD3 RID: 4051
	private float TimeX = 1f;

	// Token: 0x04000FD4 RID: 4052
	private Material SCMaterial;

	// Token: 0x04000FD5 RID: 4053
	[Range(-1f, 1f)]
	public float _Y;

	// Token: 0x04000FD6 RID: 4054
	[Range(-1f, 1f)]
	public float _U;

	// Token: 0x04000FD7 RID: 4055
	[Range(-1f, 1f)]
	public float _V;
}
