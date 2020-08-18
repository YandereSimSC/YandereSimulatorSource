using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Screen")]
public class CameraFilterPack_Blend2Camera_Screen : MonoBehaviour
{
	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000659A9 File Offset: 0x00063BA9
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

	// Token: 0x06000C2B RID: 3115 RVA: 0x000659E0 File Offset: 0x00063BE0
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

	// Token: 0x06000C2C RID: 3116 RVA: 0x00065A44 File Offset: 0x00063C44
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

	// Token: 0x06000C2D RID: 3117 RVA: 0x00065B34 File Offset: 0x00063D34
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00065B34 File Offset: 0x00063D34
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00065B6C File Offset: 0x00063D6C
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

	// Token: 0x04000ED5 RID: 3797
	private string ShaderName = "CameraFilterPack/Blend2Camera_Screen";

	// Token: 0x04000ED6 RID: 3798
	public Shader SCShader;

	// Token: 0x04000ED7 RID: 3799
	public Camera Camera2;

	// Token: 0x04000ED8 RID: 3800
	private float TimeX = 1f;

	// Token: 0x04000ED9 RID: 3801
	private Material SCMaterial;

	// Token: 0x04000EDA RID: 3802
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EDB RID: 3803
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EDC RID: 3804
	private RenderTexture Camera2tex;
}
