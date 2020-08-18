using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000EB RID: 235 RVA: 0x00011F21 File Offset: 0x00010121
	protected override InvGameItem observedItem
	{
		get
		{
			if (!(this.storage != null))
			{
				return null;
			}
			return this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00011F44 File Offset: 0x00010144
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.storage != null))
		{
			return item;
		}
		return this.storage.Replace(this.slot, item);
	}

	// Token: 0x04000287 RID: 647
	public UIItemStorage storage;

	// Token: 0x04000288 RID: 648
	public int slot;
}
