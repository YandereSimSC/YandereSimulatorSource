using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	public class GrainModel : PostProcessingModel
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0017E418 File Offset: 0x0017C618
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x0017E420 File Offset: 0x0017C620
		public GrainModel.Settings settings
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

		// Token: 0x06001EED RID: 7917 RVA: 0x0017E429 File Offset: 0x0017C629
		public override void Reset()
		{
			this.m_Settings = GrainModel.Settings.defaultSettings;
		}

		// Token: 0x04003C84 RID: 15492
		[SerializeField]
		private GrainModel.Settings m_Settings = GrainModel.Settings.defaultSettings;

		// Token: 0x020006ED RID: 1773
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x06002C18 RID: 11288 RVA: 0x001C8A44 File Offset: 0x001C6C44
			public static GrainModel.Settings defaultSettings
			{
				get
				{
					return new GrainModel.Settings
					{
						colored = true,
						intensity = 0.5f,
						size = 1f,
						luminanceContribution = 0.8f
					};
				}
			}

			// Token: 0x04004821 RID: 18465
			[Tooltip("Enable the use of colored grain.")]
			public bool colored;

			// Token: 0x04004822 RID: 18466
			[Range(0f, 1f)]
			[Tooltip("Grain strength. Higher means more visible grain.")]
			public float intensity;

			// Token: 0x04004823 RID: 18467
			[Range(0.3f, 3f)]
			[Tooltip("Grain particle size.")]
			public float size;

			// Token: 0x04004824 RID: 18468
			[Range(0f, 1f)]
			[Tooltip("Controls the noisiness response curve based on scene luminance. Lower values mean less noise in dark areas.")]
			public float luminanceContribution;
		}
	}
}
