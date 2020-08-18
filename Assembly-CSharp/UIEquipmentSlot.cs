using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000DA RID: 218 RVA: 0x000118F4 File Offset: 0x0000FAF4
	protected override InvGameItem observedItem
	{
		get
		{
			if (!(this.equipment != null))
			{
				return null;
			}
			return this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00011917 File Offset: 0x0000FB17
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.equipment != null))
		{
			return item;
		}
		return this.equipment.Replace(this.slot, item);
	}

	// Token: 0x04000274 RID: 628
	public InvEquipment equipment;

	// Token: 0x04000275 RID: 629
	public InvBaseItem.Slot slot;
}
