using System;
using UnityEngine;

// Token: 0x02000195 RID: 405
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Edge/Sigmoid")]
public class CameraFilterPack_Edge_Sigmoid : MonoBehaviour
{
	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0006FB2F File Offset: 0x0006DD2F
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

	// Token: 0x06000E92 RID: 3730 RVA: 0x0006FB63 File Offset: 0x0006DD63
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Edge_Sigmoid");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0006FB84 File Offset: 0x0006DD84
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
			this.material.SetFloat("_Gain", this.Gain);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0006FC33 File Offset: 0x0006DE33
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001153 RID: 4435
	public Shader SCShader;

	// Token: 0x04001154 RID: 4436
	private float TimeX = 1f;

	// Token: 0x04001155 RID: 4437
	private Material SCMaterial;

	// Token: 0x04001156 RID: 4438
	[Range(1f, 10f)]
	public float Gain = 3f;
}
