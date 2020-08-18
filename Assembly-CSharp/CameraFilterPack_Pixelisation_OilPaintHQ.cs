using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/OilPaintHQ")]
public class CameraFilterPack_Pixelisation_OilPaintHQ : MonoBehaviour
{
	// Token: 0x1700030C RID: 780
	// (get) Token: 0x060010BD RID: 4285 RVA: 0x00079C7E File Offset: 0x00077E7E
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

	// Token: 0x060010BE RID: 4286 RVA: 0x00079CB2 File Offset: 0x00077EB2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_OilPaintHQ");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x00079CD4 File Offset: 0x00077ED4
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Value", this.Value);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00079D8A File Offset: 0x00077F8A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013BC RID: 5052
	public Shader SCShader;

	// Token: 0x040013BD RID: 5053
	private float TimeX = 1f;

	// Token: 0x040013BE RID: 5054
	private Material SCMaterial;

	// Token: 0x040013BF RID: 5055
	[Range(0f, 5f)]
	public float Value = 2f;
}
