using System;
using UnityEngine;

// Token: 0x020001FF RID: 511
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Horror")]
public class CameraFilterPack_TV_Horror : MonoBehaviour
{
	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06001130 RID: 4400 RVA: 0x0007BDCF File Offset: 0x00079FCF
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

	// Token: 0x06001131 RID: 4401 RVA: 0x0007BE03 File Offset: 0x0007A003
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_HorrorFX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/TV_Horror");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x0007BE3C File Offset: 0x0007A03C
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x0007BF1E File Offset: 0x0007A11E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400143C RID: 5180
	public Shader SCShader;

	// Token: 0x0400143D RID: 5181
	private float TimeX = 1f;

	// Token: 0x0400143E RID: 5182
	private Material SCMaterial;

	// Token: 0x0400143F RID: 5183
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001440 RID: 5184
	[Range(0f, 1f)]
	public float Distortion = 1f;

	// Token: 0x04001441 RID: 5185
	private Texture2D Texture2;
}
