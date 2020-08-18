using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C3 RID: 1219
	[Serializable]
	public class EyeAdaptationModel : PostProcessingModel
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0017E3B6 File Offset: 0x0017C5B6
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x0017E3BE File Offset: 0x0017C5BE
		public EyeAdaptationModel.Settings settings
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

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0017E3C7 File Offset: 0x0017C5C7
		public override void Reset()
		{
			this.m_Settings = EyeAdaptationModel.Settings.defaultSettings;
		}

		// Token: 0x04003C82 RID: 15490
		[SerializeField]
		private EyeAdaptationModel.Settings m_Settings = EyeAdaptationModel.Settings.defaultSettings;

		// Token: 0x020006EA RID: 1770
		public enum EyeAdaptationType
		{
			// Token: 0x04004813 RID: 18451
			Progressive,
			// Token: 0x04004814 RID: 18452
			Fixed
		}

		// Token: 0x020006EB RID: 1771
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x06002C16 RID: 11286 RVA: 0x001C8998 File Offset: 0x001C6B98
			public static EyeAdaptationModel.Settings defaultSettings
			{
				get
				{
					return new EyeAdaptationModel.Settings
					{
						lowPercent = 45f,
						highPercent = 95f,
						minLuminance = -5f,
						maxLuminance = 1f,
						keyValue = 0.25f,
						dynamicKeyValue = true,
						adaptationType = EyeAdaptationModel.EyeAdaptationType.Progressive,
						speedUp = 2f,
						speedDown = 1f,
						logMin = -8,
						logMax = 4
					};
				}
			}

			// Token: 0x04004815 RID: 18453
			[Range(1f, 99f)]
			[Tooltip("Filters the dark part of the histogram when computing the average luminance to avoid very dark pixels from contributing to the auto exposure. Unit is in percent.")]
			public float lowPercent;

			// Token: 0x04004816 RID: 18454
			[Range(1f, 99f)]
			[Tooltip("Filters the bright part of the histogram when computing the average luminance to avoid very dark pixels from contributing to the auto exposure. Unit is in percent.")]
			public float highPercent;

			// Token: 0x04004817 RID: 18455
			[Tooltip("Minimum average luminance to consider for auto exposure (in EV).")]
			public float minLuminance;

			// Token: 0x04004818 RID: 18456
			[Tooltip("Maximum average luminance to consider for auto exposure (in EV).")]
			public float maxLuminance;

			// Token: 0x04004819 RID: 18457
			[Min(0f)]
			[Tooltip("Exposure bias. Use this to offset the global exposure of the scene.")]
			public float keyValue;

			// Token: 0x0400481A RID: 18458
			[Tooltip("Set this to true to let Unity handle the key value automatically based on average luminance.")]
			public bool dynamicKeyValue;

			// Token: 0x0400481B RID: 18459
			[Tooltip("Use \"Progressive\" if you want the auto exposure to be animated. Use \"Fixed\" otherwise.")]
			public EyeAdaptationModel.EyeAdaptationType adaptationType;

			// Token: 0x0400481C RID: 18460
			[Min(0f)]
			[Tooltip("Adaptation speed from a dark to a light environment.")]
			public float speedUp;

			// Token: 0x0400481D RID: 18461
			[Min(0f)]
			[Tooltip("Adaptation speed from a light to a dark environment.")]
			public float speedDown;

			// Token: 0x0400481E RID: 18462
			[Range(-16f, -1f)]
			[Tooltip("Lower bound for the brightness range of the generated histogram (in EV). The bigger the spread between min & max, the lower the precision will be.")]
			public int logMin;

			// Token: 0x0400481F RID: 18463
			[Range(1f, 16f)]
			[Tooltip("Upper bound for the brightness range of the generated histogram (in EV). The bigger the spread between min & max, the lower the precision will be.")]
			public int logMax;
		}
	}
}
