using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004EA RID: 1258
	public class Chair : MonoBehaviour
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x00184150 File Offset: 0x00182350
		public static Chairs AllChairs
		{
			get
			{
				if (Chair.chairs == null || Chair.chairs.Count == 0)
				{
					Chair.chairs = new Chairs();
					foreach (Chair item in UnityEngine.Object.FindObjectsOfType<Chair>())
					{
						Chair.chairs.Add(item);
					}
				}
				return Chair.chairs;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x001841A4 File Offset: 0x001823A4
		public static Chair RandomChair
		{
			get
			{
				Chairs chairs = new Chairs();
				foreach (Chair chair in Chair.AllChairs)
				{
					if (chair.available)
					{
						chairs.Add(chair);
					}
				}
				if (chairs.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, chairs.Count);
					chairs[index].available = false;
					return chairs[index];
				}
				return null;
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0018422C File Offset: 0x0018242C
		private void OnDestroy()
		{
			Chair.chairs = null;
		}

		// Token: 0x04003D81 RID: 15745
		private static Chairs chairs;

		// Token: 0x04003D82 RID: 15746
		public bool available = true;
	}
}
