using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C6 RID: 1222
	[Serializable]
	public class MotionBlurModel : PostProcessingModel
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0017E449 File Offset: 0x0017C649
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x0017E451 File Offset: 0x0017C651
		public MotionBlurModel.Settings settings
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

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0017E45A File Offset: 0x0017C65A
		public override void Reset()
		{
			this.m_Settings = MotionBlurModel.Settings.defaultSettings;
		}

		// Token: 0x04003C85 RID: 15493
		[SerializeField]
		private MotionBlurModel.Settings m_Settings = MotionBlurModel.Settings.defaultSettings;

		// Token: 0x020006EE RID: 1774
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x06002C19 RID: 11289 RVA: 0x001C8A88 File Offset: 0x001C6C88
			public static MotionBlurModel.Settings defaultSettings
			{
				get
				{
					return new MotionBlurModel.Settings
					{
						shutterAngle = 270f,
						sampleCount = 10,
						frameBlending = 0f
					};
				}
			}

			// Token: 0x04004825 RID: 18469
			[Range(0f, 360f)]
			[Tooltip("The angle of rotary shutter. Larger values give longer exposure.")]
			public float shutterAngle;

			// Token: 0x04004826 RID: 18470
			[Range(4f, 32f)]
			[Tooltip("The amount of sample points, which affects quality and performances.")]
			public int sampleCount;

			// Token: 0x04004827 RID: 18471
			[Range(0f, 1f)]
			[Tooltip("The strength of multiple frame blending. The opacity of preceding frames are determined from this coefficient and time differences.")]
			public float frameBlending;
		}
	}
}
