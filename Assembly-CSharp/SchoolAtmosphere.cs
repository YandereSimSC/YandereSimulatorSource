using System;

// Token: 0x020003CA RID: 970
public static class SchoolAtmosphere
{
	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x06001A48 RID: 6728 RVA: 0x001018A0 File Offset: 0x000FFAA0
	public static SchoolAtmosphereType Type
	{
		get
		{
			float schoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
			if (schoolAtmosphere > 0.6666667f)
			{
				return SchoolAtmosphereType.High;
			}
			if (schoolAtmosphere > 0.33333334f)
			{
				return SchoolAtmosphereType.Medium;
			}
			return SchoolAtmosphereType.Low;
		}
	}
}
