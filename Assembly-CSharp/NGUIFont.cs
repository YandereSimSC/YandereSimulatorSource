using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009A RID: 154
[ExecuteInEditMode]
public class NGUIFont : ScriptableObject, INGUIFont
{
	// Token: 0x17000105 RID: 261
	// (get) Token: 0x0600069C RID: 1692 RVA: 0x00036E2C File Offset: 0x0003502C
	// (set) Token: 0x0600069D RID: 1693 RVA: 0x00036E50 File Offset: 0x00035050
	public BMFont bmFont
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement == null)
			{
				return this.mFont;
			}
			return replacement.bmFont;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.bmFont = value;
				return;
			}
			this.mFont = value;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x0600069E RID: 1694 RVA: 0x00036E78 File Offset: 0x00035078
	// (set) Token: 0x0600069F RID: 1695 RVA: 0x00036EAC File Offset: 0x000350AC
	public int texWidth
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.texWidth;
			}
			if (this.mFont == null)
			{
				return 1;
			}
			return this.mFont.texWidth;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.texWidth = value;
				return;
			}
			if (this.mFont != null)
			{
				this.mFont.texWidth = value;
			}
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00036EE0 File Offset: 0x000350E0
	// (set) Token: 0x060006A1 RID: 1697 RVA: 0x00036F14 File Offset: 0x00035114
	public int texHeight
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.texHeight;
			}
			if (this.mFont == null)
			{
				return 1;
			}
			return this.mFont.texHeight;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.texHeight = value;
				return;
			}
			if (this.mFont != null)
			{
				this.mFont.texHeight = value;
			}
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00036F48 File Offset: 0x00035148
	public bool hasSymbols
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement == null)
			{
				return this.mSymbols != null && this.mSymbols.Count != 0;
			}
			return replacement.hasSymbols;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00036F80 File Offset: 0x00035180
	// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00036FA4 File Offset: 0x000351A4
	public List<BMSymbol> symbols
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement == null)
			{
				return this.mSymbols;
			}
			return replacement.symbols;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.symbols = value;
				return;
			}
			this.mSymbols = value;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00036FCC File Offset: 0x000351CC
	// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00036FF8 File Offset: 0x000351F8
	public INGUIAtlas atlas
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.atlas;
			}
			return this.mAtlas as INGUIAtlas;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.atlas = value;
				return;
			}
			if (this.mAtlas as INGUIAtlas != value)
			{
				this.mPMA = -1;
				this.mAtlas = (value as UnityEngine.Object);
				if (value != null)
				{
					this.mMat = value.spriteMaterial;
					if (this.sprite != null)
					{
						this.mUVRect = this.uvRect;
					}
				}
				else
				{
					this.mAtlas = null;
					this.mMat = null;
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00037074 File Offset: 0x00035274
	public UISpriteData GetSprite(string spriteName)
	{
		INGUIAtlas atlas = this.atlas;
		if (atlas != null)
		{
			return atlas.GetSprite(spriteName);
		}
		return null;
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00037094 File Offset: 0x00035294
	// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0003713C File Offset: 0x0003533C
	public Material material
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.material;
			}
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			if (inguiatlas != null)
			{
				return inguiatlas.spriteMaterial;
			}
			if (this.mMat != null)
			{
				if (this.mDynamicFont != null && this.mMat != this.mDynamicFont.material)
				{
					this.mMat.mainTexture = this.mDynamicFont.material.mainTexture;
				}
				return this.mMat;
			}
			if (this.mDynamicFont != null)
			{
				return this.mDynamicFont.material;
			}
			return null;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.material = value;
				return;
			}
			if (this.mMat != value)
			{
				this.mPMA = -1;
				this.mMat = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x060006AA RID: 1706 RVA: 0x0003717D File Offset: 0x0003537D
	[Obsolete("Use premultipliedAlphaShader instead")]
	public bool premultipliedAlpha
	{
		get
		{
			return this.premultipliedAlphaShader;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x060006AB RID: 1707 RVA: 0x00037188 File Offset: 0x00035388
	public bool premultipliedAlphaShader
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.premultipliedAlphaShader;
			}
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			if (inguiatlas != null)
			{
				return inguiatlas.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((material != null && material.shader != null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x060006AC RID: 1708 RVA: 0x00037210 File Offset: 0x00035410
	public bool packedFontShader
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.packedFontShader;
			}
			if (this.mAtlas != null)
			{
				return false;
			}
			if (this.mPacked == -1)
			{
				Material material = this.material;
				this.mPacked = ((material != null && material.shader != null && material.shader.name.Contains("Packed")) ? 1 : 0);
			}
			return this.mPacked == 1;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x00037290 File Offset: 0x00035490
	public Texture2D texture
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.texture;
			}
			Material material = this.material;
			if (!(material != null))
			{
				return null;
			}
			return material.mainTexture as Texture2D;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x060006AE RID: 1710 RVA: 0x000372CC File Offset: 0x000354CC
	// (set) Token: 0x060006AF RID: 1711 RVA: 0x00037320 File Offset: 0x00035520
	public Rect uvRect
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.uvRect;
			}
			if (!(this.mAtlas != null) || this.sprite == null)
			{
				return new Rect(0f, 0f, 1f, 1f);
			}
			return this.mUVRect;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.uvRect = value;
				return;
			}
			if (this.sprite == null && this.mUVRect != value)
			{
				this.mUVRect = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00037364 File Offset: 0x00035564
	// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00037390 File Offset: 0x00035590
	public string spriteName
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement == null)
			{
				return this.mFont.spriteName;
			}
			return replacement.spriteName;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.spriteName = value;
				return;
			}
			if (this.mFont.spriteName != value)
			{
				this.mFont.spriteName = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000373D4 File Offset: 0x000355D4
	public bool isValid
	{
		get
		{
			return this.mDynamicFont != null || this.mFont.isValid;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x060006B3 RID: 1715 RVA: 0x000373F1 File Offset: 0x000355F1
	// (set) Token: 0x060006B4 RID: 1716 RVA: 0x000373F9 File Offset: 0x000355F9
	[Obsolete("Use defaultSize instead")]
	public int size
	{
		get
		{
			return this.defaultSize;
		}
		set
		{
			this.defaultSize = value;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00037404 File Offset: 0x00035604
	// (set) Token: 0x060006B6 RID: 1718 RVA: 0x00037444 File Offset: 0x00035644
	public int defaultSize
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.defaultSize;
			}
			if (this.isDynamic || this.mFont == null)
			{
				return this.mDynamicFontSize;
			}
			return this.mFont.charSize;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.defaultSize = value;
				return;
			}
			this.mDynamicFontSize = value;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0003746C File Offset: 0x0003566C
	public UISpriteData sprite
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.sprite;
			}
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			if (this.mSprite == null && inguiatlas != null && this.mFont != null && !string.IsNullOrEmpty(this.mFont.spriteName))
			{
				this.mSprite = inguiatlas.GetSprite(this.mFont.spriteName);
				if (this.mSprite == null)
				{
					this.mSprite = inguiatlas.GetSprite(base.name);
				}
				if (this.mSprite == null)
				{
					this.mFont.spriteName = null;
				}
				else
				{
					this.UpdateUVRect();
				}
				int i = 0;
				int count = this.mSymbols.Count;
				while (i < count)
				{
					this.symbols[i].MarkAsChanged();
					i++;
				}
			}
			return this.mSprite;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00037540 File Offset: 0x00035740
	// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00037560 File Offset: 0x00035760
	public INGUIFont replacement
	{
		get
		{
			if (this.mReplacement == null)
			{
				return null;
			}
			return this.mReplacement as INGUIFont;
		}
		set
		{
			INGUIFont inguifont = value;
			if (inguifont == this)
			{
				inguifont = null;
			}
			if (this.mReplacement as INGUIFont != inguifont)
			{
				if (inguifont != null && inguifont.replacement == this)
				{
					inguifont.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsChanged();
				}
				this.mReplacement = (inguifont as UnityEngine.Object);
				if (inguifont != null)
				{
					this.mPMA = -1;
					this.mMat = null;
					this.mFont = null;
					this.mDynamicFont = null;
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x060006BA RID: 1722 RVA: 0x000375DC File Offset: 0x000357DC
	public INGUIFont finalFont
	{
		get
		{
			INGUIFont inguifont = this;
			for (int i = 0; i < 10; i++)
			{
				INGUIFont replacement = inguifont.replacement;
				if (replacement != null)
				{
					inguifont = replacement;
				}
			}
			return inguifont;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x060006BB RID: 1723 RVA: 0x00037608 File Offset: 0x00035808
	public bool isDynamic
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement == null)
			{
				return this.mDynamicFont != null;
			}
			return replacement.isDynamic;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060006BC RID: 1724 RVA: 0x00037634 File Offset: 0x00035834
	// (set) Token: 0x060006BD RID: 1725 RVA: 0x00037658 File Offset: 0x00035858
	public Font dynamicFont
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement == null)
			{
				return this.mDynamicFont;
			}
			return replacement.dynamicFont;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.dynamicFont = value;
				return;
			}
			if (this.mDynamicFont != value)
			{
				if (this.mDynamicFont != null)
				{
					this.material = null;
				}
				this.mDynamicFont = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060006BE RID: 1726 RVA: 0x000376A8 File Offset: 0x000358A8
	// (set) Token: 0x060006BF RID: 1727 RVA: 0x000376CC File Offset: 0x000358CC
	public FontStyle dynamicFontStyle
	{
		get
		{
			INGUIFont replacement = this.replacement;
			if (replacement == null)
			{
				return this.mDynamicFontStyle;
			}
			return replacement.dynamicFontStyle;
		}
		set
		{
			INGUIFont replacement = this.replacement;
			if (replacement != null)
			{
				replacement.dynamicFontStyle = value;
				return;
			}
			if (this.mDynamicFontStyle != value)
			{
				this.mDynamicFontStyle = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00037704 File Offset: 0x00035904
	private void Trim()
	{
		Texture x = null;
		INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
		if (inguiatlas != null)
		{
			x = inguiatlas.texture;
		}
		if (x != null && this.mSprite != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(this.mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2 = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
			int xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			this.mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00037808 File Offset: 0x00035A08
	public bool References(INGUIFont font)
	{
		if (font == null)
		{
			return false;
		}
		if (font == this)
		{
			return true;
		}
		INGUIFont replacement = this.replacement;
		return replacement != null && replacement.References(font);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00037834 File Offset: 0x00035A34
	public void MarkAsChanged()
	{
		INGUIFont replacement = this.replacement;
		if (replacement != null)
		{
			replacement.MarkAsChanged();
		}
		this.mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.enabled && NGUITools.GetActive(uilabel.gameObject) && NGUITools.CheckIfRelated(this, uilabel.bitmapFont))
			{
				INGUIFont bitmapFont = uilabel.bitmapFont;
				uilabel.bitmapFont = null;
				uilabel.bitmapFont = bitmapFont;
			}
			i++;
		}
		int j = 0;
		int count = this.symbols.Count;
		while (j < count)
		{
			this.symbols[j].MarkAsChanged();
			j++;
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x000378E4 File Offset: 0x00035AE4
	public void UpdateUVRect()
	{
		if (this.mAtlas == null)
		{
			return;
		}
		Texture texture = null;
		INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
		if (inguiatlas != null)
		{
			texture = inguiatlas.texture;
		}
		if (texture != null)
		{
			this.mUVRect = new Rect((float)(this.mSprite.x - this.mSprite.paddingLeft), (float)(this.mSprite.y - this.mSprite.paddingTop), (float)(this.mSprite.width + this.mSprite.paddingLeft + this.mSprite.paddingRight), (float)(this.mSprite.height + this.mSprite.paddingTop + this.mSprite.paddingBottom));
			this.mUVRect = NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
			if (this.mSprite.hasPadding)
			{
				this.Trim();
			}
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x000379D8 File Offset: 0x00035BD8
	private BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		List<BMSymbol> symbols = this.symbols;
		int i = 0;
		int count = symbols.Count;
		while (i < count)
		{
			BMSymbol bmsymbol = symbols[i];
			if (bmsymbol.sequence == sequence)
			{
				return bmsymbol;
			}
			i++;
		}
		if (createIfMissing)
		{
			BMSymbol bmsymbol2 = new BMSymbol();
			bmsymbol2.sequence = sequence;
			symbols.Add(bmsymbol2);
			return bmsymbol2;
		}
		return null;
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00037A38 File Offset: 0x00035C38
	public BMSymbol MatchSymbol(string text, int offset, int textLength)
	{
		INGUIFont replacement = this.replacement;
		if (replacement != null)
		{
			return replacement.MatchSymbol(text, offset, textLength);
		}
		int count = this.mSymbols.Count;
		if (count == 0)
		{
			return null;
		}
		textLength -= offset;
		for (int i = 0; i < count; i++)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			int length = bmsymbol.length;
			if (length != 0 && textLength >= length)
			{
				bool flag = true;
				for (int j = 0; j < length; j++)
				{
					if (text[offset + j] != bmsymbol.sequence[j])
					{
						flag = false;
						break;
					}
				}
				if (flag && bmsymbol.Validate(this.atlas))
				{
					return bmsymbol;
				}
			}
		}
		return null;
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00037AE0 File Offset: 0x00035CE0
	public void AddSymbol(string sequence, string spriteName)
	{
		INGUIFont replacement = this.replacement;
		if (replacement != null)
		{
			replacement.AddSymbol(sequence, spriteName);
			return;
		}
		this.GetSymbol(sequence, true).spriteName = spriteName;
		this.MarkAsChanged();
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00037B14 File Offset: 0x00035D14
	public void RemoveSymbol(string sequence)
	{
		INGUIFont replacement = this.replacement;
		if (replacement != null)
		{
			replacement.RemoveSymbol(sequence);
			return;
		}
		BMSymbol symbol = this.GetSymbol(sequence, false);
		if (symbol != null)
		{
			this.symbols.Remove(symbol);
		}
		this.MarkAsChanged();
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00037B54 File Offset: 0x00035D54
	public void RenameSymbol(string before, string after)
	{
		INGUIFont replacement = this.replacement;
		if (replacement != null)
		{
			replacement.RenameSymbol(before, after);
			return;
		}
		BMSymbol symbol = this.GetSymbol(before, false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		this.MarkAsChanged();
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00037B90 File Offset: 0x00035D90
	public bool UsesSprite(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			if (s.Equals(this.spriteName))
			{
				return true;
			}
			List<BMSymbol> symbols = this.symbols;
			int i = 0;
			int count = symbols.Count;
			while (i < count)
			{
				BMSymbol bmsymbol = symbols[i];
				if (s.Equals(bmsymbol.spriteName))
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x0400061C RID: 1564
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x0400061D RID: 1565
	[HideInInspector]
	[SerializeField]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x0400061E RID: 1566
	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	// Token: 0x0400061F RID: 1567
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mAtlas;

	// Token: 0x04000620 RID: 1568
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mReplacement;

	// Token: 0x04000621 RID: 1569
	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	// Token: 0x04000622 RID: 1570
	[HideInInspector]
	[SerializeField]
	private Font mDynamicFont;

	// Token: 0x04000623 RID: 1571
	[HideInInspector]
	[SerializeField]
	private int mDynamicFontSize = 16;

	// Token: 0x04000624 RID: 1572
	[HideInInspector]
	[SerializeField]
	private FontStyle mDynamicFontStyle;

	// Token: 0x04000625 RID: 1573
	[NonSerialized]
	private UISpriteData mSprite;

	// Token: 0x04000626 RID: 1574
	[NonSerialized]
	private int mPMA = -1;

	// Token: 0x04000627 RID: 1575
	[NonSerialized]
	private int mPacked = -1;
}
