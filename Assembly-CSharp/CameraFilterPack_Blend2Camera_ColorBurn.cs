using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/ColorBurn")]
public class CameraFilterPack_Blend2Camera_ColorBurn : MonoBehaviour
{
	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00062509 File Offset: 0x00060709
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

	// Token: 0x06000B74 RID: 2932 RVA: 0x00062540 File Offset: 0x00060740
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

	// Token: 0x06000B75 RID: 2933 RVA: 0x000625A4 File Offset: 0x000607A4
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

	// Token: 0x06000B76 RID: 2934 RVA: 0x00062694 File Offset: 0x00060894
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00062694 File Offset: 0x00060894
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000626CC File Offset: 0x000608CC
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

	// Token: 0x04000E0E RID: 3598
	private string ShaderName = "CameraFilterPack/Blend2Camera_ColorBurn";

	// Token: 0x04000E0F RID: 3599
	public Shader SCShader;

	// Token: 0x04000E10 RID: 3600
	public Camera Camera2;

	// Token: 0x04000E11 RID: 3601
	private float TimeX = 1f;

	// Token: 0x04000E12 RID: 3602
	private Material SCMaterial;

	// Token: 0x04000E13 RID: 3603
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E14 RID: 3604
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E15 RID: 3605
	private RenderTexture Camera2tex;
}
