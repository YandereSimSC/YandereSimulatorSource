using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/HardLight")]
public class CameraFilterPack_Blend2Camera_HardLight : MonoBehaviour
{
	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0006391F File Offset: 0x00061B1F
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

	// Token: 0x06000BBA RID: 3002 RVA: 0x00063954 File Offset: 0x00061B54
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

	// Token: 0x06000BBB RID: 3003 RVA: 0x000639B8 File Offset: 0x00061BB8
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

	// Token: 0x06000BBC RID: 3004 RVA: 0x00063AA8 File Offset: 0x00061CA8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x00063AA8 File Offset: 0x00061CA8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00063AE0 File Offset: 0x00061CE0
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

	// Token: 0x04000E63 RID: 3683
	private string ShaderName = "CameraFilterPack/Blend2Camera_HardLight";

	// Token: 0x04000E64 RID: 3684
	public Shader SCShader;

	// Token: 0x04000E65 RID: 3685
	public Camera Camera2;

	// Token: 0x04000E66 RID: 3686
	private float TimeX = 1f;

	// Token: 0x04000E67 RID: 3687
	private Material SCMaterial;

	// Token: 0x04000E68 RID: 3688
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E69 RID: 3689
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E6A RID: 3690
	private RenderTexture Camera2tex;
}
