using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/PinLight")]
public class CameraFilterPack_Blend2Camera_PinLight : MonoBehaviour
{
	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0006556C File Offset: 0x0006376C
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

	// Token: 0x06000C1B RID: 3099 RVA: 0x000655A0 File Offset: 0x000637A0
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

	// Token: 0x06000C1C RID: 3100 RVA: 0x00065604 File Offset: 0x00063804
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

	// Token: 0x06000C1D RID: 3101 RVA: 0x000656F4 File Offset: 0x000638F4
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x000656F4 File Offset: 0x000638F4
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0006572C File Offset: 0x0006392C
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

	// Token: 0x04000EC5 RID: 3781
	private string ShaderName = "CameraFilterPack/Blend2Camera_PinLight";

	// Token: 0x04000EC6 RID: 3782
	public Shader SCShader;

	// Token: 0x04000EC7 RID: 3783
	public Camera Camera2;

	// Token: 0x04000EC8 RID: 3784
	private float TimeX = 1f;

	// Token: 0x04000EC9 RID: 3785
	private Material SCMaterial;

	// Token: 0x04000ECA RID: 3786
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000ECB RID: 3787
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000ECC RID: 3788
	private RenderTexture Camera2tex;
}
