using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000097 RID: 151
public interface INGUIAtlas
{
	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000651 RID: 1617
	// (set) Token: 0x06000652 RID: 1618
	Material spriteMaterial { get; set; }

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000653 RID: 1619
	// (set) Token: 0x06000654 RID: 1620
	List<UISpriteData> spriteList { get; set; }

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000655 RID: 1621
	Texture texture { get; }

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000656 RID: 1622
	// (set) Token: 0x06000657 RID: 1623
	float pixelSize { get; set; }

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06000658 RID: 1624
	bool premultipliedAlpha { get; }

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000659 RID: 1625
	// (set) Token: 0x0600065A RID: 1626
	INGUIAtlas replacement { get; set; }

	// Token: 0x0600065B RID: 1627
	UISpriteData GetSprite(string name);

	// Token: 0x0600065C RID: 1628
	BetterList<string> GetListOfSprites();

	// Token: 0x0600065D RID: 1629
	BetterList<string> GetListOfSprites(string match);

	// Token: 0x0600065E RID: 1630
	bool References(INGUIAtlas atlas);

	// Token: 0x0600065F RID: 1631
	void MarkAsChanged();

	// Token: 0x06000660 RID: 1632
	void SortAlphabetically();
}
