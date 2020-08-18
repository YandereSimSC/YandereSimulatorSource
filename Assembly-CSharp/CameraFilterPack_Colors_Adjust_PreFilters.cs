using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/ColorsAdjust/Photo Filters")]
public class CameraFilterPack_Colors_Adjust_PreFilters : MonoBehaviour
{
	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00069FC6 File Offset: 0x000681C6
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

	// Token: 0x06000D2D RID: 3373 RVA: 0x00069FFC File Offset: 0x000681FC
	private void ChangeFilters()
	{
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Normal)
		{
			this.Matrix9 = new float[]
			{
				100f,
				0f,
				0f,
				0f,
				100f,
				0f,
				0f,
				0f,
				100f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Deuteranomaly)
		{
			this.Matrix9 = new float[]
			{
				80f,
				20f,
				0f,
				25.833f,
				74.167f,
				0f,
				0f,
				14.167f,
				85.833f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Protanopia)
		{
			this.Matrix9 = new float[]
			{
				56.667f,
				43.333f,
				0f,
				55.833f,
				44.167f,
				0f,
				0f,
				24.167f,
				75.833f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Protanomaly)
		{
			this.Matrix9 = new float[]
			{
				81.667f,
				18.333f,
				0f,
				33.333f,
				66.667f,
				0f,
				0f,
				12.5f,
				87.5f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Deuteranopia)
		{
			this.Matrix9 = new float[]
			{
				62.5f,
				37.5f,
				0f,
				70f,
				30f,
				0f,
				0f,
				30f,
				70f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Tritanomaly)
		{
			this.Matrix9 = new float[]
			{
				96.667f,
				3.333f,
				0f,
				0f,
				73.333f,
				26.667f,
				0f,
				18.333f,
				81.667f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Achromatopsia)
		{
			this.Matrix9 = new float[]
			{
				29.9f,
				58.7f,
				11.4f,
				29.9f,
				58.7f,
				11.4f,
				29.9f,
				58.7f,
				11.4f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Achromatomaly)
		{
			this.Matrix9 = new float[]
			{
				61.8f,
				32f,
				6.2f,
				16.3f,
				77.5f,
				6.2f,
				16.3f,
				32f,
				51.6f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Blindness_Tritanopia)
		{
			this.Matrix9 = new float[]
			{
				95f,
				5f,
				0f,
				0f,
				43.333f,
				56.667f,
				0f,
				47.5f,
				52.5f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlueLagoon)
		{
			this.Matrix9 = new float[]
			{
				100f,
				102f,
				0f,
				18f,
				100f,
				4f,
				28f,
				-26f,
				100f,
				-64f,
				0f,
				12f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.GoldenPink)
		{
			this.Matrix9 = new float[]
			{
				70f,
				200f,
				0f,
				0f,
				100f,
				8f,
				6f,
				12f,
				110f,
				0f,
				0f,
				-6f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlueMoon)
		{
			this.Matrix9 = new float[]
			{
				200f,
				98f,
				-116f,
				24f,
				100f,
				2f,
				30f,
				52f,
				20f,
				-48f,
				-20f,
				12f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.DarkPink)
		{
			this.Matrix9 = new float[]
			{
				60f,
				112f,
				36f,
				24f,
				100f,
				2f,
				0f,
				-26f,
				100f,
				-56f,
				-20f,
				12f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.RedWhite)
		{
			this.Matrix9 = new float[]
			{
				-42f,
				68f,
				108f,
				-96f,
				100f,
				116f,
				-92f,
				104f,
				96f,
				0f,
				2f,
				4f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.VintageYellow)
		{
			this.Matrix9 = new float[]
			{
				200f,
				109f,
				-104f,
				42f,
				126f,
				-1f,
				-40f,
				121f,
				-31f,
				-48f,
				-20f,
				12f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.NashVille)
		{
			this.Matrix9 = new float[]
			{
				130f,
				8f,
				7f,
				19f,
				89f,
				3f,
				-1f,
				11f,
				57f,
				10f,
				19f,
				47f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.PopRocket)
		{
			this.Matrix9 = new float[]
			{
				100f,
				6f,
				-17f,
				0f,
				107f,
				0f,
				10f,
				21f,
				100f,
				40f,
				0f,
				8f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.RedSoftLight)
		{
			this.Matrix9 = new float[]
			{
				-4f,
				200f,
				-30f,
				-58f,
				200f,
				-30f,
				-58f,
				200f,
				-30f,
				-11f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlackAndWhite_Blue)
		{
			this.Matrix9 = new float[]
			{
				0f,
				0f,
				100f,
				0f,
				0f,
				100f,
				0f,
				0f,
				100f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlackAndWhite_Green)
		{
			this.Matrix9 = new float[]
			{
				0f,
				100f,
				0f,
				0f,
				100f,
				0f,
				0f,
				100f,
				0f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlackAndWhite_Orange)
		{
			this.Matrix9 = new float[]
			{
				50f,
				50f,
				0f,
				50f,
				50f,
				0f,
				50f,
				50f,
				0f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlackAndWhite_Red)
		{
			this.Matrix9 = new float[]
			{
				100f,
				0f,
				0f,
				100f,
				0f,
				0f,
				100f,
				0f,
				0f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlackAndWhite_Yellow)
		{
			this.Matrix9 = new float[]
			{
				34f,
				66f,
				0f,
				34f,
				66f,
				0f,
				34f,
				66f,
				0f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.YellowSunSet)
		{
			this.Matrix9 = new float[]
			{
				117f,
				-6f,
				53f,
				-68f,
				135f,
				19f,
				-146f,
				-61f,
				200f,
				0f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Walden)
		{
			this.Matrix9 = new float[]
			{
				99f,
				2f,
				13f,
				100f,
				1f,
				40f,
				50f,
				8f,
				71f,
				0f,
				2f,
				7f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.WhiteShine)
		{
			this.Matrix9 = new float[]
			{
				190f,
				24f,
				-33f,
				2f,
				200f,
				-6f,
				-10f,
				27f,
				200f,
				-6f,
				-13f,
				15f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Fluo)
		{
			this.Matrix9 = new float[]
			{
				100f,
				0f,
				0f,
				0f,
				113f,
				0f,
				200f,
				-200f,
				-200f,
				0f,
				0f,
				36f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.MarsSunRise)
		{
			this.Matrix9 = new float[]
			{
				50f,
				141f,
				-81f,
				-17f,
				62f,
				29f,
				-159f,
				-137f,
				-200f,
				7f,
				-34f,
				-6f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.Amelie)
		{
			this.Matrix9 = new float[]
			{
				100f,
				11f,
				39f,
				1f,
				63f,
				53f,
				-24f,
				71f,
				20f,
				-25f,
				-10f,
				-24f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlueJeans)
		{
			this.Matrix9 = new float[]
			{
				181f,
				11f,
				15f,
				40f,
				40f,
				20f,
				40f,
				40f,
				20f,
				-59f,
				0f,
				0f
			};
		}
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.NightVision)
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
		if (this.filterchoice == CameraFilterPack_Colors_Adjust_PreFilters.filters.BlueParadise)
		{
			this.Matrix9 = new float[]
			{
				66f,
				200f,
				-200f,
				25f,
				38f,
				36f,
				30f,
				150f,
				114f,
				17f,
				0f,
				65f
			};
		}
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0006A43F File Offset: 0x0006863F
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

	// Token: 0x06000D2F RID: 3375 RVA: 0x0006A468 File Offset: 0x00068668
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

	// Token: 0x06000D30 RID: 3376 RVA: 0x0006A689 File Offset: 0x00068889
	private void OnValidate()
	{
		this.ChangeFilters();
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0006A691 File Offset: 0x00068891
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FEE RID: 4078
	private string ShaderName = "CameraFilterPack/Colors_Adjust_PreFilters";

	// Token: 0x04000FEF RID: 4079
	public Shader SCShader;

	// Token: 0x04000FF0 RID: 4080
	public CameraFilterPack_Colors_Adjust_PreFilters.filters filterchoice;

	// Token: 0x04000FF1 RID: 4081
	[Range(0f, 1f)]
	public float FadeFX = 1f;

	// Token: 0x04000FF2 RID: 4082
	private float TimeX = 1f;

	// Token: 0x04000FF3 RID: 4083
	private Material SCMaterial;

	// Token: 0x04000FF4 RID: 4084
	private float[] Matrix9;

	// Token: 0x02000698 RID: 1688
	public enum filters
	{
		// Token: 0x04004632 RID: 17970
		Normal,
		// Token: 0x04004633 RID: 17971
		BlueLagoon,
		// Token: 0x04004634 RID: 17972
		BlueMoon,
		// Token: 0x04004635 RID: 17973
		RedWhite,
		// Token: 0x04004636 RID: 17974
		NashVille,
		// Token: 0x04004637 RID: 17975
		VintageYellow,
		// Token: 0x04004638 RID: 17976
		GoldenPink,
		// Token: 0x04004639 RID: 17977
		DarkPink,
		// Token: 0x0400463A RID: 17978
		PopRocket,
		// Token: 0x0400463B RID: 17979
		RedSoftLight,
		// Token: 0x0400463C RID: 17980
		YellowSunSet,
		// Token: 0x0400463D RID: 17981
		Walden,
		// Token: 0x0400463E RID: 17982
		WhiteShine,
		// Token: 0x0400463F RID: 17983
		Fluo,
		// Token: 0x04004640 RID: 17984
		MarsSunRise,
		// Token: 0x04004641 RID: 17985
		Amelie,
		// Token: 0x04004642 RID: 17986
		BlueJeans,
		// Token: 0x04004643 RID: 17987
		NightVision,
		// Token: 0x04004644 RID: 17988
		BlueParadise,
		// Token: 0x04004645 RID: 17989
		Blindness_Deuteranomaly,
		// Token: 0x04004646 RID: 17990
		Blindness_Protanopia,
		// Token: 0x04004647 RID: 17991
		Blindness_Protanomaly,
		// Token: 0x04004648 RID: 17992
		Blindness_Deuteranopia,
		// Token: 0x04004649 RID: 17993
		Blindness_Tritanomaly,
		// Token: 0x0400464A RID: 17994
		Blindness_Achromatopsia,
		// Token: 0x0400464B RID: 17995
		Blindness_Achromatomaly,
		// Token: 0x0400464C RID: 17996
		Blindness_Tritanopia,
		// Token: 0x0400464D RID: 17997
		BlackAndWhite_Blue,
		// Token: 0x0400464E RID: 17998
		BlackAndWhite_Green,
		// Token: 0x0400464F RID: 17999
		BlackAndWhite_Orange,
		// Token: 0x04004650 RID: 18000
		BlackAndWhite_Red,
		// Token: 0x04004651 RID: 18001
		BlackAndWhite_Yellow
	}
}
