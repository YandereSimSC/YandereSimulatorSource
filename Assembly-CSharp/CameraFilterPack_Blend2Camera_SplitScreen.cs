using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Split Screen/SideBySide")]
public class CameraFilterPack_Blend2Camera_SplitScreen : MonoBehaviour
{
	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00065DE9 File Offset: 0x00063FE9
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

	// Token: 0x06000C3B RID: 3131 RVA: 0x00065E1D File Offset: 0x0006401D
	private void OnValidate()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00065E44 File Offset: 0x00064044
	private void Start()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture((int)this.ScreenSize.x, (int)this.ScreenSize.y, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00065EB8 File Offset: 0x000640B8
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
			this.material.SetFloat("_Value3", this.SplitX);
			this.material.SetFloat("_Value6", this.SplitY);
			this.material.SetFloat("_Value4", this.Smooth);
			this.material.SetFloat("_Value5", this.Rotation);
			this.material.SetInt("_ForceYSwap", this.ForceYSwap ? 0 : 1);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00065E1D File Offset: 0x0006401D
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00065FEF File Offset: 0x000641EF
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00065FF7 File Offset: 0x000641F7
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

	// Token: 0x04000EE5 RID: 3813
	private string ShaderName = "CameraFilterPack/Blend2Camera_SplitScreen";

	// Token: 0x04000EE6 RID: 3814
	public Shader SCShader;

	// Token: 0x04000EE7 RID: 3815
	public Camera Camera2;

	// Token: 0x04000EE8 RID: 3816
	private float TimeX = 1f;

	// Token: 0x04000EE9 RID: 3817
	private Material SCMaterial;

	// Token: 0x04000EEA RID: 3818
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EEB RID: 3819
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000EEC RID: 3820
	[Range(-3f, 3f)]
	public float SplitX = 0.5f;

	// Token: 0x04000EED RID: 3821
	[Range(-3f, 3f)]
	public float SplitY = 0.5f;

	// Token: 0x04000EEE RID: 3822
	[Range(0f, 2f)]
	public float Smooth = 0.1f;

	// Token: 0x04000EEF RID: 3823
	[Range(-3.14f, 3.14f)]
	public float Rotation = 3.14f;

	// Token: 0x04000EF0 RID: 3824
	private bool ForceYSwap;

	// Token: 0x04000EF1 RID: 3825
	private RenderTexture Camera2tex;

	// Token: 0x04000EF2 RID: 3826
	private Vector2 ScreenSize;
}
