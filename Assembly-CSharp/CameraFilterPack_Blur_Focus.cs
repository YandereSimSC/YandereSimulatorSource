using System;
using UnityEngine;

// Token: 0x0200013D RID: 317
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Focus")]
public class CameraFilterPack_Blur_Focus : MonoBehaviour
{
	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0006713F File Offset: 0x0006533F
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

	// Token: 0x06000C7F RID: 3199 RVA: 0x00067173 File Offset: 0x00065373
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Focus");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00067194 File Offset: 0x00065394
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
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			float value = Mathf.Round(this._Size / 0.2f) * 0.2f;
			this.material.SetFloat("_Size", value);
			this.material.SetFloat("_Circle", this._Eyes);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x00067298 File Offset: 0x00065498
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F39 RID: 3897
	public Shader SCShader;

	// Token: 0x04000F3A RID: 3898
	private float TimeX = 1f;

	// Token: 0x04000F3B RID: 3899
	private Material SCMaterial;

	// Token: 0x04000F3C RID: 3900
	[Range(-1f, 1f)]
	public float CenterX;

	// Token: 0x04000F3D RID: 3901
	[Range(-1f, 1f)]
	public float CenterY;

	// Token: 0x04000F3E RID: 3902
	[Range(0f, 10f)]
	public float _Size = 5f;

	// Token: 0x04000F3F RID: 3903
	[Range(0.12f, 64f)]
	public float _Eyes = 2f;
}
