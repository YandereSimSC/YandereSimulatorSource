using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/WaterDrop")]
public class CameraFilterPack_AAA_WaterDrop : MonoBehaviour
{
	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00060C5C File Offset: 0x0005EE5C
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

	// Token: 0x06000B26 RID: 2854 RVA: 0x00060C90 File Offset: 0x0005EE90
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_WaterDrop") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_WaterDrop");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00060CC8 File Offset: 0x0005EEC8
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
			this.material.SetFloat("_SizeX", this.SizeX);
			this.material.SetFloat("_SizeY", this.SizeY);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00060DA9 File Offset: 0x0005EFA9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D9B RID: 3483
	public Shader SCShader;

	// Token: 0x04000D9C RID: 3484
	private float TimeX = 1f;

	// Token: 0x04000D9D RID: 3485
	[Range(8f, 64f)]
	public float Distortion = 8f;

	// Token: 0x04000D9E RID: 3486
	[Range(0f, 7f)]
	public float SizeX = 1f;

	// Token: 0x04000D9F RID: 3487
	[Range(0f, 7f)]
	public float SizeY = 0.5f;

	// Token: 0x04000DA0 RID: 3488
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04000DA1 RID: 3489
	private Material SCMaterial;

	// Token: 0x04000DA2 RID: 3490
	private Texture2D Texture2;
}
