using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Noise/TV")]
public class CameraFilterPack_Noise_TV : MonoBehaviour
{
	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06001065 RID: 4197 RVA: 0x000782E4 File Offset: 0x000764E4
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

	// Token: 0x06001066 RID: 4198 RVA: 0x00078318 File Offset: 0x00076518
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_Noise") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Noise_TV");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x00078350 File Offset: 0x00076550
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0007845E File Offset: 0x0007665E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001359 RID: 4953
	public Shader SCShader;

	// Token: 0x0400135A RID: 4954
	private float TimeX = 1f;

	// Token: 0x0400135B RID: 4955
	private Material SCMaterial;

	// Token: 0x0400135C RID: 4956
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x0400135D RID: 4957
	[Range(0f, 10f)]
	private float Value2 = 1f;

	// Token: 0x0400135E RID: 4958
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x0400135F RID: 4959
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x04001360 RID: 4960
	private Texture2D Texture2;
}
