using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A4 RID: 164
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Label")]
public class UILabel : UIWidget
{
	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00040417 File Offset: 0x0003E617
	public int finalFontSize
	{
		get
		{
			if (this.trueTypeFont)
			{
				return Mathf.RoundToInt(this.mScale * (float)this.mFinalFontSize);
			}
			return Mathf.RoundToInt((float)this.mFontSize * this.mScale);
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x0004044D File Offset: 0x0003E64D
	// (set) Token: 0x060007BB RID: 1979 RVA: 0x00040455 File Offset: 0x0003E655
	private bool shouldBeProcessed
	{
		get
		{
			return this.mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
				return;
			}
			this.mShouldBeProcessed = false;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x060007BC RID: 1980 RVA: 0x00040470 File Offset: 0x0003E670
	public override bool isAnchoredHorizontally
	{
		get
		{
			return base.isAnchoredHorizontally || this.mOverflow == UILabel.Overflow.ResizeFreely;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x060007BD RID: 1981 RVA: 0x00040485 File Offset: 0x0003E685
	public override bool isAnchoredVertically
	{
		get
		{
			return base.isAnchoredVertically || this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060007BE RID: 1982 RVA: 0x000404A4 File Offset: 0x0003E6A4
	// (set) Token: 0x060007BF RID: 1983 RVA: 0x000404F2 File Offset: 0x0003E6F2
	public override Material material
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat;
			}
			INGUIFont bitmapFont = this.bitmapFont;
			if (bitmapFont != null)
			{
				return bitmapFont.material;
			}
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont.material;
			}
			return null;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000404FC File Offset: 0x0003E6FC
	// (set) Token: 0x060007C1 RID: 1985 RVA: 0x00040545 File Offset: 0x0003E745
	public override Texture mainTexture
	{
		get
		{
			INGUIFont bitmapFont = this.bitmapFont;
			if (bitmapFont != null)
			{
				return bitmapFont.texture;
			}
			if (this.mTrueTypeFont != null)
			{
				Material material = this.mTrueTypeFont.material;
				if (material != null)
				{
					return material.mainTexture;
				}
			}
			return null;
		}
		set
		{
			base.mainTexture = value;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0004054E File Offset: 0x0003E74E
	// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0004055B File Offset: 0x0003E75B
	[Obsolete("Use UILabel.bitmapFont instead")]
	public UnityEngine.Object font
	{
		get
		{
			return this.bitmapFont as UnityEngine.Object;
		}
		set
		{
			this.bitmapFont = (value as INGUIFont);
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00040569 File Offset: 0x0003E769
	// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00040576 File Offset: 0x0003E776
	public INGUIFont bitmapFont
	{
		get
		{
			return this.mFont as INGUIFont;
		}
		set
		{
			if (this.mFont as INGUIFont != value)
			{
				base.RemoveFromPanel();
				this.mFont = (value as UnityEngine.Object);
				this.mTrueTypeFont = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x060007C6 RID: 1990 RVA: 0x000405A8 File Offset: 0x0003E7A8
	// (set) Token: 0x060007C7 RID: 1991 RVA: 0x000405C8 File Offset: 0x0003E7C8
	public INGUIAtlas atlas
	{
		get
		{
			INGUIFont bitmapFont = this.bitmapFont;
			if (bitmapFont != null)
			{
				return bitmapFont.atlas;
			}
			return null;
		}
		set
		{
			INGUIFont bitmapFont = this.bitmapFont;
			if (bitmapFont != null)
			{
				bitmapFont.atlas = value;
			}
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000405E8 File Offset: 0x0003E7E8
	// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0004061C File Offset: 0x0003E81C
	public Font trueTypeFont
	{
		get
		{
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont;
			}
			INGUIFont bitmapFont = this.bitmapFont;
			if (bitmapFont != null)
			{
				return bitmapFont.dynamicFont;
			}
			return null;
		}
		set
		{
			if (this.mTrueTypeFont != value)
			{
				this.SetActiveFont(null);
				base.RemoveFromPanel();
				this.mTrueTypeFont = value;
				this.shouldBeProcessed = true;
				this.mFont = null;
				this.SetActiveFont(value);
				this.ProcessAndRequest();
				if (this.mActiveTTF != null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x060007CA RID: 1994 RVA: 0x0004067A File Offset: 0x0003E87A
	// (set) Token: 0x060007CB RID: 1995 RVA: 0x00040698 File Offset: 0x0003E898
	public UnityEngine.Object ambigiousFont
	{
		get
		{
			if (!(this.mFont != null))
			{
				return this.mTrueTypeFont;
			}
			return this.mFont;
		}
		set
		{
			INGUIFont inguifont = value as INGUIFont;
			if (inguifont != null)
			{
				this.bitmapFont = inguifont;
				return;
			}
			this.trueTypeFont = (value as Font);
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x060007CC RID: 1996 RVA: 0x000406C3 File Offset: 0x0003E8C3
	// (set) Token: 0x060007CD RID: 1997 RVA: 0x000406CC File Offset: 0x0003E8CC
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (this.mText == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = "";
					this.MarkAsChanged();
					this.ProcessAndRequest();
					if (this.autoResizeBoxCollider)
					{
						base.ResizeCollider();
						return;
					}
				}
			}
			else if (this.mText != value)
			{
				this.mText = value;
				this.MarkAsChanged();
				this.ProcessAndRequest();
				if (this.autoResizeBoxCollider)
				{
					base.ResizeCollider();
				}
			}
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x060007CE RID: 1998 RVA: 0x00040754 File Offset: 0x0003E954
	public int defaultFontSize
	{
		get
		{
			if (this.trueTypeFont != null)
			{
				return this.mFontSize;
			}
			INGUIFont bitmapFont = this.bitmapFont;
			if (bitmapFont != null)
			{
				return bitmapFont.defaultSize;
			}
			return 16;
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x060007CF RID: 1999 RVA: 0x00040789 File Offset: 0x0003E989
	// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00040791 File Offset: 0x0003E991
	public int fontSize
	{
		get
		{
			return this.mFontSize;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (this.mFontSize != value)
			{
				this.mFontSize = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000407BE File Offset: 0x0003E9BE
	// (set) Token: 0x060007D2 RID: 2002 RVA: 0x000407C6 File Offset: 0x0003E9C6
	public FontStyle fontStyle
	{
		get
		{
			return this.mFontStyle;
		}
		set
		{
			if (this.mFontStyle != value)
			{
				this.mFontStyle = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x060007D3 RID: 2003 RVA: 0x000407E5 File Offset: 0x0003E9E5
	// (set) Token: 0x060007D4 RID: 2004 RVA: 0x000407ED File Offset: 0x0003E9ED
	public NGUIText.Alignment alignment
	{
		get
		{
			return this.mAlignment;
		}
		set
		{
			if (this.mAlignment != value)
			{
				this.mAlignment = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0004080C File Offset: 0x0003EA0C
	// (set) Token: 0x060007D6 RID: 2006 RVA: 0x00040814 File Offset: 0x0003EA14
	public bool applyGradient
	{
		get
		{
			return this.mApplyGradient;
		}
		set
		{
			if (this.mApplyGradient != value)
			{
				this.mApplyGradient = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0004082C File Offset: 0x0003EA2C
	// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00040834 File Offset: 0x0003EA34
	public Color gradientTop
	{
		get
		{
			return this.mGradientTop;
		}
		set
		{
			if (this.mGradientTop != value)
			{
				this.mGradientTop = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00040859 File Offset: 0x0003EA59
	// (set) Token: 0x060007DA RID: 2010 RVA: 0x00040861 File Offset: 0x0003EA61
	public Color gradientBottom
	{
		get
		{
			return this.mGradientBottom;
		}
		set
		{
			if (this.mGradientBottom != value)
			{
				this.mGradientBottom = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x060007DB RID: 2011 RVA: 0x00040886 File Offset: 0x0003EA86
	// (set) Token: 0x060007DC RID: 2012 RVA: 0x0004088E File Offset: 0x0003EA8E
	public int spacingX
	{
		get
		{
			return this.mSpacingX;
		}
		set
		{
			if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x060007DD RID: 2013 RVA: 0x000408A6 File Offset: 0x0003EAA6
	// (set) Token: 0x060007DE RID: 2014 RVA: 0x000408AE File Offset: 0x0003EAAE
	public int spacingY
	{
		get
		{
			return this.mSpacingY;
		}
		set
		{
			if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x060007DF RID: 2015 RVA: 0x000408C6 File Offset: 0x0003EAC6
	// (set) Token: 0x060007E0 RID: 2016 RVA: 0x000408CE File Offset: 0x0003EACE
	public bool useFloatSpacing
	{
		get
		{
			return this.mUseFloatSpacing;
		}
		set
		{
			if (this.mUseFloatSpacing != value)
			{
				this.mUseFloatSpacing = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x060007E1 RID: 2017 RVA: 0x000408E7 File Offset: 0x0003EAE7
	// (set) Token: 0x060007E2 RID: 2018 RVA: 0x000408EF File Offset: 0x0003EAEF
	public float floatSpacingX
	{
		get
		{
			return this.mFloatSpacingX;
		}
		set
		{
			if (!Mathf.Approximately(this.mFloatSpacingX, value))
			{
				this.mFloatSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0004090C File Offset: 0x0003EB0C
	// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00040914 File Offset: 0x0003EB14
	public float floatSpacingY
	{
		get
		{
			return this.mFloatSpacingY;
		}
		set
		{
			if (!Mathf.Approximately(this.mFloatSpacingY, value))
			{
				this.mFloatSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00040931 File Offset: 0x0003EB31
	public float effectiveSpacingY
	{
		get
		{
			if (!this.mUseFloatSpacing)
			{
				return (float)this.mSpacingY;
			}
			return this.mFloatSpacingY;
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00040949 File Offset: 0x0003EB49
	public float effectiveSpacingX
	{
		get
		{
			if (!this.mUseFloatSpacing)
			{
				return (float)this.mSpacingX;
			}
			return this.mFloatSpacingX;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00040961 File Offset: 0x0003EB61
	// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00040969 File Offset: 0x0003EB69
	public bool overflowEllipsis
	{
		get
		{
			return this.mOverflowEllipsis;
		}
		set
		{
			if (this.mOverflowEllipsis != value)
			{
				this.mOverflowEllipsis = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00040981 File Offset: 0x0003EB81
	// (set) Token: 0x060007EA RID: 2026 RVA: 0x00040989 File Offset: 0x0003EB89
	public int overflowWidth
	{
		get
		{
			return this.mOverflowWidth;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (this.mOverflowWidth != value)
			{
				this.mOverflowWidth = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x060007EB RID: 2027 RVA: 0x000409A8 File Offset: 0x0003EBA8
	// (set) Token: 0x060007EC RID: 2028 RVA: 0x000409B0 File Offset: 0x0003EBB0
	public int overflowHeight
	{
		get
		{
			return this.mOverflowHeight;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (this.mOverflowHeight != value)
			{
				this.mOverflowHeight = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x000409CF File Offset: 0x0003EBCF
	private bool keepCrisp
	{
		get
		{
			return this.trueTypeFont != null && this.keepCrispWhenShrunk != UILabel.Crispness.Never;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060007EE RID: 2030 RVA: 0x000409EA File Offset: 0x0003EBEA
	// (set) Token: 0x060007EF RID: 2031 RVA: 0x000409F2 File Offset: 0x0003EBF2
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00040A0B File Offset: 0x0003EC0B
	// (set) Token: 0x060007F1 RID: 2033 RVA: 0x00040A13 File Offset: 0x0003EC13
	public NGUIText.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00040A2C File Offset: 0x0003EC2C
	// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00040A34 File Offset: 0x0003EC34
	public UILabel.Overflow overflowMethod
	{
		get
		{
			return this.mOverflow;
		}
		set
		{
			if (this.mOverflow != value)
			{
				this.mOverflow = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00040A4D File Offset: 0x0003EC4D
	// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00040A55 File Offset: 0x0003EC55
	[Obsolete("Use 'width' instead")]
	public int lineWidth
	{
		get
		{
			return base.width;
		}
		set
		{
			base.width = value;
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00040A5E File Offset: 0x0003EC5E
	// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00040A66 File Offset: 0x0003EC66
	[Obsolete("Use 'height' instead")]
	public int lineHeight
	{
		get
		{
			return base.height;
		}
		set
		{
			base.height = value;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00040A6F File Offset: 0x0003EC6F
	// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00040A7D File Offset: 0x0003EC7D
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = (value ? 0 : 1);
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060007FA RID: 2042 RVA: 0x00040AA2 File Offset: 0x0003ECA2
	public override Vector3[] localCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.localCorners;
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060007FB RID: 2043 RVA: 0x00040ABA File Offset: 0x0003ECBA
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.worldCorners;
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060007FC RID: 2044 RVA: 0x00040AD2 File Offset: 0x0003ECD2
	public override Vector4 drawingDimensions
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.drawingDimensions;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060007FD RID: 2045 RVA: 0x00040AEA File Offset: 0x0003ECEA
	// (set) Token: 0x060007FE RID: 2046 RVA: 0x00040AF2 File Offset: 0x0003ECF2
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				this.shouldBeProcessed = true;
				if (this.overflowMethod == UILabel.Overflow.ShrinkContent)
				{
					this.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x060007FF RID: 2047 RVA: 0x00040B1F File Offset: 0x0003ED1F
	// (set) Token: 0x06000800 RID: 2048 RVA: 0x00040B27 File Offset: 0x0003ED27
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06000801 RID: 2049 RVA: 0x00040B40 File Offset: 0x0003ED40
	// (set) Token: 0x06000802 RID: 2050 RVA: 0x00040B48 File Offset: 0x0003ED48
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (this.mEffectColor != value)
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.shouldBeProcessed = true;
				}
			}
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x00040B6E File Offset: 0x0003ED6E
	// (set) Token: 0x06000804 RID: 2052 RVA: 0x00040B76 File Offset: 0x0003ED76
	public Vector2 effectDistance
	{
		get
		{
			return this.mEffectDistance;
		}
		set
		{
			if (this.mEffectDistance != value)
			{
				this.mEffectDistance = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06000805 RID: 2053 RVA: 0x00040B94 File Offset: 0x0003ED94
	public int quadsPerCharacter
	{
		get
		{
			if (this.mEffectStyle == UILabel.Effect.Shadow)
			{
				return 2;
			}
			if (this.mEffectStyle == UILabel.Effect.Outline)
			{
				return 5;
			}
			if (this.mEffectStyle == UILabel.Effect.Outline8)
			{
				return 9;
			}
			return 1;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06000806 RID: 2054 RVA: 0x00040BB9 File Offset: 0x0003EDB9
	// (set) Token: 0x06000807 RID: 2055 RVA: 0x00040BC4 File Offset: 0x0003EDC4
	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return this.mOverflow == UILabel.Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				this.overflowMethod = UILabel.Overflow.ShrinkContent;
			}
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06000808 RID: 2056 RVA: 0x00040BD0 File Offset: 0x0003EDD0
	public string processedText
	{
		get
		{
			if (this.mLastWidth != this.mWidth || this.mLastHeight != this.mHeight)
			{
				this.mLastWidth = this.mWidth;
				this.mLastHeight = this.mHeight;
				this.mShouldBeProcessed = true;
			}
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000809 RID: 2057 RVA: 0x00040C2E File Offset: 0x0003EE2E
	public Vector2 printedSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return this.mCalculatedSize;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x0600080A RID: 2058 RVA: 0x00040C46 File Offset: 0x0003EE46
	public override Vector2 localSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.localSize;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x0600080B RID: 2059 RVA: 0x00040C5E File Offset: 0x0003EE5E
	private bool isValid
	{
		get
		{
			return this.mFont != null || this.mTrueTypeFont != null;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x0600080C RID: 2060 RVA: 0x00040C7C File Offset: 0x0003EE7C
	// (set) Token: 0x0600080D RID: 2061 RVA: 0x00040C84 File Offset: 0x0003EE84
	public UILabel.Modifier modifier
	{
		get
		{
			return this.mModifier;
		}
		set
		{
			if (this.mModifier != value)
			{
				this.mModifier = value;
				this.MarkAsChanged();
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00040CA2 File Offset: 0x0003EEA2
	protected override void OnInit()
	{
		base.OnInit();
		UILabel.mList.Add(this);
		this.SetActiveFont(this.trueTypeFont);
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00040CC1 File Offset: 0x0003EEC1
	protected override void OnDisable()
	{
		this.SetActiveFont(null);
		UILabel.mList.Remove(this);
		base.OnDisable();
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00040CDC File Offset: 0x0003EEDC
	protected void SetActiveFont(Font fnt)
	{
		if (this.mActiveTTF != fnt)
		{
			Font font = this.mActiveTTF;
			int num;
			if (font != null && UILabel.mFontUsage.TryGetValue(font, out num))
			{
				num = Mathf.Max(0, --num);
				if (num == 0)
				{
					UILabel.mFontUsage.Remove(font);
				}
				else
				{
					UILabel.mFontUsage[font] = num;
				}
			}
			this.mActiveTTF = fnt;
			if (fnt != null)
			{
				int num2 = 0;
				UILabel.mFontUsage[fnt] = num2 + 1;
			}
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06000811 RID: 2065 RVA: 0x00040D64 File Offset: 0x0003EF64
	public string printedText
	{
		get
		{
			if (!string.IsNullOrEmpty(this.mText))
			{
				if (this.mModifier == UILabel.Modifier.None)
				{
					return this.mText;
				}
				if (this.mModifier == UILabel.Modifier.ToLowercase)
				{
					return this.mText.ToLower();
				}
				if (this.mModifier == UILabel.Modifier.ToUppercase)
				{
					return this.mText.ToUpper();
				}
				if (this.mModifier == UILabel.Modifier.Custom && this.customModifier != null)
				{
					return this.customModifier(this.mText);
				}
			}
			return this.mText;
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00040DE4 File Offset: 0x0003EFE4
	private static void OnFontChanged(Font font)
	{
		for (int i = 0; i < UILabel.mList.size; i++)
		{
			UILabel uilabel = UILabel.mList.buffer[i];
			if (uilabel != null)
			{
				Font trueTypeFont = uilabel.trueTypeFont;
				if (trueTypeFont == font)
				{
					trueTypeFont.RequestCharactersInTexture(uilabel.mText, uilabel.mFinalFontSize, uilabel.mFontStyle);
					uilabel.MarkAsChanged();
					if (uilabel.panel == null)
					{
						uilabel.CreatePanel();
					}
					if (UILabel.mTempDrawcalls == null)
					{
						UILabel.mTempDrawcalls = new BetterList<UIDrawCall>();
					}
					if (uilabel.drawCall != null && !UILabel.mTempDrawcalls.Contains(uilabel.drawCall))
					{
						UILabel.mTempDrawcalls.Add(uilabel.drawCall);
					}
				}
			}
		}
		if (UILabel.mTempDrawcalls != null)
		{
			int j = 0;
			int size = UILabel.mTempDrawcalls.size;
			while (j < size)
			{
				UIDrawCall uidrawCall = UILabel.mTempDrawcalls.buffer[j];
				if (uidrawCall.panel != null)
				{
					uidrawCall.panel.FillDrawCall(uidrawCall);
				}
				j++;
			}
			UILabel.mTempDrawcalls.Clear();
		}
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00040EFF File Offset: 0x0003F0FF
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.shouldBeProcessed)
		{
			this.ProcessText(false, true);
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00040F18 File Offset: 0x0003F118
	protected override void UpgradeFrom265()
	{
		this.ProcessText(true, true);
		if (this.mShrinkToFit)
		{
			this.overflowMethod = UILabel.Overflow.ShrinkContent;
			this.mMaxLineCount = 0;
		}
		if (this.mMaxLineWidth != 0)
		{
			base.width = this.mMaxLineWidth;
			this.overflowMethod = ((this.mMaxLineCount > 0) ? UILabel.Overflow.ResizeHeight : UILabel.Overflow.ShrinkContent);
		}
		else
		{
			this.overflowMethod = UILabel.Overflow.ResizeFreely;
		}
		if (this.mMaxLineHeight != 0)
		{
			base.height = this.mMaxLineHeight;
		}
		if (this.mFont != null)
		{
			int defaultFontSize = this.defaultFontSize;
			if (base.height < defaultFontSize)
			{
				base.height = defaultFontSize;
			}
			this.fontSize = defaultFontSize;
		}
		this.mMaxLineWidth = 0;
		this.mMaxLineHeight = 0;
		this.mShrinkToFit = false;
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00040FD4 File Offset: 0x0003F1D4
	protected override void OnAnchor()
	{
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			if (base.isFullyAnchored)
			{
				this.mOverflow = UILabel.Overflow.ShrinkContent;
			}
		}
		else if (this.mOverflow == UILabel.Overflow.ResizeHeight && this.topAnchor.target != null && this.bottomAnchor.target != null)
		{
			this.mOverflow = UILabel.Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00041037 File Offset: 0x0003F237
	private void ProcessAndRequest()
	{
		if (this.ambigiousFont != null)
		{
			this.ProcessText(false, true);
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0004104F File Offset: 0x0003F24F
	protected override void OnEnable()
	{
		base.OnEnable();
		if (!UILabel.mTexRebuildAdded)
		{
			UILabel.mTexRebuildAdded = true;
			Font.textureRebuilt += UILabel.OnFontChanged;
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00041078 File Offset: 0x0003F278
	protected override void OnStart()
	{
		base.OnStart();
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
		this.mPremultiply = (this.material != null && this.material.shader != null && this.material.shader.name.Contains("Premultiplied"));
		this.ProcessAndRequest();
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00041114 File Offset: 0x0003F314
	public override void MarkAsChanged()
	{
		this.shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00041124 File Offset: 0x0003F324
	public void ProcessText(bool legacyMode = false, bool full = true)
	{
		if (!this.isValid)
		{
			return;
		}
		this.mChanged = true;
		this.shouldBeProcessed = false;
		float num = this.mDrawRegion.z - this.mDrawRegion.x;
		float num2 = this.mDrawRegion.w - this.mDrawRegion.y;
		NGUIText.rectWidth = (legacyMode ? ((this.mMaxLineWidth != 0) ? this.mMaxLineWidth : 1000000) : base.width);
		NGUIText.rectHeight = (legacyMode ? ((this.mMaxLineHeight != 0) ? this.mMaxLineHeight : 1000000) : base.height);
		NGUIText.regionWidth = ((num != 1f) ? Mathf.RoundToInt((float)NGUIText.rectWidth * num) : NGUIText.rectWidth);
		NGUIText.regionHeight = ((num2 != 1f) ? Mathf.RoundToInt((float)NGUIText.rectHeight * num2) : NGUIText.rectHeight);
		this.mFinalFontSize = Mathf.Abs(legacyMode ? Mathf.RoundToInt(base.cachedTransform.localScale.x) : this.defaultFontSize);
		this.mScale = 1f;
		if (NGUIText.regionWidth < 1 || NGUIText.regionHeight < 0)
		{
			this.mProcessedText = "";
			return;
		}
		if (this.trueTypeFont != null && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				this.mDensity = ((root != null) ? root.pixelSizeAdjustment : 1f);
			}
		}
		else
		{
			this.mDensity = 1f;
		}
		if (full)
		{
			this.UpdateNGUIText();
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			if (this.mOverflowWidth > 0)
			{
				NGUIText.rectWidth = this.mOverflowWidth;
				NGUIText.regionWidth = this.mOverflowWidth;
			}
			else
			{
				NGUIText.rectWidth = 1000000;
				NGUIText.regionWidth = 1000000;
			}
			if (this.mOverflowHeight > 0)
			{
				NGUIText.rectHeight = this.mOverflowHeight;
				NGUIText.regionHeight = this.mOverflowHeight;
			}
			else
			{
				NGUIText.rectHeight = 1000000;
				NGUIText.regionHeight = 1000000;
			}
		}
		else if (this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight)
		{
			NGUIText.rectHeight = 1000000;
			NGUIText.regionHeight = 1000000;
		}
		if (this.mFinalFontSize > 0)
		{
			bool keepCrisp = this.keepCrisp;
			for (int i = this.mFinalFontSize; i > 0; i--)
			{
				if (keepCrisp)
				{
					this.mFinalFontSize = i;
					NGUIText.fontSize = this.mFinalFontSize;
				}
				else
				{
					this.mScale = (float)i / (float)this.mFinalFontSize;
					if (this.bitmapFont != null)
					{
						NGUIText.fontScale = (float)this.mFontSize / (float)this.defaultFontSize * this.mScale;
					}
					else
					{
						NGUIText.fontScale = this.mScale;
					}
				}
				NGUIText.Update(false);
				bool flag = NGUIText.WrapText(this.printedText, out this.mProcessedText, false, false, this.mOverflow == UILabel.Overflow.ClampContent && this.mOverflowEllipsis);
				if (this.mOverflow == UILabel.Overflow.ShrinkContent && !flag)
				{
					if (--i <= 1)
					{
						break;
					}
				}
				else
				{
					if (this.mOverflow == UILabel.Overflow.ResizeFreely)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						if (!flag && this.mOverflowWidth > 0)
						{
							if (--i > 1)
							{
								goto IL_4A7;
							}
							break;
						}
						else
						{
							int num3 = Mathf.Max(this.minWidth, Mathf.RoundToInt(this.mCalculatedSize.x));
							if (num != 1f)
							{
								num3 = Mathf.RoundToInt((float)num3 / num);
							}
							int num4 = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
							if (num2 != 1f)
							{
								num4 = Mathf.RoundToInt((float)num4 / num2);
							}
							if ((num3 & 1) == 1)
							{
								num3++;
							}
							if ((num4 & 1) == 1)
							{
								num4++;
							}
							if (this.mWidth != num3 || this.mHeight != num4)
							{
								this.mWidth = num3;
								this.mHeight = num4;
								if (this.onChange != null)
								{
									this.onChange();
								}
							}
						}
					}
					else if (this.mOverflow == UILabel.Overflow.ResizeHeight)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						int num5 = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if (num2 != 1f)
						{
							num5 = Mathf.RoundToInt((float)num5 / num2);
						}
						if ((num5 & 1) == 1)
						{
							num5++;
						}
						if (this.mHeight != num5)
						{
							this.mHeight = num5;
							if (this.onChange != null)
							{
								this.onChange();
							}
						}
					}
					else
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
					}
					if (legacyMode)
					{
						base.width = Mathf.RoundToInt(this.mCalculatedSize.x);
						base.height = Mathf.RoundToInt(this.mCalculatedSize.y);
						base.cachedTransform.localScale = Vector3.one;
						break;
					}
					break;
				}
				IL_4A7:;
			}
		}
		else
		{
			base.cachedTransform.localScale = Vector3.one;
			this.mProcessedText = "";
			this.mScale = 1f;
		}
		if (full)
		{
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00041620 File Offset: 0x0003F820
	public override void MakePixelPerfect()
	{
		if (!(this.ambigiousFont != null))
		{
			base.MakePixelPerfect();
			return;
		}
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
		localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		base.cachedTransform.localPosition = localPosition;
		base.cachedTransform.localScale = Vector3.one;
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			this.AssumeNaturalSize();
			return;
		}
		int width = base.width;
		int height = base.height;
		UILabel.Overflow overflow = this.mOverflow;
		if (overflow != UILabel.Overflow.ResizeHeight)
		{
			this.mWidth = 100000;
		}
		this.mHeight = 100000;
		this.mOverflow = UILabel.Overflow.ShrinkContent;
		this.ProcessText(false, true);
		this.mOverflow = overflow;
		int num = Mathf.RoundToInt(this.mCalculatedSize.x);
		int num2 = Mathf.RoundToInt(this.mCalculatedSize.y);
		num = Mathf.Max(num, base.minWidth);
		num2 = Mathf.Max(num2, base.minHeight);
		if ((num & 1) == 1)
		{
			num++;
		}
		if ((num2 & 1) == 1)
		{
			num2++;
		}
		this.mWidth = Mathf.Max(width, num);
		this.mHeight = Mathf.Max(height, num2);
		this.MarkAsChanged();
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0004177C File Offset: 0x0003F97C
	public void AssumeNaturalSize()
	{
		if (this.ambigiousFont != null)
		{
			this.mWidth = 100000;
			this.mHeight = 100000;
			this.ProcessText(false, true);
			this.mWidth = Mathf.RoundToInt(this.mCalculatedSize.x);
			this.mHeight = Mathf.RoundToInt(this.mCalculatedSize.y);
			if ((this.mWidth & 1) == 1)
			{
				this.mWidth++;
			}
			if ((this.mHeight & 1) == 1)
			{
				this.mHeight++;
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0004181C File Offset: 0x0003FA1C
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		return this.GetCharacterIndexAtPosition(worldPos, false);
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00041826 File Offset: 0x0003FA26
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		return this.GetCharacterIndexAtPosition(localPos, false);
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00041830 File Offset: 0x0003FA30
	public int GetCharacterIndexAtPosition(Vector3 worldPos, bool precise)
	{
		Vector2 localPos = base.cachedTransform.InverseTransformPoint(worldPos);
		return this.GetCharacterIndexAtPosition(localPos, precise);
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00041858 File Offset: 0x0003FA58
	public int GetCharacterIndexAtPosition(Vector2 localPos, bool precise)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			this.UpdateNGUIText();
			if (precise)
			{
				NGUIText.PrintExactCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			}
			else
			{
				NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			}
			if (UILabel.mTempVerts.Count > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int result = precise ? NGUIText.GetExactCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos) : NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos);
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
				NGUIText.bitmapFont = null;
				NGUIText.dynamicFont = null;
				return result;
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
		return 0;
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0004191C File Offset: 0x0003FB1C
	public string GetWordAtPosition(Vector3 worldPos)
	{
		int characterIndexAtPosition = this.GetCharacterIndexAtPosition(worldPos, true);
		return this.GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0004193C File Offset: 0x0003FB3C
	public string GetWordAtPosition(Vector2 localPos)
	{
		int characterIndexAtPosition = this.GetCharacterIndexAtPosition(localPos, true);
		return this.GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0004195C File Offset: 0x0003FB5C
	public string GetWordAtCharacterIndex(int characterIndex)
	{
		string printedText = this.printedText;
		if (characterIndex != -1 && characterIndex < printedText.Length)
		{
			int num = printedText.LastIndexOfAny(new char[]
			{
				' ',
				'\n'
			}, characterIndex) + 1;
			int num2 = printedText.IndexOfAny(new char[]
			{
				' ',
				'\n',
				',',
				'.'
			}, characterIndex);
			if (num2 == -1)
			{
				num2 = printedText.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					return NGUIText.StripSymbols(printedText.Substring(num, num3));
				}
			}
		}
		return null;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x000419D6 File Offset: 0x0003FBD6
	public string GetUrlAtPosition(Vector3 worldPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos, true));
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x000419E6 File Offset: 0x0003FBE6
	public string GetUrlAtPosition(Vector2 localPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos, true));
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x000419F8 File Offset: 0x0003FBF8
	public string GetUrlAtCharacterIndex(int characterIndex)
	{
		string printedText = this.printedText;
		if (characterIndex != -1 && characterIndex < printedText.Length - 6)
		{
			int num;
			if (printedText[characterIndex] == '[' && printedText[characterIndex + 1] == 'u' && printedText[characterIndex + 2] == 'r' && printedText[characterIndex + 3] == 'l' && printedText[characterIndex + 4] == '=')
			{
				num = characterIndex;
			}
			else
			{
				num = printedText.LastIndexOf("[url=", characterIndex);
			}
			if (num == -1)
			{
				return null;
			}
			num += 5;
			int num2 = printedText.IndexOf("]", num);
			if (num2 == -1)
			{
				return null;
			}
			int num3 = printedText.IndexOf("[/url]", num2);
			if (num3 == -1 || characterIndex <= num3)
			{
				return printedText.Substring(num, num2 - num);
			}
		}
		return null;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00041AB0 File Offset: 0x0003FCB0
	public int GetCharacterIndex(int currentIndex, KeyCode key)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			int defaultFontSize = this.defaultFontSize;
			this.UpdateNGUIText();
			NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			if (UILabel.mTempVerts.Count > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int i = 0;
				int count = UILabel.mTempIndices.Count;
				while (i < count)
				{
					if (UILabel.mTempIndices[i] == currentIndex)
					{
						Vector2 pos = UILabel.mTempVerts[i];
						if (key == KeyCode.UpArrow)
						{
							pos.y += (float)defaultFontSize + this.effectiveSpacingY;
						}
						else if (key == KeyCode.DownArrow)
						{
							pos.y -= (float)defaultFontSize + this.effectiveSpacingY;
						}
						else if (key == KeyCode.Home)
						{
							pos.x -= 1000f;
						}
						else if (key == KeyCode.End)
						{
							pos.x += 1000f;
						}
						int approximateCharacterIndex = NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, pos);
						if (approximateCharacterIndex != currentIndex)
						{
							UILabel.mTempVerts.Clear();
							UILabel.mTempIndices.Clear();
							return approximateCharacterIndex;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
			if (key == KeyCode.UpArrow || key == KeyCode.Home)
			{
				return 0;
			}
			if (key == KeyCode.DownArrow || key == KeyCode.End)
			{
				return processedText.Length;
			}
		}
		return currentIndex;
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00041C3C File Offset: 0x0003FE3C
	public void PrintOverlay(int start, int end, UIGeometry caret, UIGeometry highlight, Color caretColor, Color highlightColor)
	{
		if (caret != null)
		{
			caret.Clear();
		}
		if (highlight != null)
		{
			highlight.Clear();
		}
		if (!this.isValid)
		{
			return;
		}
		string processedText = this.processedText;
		this.UpdateNGUIText();
		int count = caret.verts.Count;
		Vector2 item = new Vector2(0.5f, 0.5f);
		float finalAlpha = this.finalAlpha;
		if (highlight != null && start != end)
		{
			int count2 = highlight.verts.Count;
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, highlight.verts);
			if (highlight.verts.Count > count2)
			{
				this.ApplyOffset(highlight.verts, count2);
				Color item2 = new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * finalAlpha);
				int i = count2;
				int count3 = highlight.verts.Count;
				while (i < count3)
				{
					highlight.uvs.Add(item);
					highlight.cols.Add(item2);
					i++;
				}
			}
		}
		else
		{
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, null);
		}
		this.ApplyOffset(caret.verts, count);
		Color item3 = new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * finalAlpha);
		int j = count;
		int count4 = caret.verts.Count;
		while (j < count4)
		{
			caret.uvs.Add(item);
			caret.cols.Add(item3);
			j++;
		}
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000829 RID: 2089 RVA: 0x00041DD4 File Offset: 0x0003FFD4
	private bool premultipliedAlphaShader
	{
		get
		{
			INGUIFont bitmapFont = this.bitmapFont;
			return bitmapFont != null && bitmapFont.premultipliedAlphaShader;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x0600082A RID: 2090 RVA: 0x00041DF4 File Offset: 0x0003FFF4
	private bool packedFontShader
	{
		get
		{
			INGUIFont bitmapFont = this.bitmapFont;
			return bitmapFont != null && bitmapFont.packedFontShader;
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00041E14 File Offset: 0x00040014
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		if (!this.isValid)
		{
			return;
		}
		int num = verts.Count;
		Color color = base.color;
		color.a = this.finalAlpha;
		if (this.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		string processedText = this.processedText;
		int count = verts.Count;
		this.UpdateNGUIText();
		NGUIText.tint = color;
		NGUIText.Print(processedText, verts, uvs, cols);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		Vector2 vector = this.ApplyOffset(verts, count);
		if (this.packedFontShader)
		{
			return;
		}
		if (this.effectStyle != UILabel.Effect.None)
		{
			int count2 = verts.Count;
			vector.x = this.mEffectDistance.x;
			vector.y = this.mEffectDistance.y;
			this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, -vector.y);
			if (this.effectStyle == UILabel.Effect.Outline || this.effectStyle == UILabel.Effect.Outline8)
			{
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, vector.y);
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, vector.y);
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, -vector.y);
				if (this.effectStyle == UILabel.Effect.Outline8)
				{
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, 0f);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, 0f);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, 0f, vector.y);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, 0f, -vector.y);
				}
			}
		}
		if (NGUIText.symbolStyle == NGUIText.SymbolStyle.NoOutline)
		{
			int i = 0;
			int count3 = cols.Count;
			while (i < count3)
			{
				if (cols[i].r == -1f)
				{
					cols[i] = Color.white;
				}
				i++;
			}
		}
		if (this.onPostFill != null)
		{
			this.onPostFill(this, num, verts, uvs, cols);
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00042054 File Offset: 0x00040254
	public Vector2 ApplyOffset(List<Vector3> verts, int start)
	{
		Vector2 pivotOffset = base.pivotOffset;
		float num = Mathf.Lerp(0f, (float)(-(float)this.mWidth), pivotOffset.x);
		float num2 = Mathf.Lerp((float)this.mHeight, 0f, pivotOffset.y) + Mathf.Lerp(this.mCalculatedSize.y - (float)this.mHeight, 0f, pivotOffset.y);
		num = Mathf.Round(num);
		num2 = Mathf.Round(num2);
		int i = start;
		int count = verts.Count;
		while (i < count)
		{
			Vector3 value = verts[i];
			value.x += num;
			value.y += num2;
			verts[i] = value;
			i++;
		}
		return new Vector2(num, num2);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00042118 File Offset: 0x00040318
	public void ApplyShadow(List<Vector3> verts, List<Vector2> uvs, List<Color> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= this.finalAlpha;
		if (this.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		Color value = color;
		for (int i = start; i < end; i++)
		{
			verts.Add(verts[i]);
			uvs.Add(uvs[i]);
			cols.Add(cols[i]);
			Vector3 value2 = verts[i];
			value2.x += x;
			value2.y += y;
			verts[i] = value2;
			Color color2 = cols[i];
			if (color2.a == 1f)
			{
				cols[i] = value;
			}
			else
			{
				Color value3 = color;
				value3.a = color2.a * color.a;
				cols[i] = value3;
			}
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000421F5 File Offset: 0x000403F5
	public int CalculateOffsetToFit(string text)
	{
		this.UpdateNGUIText();
		NGUIText.encoding = false;
		NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
		int result = NGUIText.CalculateOffsetToFit(text);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0004221C File Offset: 0x0004041C
	public void SetCurrentProgress()
	{
		if (UIProgressBar.current != null)
		{
			this.text = UIProgressBar.current.value.ToString("F");
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00042253 File Offset: 0x00040453
	public void SetCurrentPercent()
	{
		if (UIProgressBar.current != null)
		{
			this.text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0004228C File Offset: 0x0004048C
	public void SetCurrentSelection()
	{
		if (UIPopupList.current != null)
		{
			this.text = (UIPopupList.current.isLocalized ? Localization.Get(UIPopupList.current.value, true) : UIPopupList.current.value);
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x000422C9 File Offset: 0x000404C9
	public bool Wrap(string text, out string final)
	{
		return this.Wrap(text, out final, 1000000);
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x000422D8 File Offset: 0x000404D8
	public bool Wrap(string text, out string final, int height)
	{
		this.UpdateNGUIText();
		NGUIText.rectHeight = height;
		NGUIText.regionHeight = height;
		bool result = NGUIText.WrapText(text, out final, false);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00042300 File Offset: 0x00040500
	public void UpdateNGUIText()
	{
		Font trueTypeFont = this.trueTypeFont;
		bool flag = trueTypeFont != null;
		NGUIText.fontSize = this.mFinalFontSize;
		NGUIText.fontStyle = this.mFontStyle;
		NGUIText.rectWidth = this.mWidth;
		NGUIText.rectHeight = this.mHeight;
		NGUIText.regionWidth = Mathf.RoundToInt((float)this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
		NGUIText.regionHeight = Mathf.RoundToInt((float)this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
		NGUIText.gradient = (this.mApplyGradient && !this.packedFontShader);
		NGUIText.gradientTop = this.mGradientTop;
		NGUIText.gradientBottom = this.mGradientBottom;
		NGUIText.encoding = this.mEncoding;
		NGUIText.premultiply = this.mPremultiply;
		NGUIText.symbolStyle = this.mSymbols;
		NGUIText.maxLines = this.mMaxLineCount;
		NGUIText.spacingX = this.effectiveSpacingX;
		NGUIText.spacingY = this.effectiveSpacingY;
		INGUIFont inguifont = this.bitmapFont;
		if (flag)
		{
			NGUIText.fontScale = this.mScale;
		}
		else if (inguifont != null)
		{
			inguifont = inguifont.finalFont;
			NGUIText.fontScale = (float)this.mFontSize / (float)inguifont.defaultSize * this.mScale;
		}
		else
		{
			NGUIText.fontScale = this.mScale;
		}
		if (inguifont != null)
		{
			if (trueTypeFont != null)
			{
				NGUIText.dynamicFont = trueTypeFont;
				NGUIText.bitmapFont = null;
			}
			else
			{
				NGUIText.dynamicFont = null;
				NGUIText.bitmapFont = inguifont;
			}
		}
		else
		{
			NGUIText.dynamicFont = trueTypeFont;
			NGUIText.bitmapFont = null;
		}
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				NGUIText.pixelDensity = ((root != null) ? root.pixelSizeAdjustment : 1f);
			}
		}
		else
		{
			NGUIText.pixelDensity = 1f;
		}
		if (this.mDensity != NGUIText.pixelDensity)
		{
			this.ProcessText(false, false);
			NGUIText.rectWidth = this.mWidth;
			NGUIText.rectHeight = this.mHeight;
			NGUIText.regionWidth = Mathf.RoundToInt((float)this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
			NGUIText.regionHeight = Mathf.RoundToInt((float)this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
		}
		if (this.alignment == NGUIText.Alignment.Automatic)
		{
			UIWidget.Pivot pivot = base.pivot;
			if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
			{
				NGUIText.alignment = NGUIText.Alignment.Left;
			}
			else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
			{
				NGUIText.alignment = NGUIText.Alignment.Right;
			}
			else
			{
				NGUIText.alignment = NGUIText.Alignment.Center;
			}
		}
		else
		{
			NGUIText.alignment = this.alignment;
		}
		NGUIText.Update();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00042593 File Offset: 0x00040793
	private void OnApplicationPause(bool paused)
	{
		if (!paused && this.mTrueTypeFont != null)
		{
			this.Invalidate(false);
		}
	}

	// Token: 0x040006FD RID: 1789
	public UILabel.Crispness keepCrispWhenShrunk = UILabel.Crispness.OnDesktop;

	// Token: 0x040006FE RID: 1790
	[HideInInspector]
	[SerializeField]
	private Font mTrueTypeFont;

	// Token: 0x040006FF RID: 1791
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mFont;

	// Token: 0x04000700 RID: 1792
	[Multiline(6)]
	[HideInInspector]
	[SerializeField]
	private string mText = "";

	// Token: 0x04000701 RID: 1793
	[HideInInspector]
	[SerializeField]
	private int mFontSize = 16;

	// Token: 0x04000702 RID: 1794
	[HideInInspector]
	[SerializeField]
	private FontStyle mFontStyle;

	// Token: 0x04000703 RID: 1795
	[HideInInspector]
	[SerializeField]
	private NGUIText.Alignment mAlignment;

	// Token: 0x04000704 RID: 1796
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x04000705 RID: 1797
	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	// Token: 0x04000706 RID: 1798
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x04000707 RID: 1799
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x04000708 RID: 1800
	[HideInInspector]
	[SerializeField]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	// Token: 0x04000709 RID: 1801
	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x0400070A RID: 1802
	[HideInInspector]
	[SerializeField]
	private UILabel.Overflow mOverflow;

	// Token: 0x0400070B RID: 1803
	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	// Token: 0x0400070C RID: 1804
	[HideInInspector]
	[SerializeField]
	private Color mGradientTop = Color.white;

	// Token: 0x0400070D RID: 1805
	[HideInInspector]
	[SerializeField]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x0400070E RID: 1806
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x0400070F RID: 1807
	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	// Token: 0x04000710 RID: 1808
	[HideInInspector]
	[SerializeField]
	private bool mUseFloatSpacing;

	// Token: 0x04000711 RID: 1809
	[HideInInspector]
	[SerializeField]
	private float mFloatSpacingX;

	// Token: 0x04000712 RID: 1810
	[HideInInspector]
	[SerializeField]
	private float mFloatSpacingY;

	// Token: 0x04000713 RID: 1811
	[HideInInspector]
	[SerializeField]
	private bool mOverflowEllipsis;

	// Token: 0x04000714 RID: 1812
	[HideInInspector]
	[SerializeField]
	private int mOverflowWidth;

	// Token: 0x04000715 RID: 1813
	[HideInInspector]
	[SerializeField]
	private int mOverflowHeight;

	// Token: 0x04000716 RID: 1814
	[HideInInspector]
	[SerializeField]
	private UILabel.Modifier mModifier;

	// Token: 0x04000717 RID: 1815
	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	// Token: 0x04000718 RID: 1816
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x04000719 RID: 1817
	[HideInInspector]
	[SerializeField]
	private int mMaxLineHeight;

	// Token: 0x0400071A RID: 1818
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x0400071B RID: 1819
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x0400071C RID: 1820
	[NonSerialized]
	private Font mActiveTTF;

	// Token: 0x0400071D RID: 1821
	[NonSerialized]
	private float mDensity = 1f;

	// Token: 0x0400071E RID: 1822
	[NonSerialized]
	private bool mShouldBeProcessed = true;

	// Token: 0x0400071F RID: 1823
	[NonSerialized]
	private string mProcessedText;

	// Token: 0x04000720 RID: 1824
	[NonSerialized]
	private bool mPremultiply;

	// Token: 0x04000721 RID: 1825
	[NonSerialized]
	private Vector2 mCalculatedSize = Vector2.zero;

	// Token: 0x04000722 RID: 1826
	[NonSerialized]
	private float mScale = 1f;

	// Token: 0x04000723 RID: 1827
	[NonSerialized]
	private int mFinalFontSize;

	// Token: 0x04000724 RID: 1828
	[NonSerialized]
	private int mLastWidth;

	// Token: 0x04000725 RID: 1829
	[NonSerialized]
	private int mLastHeight;

	// Token: 0x04000726 RID: 1830
	public UILabel.ModifierFunc customModifier;

	// Token: 0x04000727 RID: 1831
	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	// Token: 0x04000728 RID: 1832
	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	// Token: 0x04000729 RID: 1833
	[NonSerialized]
	private static BetterList<UIDrawCall> mTempDrawcalls;

	// Token: 0x0400072A RID: 1834
	private static bool mTexRebuildAdded = false;

	// Token: 0x0400072B RID: 1835
	private static List<Vector3> mTempVerts = new List<Vector3>();

	// Token: 0x0400072C RID: 1836
	private static List<int> mTempIndices = new List<int>();

	// Token: 0x0200067D RID: 1661
	[DoNotObfuscateNGUI]
	public enum Effect
	{
		// Token: 0x040045C5 RID: 17861
		None,
		// Token: 0x040045C6 RID: 17862
		Shadow,
		// Token: 0x040045C7 RID: 17863
		Outline,
		// Token: 0x040045C8 RID: 17864
		Outline8
	}

	// Token: 0x0200067E RID: 1662
	[DoNotObfuscateNGUI]
	public enum Overflow
	{
		// Token: 0x040045CA RID: 17866
		ShrinkContent,
		// Token: 0x040045CB RID: 17867
		ClampContent,
		// Token: 0x040045CC RID: 17868
		ResizeFreely,
		// Token: 0x040045CD RID: 17869
		ResizeHeight
	}

	// Token: 0x0200067F RID: 1663
	[DoNotObfuscateNGUI]
	public enum Crispness
	{
		// Token: 0x040045CF RID: 17871
		Never,
		// Token: 0x040045D0 RID: 17872
		OnDesktop,
		// Token: 0x040045D1 RID: 17873
		Always
	}

	// Token: 0x02000680 RID: 1664
	[DoNotObfuscateNGUI]
	public enum Modifier
	{
		// Token: 0x040045D3 RID: 17875
		None,
		// Token: 0x040045D4 RID: 17876
		ToUppercase,
		// Token: 0x040045D5 RID: 17877
		ToLowercase,
		// Token: 0x040045D6 RID: 17878
		Custom = 255
	}

	// Token: 0x02000681 RID: 1665
	// (Invoke) Token: 0x06002B4A RID: 11082
	public delegate string ModifierFunc(string s);
}
