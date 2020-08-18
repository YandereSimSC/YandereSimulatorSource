using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	public class DepthOfFieldModel : PostProcessingModel
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0017E354 File Offset: 0x0017C554
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0017E35C File Offset: 0x0017C55C
		public DepthOfFieldModel.Settings settings
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

		// Token: 0x06001EDD RID: 7901 RVA: 0x0017E365 File Offset: 0x0017C565
		public override void Reset()
		{
			this.m_Settings = DepthOfFieldModel.Settings.defaultSettings;
		}

		// Token: 0x04003C80 RID: 15488
		[SerializeField]
		private DepthOfFieldModel.Settings m_Settings = DepthOfFieldModel.Settings.defaultSettings;

		// Token: 0x020006E7 RID: 1767
		public enum KernelSize
		{
			// Token: 0x04004809 RID: 18441
			Small,
			// Token: 0x0400480A RID: 18442
			Medium,
			// Token: 0x0400480B RID: 18443
			Large,
			// Token: 0x0400480C RID: 18444
			VeryLarge
		}

		// Token: 0x020006E8 RID: 1768
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x06002C14 RID: 11284 RVA: 0x001C8934 File Offset: 0x001C6B34
			public static DepthOfFieldModel.Settings defaultSettings
			{
				get
				{
					return new DepthOfFieldModel.Settings
					{
						focusDistance = 10f,
						aperture = 5.6f,
						focalLength = 50f,
						useCameraFov = false,
						kernelSize = DepthOfFieldModel.KernelSize.Medium
					};
				}
			}

			// Token: 0x0400480D RID: 18445
			[Min(0.1f)]
			[Tooltip("Distance to the point of focus.")]
			public float focusDistance;

			// Token: 0x0400480E RID: 18446
			[Range(0.05f, 32f)]
			[Tooltip("Ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
			public float aperture;

			// Token: 0x0400480F RID: 18447
			[Range(1f, 300f)]
			[Tooltip("Distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
			public float focalLength;

			// Token: 0x04004810 RID: 18448
			[Tooltip("Calculate the focal length automatically from the field-of-view value set on the camera. Using this setting isn't recommended.")]
			public bool useCameraFov;

			// Token: 0x04004811 RID: 18449
			[Tooltip("Convolution kernel size of the bokeh filter, which determines the maximum radius of bokeh. It also affects the performance (the larger the kernel is, the longer the GPU time is required).")]
			public DepthOfFieldModel.KernelSize kernelSize;
		}
	}
}
