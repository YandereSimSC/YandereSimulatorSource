using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Lut/Plus")]
public class CameraFilterPack_Lut_Plus : MonoBehaviour
{
	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x0600100F RID: 4111 RVA: 0x0007667F File Offset: 0x0007487F
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

	// Token: 0x06001010 RID: 4112 RVA: 0x000766B3 File Offset: 0x000748B3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Lut_Plus");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x000766D4 File Offset: 0x000748D4
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
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0007586F File Offset: 0x00073A6F
	public bool ValidDimensions(Texture2D tex2d)
	{
		return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x000767AC File Offset: 0x000749AC
	public void Convert(Texture2D temp2DTex)
	{
		if (!temp2DTex)
		{
			this.SetIdentityLut();
			return;
		}
		int num = temp2DTex.width * temp2DTex.height;
		num = temp2DTex.height;
		if (!this.ValidDimensions(temp2DTex))
		{
			Debug.LogWarning("The given 2D texture " + temp2DTex.name + " cannot be used as a 3D LUT.");
			return;
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
		if (this.converted3DLut)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut.SetPixels(array);
		this.converted3DLut.Apply();
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x000768B0 File Offset: 0x00074AB0
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
				this.Convert(this.LutTexture);
			}
			this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			this.material.SetFloat("_Blend", this.Blend);
			this.material.SetTexture("_LutTex", this.converted3DLut);
			Graphics.Blit(sourceTexture, destTexture, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void OnValidate()
	{
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x00076972 File Offset: 0x00074B72
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012FB RID: 4859
	public Shader SCShader;

	// Token: 0x040012FC RID: 4860
	private float TimeX = 1f;

	// Token: 0x040012FD RID: 4861
	private Vector4 ScreenResolution;

	// Token: 0x040012FE RID: 4862
	private Material SCMaterial;

	// Token: 0x040012FF RID: 4863
	public Texture2D LutTexture;

	// Token: 0x04001300 RID: 4864
	private Texture3D converted3DLut;

	// Token: 0x04001301 RID: 4865
	[Range(0f, 1f)]
	public float Blend = 1f;

	// Token: 0x04001302 RID: 4866
	private string MemoPath;
}
