using System;
using UnityEngine;

// Token: 0x02000163 RID: 355
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Convert/NormalMap")]
public class CameraFilterPack_Convert_Normal : MonoBehaviour
{
	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0006B172 File Offset: 0x00069372
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

	// Token: 0x06000D65 RID: 3429 RVA: 0x0006B1A6 File Offset: 0x000693A6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Convert_Normal");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0006B1C8 File Offset: 0x000693C8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_Heigh", this._Heigh);
			this.material.SetFloat("_Intervale", this._Intervale);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0006B224 File Offset: 0x00069424
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001022 RID: 4130
	public Shader SCShader;

	// Token: 0x04001023 RID: 4131
	[Range(0f, 0.5f)]
	public float _Heigh = 0.0125f;

	// Token: 0x04001024 RID: 4132
	[Range(0f, 0.25f)]
	public float _Intervale = 0.0025f;

	// Token: 0x04001025 RID: 4133
	private Material SCMaterial;
}
