using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class RealTime : MonoBehaviour
{
	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0002CD63 File Offset: 0x0002AF63
	public static float time
	{
		get
		{
			return Time.unscaledTime;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0002CD6A File Offset: 0x0002AF6A
	public static float deltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}
