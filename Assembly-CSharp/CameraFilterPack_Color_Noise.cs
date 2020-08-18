using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Noise")]
public class CameraFilterPack_Color_Noise : MonoBehaviour
{
	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0006952E File Offset: 0x0006772E
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

	// Token: 0x06000D03 RID: 3331 RVA: 0x00069562 File Offset: 0x00067762
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Noise");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x00069584 File Offset: 0x00067784
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
			this.material.SetFloat("_Noise", this.Noise);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0006963A File Offset: 0x0006783A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FC2 RID: 4034
	public Shader SCShader;

	// Token: 0x04000FC3 RID: 4035
	private float TimeX = 1f;

	// Token: 0x04000FC4 RID: 4036
	private Material SCMaterial;

	// Token: 0x04000FC5 RID: 4037
	[Range(0f, 1f)]
	public float Noise = 0.235f;
}
