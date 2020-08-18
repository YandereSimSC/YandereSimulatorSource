using System;
using UnityEngine;

// Token: 0x020001E2 RID: 482
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 2")]
public class CameraFilterPack_Oculus_NightVision2 : MonoBehaviour
{
	// Token: 0x17000302 RID: 770
	// (get) Token: 0x0600107D RID: 4221 RVA: 0x000789B8 File Offset: 0x00076BB8
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

	// Token: 0x0600107E RID: 4222 RVA: 0x000789EC File Offset: 0x00076BEC
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

	// Token: 0x0600107F RID: 4223 RVA: 0x00078A06 File Offset: 0x00076C06
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

	// Token: 0x06001080 RID: 4224 RVA: 0x00078A30 File Offset: 0x00076C30
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

	// Token: 0x06001081 RID: 4225 RVA: 0x00078C51 File Offset: 0x00076E51
	private void OnValidate()
	{
		this.ChangeFilters();
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x00078C59 File Offset: 0x00076E59
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001377 RID: 4983
	private string ShaderName = "CameraFilterPack/Oculus_NightVision2";

	// Token: 0x04001378 RID: 4984
	public Shader SCShader;

	// Token: 0x04001379 RID: 4985
	[Range(0f, 1f)]
	public float FadeFX = 1f;

	// Token: 0x0400137A RID: 4986
	private float TimeX = 1f;

	// Token: 0x0400137B RID: 4987
	private Material SCMaterial;

	// Token: 0x0400137C RID: 4988
	private float[] Matrix9;
}
