using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LinearLight")]
public class CameraFilterPack_Blend2Camera_LinearLight : MonoBehaviour
{
	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x000647FD File Offset: 0x000629FD
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

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00064834 File Offset: 0x00062A34
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

	// Token: 0x06000BF3 RID: 3059 RVA: 0x00064898 File Offset: 0x00062A98
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

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00064988 File Offset: 0x00062B88
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x00064988 File Offset: 0x00062B88
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x000649C0 File Offset: 0x00062BC0
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

	// Token: 0x04000E9B RID: 3739
	private string ShaderName = "CameraFilterPack/Blend2Camera_LinearLight";

	// Token: 0x04000E9C RID: 3740
	public Shader SCShader;

	// Token: 0x04000E9D RID: 3741
	public Camera Camera2;

	// Token: 0x04000E9E RID: 3742
	private float TimeX = 1f;

	// Token: 0x04000E9F RID: 3743
	private Material SCMaterial;

	// Token: 0x04000EA0 RID: 3744
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EA1 RID: 3745
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EA2 RID: 3746
	private RenderTexture Camera2tex;
}
