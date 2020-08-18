using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000028 RID: 40
[Serializable]
public class InvBaseItem
{
	// Token: 0x0400028C RID: 652
	public int id16;

	// Token: 0x0400028D RID: 653
	public string name;

	// Token: 0x0400028E RID: 654
	public string description;

	// Token: 0x0400028F RID: 655
	public InvBaseItem.Slot slot;

	// Token: 0x04000290 RID: 656
	public int minItemLevel = 1;

	// Token: 0x04000291 RID: 657
	public int maxItemLevel = 50;

	// Token: 0x04000292 RID: 658
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x04000293 RID: 659
	public GameObject attachment;

	// Token: 0x04000294 RID: 660
	public Color color = Color.white;

	// Token: 0x04000295 RID: 661
	public UnityEngine.Object iconAtlas;

	// Token: 0x04000296 RID: 662
	public string iconName = "";

	// Token: 0x02000609 RID: 1545
	public enum Slot
	{
		// Token: 0x04004450 RID: 17488
		None,
		// Token: 0x04004451 RID: 17489
		Weapon,
		// Token: 0x04004452 RID: 17490
		Shield,
		// Token: 0x04004453 RID: 17491
		Body,
		// Token: 0x04004454 RID: 17492
		Shoulders,
		// Token: 0x04004455 RID: 17493
		Bracers,
		// Token: 0x04004456 RID: 17494
		Boots,
		// Token: 0x04004457 RID: 17495
		Trinket,
		// Token: 0x04004458 RID: 17496
		_LastDoNotUse
	}
}
