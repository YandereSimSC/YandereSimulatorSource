using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/BigFace")]
public class CameraFilterPack_Distortion_BigFace : MonoBehaviour
{
	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0006B40B File Offset: 0x0006960B
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

	// Token: 0x06000D71 RID: 3441 RVA: 0x0006B43F File Offset: 0x0006963F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_BigFace");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0006B460 File Offset: 0x00069660
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_Size", this._Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0006B52C File Offset: 0x0006972C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400102D RID: 4141
	public Shader SCShader;

	// Token: 0x0400102E RID: 4142
	private float TimeX = 6.5f;

	// Token: 0x0400102F RID: 4143
	private Material SCMaterial;

	// Token: 0x04001030 RID: 4144
	public float _Size = 5f;

	// Token: 0x04001031 RID: 4145
	[Range(2f, 10f)]
	public float Distortion = 2.5f;
}
