using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/HardMix")]
public class CameraFilterPack_Blend2Camera_HardMix : MonoBehaviour
{
	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00063B3D File Offset: 0x00061D3D
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

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00063B74 File Offset: 0x00061D74
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

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00063BD8 File Offset: 0x00061DD8
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

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00063CC8 File Offset: 0x00061EC8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00063CC8 File Offset: 0x00061EC8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00063D00 File Offset: 0x00061F00
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

	// Token: 0x04000E6B RID: 3691
	private string ShaderName = "CameraFilterPack/Blend2Camera_HardMix";

	// Token: 0x04000E6C RID: 3692
	public Shader SCShader;

	// Token: 0x04000E6D RID: 3693
	public Camera Camera2;

	// Token: 0x04000E6E RID: 3694
	private float TimeX = 1f;

	// Token: 0x04000E6F RID: 3695
	private Material SCMaterial;

	// Token: 0x04000E70 RID: 3696
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E71 RID: 3697
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E72 RID: 3698
	private RenderTexture Camera2tex;
}
