using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Multiply")]
public class CameraFilterPack_Blend2Camera_Multiply : MonoBehaviour
{
	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00064C3D File Offset: 0x00062E3D
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

	// Token: 0x06000C02 RID: 3074 RVA: 0x00064C74 File Offset: 0x00062E74
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

	// Token: 0x06000C03 RID: 3075 RVA: 0x00064CD8 File Offset: 0x00062ED8
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

	// Token: 0x06000C04 RID: 3076 RVA: 0x00064DC8 File Offset: 0x00062FC8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00064DC8 File Offset: 0x00062FC8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00064E00 File Offset: 0x00063000
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

	// Token: 0x04000EAB RID: 3755
	private string ShaderName = "CameraFilterPack/Blend2Camera_Multiply";

	// Token: 0x04000EAC RID: 3756
	public Shader SCShader;

	// Token: 0x04000EAD RID: 3757
	public Camera Camera2;

	// Token: 0x04000EAE RID: 3758
	private float TimeX = 1f;

	// Token: 0x04000EAF RID: 3759
	private Material SCMaterial;

	// Token: 0x04000EB0 RID: 3760
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EB1 RID: 3761
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EB2 RID: 3762
	private RenderTexture Camera2tex;
}
