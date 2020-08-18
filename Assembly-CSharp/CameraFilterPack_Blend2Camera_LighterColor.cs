using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LighterColor")]
public class CameraFilterPack_Blend2Camera_LighterColor : MonoBehaviour
{
	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0006419D File Offset: 0x0006239D
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

	// Token: 0x06000BDA RID: 3034 RVA: 0x000641D4 File Offset: 0x000623D4
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

	// Token: 0x06000BDB RID: 3035 RVA: 0x00064238 File Offset: 0x00062438
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

	// Token: 0x06000BDC RID: 3036 RVA: 0x00064328 File Offset: 0x00062528
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00064328 File Offset: 0x00062528
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00064360 File Offset: 0x00062560
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

	// Token: 0x04000E83 RID: 3715
	private string ShaderName = "CameraFilterPack/Blend2Camera_LighterColor";

	// Token: 0x04000E84 RID: 3716
	public Shader SCShader;

	// Token: 0x04000E85 RID: 3717
	public Camera Camera2;

	// Token: 0x04000E86 RID: 3718
	private float TimeX = 1f;

	// Token: 0x04000E87 RID: 3719
	private Material SCMaterial;

	// Token: 0x04000E88 RID: 3720
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E89 RID: 3721
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E8A RID: 3722
	private RenderTexture Camera2tex;
}
