using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Exclusion")]
public class CameraFilterPack_Blend2Camera_Exclusion : MonoBehaviour
{
	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0006346D File Offset: 0x0006166D
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

	// Token: 0x06000BAB RID: 2987 RVA: 0x000634A4 File Offset: 0x000616A4
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

	// Token: 0x06000BAC RID: 2988 RVA: 0x00063508 File Offset: 0x00061708
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

	// Token: 0x06000BAD RID: 2989 RVA: 0x000635F8 File Offset: 0x000617F8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x000635F8 File Offset: 0x000617F8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x00063630 File Offset: 0x00061830
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

	// Token: 0x04000E4D RID: 3661
	private string ShaderName = "CameraFilterPack/Blend2Camera_Exclusion";

	// Token: 0x04000E4E RID: 3662
	public Shader SCShader;

	// Token: 0x04000E4F RID: 3663
	public Camera Camera2;

	// Token: 0x04000E50 RID: 3664
	private float TimeX = 1f;

	// Token: 0x04000E51 RID: 3665
	private Material SCMaterial;

	// Token: 0x04000E52 RID: 3666
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E53 RID: 3667
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E54 RID: 3668
	private RenderTexture Camera2tex;
}
