using System;
using UnityEngine;

// Token: 0x020001DD RID: 477
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 4")]
public class CameraFilterPack_NightVision_4 : MonoBehaviour
{
	// Token: 0x170002FD RID: 765
	// (get) Token: 0x0600105D RID: 4189 RVA: 0x00078002 File Offset: 0x00076202
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

	// Token: 0x0600105E RID: 4190 RVA: 0x00078036 File Offset: 0x00076236
	private void ChangeFilters()
	{
		this.Matrix9 = new float[]
		{
			200f,
			-200f,
			-200f,
			195f,
			4f,
			-160f,
			200f,
			-200f,
			-200f,
			-200f,
			10f,
			-200f
		};
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x00078050 File Offset: 0x00076250
	private void Start()
	{
		this.ChangeFilters();
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x00078078 File Offset: 0x00076278
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Red_R", this.Matrix9[0] / 100f);
			this.material.SetFloat("_Red_G", this.Matrix9[1] / 100f);
			this.material.SetFloat("_Red_B", this.Matrix9[2] / 100f);
			this.material.SetFloat("_Green_R", this.Matrix9[3] / 100f);
			this.material.SetFloat("_Green_G", this.Matrix9[4] / 100f);
			this.material.SetFloat("_Green_B", this.Matrix9[5] / 100f);
			this.material.SetFloat("_Blue_R", this.Matrix9[6] / 100f);
			this.material.SetFloat("_Blue_G", this.Matrix9[7] / 100f);
			this.material.SetFloat("_Blue_B", this.Matrix9[8] / 100f);
			this.material.SetFloat("_Red_C", this.Matrix9[9] / 100f);
			this.material.SetFloat("_Green_C", this.Matrix9[10] / 100f);
			this.material.SetFloat("_Blue_C", this.Matrix9[11] / 100f);
			this.material.SetFloat("_FadeFX", this.FadeFX);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x00078299 File Offset: 0x00076499
	private void OnValidate()
	{
		this.ChangeFilters();
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x000782A1 File Offset: 0x000764A1
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001353 RID: 4947
	private string ShaderName = "CameraFilterPack/NightVision_4";

	// Token: 0x04001354 RID: 4948
	public Shader SCShader;

	// Token: 0x04001355 RID: 4949
	[Range(0f, 1f)]
	public float FadeFX = 1f;

	// Token: 0x04001356 RID: 4950
	private float TimeX = 1f;

	// Token: 0x04001357 RID: 4951
	private Material SCMaterial;

	// Token: 0x04001358 RID: 4952
	private float[] Matrix9;
}
