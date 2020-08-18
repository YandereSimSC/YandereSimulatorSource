using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/SoftLight")]
public class CameraFilterPack_Blend2Camera_SoftLight : MonoBehaviour
{
	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00065BC9 File Offset: 0x00063DC9
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

	// Token: 0x06000C33 RID: 3123 RVA: 0x00065C00 File Offset: 0x00063E00
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

	// Token: 0x06000C34 RID: 3124 RVA: 0x00065C64 File Offset: 0x00063E64
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

	// Token: 0x06000C35 RID: 3125 RVA: 0x00065D54 File Offset: 0x00063F54
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00065D54 File Offset: 0x00063F54
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00065D8C File Offset: 0x00063F8C
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

	// Token: 0x04000EDD RID: 3805
	private string ShaderName = "CameraFilterPack/Blend2Camera_SoftLight";

	// Token: 0x04000EDE RID: 3806
	public Shader SCShader;

	// Token: 0x04000EDF RID: 3807
	public Camera Camera2;

	// Token: 0x04000EE0 RID: 3808
	private float TimeX = 1f;

	// Token: 0x04000EE1 RID: 3809
	private Material SCMaterial;

	// Token: 0x04000EE2 RID: 3810
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EE3 RID: 3811
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EE4 RID: 3812
	private RenderTexture Camera2tex;
}
