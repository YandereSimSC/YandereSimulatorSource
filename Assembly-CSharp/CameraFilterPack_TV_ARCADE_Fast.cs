using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/ARCADE_Fast")]
public class CameraFilterPack_TV_ARCADE_Fast : MonoBehaviour
{
	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06001100 RID: 4352 RVA: 0x0007AE99 File Offset: 0x00079099
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

	// Token: 0x06001101 RID: 4353 RVA: 0x0007AECD File Offset: 0x000790CD
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_Arcade1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/TV_ARCADE_Fast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0007AF04 File Offset: 0x00079104
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
			this.material.SetFloat("_Value", this.Interferance_Size);
			this.material.SetFloat("_Value2", this.Interferance_Speed);
			this.material.SetFloat("_Value3", this.Contrast);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0007B012 File Offset: 0x00079212
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013FE RID: 5118
	public Shader SCShader;

	// Token: 0x040013FF RID: 5119
	private float TimeX = 1f;

	// Token: 0x04001400 RID: 5120
	private Material SCMaterial;

	// Token: 0x04001401 RID: 5121
	[Range(0f, 0.05f)]
	public float Interferance_Size = 0.02f;

	// Token: 0x04001402 RID: 5122
	[Range(0f, 4f)]
	public float Interferance_Speed = 0.5f;

	// Token: 0x04001403 RID: 5123
	[Range(0f, 10f)]
	public float Contrast = 1f;

	// Token: 0x04001404 RID: 5124
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001405 RID: 5125
	private Texture2D Texture2;
}
