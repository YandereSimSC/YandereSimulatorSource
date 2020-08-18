using System;
using UnityEngine;

// Token: 0x02000122 RID: 290
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Chroma Key/GreenScreen")]
public class CameraFilterPack_Blend2Camera_GreenScreen : MonoBehaviour
{
	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0006368D File Offset: 0x0006188D
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

	// Token: 0x06000BB3 RID: 2995 RVA: 0x000636C4 File Offset: 0x000618C4
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

	// Token: 0x06000BB4 RID: 2996 RVA: 0x00063738 File Offset: 0x00061938
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

	// Token: 0x06000BB5 RID: 2997 RVA: 0x00063869 File Offset: 0x00061A69
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00063893 File Offset: 0x00061A93
	private void OnEnable()
	{
		this.Start();
		this.Update();
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x000638A4 File Offset: 0x00061AA4
	private void OnDisable()
	{
		if (this.Camera2 != null && this.Camera2.targetTexture != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000E55 RID: 3669
	private string ShaderName = "CameraFilterPack/Blend2Camera_GreenScreen";

	// Token: 0x04000E56 RID: 3670
	public Shader SCShader;

	// Token: 0x04000E57 RID: 3671
	public Camera Camera2;

	// Token: 0x04000E58 RID: 3672
	private float TimeX = 1f;

	// Token: 0x04000E59 RID: 3673
	private Material SCMaterial;

	// Token: 0x04000E5A RID: 3674
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000E5B RID: 3675
	[Range(-0.2f, 0.2f)]
	public float Adjust;

	// Token: 0x04000E5C RID: 3676
	[Range(-0.2f, 0.2f)]
	public float Precision;

	// Token: 0x04000E5D RID: 3677
	[Range(-0.2f, 0.2f)]
	public float Luminosity;

	// Token: 0x04000E5E RID: 3678
	[Range(-0.3f, 0.3f)]
	public float Change_Red;

	// Token: 0x04000E5F RID: 3679
	[Range(-0.3f, 0.3f)]
	public float Change_Green;

	// Token: 0x04000E60 RID: 3680
	[Range(-0.3f, 0.3f)]
	public float Change_Blue;

	// Token: 0x04000E61 RID: 3681
	private RenderTexture Camera2tex;

	// Token: 0x04000E62 RID: 3682
	private Vector2 ScreenSize;
}
