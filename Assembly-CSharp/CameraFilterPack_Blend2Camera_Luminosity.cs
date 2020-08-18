using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Luminosity")]
public class CameraFilterPack_Blend2Camera_Luminosity : MonoBehaviour
{
	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00064A1D File Offset: 0x00062C1D
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

	// Token: 0x06000BFA RID: 3066 RVA: 0x00064A54 File Offset: 0x00062C54
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

	// Token: 0x06000BFB RID: 3067 RVA: 0x00064AB8 File Offset: 0x00062CB8
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

	// Token: 0x06000BFC RID: 3068 RVA: 0x00064BA8 File Offset: 0x00062DA8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00064BA8 File Offset: 0x00062DA8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x00064BE0 File Offset: 0x00062DE0
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

	// Token: 0x04000EA3 RID: 3747
	private string ShaderName = "CameraFilterPack/Blend2Camera_Luminosity";

	// Token: 0x04000EA4 RID: 3748
	public Shader SCShader;

	// Token: 0x04000EA5 RID: 3749
	public Camera Camera2;

	// Token: 0x04000EA6 RID: 3750
	private float TimeX = 1f;

	// Token: 0x04000EA7 RID: 3751
	private Material SCMaterial;

	// Token: 0x04000EA8 RID: 3752
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EA9 RID: 3753
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EAA RID: 3754
	private RenderTexture Camera2tex;
}
