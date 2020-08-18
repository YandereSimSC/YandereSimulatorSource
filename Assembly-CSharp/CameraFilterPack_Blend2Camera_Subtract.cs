using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Subtract")]
public class CameraFilterPack_Blend2Camera_Subtract : MonoBehaviour
{
	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0006639D File Offset: 0x0006459D
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

	// Token: 0x06000C4B RID: 3147 RVA: 0x000663D4 File Offset: 0x000645D4
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

	// Token: 0x06000C4C RID: 3148 RVA: 0x00066438 File Offset: 0x00064638
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

	// Token: 0x06000C4D RID: 3149 RVA: 0x00066528 File Offset: 0x00064728
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00066528 File Offset: 0x00064728
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00066560 File Offset: 0x00064760
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

	// Token: 0x04000F04 RID: 3844
	private string ShaderName = "CameraFilterPack/Blend2Camera_Subtract";

	// Token: 0x04000F05 RID: 3845
	public Shader SCShader;

	// Token: 0x04000F06 RID: 3846
	public Camera Camera2;

	// Token: 0x04000F07 RID: 3847
	private float TimeX = 1f;

	// Token: 0x04000F08 RID: 3848
	private Material SCMaterial;

	// Token: 0x04000F09 RID: 3849
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000F0A RID: 3850
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000F0B RID: 3851
	private RenderTexture Camera2tex;
}
