using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Dream2")]
public class CameraFilterPack_Distortion_Dream2 : MonoBehaviour
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0006B9B6 File Offset: 0x00069BB6
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

	// Token: 0x06000D89 RID: 3465 RVA: 0x0006B9EA File Offset: 0x00069BEA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Dream2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0006BA0C File Offset: 0x00069C0C
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
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0006BAD8 File Offset: 0x00069CD8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001044 RID: 4164
	public Shader SCShader;

	// Token: 0x04001045 RID: 4165
	private float TimeX = 1f;

	// Token: 0x04001046 RID: 4166
	private Material SCMaterial;

	// Token: 0x04001047 RID: 4167
	[Range(0f, 100f)]
	public float Distortion = 6f;

	// Token: 0x04001048 RID: 4168
	[Range(0f, 32f)]
	public float Speed = 5f;
}
