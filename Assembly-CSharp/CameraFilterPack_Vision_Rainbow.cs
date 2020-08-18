using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Rainbow")]
public class CameraFilterPack_Vision_Rainbow : MonoBehaviour
{
	// Token: 0x1700033F RID: 831
	// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0007EDCD File Offset: 0x0007CFCD
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

	// Token: 0x060011F1 RID: 4593 RVA: 0x0007EE01 File Offset: 0x0007D001
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Rainbow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x0007EE24 File Offset: 0x0007D024
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.PosX);
			this.material.SetFloat("_Value3", this.PosY);
			this.material.SetFloat("_Value4", this.Colors);
			this.material.SetFloat("_Value5", this.Vision);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x0007EF32 File Offset: 0x0007D132
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014FE RID: 5374
	public Shader SCShader;

	// Token: 0x040014FF RID: 5375
	private float TimeX = 1f;

	// Token: 0x04001500 RID: 5376
	private Material SCMaterial;

	// Token: 0x04001501 RID: 5377
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001502 RID: 5378
	[Range(0f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x04001503 RID: 5379
	[Range(0f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x04001504 RID: 5380
	[Range(0f, 5f)]
	public float Colors = 0.5f;

	// Token: 0x04001505 RID: 5381
	[Range(0f, 1f)]
	public float Vision = 0.5f;
}
