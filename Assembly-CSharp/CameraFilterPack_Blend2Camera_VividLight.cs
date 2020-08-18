using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/VividLight")]
public class CameraFilterPack_Blend2Camera_VividLight : MonoBehaviour
{
	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000665BD File Offset: 0x000647BD
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

	// Token: 0x06000C53 RID: 3155 RVA: 0x000665F4 File Offset: 0x000647F4
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

	// Token: 0x06000C54 RID: 3156 RVA: 0x00066658 File Offset: 0x00064858
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

	// Token: 0x06000C55 RID: 3157 RVA: 0x00066748 File Offset: 0x00064948
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00066748 File Offset: 0x00064948
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00066780 File Offset: 0x00064980
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

	// Token: 0x04000F0C RID: 3852
	private string ShaderName = "CameraFilterPack/Blend2Camera_VividLight";

	// Token: 0x04000F0D RID: 3853
	public Shader SCShader;

	// Token: 0x04000F0E RID: 3854
	public Camera Camera2;

	// Token: 0x04000F0F RID: 3855
	private float TimeX = 1f;

	// Token: 0x04000F10 RID: 3856
	private Material SCMaterial;

	// Token: 0x04000F11 RID: 3857
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000F12 RID: 3858
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000F13 RID: 3859
	private RenderTexture Camera2tex;
}
