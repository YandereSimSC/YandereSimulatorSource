using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C8 RID: 1224
	[Serializable]
	public class UserLutModel : PostProcessingModel
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0017E4AB File Offset: 0x0017C6AB
		// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x0017E4B3 File Offset: 0x0017C6B3
		public UserLutModel.Settings settings
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

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0017E4BC File Offset: 0x0017C6BC
		public override void Reset()
		{
			this.m_Settings = UserLutModel.Settings.defaultSettings;
		}

		// Token: 0x04003C87 RID: 15495
		[SerializeField]
		private UserLutModel.Settings m_Settings = UserLutModel.Settings.defaultSettings;

		// Token: 0x020006F5 RID: 1781
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x06002C1B RID: 11291 RVA: 0x001C8B94 File Offset: 0x001C6D94
			public static UserLutModel.Settings defaultSettings
			{
				get
				{
					return new UserLutModel.Settings
					{
						lut = null,
						contribution = 1f
					};
				}
			}

			// Token: 0x0400483E RID: 18494
			[Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
			public Texture2D lut;

			// Token: 0x0400483F RID: 18495
			[Range(0f, 1f)]
			[Tooltip("Blending factor.")]
			public float contribution;
		}
	}
}
