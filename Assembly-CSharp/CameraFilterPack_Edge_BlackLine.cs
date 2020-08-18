using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Edge/BlackLine")]
public class CameraFilterPack_Edge_BlackLine : MonoBehaviour
{
	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0006F659 File Offset: 0x0006D859
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

	// Token: 0x06000E7A RID: 3706 RVA: 0x0006F68D File Offset: 0x0006D88D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Edge_BlackLine");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x0006F6B0 File Offset: 0x0006D8B0
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x0006F746 File Offset: 0x0006D946
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001143 RID: 4419
	public Shader SCShader;

	// Token: 0x04001144 RID: 4420
	private float TimeX = 1f;

	// Token: 0x04001145 RID: 4421
	private Material SCMaterial;
}
