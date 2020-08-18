using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000099 RID: 153
public interface INGUIFont
{
	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000673 RID: 1651
	// (set) Token: 0x06000674 RID: 1652
	BMFont bmFont { get; set; }

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000675 RID: 1653
	// (set) Token: 0x06000676 RID: 1654
	int texWidth { get; set; }

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000677 RID: 1655
	// (set) Token: 0x06000678 RID: 1656
	int texHeight { get; set; }

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000679 RID: 1657
	bool hasSymbols { get; }

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x0600067A RID: 1658
	// (set) Token: 0x0600067B RID: 1659
	List<BMSymbol> symbols { get; set; }

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x0600067C RID: 1660
	// (set) Token: 0x0600067D RID: 1661
	INGUIAtlas atlas { get; set; }

	// Token: 0x0600067E RID: 1662
	UISpriteData GetSprite(string spriteName);

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x0600067F RID: 1663
	// (set) Token: 0x06000680 RID: 1664
	Material material { get; set; }

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000681 RID: 1665
	bool premultipliedAlphaShader { get; }

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000682 RID: 1666
	bool packedFontShader { get; }

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000683 RID: 1667
	Texture2D texture { get; }

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000684 RID: 1668
	// (set) Token: 0x06000685 RID: 1669
	Rect uvRect { get; set; }

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000686 RID: 1670
	// (set) Token: 0x06000687 RID: 1671
	string spriteName { get; set; }

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000688 RID: 1672
	bool isValid { get; }

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000689 RID: 1673
	// (set) Token: 0x0600068A RID: 1674
	int defaultSize { get; set; }

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x0600068B RID: 1675
	UISpriteData sprite { get; }

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x0600068C RID: 1676
	// (set) Token: 0x0600068D RID: 1677
	INGUIFont replacement { get; set; }

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x0600068E RID: 1678
	INGUIFont finalFont { get; }

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x0600068F RID: 1679
	bool isDynamic { get; }

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000690 RID: 1680
	// (set) Token: 0x06000691 RID: 1681
	Font dynamicFont { get; set; }

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000692 RID: 1682
	// (set) Token: 0x06000693 RID: 1683
	FontStyle dynamicFontStyle { get; set; }

	// Token: 0x06000694 RID: 1684
	bool References(INGUIFont font);

	// Token: 0x06000695 RID: 1685
	void MarkAsChanged();

	// Token: 0x06000696 RID: 1686
	void UpdateUVRect();

	// Token: 0x06000697 RID: 1687
	BMSymbol MatchSymbol(string text, int offset, int textLength);

	// Token: 0x06000698 RID: 1688
	void AddSymbol(string sequence, string spriteName);

	// Token: 0x06000699 RID: 1689
	void RemoveSymbol(string sequence);

	// Token: 0x0600069A RID: 1690
	void RenameSymbol(string before, string after);

	// Token: 0x0600069B RID: 1691
	bool UsesSprite(string s);
}
