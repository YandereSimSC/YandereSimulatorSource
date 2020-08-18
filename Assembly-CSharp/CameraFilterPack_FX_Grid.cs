using System;
using UnityEngine;

// Token: 0x020001A7 RID: 423
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Grid")]
public class CameraFilterPack_FX_Grid : MonoBehaviour
{
	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0007162B File Offset: 0x0006F82B
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

	// Token: 0x06000EFE RID: 3838 RVA: 0x0007165F File Offset: 0x0006F85F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Grid");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x00071680 File Offset: 0x0006F880
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x00071706 File Offset: 0x0006F906
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011C0 RID: 4544
	public Shader SCShader;

	// Token: 0x040011C1 RID: 4545
	private float TimeX = 1f;

	// Token: 0x040011C2 RID: 4546
	private Material SCMaterial;

	// Token: 0x040011C3 RID: 4547
	[Range(0f, 5f)]
	public float Distortion = 1f;

	// Token: 0x040011C4 RID: 4548
	public static float ChangeDistortion;
}
