using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Rain_Pro")]
public class CameraFilterPack_Atmosphere_Rain_Pro : MonoBehaviour
{
	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00061693 File Offset: 0x0005F893
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

	// Token: 0x06000B4A RID: 2890 RVA: 0x000616C7 File Offset: 0x0005F8C7
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Rain_Pro");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x00061700 File Offset: 0x0005F900
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value3", this.DirectionX);
			this.material.SetFloat("_Value4", this.Speed);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Value6", this.Distortion);
			this.material.SetFloat("_Value7", this.StormFlashOnOff);
			this.material.SetFloat("_Value8", this.DropOnOff);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00061866 File Offset: 0x0005FA66
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DC9 RID: 3529
	public Shader SCShader;

	// Token: 0x04000DCA RID: 3530
	private float TimeX = 1f;

	// Token: 0x04000DCB RID: 3531
	private Material SCMaterial;

	// Token: 0x04000DCC RID: 3532
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000DCD RID: 3533
	[Range(0f, 2f)]
	public float Intensity = 0.5f;

	// Token: 0x04000DCE RID: 3534
	[Range(-0.25f, 0.25f)]
	public float DirectionX = 0.12f;

	// Token: 0x04000DCF RID: 3535
	[Range(0.4f, 2f)]
	public float Size = 1.5f;

	// Token: 0x04000DD0 RID: 3536
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000DD1 RID: 3537
	[Range(0f, 0.5f)]
	public float Distortion = 0.025f;

	// Token: 0x04000DD2 RID: 3538
	[Range(0f, 1f)]
	public float StormFlashOnOff = 1f;

	// Token: 0x04000DD3 RID: 3539
	[Range(0f, 1f)]
	public float DropOnOff = 1f;

	// Token: 0x04000DD4 RID: 3540
	private Texture2D Texture2;
}
