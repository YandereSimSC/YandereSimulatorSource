using System;
using UnityEngine;

// Token: 0x02000149 RID: 329
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Broken/Simple")]
public class CameraFilterPack_Broken_Simple : MonoBehaviour
{
	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00068600 File Offset: 0x00066800
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

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00068634 File Offset: 0x00066834
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_Broken_Simple");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00068658 File Offset: 0x00066858
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
			this.material.SetFloat("Broke1", this._Broke1);
			this.material.SetFloat("Broke2", this._Broke2);
			this.material.SetFloat("PosX", this._PosX);
			this.material.SetFloat("PosY", this._PosY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00068766 File Offset: 0x00066966
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F86 RID: 3974
	public Shader SCShader;

	// Token: 0x04000F87 RID: 3975
	private float TimeX = 1f;

	// Token: 0x04000F88 RID: 3976
	private Material SCMaterial;

	// Token: 0x04000F89 RID: 3977
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04000F8A RID: 3978
	[Range(0f, 1f)]
	public float _Broke1 = 1f;

	// Token: 0x04000F8B RID: 3979
	[Range(0f, 1f)]
	public float _Broke2 = 1f;

	// Token: 0x04000F8C RID: 3980
	[Range(0f, 1f)]
	public float _PosX = 0.5f;

	// Token: 0x04000F8D RID: 3981
	[Range(0f, 1f)]
	public float _PosY = 0.5f;
}
