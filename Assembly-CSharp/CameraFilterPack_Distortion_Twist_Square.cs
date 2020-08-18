using System;
using UnityEngine;

// Token: 0x02000174 RID: 372
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Twist_Square")]
public class CameraFilterPack_Distortion_Twist_Square : MonoBehaviour
{
	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0006C942 File Offset: 0x0006AB42
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

	// Token: 0x06000DCB RID: 3531 RVA: 0x0006C976 File Offset: 0x0006AB76
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Twist_Square");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x0006C998 File Offset: 0x0006AB98
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_Size", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0006CA89 File Offset: 0x0006AC89
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001080 RID: 4224
	public Shader SCShader;

	// Token: 0x04001081 RID: 4225
	private float TimeX = 1f;

	// Token: 0x04001082 RID: 4226
	private Material SCMaterial;

	// Token: 0x04001083 RID: 4227
	[Range(-2f, 2f)]
	public float CenterX = 0.5f;

	// Token: 0x04001084 RID: 4228
	[Range(-2f, 2f)]
	public float CenterY = 0.5f;

	// Token: 0x04001085 RID: 4229
	[Range(-3.14f, 3.14f)]
	public float Distortion = 0.5f;

	// Token: 0x04001086 RID: 4230
	[Range(-2f, 2f)]
	public float Size = 0.25f;
}
