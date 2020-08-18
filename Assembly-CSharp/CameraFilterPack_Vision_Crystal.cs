using System;
using UnityEngine;

// Token: 0x0200021A RID: 538
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Crystal")]
public class CameraFilterPack_Vision_Crystal : MonoBehaviour
{
	// Token: 0x1700033A RID: 826
	// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0007E4F1 File Offset: 0x0007C6F1
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

	// Token: 0x060011D3 RID: 4563 RVA: 0x0007E525 File Offset: 0x0007C725
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Crystal");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x0007E548 File Offset: 0x0007C748
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.X);
			this.material.SetFloat("_Value3", this.Y);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x0007E640 File Offset: 0x0007C840
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014D7 RID: 5335
	public Shader SCShader;

	// Token: 0x040014D8 RID: 5336
	private float TimeX = 1f;

	// Token: 0x040014D9 RID: 5337
	private Material SCMaterial;

	// Token: 0x040014DA RID: 5338
	[Range(-10f, 10f)]
	public float Value = 1f;

	// Token: 0x040014DB RID: 5339
	[Range(-1f, 1f)]
	public float X = 1f;

	// Token: 0x040014DC RID: 5340
	[Range(-1f, 1f)]
	public float Y = 1f;

	// Token: 0x040014DD RID: 5341
	[Range(-1f, 1f)]
	private float Value4 = 1f;
}
