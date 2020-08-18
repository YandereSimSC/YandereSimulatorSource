﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000025 RID: 37
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x00011D0F File Offset: 0x0000FF0F
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00011D38 File Offset: 0x0000FF38
	public InvGameItem GetItem(int slot)
	{
		if (slot >= this.items.Count)
		{
			return null;
		}
		return this.mItems[slot];
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00011D56 File Offset: 0x0000FF56
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00011D7C File Offset: 0x0000FF7C
	private void Start()
	{
		if (this.template != null)
		{
			int num = 0;
			Bounds bounds = default(Bounds);
			for (int i = 0; i < this.maxRows; i++)
			{
				for (int j = 0; j < this.maxColumns; j++)
				{
					GameObject gameObject = base.gameObject.AddChild(this.template);
					gameObject.transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.storage = this;
						component.slot = num;
					}
					bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
					if (++num >= this.maxItemCount)
					{
						if (this.background != null)
						{
							this.background.transform.localScale = bounds.size;
						}
						return;
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = bounds.size;
			}
		}
	}

	// Token: 0x0400027F RID: 639
	public int maxItemCount = 8;

	// Token: 0x04000280 RID: 640
	public int maxRows = 4;

	// Token: 0x04000281 RID: 641
	public int maxColumns = 4;

	// Token: 0x04000282 RID: 642
	public GameObject template;

	// Token: 0x04000283 RID: 643
	public UIWidget background;

	// Token: 0x04000284 RID: 644
	public int spacing = 128;

	// Token: 0x04000285 RID: 645
	public int padding = 10;

	// Token: 0x04000286 RID: 646
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
