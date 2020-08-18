using System;
using UnityEngine;

// Token: 0x020001D0 RID: 464
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Lut/Mask")]
public class CameraFilterPack_Lut_Mask : MonoBehaviour
{
	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00075FA0 File Offset: 0x000741A0
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

	// Token: 0x06000FFC RID: 4092 RVA: 0x00075FD4 File Offset: 0x000741D4
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Lut_Mask");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x00075FF8 File Offset: 0x000741F8
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

	// Token: 0x06000FFE RID: 4094 RVA: 0x0007586F File Offset: 0x00073A6F
	public bool ValidDimensions(Texture2D tex2d)
	{
		return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x000760D0 File Offset: 0x000742D0
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

	// Token: 0x06001000 RID: 4096 RVA: 0x000761D4 File Offset: 0x000743D4
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
			this.material.SetTexture("_MaskTex", this.Mask);
			this.material.SetFloat("_Inverse", this.Inverse);
			Graphics.Blit(sourceTexture, destTexture, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void OnValidate()
	{
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x000762C2 File Offset: 0x000744C2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012E7 RID: 4839
	public Shader SCShader;

	// Token: 0x040012E8 RID: 4840
	private float TimeX = 1f;

	// Token: 0x040012E9 RID: 4841
	private Vector4 ScreenResolution;

	// Token: 0x040012EA RID: 4842
	private Material SCMaterial;

	// Token: 0x040012EB RID: 4843
	public Texture2D LutTexture;

	// Token: 0x040012EC RID: 4844
	private Texture3D converted3DLut;

	// Token: 0x040012ED RID: 4845
	[Range(0f, 1f)]
	public float Blend = 1f;

	// Token: 0x040012EE RID: 4846
	public Texture2D Mask;

	// Token: 0x040012EF RID: 4847
	[Range(0f, 1f)]
	public float Inverse = 1f;

	// Token: 0x040012F0 RID: 4848
	private string MemoPath;
}
