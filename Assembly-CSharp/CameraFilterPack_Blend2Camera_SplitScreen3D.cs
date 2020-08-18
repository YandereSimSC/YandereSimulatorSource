using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Split Screen/Split 3D")]
public class CameraFilterPack_Blend2Camera_SplitScreen3D : MonoBehaviour
{
	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0006608C File Offset: 0x0006428C
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

	// Token: 0x06000C43 RID: 3139 RVA: 0x000660C0 File Offset: 0x000642C0
	private void OnValidate()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x000660E4 File Offset: 0x000642E4
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

	// Token: 0x06000C45 RID: 3141 RVA: 0x00066158 File Offset: 0x00064358
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
			this.material.SetFloat("_Near", this._Distance);
			this.material.SetFloat("_Far", this._Size);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetFloat("_Value3", this.SplitX);
			this.material.SetFloat("_Value6", this.SplitY);
			this.material.SetFloat("_Value4", this.Smooth);
			this.material.SetFloat("_Value5", this.Rotation);
			this.material.SetInt("_ForceYSwap", this.ForceYSwap ? 0 : 1);
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x000660C0 File Offset: 0x000642C0
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x000662DD File Offset: 0x000644DD
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x000662E5 File Offset: 0x000644E5
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

	// Token: 0x04000EF3 RID: 3827
	private string ShaderName = "CameraFilterPack/Blend2Camera_SplitScreen3D";

	// Token: 0x04000EF4 RID: 3828
	public Shader SCShader;

	// Token: 0x04000EF5 RID: 3829
	public Camera Camera2;

	// Token: 0x04000EF6 RID: 3830
	private float TimeX = 1f;

	// Token: 0x04000EF7 RID: 3831
	private Material SCMaterial;

	// Token: 0x04000EF8 RID: 3832
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000EF9 RID: 3833
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.5f;

	// Token: 0x04000EFA RID: 3834
	[Range(0f, 0.5f)]
	public float _Size = 0.1f;

	// Token: 0x04000EFB RID: 3835
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EFC RID: 3836
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000EFD RID: 3837
	[Range(-3f, 3f)]
	public float SplitX = 0.5f;

	// Token: 0x04000EFE RID: 3838
	[Range(-3f, 3f)]
	public float SplitY = 0.5f;

	// Token: 0x04000EFF RID: 3839
	[Range(0f, 2f)]
	public float Smooth = 0.1f;

	// Token: 0x04000F00 RID: 3840
	[Range(-3.14f, 3.14f)]
	public float Rotation = 3.14f;

	// Token: 0x04000F01 RID: 3841
	private bool ForceYSwap;

	// Token: 0x04000F02 RID: 3842
	private RenderTexture Camera2tex;

	// Token: 0x04000F03 RID: 3843
	private Vector2 ScreenSize;
}
