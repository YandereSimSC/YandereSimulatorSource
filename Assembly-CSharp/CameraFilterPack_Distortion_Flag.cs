using System;
using UnityEngine;

// Token: 0x0200016B RID: 363
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Flag")]
public class CameraFilterPack_Distortion_Flag : MonoBehaviour
{
	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0006BC5E File Offset: 0x00069E5E
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

	// Token: 0x06000D95 RID: 3477 RVA: 0x0006BC92 File Offset: 0x00069E92
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Flag");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0006BCB4 File Offset: 0x00069EB4
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x0006BD63 File Offset: 0x00069F63
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400104D RID: 4173
	public Shader SCShader;

	// Token: 0x0400104E RID: 4174
	private float TimeX = 1f;

	// Token: 0x0400104F RID: 4175
	[Range(0f, 2f)]
	public float Distortion = 1f;

	// Token: 0x04001050 RID: 4176
	private Material SCMaterial;

	// Token: 0x04001051 RID: 4177
	public static float ChangeDistortion;
}
