using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C7 RID: 1223
	[Serializable]
	public class ScreenSpaceReflectionModel : PostProcessingModel
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x0017E47A File Offset: 0x0017C67A
		// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x0017E482 File Offset: 0x0017C682
		public ScreenSpaceReflectionModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0017E48B File Offset: 0x0017C68B
		public override void Reset()
		{
			this.m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;
		}

		// Token: 0x04003C86 RID: 15494
		[SerializeField]
		private ScreenSpaceReflectionModel.Settings m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;

		// Token: 0x020006EF RID: 1775
		public enum SSRResolution
		{
			// Token: 0x04004829 RID: 18473
			High,
			// Token: 0x0400482A RID: 18474
			Low = 2
		}

		// Token: 0x020006F0 RID: 1776
		public enum SSRReflectionBlendType
		{
			// Token: 0x0400482C RID: 18476
			PhysicallyBased,
			// Token: 0x0400482D RID: 18477
			Additive
		}

		// Token: 0x020006F1 RID: 1777
		[Serializable]
		public struct IntensitySettings
		{
			// Token: 0x0400482E RID: 18478
			[Tooltip("Nonphysical multiplier for the SSR reflections. 1.0 is physically based.")]
			[Range(0f, 2f)]
			public float reflectionMultiplier;

			// Token: 0x0400482F RID: 18479
			[Tooltip("How far away from the maxDistance to begin fading SSR.")]
			[Range(0f, 1000f)]
			public float fadeDistance;

			// Token: 0x04004830 RID: 18480
			[Tooltip("Amplify Fresnel fade out. Increase if floor reflections look good close to the surface and bad farther 'under' the floor.")]
			[Range(0f, 1f)]
			public float fresnelFade;

			// Token: 0x04004831 RID: 18481
			[Tooltip("Higher values correspond to a faster Fresnel fade as the reflection changes from the grazing angle.")]
			[Range(0.1f, 10f)]
			public float fresnelFadePower;
		}

		// Token: 0x020006F2 RID: 1778
		[Serializable]
		public struct ReflectionSettings
		{
			// Token: 0x04004832 RID: 18482
			[Tooltip("How the reflections are blended into the render.")]
			public ScreenSpaceReflectionModel.SSRReflectionBlendType blendType;

			// Token: 0x04004833 RID: 18483
			[Tooltip("Half resolution SSRR is much faster, but less accurate.")]
			public ScreenSpaceReflectionModel.SSRResolution reflectionQuality;

			// Token: 0x04004834 RID: 18484
			[Tooltip("Maximum reflection distance in world units.")]
			[Range(0.1f, 300f)]
			public float maxDistance;

			// Token: 0x04004835 RID: 18485
			[Tooltip("Max raytracing length.")]
			[Range(16f, 1024f)]
			public int iterationCount;

			// Token: 0x04004836 RID: 18486
			[Tooltip("Log base 2 of ray tracing coarse step size. Higher traces farther, lower gives better quality silhouettes.")]
			[Range(1f, 16f)]
			public int stepSize;

			// Token: 0x04004837 RID: 18487
			[Tooltip("Typical thickness of columns, walls, furniture, and other objects that reflection rays might pass behind.")]
			[Range(0.01f, 10f)]
			public float widthModifier;

			// Token: 0x04004838 RID: 18488
			[Tooltip("Blurriness of reflections.")]
			[Range(0.1f, 8f)]
			public float reflectionBlur;

			// Token: 0x04004839 RID: 18489
			[Tooltip("Disable for a performance gain in scenes where most glossy objects are horizontal, like floors, water, and tables. Leave on for scenes with glossy vertical objects.")]
			public bool reflectBackfaces;
		}

		// Token: 0x020006F3 RID: 1779
		[Serializable]
		public struct ScreenEdgeMask
		{
			// Token: 0x0400483A RID: 18490
			[Tooltip("Higher = fade out SSRR near the edge of the screen so that reflections don't pop under camera motion.")]
			[Range(0f, 1f)]
			public float intensity;
		}

		// Token: 0x020006F4 RID: 1780
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x06002C1A RID: 11290 RVA: 0x001C8AC0 File Offset: 0x001C6CC0
			public static ScreenSpaceReflectionModel.Settings defaultSettings
			{
				get
				{
					return new ScreenSpaceReflectionModel.Settings
					{
						reflection = new ScreenSpaceReflectionModel.ReflectionSettings
						{
							blendType = ScreenSpaceReflectionModel.SSRReflectionBlendType.PhysicallyBased,
							reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low,
							maxDistance = 100f,
							iterationCount = 256,
							stepSize = 3,
							widthModifier = 0.5f,
							reflectionBlur = 1f,
							reflectBackfaces = false
						},
						intensity = new ScreenSpaceReflectionModel.IntensitySettings
						{
							reflectionMultiplier = 1f,
							fadeDistance = 100f,
							fresnelFade = 1f,
							fresnelFadePower = 1f
						},
						screenEdgeMask = new ScreenSpaceReflectionModel.ScreenEdgeMask
						{
							intensity = 0.03f
						}
					};
				}
			}

			// Token: 0x0400483B RID: 18491
			public ScreenSpaceReflectionModel.ReflectionSettings reflection;

			// Token: 0x0400483C RID: 18492
			public ScreenSpaceReflectionModel.IntensitySettings intensity;

			// Token: 0x0400483D RID: 18493
			public ScreenSpaceReflectionModel.ScreenEdgeMask screenEdgeMask;
		}
	}
}
