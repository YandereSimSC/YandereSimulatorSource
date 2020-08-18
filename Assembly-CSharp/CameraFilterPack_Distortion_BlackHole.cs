using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/BlackHole")]
public class CameraFilterPack_Distortion_BlackHole : MonoBehaviour
{
	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0006B56F File Offset: 0x0006976F
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

	// Token: 0x06000D77 RID: 3447 RVA: 0x0006B5A3 File Offset: 0x000697A3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_BlackHole");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0006B5C4 File Offset: 0x000697C4
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
			this.material.SetFloat("_PositionX", this.PositionX);
			this.material.SetFloat("_PositionY", this.PositionY);
			this.material.SetFloat("_Distortion", this.Size);
			this.material.SetFloat("_Distortion2", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0006B6B5 File Offset: 0x000698B5
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001032 RID: 4146
	public Shader SCShader;

	// Token: 0x04001033 RID: 4147
	private float TimeX = 1f;

	// Token: 0x04001034 RID: 4148
	private Material SCMaterial;

	// Token: 0x04001035 RID: 4149
	[Range(-1f, 1f)]
	public float PositionX;

	// Token: 0x04001036 RID: 4150
	[Range(-1f, 1f)]
	public float PositionY;

	// Token: 0x04001037 RID: 4151
	[Range(-5f, 5f)]
	public float Size = 0.05f;

	// Token: 0x04001038 RID: 4152
	[Range(0f, 180f)]
	public float Distortion = 30f;
}
