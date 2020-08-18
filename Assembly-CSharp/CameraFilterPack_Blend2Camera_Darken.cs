using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Darken")]
public class CameraFilterPack_Blend2Camera_Darken : MonoBehaviour
{
	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00062BEF File Offset: 0x00060DEF
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

	// Token: 0x06000B8B RID: 2955 RVA: 0x00062C24 File Offset: 0x00060E24
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

	// Token: 0x06000B8C RID: 2956 RVA: 0x00062C88 File Offset: 0x00060E88
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

	// Token: 0x06000B8D RID: 2957 RVA: 0x00062D78 File Offset: 0x00060F78
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00062D78 File Offset: 0x00060F78
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00062DB0 File Offset: 0x00060FB0
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

	// Token: 0x04000E2D RID: 3629
	private string ShaderName = "CameraFilterPack/Blend2Camera_Darken";

	// Token: 0x04000E2E RID: 3630
	public Shader SCShader;

	// Token: 0x04000E2F RID: 3631
	public Camera Camera2;

	// Token: 0x04000E30 RID: 3632
	private float TimeX = 1f;

	// Token: 0x04000E31 RID: 3633
	private Material SCMaterial;

	// Token: 0x04000E32 RID: 3634
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E33 RID: 3635
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E34 RID: 3636
	private RenderTexture Camera2tex;
}
