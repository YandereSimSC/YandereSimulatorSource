using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Blend")]
public class CameraFilterPack_Blend2Camera_Blend : MonoBehaviour
{
	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00061E62 File Offset: 0x00060062
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

	// Token: 0x06000B5C RID: 2908 RVA: 0x00061E98 File Offset: 0x00060098
	private void Start()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00061EFC File Offset: 0x000600FC
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetTexture("_MainTex2", this.Camera2tex);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00061FC8 File Offset: 0x000601C8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00061FC8 File Offset: 0x000601C8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00062000 File Offset: 0x00060200
	private void OnDisable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DF1 RID: 3569
	private string ShaderName = "CameraFilterPack/Blend2Camera_Blend";

	// Token: 0x04000DF2 RID: 3570
	public Shader SCShader;

	// Token: 0x04000DF3 RID: 3571
	public Camera Camera2;

	// Token: 0x04000DF4 RID: 3572
	private float TimeX = 1f;

	// Token: 0x04000DF5 RID: 3573
	private Material SCMaterial;

	// Token: 0x04000DF6 RID: 3574
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000DF7 RID: 3575
	private RenderTexture Camera2tex;
}
