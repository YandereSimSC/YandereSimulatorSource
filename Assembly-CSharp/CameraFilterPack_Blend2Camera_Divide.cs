using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Divide")]
public class CameraFilterPack_Blend2Camera_Divide : MonoBehaviour
{
	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0006324D File Offset: 0x0006144D
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

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00063284 File Offset: 0x00061484
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

	// Token: 0x06000BA4 RID: 2980 RVA: 0x000632E8 File Offset: 0x000614E8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.Camera2 != null)
			{
				this.material.SetTexture("_MainTex2", this.Camera2tex);
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x000633D8 File Offset: 0x000615D8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x000633D8 File Offset: 0x000615D8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00063410 File Offset: 0x00061610
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

	// Token: 0x04000E45 RID: 3653
	private string ShaderName = "CameraFilterPack/Blend2Camera_Divide";

	// Token: 0x04000E46 RID: 3654
	public Shader SCShader;

	// Token: 0x04000E47 RID: 3655
	public Camera Camera2;

	// Token: 0x04000E48 RID: 3656
	private float TimeX = 1f;

	// Token: 0x04000E49 RID: 3657
	private Material SCMaterial;

	// Token: 0x04000E4A RID: 3658
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E4B RID: 3659
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E4C RID: 3660
	private RenderTexture Camera2tex;
}
