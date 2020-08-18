using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood")]
public class CameraFilterPack_AAA_Blood : MonoBehaviour
{
	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0005FD62 File Offset: 0x0005DF62
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

	// Token: 0x06000B02 RID: 2818 RVA: 0x0005FD96 File Offset: 0x0005DF96
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_Blood1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Blood");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0005FDCC File Offset: 0x0005DFCC
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
			this.material.SetFloat("_Value", this.LightReflect);
			this.material.SetFloat("_Value2", this.Blood1);
			this.material.SetFloat("_Value3", this.Blood2);
			this.material.SetFloat("_Value4", this.Blood3);
			this.material.SetFloat("_Value5", this.Blood4);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x0005FEC3 File Offset: 0x0005E0C3
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D50 RID: 3408
	public Shader SCShader;

	// Token: 0x04000D51 RID: 3409
	private float TimeX = 1f;

	// Token: 0x04000D52 RID: 3410
	[Range(0f, 128f)]
	public float Blood1;

	// Token: 0x04000D53 RID: 3411
	[Range(0f, 128f)]
	public float Blood2;

	// Token: 0x04000D54 RID: 3412
	[Range(0f, 128f)]
	public float Blood3;

	// Token: 0x04000D55 RID: 3413
	[Range(0f, 128f)]
	public float Blood4 = 1f;

	// Token: 0x04000D56 RID: 3414
	[Range(0f, 0.004f)]
	public float LightReflect = 0.002f;

	// Token: 0x04000D57 RID: 3415
	private Material SCMaterial;

	// Token: 0x04000D58 RID: 3416
	private Texture2D Texture2;
}
