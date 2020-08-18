using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/PhotoshopFilters")]
public class CameraFilterPack_Blend2Camera_PhotoshopFilters : MonoBehaviour
{
	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0006507D File Offset: 0x0006327D
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

	// Token: 0x06000C12 RID: 3090 RVA: 0x000650B4 File Offset: 0x000632B4
	private void ChangeFilters()
	{
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Darken)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Darken";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Multiply)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Multiply";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.ColorBurn)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_ColorBurn";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.LinearBurn)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_LinearBurn";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.DarkerColor)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_DarkerColor";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Lighten)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Lighten";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Screen)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Screen";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.ColorDodge)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_ColorDodge";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.LinearDodge)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_LinearDodge";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.LighterColor)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_LighterColor";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Overlay)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Overlay";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.SoftLight)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_SoftLight";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.HardLight)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_HardLight";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.VividLight)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_VividLight";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.LinearLight)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_LinearLight";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.PinLight)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_PinLight";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.HardMix)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_HardMix";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Difference)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Difference";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Exclusion)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Exclusion";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Subtract)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Subtract";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Divide)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Divide";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Hue)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Hue";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Saturation)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Saturation";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Color)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Color";
		}
		if (this.filterchoice == CameraFilterPack_Blend2Camera_PhotoshopFilters.filters.Luminosity)
		{
			this.ShaderName = "CameraFilterPack/Blend2Camera_Luminosity";
		}
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x000652C4 File Offset: 0x000634C4
	private void Start()
	{
		this.filterchoicememo = this.filterchoice;
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.ChangeFilters();
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0006533C File Offset: 0x0006353C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.Camera2 != null)
			{
				this.material.SetTexture("_MainTex2", this.Camera2tex);
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0006542C File Offset: 0x0006362C
	private void OnValidate()
	{
		if (this.filterchoice != this.filterchoicememo)
		{
			this.ChangeFilters();
			this.SCShader = Shader.Find(this.ShaderName);
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
		}
		this.filterchoicememo = this.filterchoice;
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x000654D7 File Offset: 0x000636D7
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x0006550F File Offset: 0x0006370F
	private void OnDisable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000EBB RID: 3771
	private string ShaderName = "CameraFilterPack/Blend2Camera_Darken";

	// Token: 0x04000EBC RID: 3772
	public Shader SCShader;

	// Token: 0x04000EBD RID: 3773
	public Camera Camera2;

	// Token: 0x04000EBE RID: 3774
	public CameraFilterPack_Blend2Camera_PhotoshopFilters.filters filterchoice;

	// Token: 0x04000EBF RID: 3775
	private CameraFilterPack_Blend2Camera_PhotoshopFilters.filters filterchoicememo;

	// Token: 0x04000EC0 RID: 3776
	private float TimeX = 1f;

	// Token: 0x04000EC1 RID: 3777
	private Material SCMaterial;

	// Token: 0x04000EC2 RID: 3778
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EC3 RID: 3779
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EC4 RID: 3780
	private RenderTexture Camera2tex;

	// Token: 0x02000697 RID: 1687
	public enum filters
	{
		// Token: 0x04004618 RID: 17944
		Darken,
		// Token: 0x04004619 RID: 17945
		Multiply,
		// Token: 0x0400461A RID: 17946
		ColorBurn,
		// Token: 0x0400461B RID: 17947
		LinearBurn,
		// Token: 0x0400461C RID: 17948
		DarkerColor,
		// Token: 0x0400461D RID: 17949
		Lighten,
		// Token: 0x0400461E RID: 17950
		Screen,
		// Token: 0x0400461F RID: 17951
		ColorDodge,
		// Token: 0x04004620 RID: 17952
		LinearDodge,
		// Token: 0x04004621 RID: 17953
		LighterColor,
		// Token: 0x04004622 RID: 17954
		Overlay,
		// Token: 0x04004623 RID: 17955
		SoftLight,
		// Token: 0x04004624 RID: 17956
		HardLight,
		// Token: 0x04004625 RID: 17957
		VividLight,
		// Token: 0x04004626 RID: 17958
		LinearLight,
		// Token: 0x04004627 RID: 17959
		PinLight,
		// Token: 0x04004628 RID: 17960
		HardMix,
		// Token: 0x04004629 RID: 17961
		Difference,
		// Token: 0x0400462A RID: 17962
		Exclusion,
		// Token: 0x0400462B RID: 17963
		Subtract,
		// Token: 0x0400462C RID: 17964
		Divide,
		// Token: 0x0400462D RID: 17965
		Hue,
		// Token: 0x0400462E RID: 17966
		Saturation,
		// Token: 0x0400462F RID: 17967
		Color,
		// Token: 0x04004630 RID: 17968
		Luminosity
	}
}
