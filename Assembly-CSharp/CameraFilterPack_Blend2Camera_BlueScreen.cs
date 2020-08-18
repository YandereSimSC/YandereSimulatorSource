using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Chroma Key/BlueScreen")]
public class CameraFilterPack_Blend2Camera_BlueScreen : MonoBehaviour
{
	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0006205D File Offset: 0x0006025D
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

	// Token: 0x06000B64 RID: 2916 RVA: 0x00062091 File Offset: 0x00060291
	private void OnValidate()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x000620B8 File Offset: 0x000602B8
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

	// Token: 0x06000B66 RID: 2918 RVA: 0x0006212C File Offset: 0x0006032C
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
			this.material.SetFloat("_Value2", this.Adjust);
			this.material.SetFloat("_Value3", this.Precision);
			this.material.SetFloat("_Value4", this.Luminosity);
			this.material.SetFloat("_Value5", this.Change_Red);
			this.material.SetFloat("_Value6", this.Change_Green);
			this.material.SetFloat("_Value7", this.Change_Blue);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0006225D File Offset: 0x0006045D
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00062287 File Offset: 0x00060487
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x0006228F File Offset: 0x0006048F
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

	// Token: 0x04000DF8 RID: 3576
	private string ShaderName = "CameraFilterPack/Blend2Camera_BlueScreen";

	// Token: 0x04000DF9 RID: 3577
	public Shader SCShader;

	// Token: 0x04000DFA RID: 3578
	public Camera Camera2;

	// Token: 0x04000DFB RID: 3579
	private float TimeX = 1f;

	// Token: 0x04000DFC RID: 3580
	private Material SCMaterial;

	// Token: 0x04000DFD RID: 3581
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000DFE RID: 3582
	[Range(-0.2f, 0.2f)]
	public float Adjust;

	// Token: 0x04000DFF RID: 3583
	[Range(-0.2f, 0.2f)]
	public float Precision;

	// Token: 0x04000E00 RID: 3584
	[Range(-0.2f, 0.2f)]
	public float Luminosity;

	// Token: 0x04000E01 RID: 3585
	[Range(-0.3f, 0.3f)]
	public float Change_Red;

	// Token: 0x04000E02 RID: 3586
	[Range(-0.3f, 0.3f)]
	public float Change_Green;

	// Token: 0x04000E03 RID: 3587
	[Range(-0.3f, 0.3f)]
	public float Change_Blue;

	// Token: 0x04000E04 RID: 3588
	private RenderTexture Camera2tex;

	// Token: 0x04000E05 RID: 3589
	private Vector2 ScreenSize;
}
