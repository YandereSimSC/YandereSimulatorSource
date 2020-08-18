using System;
using UnityEngine;

// Token: 0x020001F9 RID: 505
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Broken Glass")]
public class CameraFilterPack_TV_BrokenGlass : MonoBehaviour
{
	// Token: 0x17000319 RID: 793
	// (get) Token: 0x0600110C RID: 4364 RVA: 0x0007B211 File Offset: 0x00079411
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

	// Token: 0x0600110D RID: 4365 RVA: 0x0007B245 File Offset: 0x00079445
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_BrokenGlass1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/TV_BrokenGlass");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0007B27C File Offset: 0x0007947C
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
			this.material.SetFloat("_Value2", this.Broken_Small);
			this.material.SetFloat("_Value3", this.Broken_Medium);
			this.material.SetFloat("_Value4", this.Broken_High);
			this.material.SetFloat("_Value5", this.Broken_Big);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0007B373 File Offset: 0x00079573
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400140D RID: 5133
	public Shader SCShader;

	// Token: 0x0400140E RID: 5134
	private float TimeX = 1f;

	// Token: 0x0400140F RID: 5135
	[Range(0f, 128f)]
	public float Broken_Small;

	// Token: 0x04001410 RID: 5136
	[Range(0f, 128f)]
	public float Broken_Medium;

	// Token: 0x04001411 RID: 5137
	[Range(0f, 128f)]
	public float Broken_High;

	// Token: 0x04001412 RID: 5138
	[Range(0f, 128f)]
	public float Broken_Big = 1f;

	// Token: 0x04001413 RID: 5139
	[Range(0f, 0.004f)]
	public float LightReflect = 0.002f;

	// Token: 0x04001414 RID: 5140
	private Material SCMaterial;

	// Token: 0x04001415 RID: 5141
	private Texture2D Texture2;
}
