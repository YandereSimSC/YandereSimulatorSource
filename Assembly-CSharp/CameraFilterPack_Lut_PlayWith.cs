using System;
using UnityEngine;

// Token: 0x020001D1 RID: 465
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Lut/PlayWith")]
public class CameraFilterPack_Lut_PlayWith : MonoBehaviour
{
	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06001005 RID: 4101 RVA: 0x00076305 File Offset: 0x00074505
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

	// Token: 0x06001006 RID: 4102 RVA: 0x00076339 File Offset: 0x00074539
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Lut_PlayWith");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0007635C File Offset: 0x0007455C
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

	// Token: 0x06001008 RID: 4104 RVA: 0x0007586F File Offset: 0x00073A6F
	public bool ValidDimensions(Texture2D tex2d)
	{
		return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00076434 File Offset: 0x00074634
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

	// Token: 0x0600100A RID: 4106 RVA: 0x00076538 File Offset: 0x00074738
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
			this.material.SetTexture("_LutTex", this.converted3DLut);
			this.material.SetFloat("_Blend", this.Blend);
			this.material.SetFloat("_Intensity", this.OriginalIntensity);
			this.material.SetFloat("_Extra", this.ResultIntensity);
			this.material.SetFloat("_Extra2", this.FinalIntensity);
			Graphics.Blit(sourceTexture, destTexture, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void OnValidate()
	{
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0007663C File Offset: 0x0007483C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012F1 RID: 4849
	public Shader SCShader;

	// Token: 0x040012F2 RID: 4850
	private float TimeX = 1f;

	// Token: 0x040012F3 RID: 4851
	private Material SCMaterial;

	// Token: 0x040012F4 RID: 4852
	public Texture2D LutTexture;

	// Token: 0x040012F5 RID: 4853
	private Texture3D converted3DLut;

	// Token: 0x040012F6 RID: 4854
	[Range(0f, 1f)]
	public float Blend = 1f;

	// Token: 0x040012F7 RID: 4855
	[Range(0f, 3f)]
	public float OriginalIntensity = 1f;

	// Token: 0x040012F8 RID: 4856
	[Range(-1f, 1f)]
	public float ResultIntensity;

	// Token: 0x040012F9 RID: 4857
	[Range(-1f, 1f)]
	public float FinalIntensity;

	// Token: 0x040012FA RID: 4858
	private string MemoPath;
}
