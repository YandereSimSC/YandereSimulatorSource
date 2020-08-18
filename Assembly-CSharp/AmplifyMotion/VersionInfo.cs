using System;
using UnityEngine;

namespace AmplifyMotion
{
	// Token: 0x020004DB RID: 1243
	[Serializable]
	public class VersionInfo
	{
		// Token: 0x06001F65 RID: 8037 RVA: 0x00180DE5 File Offset: 0x0017EFE5
		public static string StaticToString()
		{
			return string.Format("{0}.{1}.{2}", 1, 8, 3) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00180E12 File Offset: 0x0017F012
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.m_major, this.m_minor, this.m_release) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00180E4E File Offset: 0x0017F04E
		public int Number
		{
			get
			{
				return this.m_major * 100 + this.m_minor * 10 + this.m_release;
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00180E6A File Offset: 0x0017F06A
		private VersionInfo()
		{
			this.m_major = 1;
			this.m_minor = 8;
			this.m_release = 3;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00180E87 File Offset: 0x0017F087
		private VersionInfo(byte major, byte minor, byte release)
		{
			this.m_major = (int)major;
			this.m_minor = (int)minor;
			this.m_release = (int)release;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00180EA4 File Offset: 0x0017F0A4
		public static VersionInfo Current()
		{
			return new VersionInfo(1, 8, 3);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00180EAE File Offset: 0x0017F0AE
		public static bool Matches(VersionInfo version)
		{
			return 1 == version.m_major && 8 == version.m_minor && 3 == version.m_release;
		}

		// Token: 0x04003CF3 RID: 15603
		public const byte Major = 1;

		// Token: 0x04003CF4 RID: 15604
		public const byte Minor = 8;

		// Token: 0x04003CF5 RID: 15605
		public const byte Release = 3;

		// Token: 0x04003CF6 RID: 15606
		private static string StageSuffix = "_dev001";

		// Token: 0x04003CF7 RID: 15607
		private static string TrialSuffix = "";

		// Token: 0x04003CF8 RID: 15608
		[SerializeField]
		private int m_major;

		// Token: 0x04003CF9 RID: 15609
		[SerializeField]
		private int m_minor;

		// Token: 0x04003CFA RID: 15610
		[SerializeField]
		private int m_release;
	}
}
