using System;
using UnityEngine;

// Token: 0x0200011C RID: 284
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Chroma Key/Color Key")]
public class CameraFilterPack_Blend2Camera_ColorKey : MonoBehaviour
{
	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00062949 File Offset: 0x00060B49
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

	// Token: 0x06000B84 RID: 2948 RVA: 0x00062980 File Offset: 0x00060B80
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

	// Token: 0x06000B85 RID: 2949 RVA: 0x000629F4 File Offset: 0x00060BF4
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
			this.material.SetColor("_ColorKey", this.ColorKey);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00062B3B File Offset: 0x00060D3B
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00062B65 File Offset: 0x00060D65
	private void OnEnable()
	{
		this.Start();
		this.Update();
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00062B74 File Offset: 0x00060D74
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

	// Token: 0x04000E1E RID: 3614
	private string ShaderName = "CameraFilterPack/Blend2Camera_ColorKey";

	// Token: 0x04000E1F RID: 3615
	public Shader SCShader;

	// Token: 0x04000E20 RID: 3616
	public Camera Camera2;

	// Token: 0x04000E21 RID: 3617
	private float TimeX = 1f;

	// Token: 0x04000E22 RID: 3618
	private Material SCMaterial;

	// Token: 0x04000E23 RID: 3619
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000E24 RID: 3620
	public Color ColorKey;

	// Token: 0x04000E25 RID: 3621
	[Range(-0.2f, 0.2f)]
	public float Adjust;

	// Token: 0x04000E26 RID: 3622
	[Range(-0.2f, 0.2f)]
	public float Precision;

	// Token: 0x04000E27 RID: 3623
	[Range(-0.2f, 0.2f)]
	public float Luminosity;

	// Token: 0x04000E28 RID: 3624
	[Range(-0.3f, 0.3f)]
	public float Change_Red;

	// Token: 0x04000E29 RID: 3625
	[Range(-0.3f, 0.3f)]
	public float Change_Green;

	// Token: 0x04000E2A RID: 3626
	[Range(-0.3f, 0.3f)]
	public float Change_Blue;

	// Token: 0x04000E2B RID: 3627
	private RenderTexture Camera2tex;

	// Token: 0x04000E2C RID: 3628
	private Vector2 ScreenSize;
}
