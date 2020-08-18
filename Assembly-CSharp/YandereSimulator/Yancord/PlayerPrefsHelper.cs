using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004A1 RID: 1185
	public static class PlayerPrefsHelper
	{
		// Token: 0x06001E2E RID: 7726 RVA: 0x000B3B48 File Offset: 0x000B1D48
		public static void SetBool(string name, bool flag)
		{
			PlayerPrefs.SetInt(name, flag ? 1 : 0);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x001791F5 File Offset: 0x001773F5
		public static bool GetBool(string name)
		{
			return PlayerPrefs.GetInt(name) == 1;
		}
	}
}
