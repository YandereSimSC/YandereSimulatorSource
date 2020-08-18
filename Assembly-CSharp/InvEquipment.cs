using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
[AddComponentMenu("NGUI/Examples/Equipment")]
public class InvEquipment : MonoBehaviour
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060000FB RID: 251 RVA: 0x000121D8 File Offset: 0x000103D8
	public InvGameItem[] equippedItems
	{
		get
		{
			return this.mItems;
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x000121E0 File Offset: 0x000103E0
	public InvGameItem Replace(InvBaseItem.Slot slot, InvGameItem item)
	{
		InvBaseItem invBaseItem = (item != null) ? item.baseItem : null;
		if (slot == InvBaseItem.Slot.None)
		{
			if (item != null)
			{
				Debug.LogWarning("Can't equip \"" + item.name + "\" because it doesn't specify an item slot");
			}
			return item;
		}
		if (invBaseItem != null && invBaseItem.slot != slot)
		{
			return item;
		}
		if (this.mItems == null)
		{
			int num = 8;
			this.mItems = new InvGameItem[num];
		}
		InvGameItem result = this.mItems[slot - InvBaseItem.Slot.Weapon];
		this.mItems[slot - InvBaseItem.Slot.Weapon] = item;
		if (this.mAttachments == null)
		{
			this.mAttachments = base.GetComponentsInChildren<InvAttachmentPoint>();
		}
		int i = 0;
		int num2 = this.mAttachments.Length;
		while (i < num2)
		{
			InvAttachmentPoint invAttachmentPoint = this.mAttachments[i];
			if (invAttachmentPoint.slot == slot)
			{
				GameObject gameObject = invAttachmentPoint.Attach((invBaseItem != null) ? invBaseItem.attachment : null);
				if (invBaseItem != null && gameObject != null)
				{
					Renderer component = gameObject.GetComponent<Renderer>();
					if (component != null)
					{
						component.material.color = invBaseItem.color;
					}
				}
			}
			i++;
		}
		return result;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x000122E0 File Offset: 0x000104E0
	public InvGameItem Equip(InvGameItem item)
	{
		if (item != null)
		{
			InvBaseItem baseItem = item.baseItem;
			if (baseItem != null)
			{
				return this.Replace(baseItem.slot, item);
			}
			Debug.LogWarning("Can't resolve the item ID of " + item.baseItemID);
		}
		return item;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00012324 File Offset: 0x00010524
	public InvGameItem Unequip(InvGameItem item)
	{
		if (item != null)
		{
			InvBaseItem baseItem = item.baseItem;
			if (baseItem != null)
			{
				return this.Replace(baseItem.slot, null);
			}
		}
		return item;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0001234D File Offset: 0x0001054D
	public InvGameItem Unequip(InvBaseItem.Slot slot)
	{
		return this.Replace(slot, null);
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00012358 File Offset: 0x00010558
	public bool HasEquipped(InvGameItem item)
	{
		if (this.mItems != null)
		{
			int i = 0;
			int num = this.mItems.Length;
			while (i < num)
			{
				if (this.mItems[i] == item)
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00012390 File Offset: 0x00010590
	public bool HasEquipped(InvBaseItem.Slot slot)
	{
		if (this.mItems != null)
		{
			int i = 0;
			int num = this.mItems.Length;
			while (i < num)
			{
				InvBaseItem baseItem = this.mItems[i].baseItem;
				if (baseItem != null && baseItem.slot == slot)
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x000123D8 File Offset: 0x000105D8
	public InvGameItem GetItem(InvBaseItem.Slot slot)
	{
		if (slot != InvBaseItem.Slot.None)
		{
			int num = slot - InvBaseItem.Slot.Weapon;
			if (this.mItems != null && num < this.mItems.Length)
			{
				return this.mItems[num];
			}
		}
		return null;
	}

	// Token: 0x0400029C RID: 668
	private InvGameItem[] mItems;

	// Token: 0x0400029D RID: 669
	private InvAttachmentPoint[] mAttachments;
}
