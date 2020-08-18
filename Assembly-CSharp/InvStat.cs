using System;

// Token: 0x0200002C RID: 44
[Serializable]
public class InvStat
{
	// Token: 0x0600010C RID: 268 RVA: 0x000127AB File Offset: 0x000109AB
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x000127BC File Offset: 0x000109BC
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0001282C File Offset: 0x00010A2C
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x000128DC File Offset: 0x00010ADC
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x040002A2 RID: 674
	public InvStat.Identifier id;

	// Token: 0x040002A3 RID: 675
	public InvStat.Modifier modifier;

	// Token: 0x040002A4 RID: 676
	public int amount;

	// Token: 0x0200060B RID: 1547
	public enum Identifier
	{
		// Token: 0x04004468 RID: 17512
		Strength,
		// Token: 0x04004469 RID: 17513
		Constitution,
		// Token: 0x0400446A RID: 17514
		Agility,
		// Token: 0x0400446B RID: 17515
		Intelligence,
		// Token: 0x0400446C RID: 17516
		Damage,
		// Token: 0x0400446D RID: 17517
		Crit,
		// Token: 0x0400446E RID: 17518
		Armor,
		// Token: 0x0400446F RID: 17519
		Health,
		// Token: 0x04004470 RID: 17520
		Mana,
		// Token: 0x04004471 RID: 17521
		Other
	}

	// Token: 0x0200060C RID: 1548
	public enum Modifier
	{
		// Token: 0x04004473 RID: 17523
		Added,
		// Token: 0x04004474 RID: 17524
		Percent
	}
}
