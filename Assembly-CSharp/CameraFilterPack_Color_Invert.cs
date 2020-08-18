using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Invert")]
public class CameraFilterPack_Color_Invert : MonoBehaviour
{
	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000693EA File Offset: 0x000675EA
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

	// Token: 0x06000CFD RID: 3325 RVA: 0x0006941E File Offset: 0x0006761E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Invert");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x00069440 File Offset: 0x00067640
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
			this.material.SetFloat("_Fade", this._Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x000694F6 File Offset: 0x000676F6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FBE RID: 4030
	public Shader SCShader;

	// Token: 0x04000FBF RID: 4031
	private float TimeX = 1f;

	// Token: 0x04000FC0 RID: 4032
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x04000FC1 RID: 4033
	private Material SCMaterial;
}
