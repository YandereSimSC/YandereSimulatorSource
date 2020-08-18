using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
public abstract class UIItemSlot : MonoBehaviour
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060000DD RID: 221
	protected abstract InvGameItem observedItem { get; }

	// Token: 0x060000DE RID: 222
	protected abstract InvGameItem Replace(InvGameItem item);

	// Token: 0x060000DF RID: 223 RVA: 0x00011944 File Offset: 0x0000FB44
	private void OnTooltip(bool show)
	{
		InvGameItem invGameItem = show ? this.mItem : null;
		if (invGameItem != null)
		{
			InvBaseItem baseItem = invGameItem.baseItem;
			if (baseItem != null)
			{
				string text = string.Concat(new string[]
				{
					"[",
					NGUIText.EncodeColor(invGameItem.color),
					"]",
					invGameItem.name,
					"[-]\n"
				});
				text = string.Concat(new object[]
				{
					text,
					"[AFAFAF]Level ",
					invGameItem.itemLevel,
					" ",
					baseItem.slot
				});
				List<InvStat> list = invGameItem.CalculateStats();
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					InvStat invStat = list[i];
					if (invStat.amount != 0)
					{
						if (invStat.amount < 0)
						{
							text = text + "\n[FF0000]" + invStat.amount;
						}
						else
						{
							text = text + "\n[00FF00]+" + invStat.amount;
						}
						if (invStat.modifier == InvStat.Modifier.Percent)
						{
							text += "%";
						}
						text = text + " " + invStat.id;
						text += "[-]";
					}
					i++;
				}
				if (!string.IsNullOrEmpty(baseItem.description))
				{
					text = text + "\n[FF9900]" + baseItem.description;
				}
				UITooltip.Show(text);
				return;
			}
		}
		UITooltip.Hide();
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00011AC2 File Offset: 0x0000FCC2
	private void OnClick()
	{
		if (UIItemSlot.mDraggedItem != null)
		{
			this.OnDrop(null);
			return;
		}
		if (this.mItem != null)
		{
			UIItemSlot.mDraggedItem = this.Replace(null);
			if (UIItemSlot.mDraggedItem != null)
			{
				NGUITools.PlaySound(this.grabSound);
			}
			this.UpdateCursor();
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00011B00 File Offset: 0x0000FD00
	private void OnDrag(Vector2 delta)
	{
		if (UIItemSlot.mDraggedItem == null && this.mItem != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UIItemSlot.mDraggedItem = this.Replace(null);
			NGUITools.PlaySound(this.grabSound);
			this.UpdateCursor();
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00011B3C File Offset: 0x0000FD3C
	private void OnDrop(GameObject go)
	{
		InvGameItem invGameItem = this.Replace(UIItemSlot.mDraggedItem);
		if (UIItemSlot.mDraggedItem == invGameItem)
		{
			NGUITools.PlaySound(this.errorSound);
		}
		else if (invGameItem != null)
		{
			NGUITools.PlaySound(this.grabSound);
		}
		else
		{
			NGUITools.PlaySound(this.placeSound);
		}
		UIItemSlot.mDraggedItem = invGameItem;
		this.UpdateCursor();
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00011B94 File Offset: 0x0000FD94
	private void UpdateCursor()
	{
		if (UIItemSlot.mDraggedItem != null && UIItemSlot.mDraggedItem.baseItem != null)
		{
			UICursor.Set(UIItemSlot.mDraggedItem.baseItem.iconAtlas as INGUIAtlas, UIItemSlot.mDraggedItem.baseItem.iconName);
			return;
		}
		UICursor.Clear();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00011BE4 File Offset: 0x0000FDE4
	private void Update()
	{
		InvGameItem observedItem = this.observedItem;
		if (this.mItem != observedItem)
		{
			this.mItem = observedItem;
			InvBaseItem invBaseItem = (observedItem != null) ? observedItem.baseItem : null;
			if (this.label != null)
			{
				string text = (observedItem != null) ? observedItem.name : null;
				if (string.IsNullOrEmpty(this.mText))
				{
					this.mText = this.label.text;
				}
				this.label.text = ((text != null) ? text : this.mText);
			}
			if (this.icon != null)
			{
				if (invBaseItem == null || invBaseItem.iconAtlas == null)
				{
					this.icon.enabled = false;
				}
				else
				{
					this.icon.atlas = (invBaseItem.iconAtlas as INGUIAtlas);
					this.icon.spriteName = invBaseItem.iconName;
					this.icon.enabled = true;
					this.icon.MakePixelPerfect();
				}
			}
			if (this.background != null)
			{
				this.background.color = ((observedItem != null) ? observedItem.color : Color.white);
			}
		}
	}

	// Token: 0x04000276 RID: 630
	public UISprite icon;

	// Token: 0x04000277 RID: 631
	public UIWidget background;

	// Token: 0x04000278 RID: 632
	public UILabel label;

	// Token: 0x04000279 RID: 633
	public AudioClip grabSound;

	// Token: 0x0400027A RID: 634
	public AudioClip placeSound;

	// Token: 0x0400027B RID: 635
	public AudioClip errorSound;

	// Token: 0x0400027C RID: 636
	private InvGameItem mItem;

	// Token: 0x0400027D RID: 637
	private string mText = "";

	// Token: 0x0400027E RID: 638
	private static InvGameItem mDraggedItem;
}
