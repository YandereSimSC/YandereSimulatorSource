using System;
using UnityEngine;

// Token: 0x020001CE RID: 462
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Lut/Lut 2 Lut")]
public class CameraFilterPack_Lut_2_Lut : MonoBehaviour
{
	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00075701 File Offset: 0x00073901
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

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00075735 File Offset: 0x00073935
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Lut_2_lut");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x00075758 File Offset: 0x00073958
	public void SetIdentityLut()
	{
		int num = 16;
		Color[] array = new Color[num * num * num];
		float num2 = 1f / (1f * (float)num - 1f);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
				}
			}
		}
		if (this.converted3DLut)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut.SetPixels(array);
		this.converted3DLut.Apply();
		if (this.converted3DLut2)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut2);
		}
		this.converted3DLut2 = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut2.SetPixels(array);
		this.converted3DLut2.Apply();
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0007586F File Offset: 0x00073A6F
	public bool ValidDimensions(Texture2D tex2d)
	{
		return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00075898 File Offset: 0x00073A98
	public Texture3D Convert(Texture2D temp2DTex, Texture3D cv3D)
	{
		int num = 4096;
		if (temp2DTex)
		{
			num = temp2DTex.width * temp2DTex.height;
			num = temp2DTex.height;
			if (!this.ValidDimensions(temp2DTex))
			{
				Debug.LogWarning("The given 2D texture " + temp2DTex.name + " cannot be used as a 3D LUT.");
				return cv3D;
			}
		}
		Color[] pixels = temp2DTex.GetPixels();
		Color[] array = new Color[pixels.Length];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					int num2 = num - j - 1;
					array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
				}
			}
		}
		if (cv3D)
		{
			UnityEngine.Object.DestroyImmediate(cv3D);
		}
		cv3D = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		cv3D.SetPixels(array);
		cv3D.Apply();
		return cv3D;
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x00075980 File Offset: 0x00073B80
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null || !SystemInfo.supports3DTextures)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.converted3DLut == null)
			{
				if (!this.LutTexture)
				{
					this.SetIdentityLut();
				}
				if (this.LutTexture)
				{
					this.converted3DLut = this.Convert(this.LutTexture, this.converted3DLut);
				}
			}
			if (this.converted3DLut2 == null)
			{
				if (!this.LutTexture2)
				{
					this.SetIdentityLut();
				}
				if (this.LutTexture2)
				{
					this.converted3DLut2 = this.Convert(this.LutTexture2, this.converted3DLut2);
				}
			}
			if (this.LutTexture)
			{
				this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			}
			if (this.LutTexture2)
			{
				this.converted3DLut2.wrapMode = TextureWrapMode.Clamp;
			}
			this.material.SetFloat("_Blend", this.Blend);
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetTexture("_LutTex", this.converted3DLut);
			this.material.SetTexture("_LutTex2", this.converted3DLut2);
			Graphics.Blit(sourceTexture, destTexture, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void OnValidate()
	{
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x00075B06 File Offset: 0x00073D06
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012CD RID: 4813
	public Shader SCShader;

	// Token: 0x040012CE RID: 4814
	private float TimeX = 1f;

	// Token: 0x040012CF RID: 4815
	private Vector4 ScreenResolution;

	// Token: 0x040012D0 RID: 4816
	private Material SCMaterial;

	// Token: 0x040012D1 RID: 4817
	public Texture2D LutTexture;

	// Token: 0x040012D2 RID: 4818
	public Texture2D LutTexture2;

	// Token: 0x040012D3 RID: 4819
	private Texture3D converted3DLut;

	// Token: 0x040012D4 RID: 4820
	private Texture3D converted3DLut2;

	// Token: 0x040012D5 RID: 4821
	[Range(0f, 1f)]
	public float Blend = 1f;

	// Token: 0x040012D6 RID: 4822
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x040012D7 RID: 4823
	private string MemoPath;

	// Token: 0x040012D8 RID: 4824
	private string MemoPath2;
}
