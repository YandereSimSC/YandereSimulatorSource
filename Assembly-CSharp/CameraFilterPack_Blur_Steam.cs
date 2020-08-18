using System;
using UnityEngine;

// Token: 0x02000144 RID: 324
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Steam")]
public class CameraFilterPack_Blur_Steam : MonoBehaviour
{
	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00067BFB File Offset: 0x00065DFB
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

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00067C2F File Offset: 0x00065E2F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Steam");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00067C50 File Offset: 0x00065E50
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
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetFloat("_Quality", this.Quality);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00067D15 File Offset: 0x00065F15
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F62 RID: 3938
	public Shader SCShader;

	// Token: 0x04000F63 RID: 3939
	private float TimeX = 1f;

	// Token: 0x04000F64 RID: 3940
	private Material SCMaterial;

	// Token: 0x04000F65 RID: 3941
	[Range(0f, 1f)]
	public float Radius = 0.1f;

	// Token: 0x04000F66 RID: 3942
	[Range(0f, 1f)]
	public float Quality = 0.75f;
}
