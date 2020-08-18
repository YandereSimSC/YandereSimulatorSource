using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000021 RID: 33
[AddComponentMenu("NGUI/Examples/Equip Random Item")]
public class EquipRandomItem : MonoBehaviour
{
	// Token: 0x060000D1 RID: 209 RVA: 0x0001160C File Offset: 0x0000F80C
	private void OnClick()
	{
		if (this.equipment == null)
		{
			return;
		}
		List<InvBaseItem> items = InvDatabase.list[0].items;
		if (items.Count == 0)
		{
			return;
		}
		int max = 12;
		int num = UnityEngine.Random.Range(0, items.Count);
		InvBaseItem invBaseItem = items[num];
		InvGameItem invGameItem = new InvGameItem(num, invBaseItem);
		invGameItem.quality = (InvGameItem.Quality)UnityEngine.Random.Range(0, max);
		invGameItem.itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel);
		this.equipment.Equip(invGameItem);
	}

	// Token: 0x0400026D RID: 621
	public InvEquipment equipment;
}
