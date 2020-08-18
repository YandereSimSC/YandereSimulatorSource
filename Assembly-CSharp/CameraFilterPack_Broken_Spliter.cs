using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Broken/Spliter")]
public class CameraFilterPack_Broken_Spliter : MonoBehaviour
{
	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000CCC RID: 3276 RVA: 0x000687D5 File Offset: 0x000669D5
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

	// Token: 0x06000CCD RID: 3277 RVA: 0x00068809 File Offset: 0x00066A09
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_Broken_Spliter");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0006882C File Offset: 0x00066A2C
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
			this.material.SetFloat("PosX", this._PosX);
			this.material.SetFloat("PosY", this._PosY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0006890E File Offset: 0x00066B0E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F8E RID: 3982
	public Shader SCShader;

	// Token: 0x04000F8F RID: 3983
	private float TimeX = 1f;

	// Token: 0x04000F90 RID: 3984
	private Material SCMaterial;

	// Token: 0x04000F91 RID: 3985
	[Range(0f, 1f)]
	private float __Speed = 1f;

	// Token: 0x04000F92 RID: 3986
	[Range(0f, 1f)]
	public float _PosX = 0.5f;

	// Token: 0x04000F93 RID: 3987
	[Range(0f, 1f)]
	public float _PosY = 0.5f;
}
