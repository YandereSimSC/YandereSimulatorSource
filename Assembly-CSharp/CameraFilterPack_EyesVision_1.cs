using System;
using UnityEngine;

// Token: 0x02000197 RID: 407
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Eyes 1")]
public class CameraFilterPack_EyesVision_1 : MonoBehaviour
{
	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0006FD8A File Offset: 0x0006DF8A
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

	// Token: 0x06000E9E RID: 3742 RVA: 0x0006FDBE File Offset: 0x0006DFBE
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_eyes_vision_1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/EyesVision_1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0006FDF4 File Offset: 0x0006DFF4
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

	// Token: 0x06000EA0 RID: 3744 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0006FED5 File Offset: 0x0006E0D5
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400115A RID: 4442
	public Shader SCShader;

	// Token: 0x0400115B RID: 4443
	private float TimeX = 1f;

	// Token: 0x0400115C RID: 4444
	[Range(1f, 32f)]
	public float _EyeWave = 15f;

	// Token: 0x0400115D RID: 4445
	[Range(0f, 10f)]
	public float _EyeSpeed = 1f;

	// Token: 0x0400115E RID: 4446
	[Range(0f, 8f)]
	public float _EyeMove = 2f;

	// Token: 0x0400115F RID: 4447
	[Range(0f, 1f)]
	public float _EyeBlink = 1f;

	// Token: 0x04001160 RID: 4448
	private Material SCMaterial;

	// Token: 0x04001161 RID: 4449
	private Texture2D Texture2;
}
