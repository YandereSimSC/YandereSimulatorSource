using System;
using UnityEngine;

// Token: 0x02000198 RID: 408
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Eyes 2")]
public class CameraFilterPack_EyesVision_2 : MonoBehaviour
{
	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0006FF2E File Offset: 0x0006E12E
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

	// Token: 0x06000EA4 RID: 3748 RVA: 0x0006FF62 File Offset: 0x0006E162
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_eyes_vision_2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/EyesVision_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x0006FF98 File Offset: 0x0006E198
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
			this.material.SetFloat("_Value", this._EyeWave);
			this.material.SetFloat("_Value2", this._EyeSpeed);
			this.material.SetFloat("_Value3", this._EyeMove);
			this.material.SetFloat("_Value4", this._EyeBlink);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00070079 File Offset: 0x0006E279
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001162 RID: 4450
	public Shader SCShader;

	// Token: 0x04001163 RID: 4451
	private float TimeX = 1f;

	// Token: 0x04001164 RID: 4452
	[Range(1f, 32f)]
	public float _EyeWave = 15f;

	// Token: 0x04001165 RID: 4453
	[Range(0f, 10f)]
	public float _EyeSpeed = 1f;

	// Token: 0x04001166 RID: 4454
	[Range(0f, 8f)]
	public float _EyeMove = 2f;

	// Token: 0x04001167 RID: 4455
	[Range(0f, 1f)]
	public float _EyeBlink = 1f;

	// Token: 0x04001168 RID: 4456
	private Material SCMaterial;

	// Token: 0x04001169 RID: 4457
	private Texture2D Texture2;
}
