using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A1 RID: 161
[ExecuteInEditMode]
public class UIFont : MonoBehaviour, INGUIFont
{
	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000755 RID: 1877 RVA: 0x0003D9CC File Offset: 0x0003BBCC
	// (set) Token: 0x06000756 RID: 1878 RVA: 0x0003D9F0 File Offset: 0x0003BBF0
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

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000757 RID: 1879 RVA: 0x0003DA18 File Offset: 0x0003BC18
	// (set) Token: 0x06000758 RID: 1880 RVA: 0x0003DA4C File Offset: 0x0003BC4C
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

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000759 RID: 1881 RVA: 0x0003DA80 File Offset: 0x0003BC80
	// (set) Token: 0x0600075A RID: 1882 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
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

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x0600075B RID: 1883 RVA: 0x0003DAE8 File Offset: 0x0003BCE8
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

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x0600075C RID: 1884 RVA: 0x0003DB20 File Offset: 0x0003BD20
	// (set) Token: 0x0600075D RID: 1885 RVA: 0x0003DB44 File Offset: 0x0003BD44
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

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x0600075E RID: 1886 RVA: 0x0003DB6C File Offset: 0x0003BD6C
	// (set) Token: 0x0600075F RID: 1887 RVA: 0x0003DB98 File Offset: 0x0003BD98
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

	// Token: 0x06000760 RID: 1888 RVA: 0x0003DC14 File Offset: 0x0003BE14
	public UISpriteData GetSprite(string spriteName)
	{
		INGUIAtlas atlas = this.atlas;
		if (atlas != null)
		{
			return atlas.GetSprite(spriteName);
		}
		return null;
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000761 RID: 1889 RVA: 0x0003DC34 File Offset: 0x0003BE34
	// (set) Token: 0x06000762 RID: 1890 RVA: 0x0003DCDC File Offset: 0x0003BEDC
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

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000763 RID: 1891 RVA: 0x0003DD1D File Offset: 0x0003BF1D
	[Obsolete("Use premultipliedAlphaShader instead")]
	public bool premultipliedAlpha
	{
		get
		{
			return this.premultipliedAlphaShader;
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000764 RID: 1892 RVA: 0x0003DD28 File Offset: 0x0003BF28
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

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000765 RID: 1893 RVA: 0x0003DDB0 File Offset: 0x0003BFB0
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

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000766 RID: 1894 RVA: 0x0003DE30 File Offset: 0x0003C030
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

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06000767 RID: 1895 RVA: 0x0003DE6C File Offset: 0x0003C06C
	// (set) Token: 0x06000768 RID: 1896 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
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

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000769 RID: 1897 RVA: 0x0003DF04 File Offset: 0x0003C104
	// (set) Token: 0x0600076A RID: 1898 RVA: 0x0003DF30 File Offset: 0x0003C130
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

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x0600076B RID: 1899 RVA: 0x0003DF74 File Offset: 0x0003C174
	public bool isValid
	{
		get
		{
			return this.mDynamicFont != null || this.mFont.isValid;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x0600076C RID: 1900 RVA: 0x0003DF91 File Offset: 0x0003C191
	// (set) Token: 0x0600076D RID: 1901 RVA: 0x0003DF99 File Offset: 0x0003C199
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

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x0600076E RID: 1902 RVA: 0x0003DFA4 File Offset: 0x0003C1A4
	// (set) Token: 0x0600076F RID: 1903 RVA: 0x0003DFE4 File Offset: 0x0003C1E4
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

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000770 RID: 1904 RVA: 0x0003E00C File Offset: 0x0003C20C
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

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000771 RID: 1905 RVA: 0x0003E0E0 File Offset: 0x0003C2E0
	// (set) Token: 0x06000772 RID: 1906 RVA: 0x0003E100 File Offset: 0x0003C300
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

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000773 RID: 1907 RVA: 0x0003E17C File Offset: 0x0003C37C
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

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06000774 RID: 1908 RVA: 0x0003E1A8 File Offset: 0x0003C3A8
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

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06000775 RID: 1909 RVA: 0x0003E1D4 File Offset: 0x0003C3D4
	// (set) Token: 0x06000776 RID: 1910 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
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

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06000777 RID: 1911 RVA: 0x0003E248 File Offset: 0x0003C448
	// (set) Token: 0x06000778 RID: 1912 RVA: 0x0003E26C File Offset: 0x0003C46C
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

	// Token: 0x06000779 RID: 1913 RVA: 0x0003E2A4 File Offset: 0x0003C4A4
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

	// Token: 0x0600077A RID: 1914 RVA: 0x0003E3A8 File Offset: 0x0003C5A8
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

	// Token: 0x0600077B RID: 1915 RVA: 0x0003E3D4 File Offset: 0x0003C5D4
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

	// Token: 0x0600077C RID: 1916 RVA: 0x0003E484 File Offset: 0x0003C684
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

	// Token: 0x0600077D RID: 1917 RVA: 0x0003E578 File Offset: 0x0003C778
	private BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		int count = this.mSymbols.Count;
		while (i < count)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
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
			this.mSymbols.Add(bmsymbol2);
			return bmsymbol2;
		}
		return null;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0003E5DC File Offset: 0x0003C7DC
	public BMSymbol MatchSymbol(string text, int offset, int textLength)
	{
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

	// Token: 0x0600077F RID: 1919 RVA: 0x0003E66C File Offset: 0x0003C86C
	public void AddSymbol(string sequence, string spriteName)
	{
		this.GetSymbol(sequence, true).spriteName = spriteName;
		this.MarkAsChanged();
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0003E684 File Offset: 0x0003C884
	public void RemoveSymbol(string sequence)
	{
		BMSymbol symbol = this.GetSymbol(sequence, false);
		if (symbol != null)
		{
			this.symbols.Remove(symbol);
		}
		this.MarkAsChanged();
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0003E6B0 File Offset: 0x0003C8B0
	public void RenameSymbol(string before, string after)
	{
		BMSymbol symbol = this.GetSymbol(before, false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		this.MarkAsChanged();
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
	public bool UsesSprite(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			if (s.Equals(this.spriteName))
			{
				return true;
			}
			int i = 0;
			int count = this.symbols.Count;
			while (i < count)
			{
				BMSymbol bmsymbol = this.symbols[i];
				if (s.Equals(bmsymbol.spriteName))
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x040006C3 RID: 1731
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x040006C4 RID: 1732
	[HideInInspector]
	[SerializeField]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040006C5 RID: 1733
	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	// Token: 0x040006C6 RID: 1734
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mAtlas;

	// Token: 0x040006C7 RID: 1735
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mReplacement;

	// Token: 0x040006C8 RID: 1736
	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	// Token: 0x040006C9 RID: 1737
	[HideInInspector]
	[SerializeField]
	private Font mDynamicFont;

	// Token: 0x040006CA RID: 1738
	[HideInInspector]
	[SerializeField]
	private int mDynamicFontSize = 16;

	// Token: 0x040006CB RID: 1739
	[HideInInspector]
	[SerializeField]
	private FontStyle mDynamicFontStyle;

	// Token: 0x040006CC RID: 1740
	[NonSerialized]
	private UISpriteData mSprite;

	// Token: 0x040006CD RID: 1741
	[NonSerialized]
	private int mPMA = -1;

	// Token: 0x040006CE RID: 1742
	[NonSerialized]
	private int mPacked = -1;
}
