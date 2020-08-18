using System;
using UnityEngine;

// Token: 0x020001D4 RID: 468
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Lut/TestMode")]
public class CameraFilterPack_Lut_TestMode : MonoBehaviour
{
	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06001023 RID: 4131 RVA: 0x00076CB5 File Offset: 0x00074EB5
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

	// Token: 0x06001024 RID: 4132 RVA: 0x00076CE9 File Offset: 0x00074EE9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Lut_TestMode");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x00076D0C File Offset: 0x00074F0C
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

	// Token: 0x06001026 RID: 4134 RVA: 0x0007586F File Offset: 0x00073A6F
	public bool ValidDimensions(Texture2D tex2d)
	{
		return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x00076DE4 File Offset: 0x00074FE4
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

	// Token: 0x06001028 RID: 4136 RVA: 0x00076EE8 File Offset: 0x000750E8
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
			this.material.SetFloat("_Extra3", this.TestMode);
			Graphics.Blit(sourceTexture, destTexture, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void OnValidate()
	{
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x00077002 File Offset: 0x00075202
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001309 RID: 4873
	public Shader SCShader;

	// Token: 0x0400130A RID: 4874
	private float TimeX = 1f;

	// Token: 0x0400130B RID: 4875
	private Material SCMaterial;

	// Token: 0x0400130C RID: 4876
	public Texture2D LutTexture;

	// Token: 0x0400130D RID: 4877
	private Texture3D converted3DLut;

	// Token: 0x0400130E RID: 4878
	[Range(0f, 1f)]
	public float Blend = 1f;

	// Token: 0x0400130F RID: 4879
	[Range(0f, 3f)]
	public float OriginalIntensity = 1f;

	// Token: 0x04001310 RID: 4880
	[Range(-1f, 1f)]
	public float ResultIntensity;

	// Token: 0x04001311 RID: 4881
	[Range(-1f, 1f)]
	public float FinalIntensity;

	// Token: 0x04001312 RID: 4882
	[Range(0f, 1f)]
	public float TestMode = 0.5f;

	// Token: 0x04001313 RID: 4883
	private string MemoPath;
}
