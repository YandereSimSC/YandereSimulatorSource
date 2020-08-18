using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Sepia")]
public class CameraFilterPack_Color_Sepia : MonoBehaviour
{
	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000697C5 File Offset: 0x000679C5
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

	// Token: 0x06000D0F RID: 3343 RVA: 0x000697F9 File Offset: 0x000679F9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Sepia");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0006981C File Offset: 0x00067A1C
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

	// Token: 0x06000D11 RID: 3345 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x000698D2 File Offset: 0x00067AD2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FCA RID: 4042
	public Shader SCShader;

	// Token: 0x04000FCB RID: 4043
	private float TimeX = 1f;

	// Token: 0x04000FCC RID: 4044
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x04000FCD RID: 4045
	private Material SCMaterial;
}
