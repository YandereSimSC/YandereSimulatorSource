using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002B RID: 43
[Serializable]
public class InvGameItem
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000104 RID: 260 RVA: 0x00012409 File Offset: 0x00010609
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000105 RID: 261 RVA: 0x00012411 File Offset: 0x00010611
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000106 RID: 262 RVA: 0x00012432 File Offset: 0x00010632
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000107 RID: 263 RVA: 0x00012464 File Offset: 0x00010664
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000108 RID: 264 RVA: 0x00012534 File Offset: 0x00010734
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				result = new Color(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				result = new Color(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				result = new Color(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				result = new Color(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959u);
				break;
			case InvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935u);
				break;
			case InvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783u);
				break;
			case InvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231u);
				break;
			case InvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111u);
				break;
			case InvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935u);
				break;
			case InvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519u);
				break;
			}
			return result;
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00012654 File Offset: 0x00010854
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00012671 File Offset: 0x00010871
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00012698 File Offset: 0x00010898
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<InvStat>(InvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x0400029E RID: 670
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x0400029F RID: 671
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x040002A0 RID: 672
	public int itemLevel = 1;

	// Token: 0x040002A1 RID: 673
	private InvBaseItem mBaseItem;

	// Token: 0x0200060A RID: 1546
	public enum Quality
	{
		// Token: 0x0400445A RID: 17498
		Broken,
		// Token: 0x0400445B RID: 17499
		Cursed,
		// Token: 0x0400445C RID: 17500
		Damaged,
		// Token: 0x0400445D RID: 17501
		Worn,
		// Token: 0x0400445E RID: 17502
		Sturdy,
		// Token: 0x0400445F RID: 17503
		Polished,
		// Token: 0x04004460 RID: 17504
		Improved,
		// Token: 0x04004461 RID: 17505
		Crafted,
		// Token: 0x04004462 RID: 17506
		Superior,
		// Token: 0x04004463 RID: 17507
		Enchanted,
		// Token: 0x04004464 RID: 17508
		Epic,
		// Token: 0x04004465 RID: 17509
		Legendary,
		// Token: 0x04004466 RID: 17510
		_LastDoNotUse
	}
}
