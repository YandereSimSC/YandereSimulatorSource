using System;
using UnityEngine;

// Token: 0x02000159 RID: 345
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/ColorsAdjust/FullColors")]
public class CameraFilterPack_Colors_Adjust_FullColors : MonoBehaviour
{
	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00069D29 File Offset: 0x00067F29
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

	// Token: 0x06000D27 RID: 3367 RVA: 0x00069D5D File Offset: 0x00067F5D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_Adjust_FullColors");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x00069D80 File Offset: 0x00067F80
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
			this.material.SetFloat("_Red_R", this.Red_R / 100f);
			this.material.SetFloat("_Red_G", this.Red_G / 100f);
			this.material.SetFloat("_Red_B", this.Red_B / 100f);
			this.material.SetFloat("_Green_R", this.Green_R / 100f);
			this.material.SetFloat("_Green_G", this.Green_G / 100f);
			this.material.SetFloat("_Green_B", this.Green_B / 100f);
			this.material.SetFloat("_Blue_R", this.Blue_R / 100f);
			this.material.SetFloat("_Blue_G", this.Blue_G / 100f);
			this.material.SetFloat("_Blue_B", this.Blue_B / 100f);
			this.material.SetFloat("_Red_C", this.Red_Constant / 100f);
			this.material.SetFloat("_Green_C", this.Green_Constant / 100f);
			this.material.SetFloat("_Blue_C", this.Blue_Constant / 100f);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x00069F70 File Offset: 0x00068170
	private void Update()
	{
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x00069F78 File Offset: 0x00068178
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FDF RID: 4063
	public Shader SCShader;

	// Token: 0x04000FE0 RID: 4064
	private float TimeX = 1f;

	// Token: 0x04000FE1 RID: 4065
	private Material SCMaterial;

	// Token: 0x04000FE2 RID: 4066
	[Range(-200f, 200f)]
	public float Red_R = 100f;

	// Token: 0x04000FE3 RID: 4067
	[Range(-200f, 200f)]
	public float Red_G;

	// Token: 0x04000FE4 RID: 4068
	[Range(-200f, 200f)]
	public float Red_B;

	// Token: 0x04000FE5 RID: 4069
	[Range(-200f, 200f)]
	public float Red_Constant;

	// Token: 0x04000FE6 RID: 4070
	[Range(-200f, 200f)]
	public float Green_R;

	// Token: 0x04000FE7 RID: 4071
	[Range(-200f, 200f)]
	public float Green_G = 100f;

	// Token: 0x04000FE8 RID: 4072
	[Range(-200f, 200f)]
	public float Green_B;

	// Token: 0x04000FE9 RID: 4073
	[Range(-200f, 200f)]
	public float Green_Constant;

	// Token: 0x04000FEA RID: 4074
	[Range(-200f, 200f)]
	public float Blue_R;

	// Token: 0x04000FEB RID: 4075
	[Range(-200f, 200f)]
	public float Blue_G;

	// Token: 0x04000FEC RID: 4076
	[Range(-200f, 200f)]
	public float Blue_B = 100f;

	// Token: 0x04000FED RID: 4077
	[Range(-200f, 200f)]
	public float Blue_Constant;
}
