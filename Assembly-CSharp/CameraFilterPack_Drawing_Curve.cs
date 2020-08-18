using System;
using UnityEngine;

// Token: 0x0200017C RID: 380
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Curve")]
public class CameraFilterPack_Drawing_Curve : MonoBehaviour
{
	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0006D562 File Offset: 0x0006B762
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

	// Token: 0x06000DFB RID: 3579 RVA: 0x0006D596 File Offset: 0x0006B796
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Curve");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0006D5B8 File Offset: 0x0006B7B8
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x0006D66E File Offset: 0x0006B86E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010B2 RID: 4274
	public Shader SCShader;

	// Token: 0x040010B3 RID: 4275
	private float TimeX = 1f;

	// Token: 0x040010B4 RID: 4276
	private Material SCMaterial;

	// Token: 0x040010B5 RID: 4277
	[Range(3f, 5f)]
	public float Size = 1f;
}
