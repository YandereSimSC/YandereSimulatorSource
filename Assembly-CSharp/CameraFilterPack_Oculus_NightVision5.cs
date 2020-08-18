using System;
using UnityEngine;

// Token: 0x020001E4 RID: 484
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 5")]
public class CameraFilterPack_Oculus_NightVision5 : MonoBehaviour
{
	// Token: 0x17000304 RID: 772
	// (get) Token: 0x0600108B RID: 4235 RVA: 0x00078DDB File Offset: 0x00076FDB
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

	// Token: 0x0600108C RID: 4236 RVA: 0x00078E0F File Offset: 0x0007700F
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

	// Token: 0x0600108D RID: 4237 RVA: 0x00078E29 File Offset: 0x00077029
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

	// Token: 0x0600108E RID: 4238 RVA: 0x00078E54 File Offset: 0x00077054
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
			this.material.SetFloat("_Size", this._Size);
			this.material.SetFloat("_Dist", this._Dist);
			this.material.SetFloat("_Smooth", this._Smooth);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x000790B7 File Offset: 0x000772B7
	private void OnValidate()
	{
		this.ChangeFilters();
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x000790BF File Offset: 0x000772BF
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001381 RID: 4993
	private string ShaderName = "CameraFilterPack/Oculus_NightVision5";

	// Token: 0x04001382 RID: 4994
	public Shader SCShader;

	// Token: 0x04001383 RID: 4995
	[Range(0f, 1f)]
	public float FadeFX = 1f;

	// Token: 0x04001384 RID: 4996
	[Range(0f, 1f)]
	public float _Size = 0.37f;

	// Token: 0x04001385 RID: 4997
	[Range(0f, 1f)]
	public float _Smooth = 0.15f;

	// Token: 0x04001386 RID: 4998
	[Range(0f, 1f)]
	public float _Dist = 0.285f;

	// Token: 0x04001387 RID: 4999
	private float TimeX = 1f;

	// Token: 0x04001388 RID: 5000
	private Material SCMaterial;

	// Token: 0x04001389 RID: 5001
	private float[] Matrix9;
}
