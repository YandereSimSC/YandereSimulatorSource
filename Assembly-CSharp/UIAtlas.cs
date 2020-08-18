using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class UIAtlas : MonoBehaviour, INGUIAtlas
{
	// Token: 0x17000126 RID: 294
	// (get) Token: 0x060006ED RID: 1773 RVA: 0x00038ECC File Offset: 0x000370CC
	// (set) Token: 0x060006EE RID: 1774 RVA: 0x00038EF0 File Offset: 0x000370F0
	public Material spriteMaterial
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement == null)
			{
				return this.material;
			}
			return replacement.spriteMaterial;
		}
		set
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				replacement.spriteMaterial = value;
				return;
			}
			if (this.material == null)
			{
				this.mPMA = 0;
				this.material = value;
				return;
			}
			this.MarkAsChanged();
			this.mPMA = -1;
			this.material = value;
			this.MarkAsChanged();
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x060006EF RID: 1775 RVA: 0x00038F48 File Offset: 0x00037148
	public bool premultipliedAlpha
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material spriteMaterial = this.spriteMaterial;
				this.mPMA = ((spriteMaterial != null && spriteMaterial.shader != null && spriteMaterial.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00038FB8 File Offset: 0x000371B8
	// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00038FF0 File Offset: 0x000371F0
	public List<UISpriteData> spriteList
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.spriteList;
			}
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			return this.mSprites;
		}
		set
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				replacement.spriteList = value;
				return;
			}
			this.mSprites = value;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00039018 File Offset: 0x00037218
	public Texture texture
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.texture;
			}
			if (!(this.material != null))
			{
				return null;
			}
			return this.material.mainTexture;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00039054 File Offset: 0x00037254
	// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00039078 File Offset: 0x00037278
	public float pixelSize
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement == null)
			{
				return this.mPixelSize;
			}
			return replacement.pixelSize;
		}
		set
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				replacement.pixelSize = value;
				return;
			}
			float num = Mathf.Clamp(value, 0.25f, 4f);
			if (this.mPixelSize != num)
			{
				this.mPixelSize = num;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x060006F5 RID: 1781 RVA: 0x000390BE File Offset: 0x000372BE
	// (set) Token: 0x060006F6 RID: 1782 RVA: 0x000390CC File Offset: 0x000372CC
	public INGUIAtlas replacement
	{
		get
		{
			return this.mReplacement as INGUIAtlas;
		}
		set
		{
			INGUIAtlas inguiatlas = value;
			if (inguiatlas == this)
			{
				inguiatlas = null;
			}
			if (this.mReplacement as INGUIAtlas != inguiatlas)
			{
				if (inguiatlas != null && inguiatlas.replacement == this)
				{
					inguiatlas.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsChanged();
				}
				this.mReplacement = (inguiatlas as UnityEngine.Object);
				if (inguiatlas != null)
				{
					this.material = null;
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00039134 File Offset: 0x00037334
	public UISpriteData GetSprite(string name)
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			return replacement.GetSprite(name);
		}
		if (!string.IsNullOrEmpty(name))
		{
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			if (this.mSprites.Count == 0)
			{
				return null;
			}
			if (this.mSpriteIndices.Count != this.mSprites.Count)
			{
				this.MarkSpriteListAsChanged();
			}
			int num;
			if (this.mSpriteIndices.TryGetValue(name, out num))
			{
				if (num > -1 && num < this.mSprites.Count)
				{
					return this.mSprites[num];
				}
				this.MarkSpriteListAsChanged();
				if (!this.mSpriteIndices.TryGetValue(name, out num))
				{
					return null;
				}
				return this.mSprites[num];
			}
			else
			{
				int i = 0;
				int count = this.mSprites.Count;
				while (i < count)
				{
					UISpriteData uispriteData = this.mSprites[i];
					if (!string.IsNullOrEmpty(uispriteData.name) && name == uispriteData.name)
					{
						this.MarkSpriteListAsChanged();
						return uispriteData;
					}
					i++;
				}
			}
		}
		return null;
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00039240 File Offset: 0x00037440
	public void MarkSpriteListAsChanged()
	{
		this.mSpriteIndices.Clear();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			this.mSpriteIndices[this.mSprites[i].name] = i;
			i++;
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0003928D File Offset: 0x0003748D
	public void SortAlphabetically()
	{
		this.mSprites.Sort((UISpriteData s1, UISpriteData s2) => s1.name.CompareTo(s2.name));
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x000392BC File Offset: 0x000374BC
	public BetterList<string> GetListOfSprites()
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			return replacement.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name))
			{
				betterList.Add(uispriteData.name);
			}
			i++;
		}
		return betterList;
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0003933C File Offset: 0x0003753C
	public BetterList<string> GetListOfSprites(string match)
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			return replacement.GetListOfSprites(match);
		}
		if (string.IsNullOrEmpty(match))
		{
			return this.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name) && string.Equals(match, uispriteData.name, StringComparison.OrdinalIgnoreCase))
			{
				betterList.Add(uispriteData.name);
				return betterList;
			}
			i++;
		}
		string[] array = match.Split(new char[]
		{
			' '
		}, StringSplitOptions.RemoveEmptyEntries);
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = array[j].ToLower();
		}
		int k = 0;
		int count2 = this.mSprites.Count;
		while (k < count2)
		{
			UISpriteData uispriteData2 = this.mSprites[k];
			if (uispriteData2 != null && !string.IsNullOrEmpty(uispriteData2.name))
			{
				string text = uispriteData2.name.ToLower();
				int num = 0;
				for (int l = 0; l < array.Length; l++)
				{
					if (text.Contains(array[l]))
					{
						num++;
					}
				}
				if (num == array.Length)
				{
					betterList.Add(uispriteData2.name);
				}
			}
			k++;
		}
		return betterList;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0003949C File Offset: 0x0003769C
	public bool References(INGUIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (atlas == this)
		{
			return true;
		}
		INGUIAtlas replacement = this.replacement;
		return replacement != null && replacement.References(atlas);
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x000394C8 File Offset: 0x000376C8
	public void MarkAsChanged()
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			replacement.MarkAsChanged();
		}
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (NGUITools.CheckIfRelated(this, uisprite.atlas))
			{
				INGUIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		NGUIFont[] array2 = Resources.FindObjectsOfTypeAll<NGUIFont>();
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			NGUIFont nguifont = array2[j];
			if (nguifont.atlas != null && NGUITools.CheckIfRelated(this, nguifont.atlas))
			{
				INGUIAtlas atlas2 = nguifont.atlas;
				nguifont.atlas = null;
				nguifont.atlas = atlas2;
			}
			j++;
		}
		UIFont[] array3 = Resources.FindObjectsOfTypeAll<UIFont>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			UIFont uifont = array3[k];
			if (NGUITools.CheckIfRelated(this, uifont.atlas))
			{
				INGUIAtlas atlas3 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas3;
			}
			k++;
		}
		UILabel[] array4 = NGUITools.FindActive<UILabel>();
		int l = 0;
		int num4 = array4.Length;
		while (l < num4)
		{
			UILabel uilabel = array4[l];
			if (uilabel.atlas != null && NGUITools.CheckIfRelated(this, uilabel.atlas))
			{
				INGUIAtlas atlas4 = uilabel.atlas;
				INGUIFont bitmapFont = uilabel.bitmapFont;
				uilabel.bitmapFont = null;
				uilabel.bitmapFont = bitmapFont;
			}
			l++;
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00039630 File Offset: 0x00037830
	private bool Upgrade()
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			UIAtlas uiatlas = replacement as UIAtlas;
			if (uiatlas != null)
			{
				return uiatlas.Upgrade();
			}
		}
		if (this.mSprites.Count == 0 && this.sprites.Count > 0 && this.material)
		{
			Texture mainTexture = this.material.mainTexture;
			int width = (mainTexture != null) ? mainTexture.width : 512;
			int height = (mainTexture != null) ? mainTexture.height : 512;
			for (int i = 0; i < this.sprites.Count; i++)
			{
				UIAtlas.Sprite sprite = this.sprites[i];
				Rect outer = sprite.outer;
				Rect inner = sprite.inner;
				if (this.mCoordinates == UIAtlas.Coordinates.TexCoords)
				{
					NGUIMath.ConvertToPixels(outer, width, height, true);
					NGUIMath.ConvertToPixels(inner, width, height, true);
				}
				UISpriteData uispriteData = new UISpriteData();
				uispriteData.name = sprite.name;
				uispriteData.x = Mathf.RoundToInt(outer.xMin);
				uispriteData.y = Mathf.RoundToInt(outer.yMin);
				uispriteData.width = Mathf.RoundToInt(outer.width);
				uispriteData.height = Mathf.RoundToInt(outer.height);
				uispriteData.paddingLeft = Mathf.RoundToInt(sprite.paddingLeft * outer.width);
				uispriteData.paddingRight = Mathf.RoundToInt(sprite.paddingRight * outer.width);
				uispriteData.paddingBottom = Mathf.RoundToInt(sprite.paddingBottom * outer.height);
				uispriteData.paddingTop = Mathf.RoundToInt(sprite.paddingTop * outer.height);
				uispriteData.borderLeft = Mathf.RoundToInt(inner.xMin - outer.xMin);
				uispriteData.borderRight = Mathf.RoundToInt(outer.xMax - inner.xMax);
				uispriteData.borderBottom = Mathf.RoundToInt(outer.yMax - inner.yMax);
				uispriteData.borderTop = Mathf.RoundToInt(inner.yMin - outer.yMin);
				this.mSprites.Add(uispriteData);
			}
			this.sprites.Clear();
			return true;
		}
		return false;
	}

	// Token: 0x04000643 RID: 1603
	[HideInInspector]
	[SerializeField]
	private Material material;

	// Token: 0x04000644 RID: 1604
	[HideInInspector]
	[SerializeField]
	private List<UISpriteData> mSprites = new List<UISpriteData>();

	// Token: 0x04000645 RID: 1605
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x04000646 RID: 1606
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mReplacement;

	// Token: 0x04000647 RID: 1607
	[HideInInspector]
	[SerializeField]
	private UIAtlas.Coordinates mCoordinates;

	// Token: 0x04000648 RID: 1608
	[HideInInspector]
	[SerializeField]
	private List<UIAtlas.Sprite> sprites = new List<UIAtlas.Sprite>();

	// Token: 0x04000649 RID: 1609
	[NonSerialized]
	private int mPMA = -1;

	// Token: 0x0400064A RID: 1610
	[NonSerialized]
	private Dictionary<string, int> mSpriteIndices = new Dictionary<string, int>();

	// Token: 0x0200065B RID: 1627
	[Serializable]
	private class Sprite
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x001C443C File Offset: 0x001C263C
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x04004571 RID: 17777
		public string name = "Unity Bug";

		// Token: 0x04004572 RID: 17778
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04004573 RID: 17779
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04004574 RID: 17780
		public bool rotated;

		// Token: 0x04004575 RID: 17781
		public float paddingLeft;

		// Token: 0x04004576 RID: 17782
		public float paddingRight;

		// Token: 0x04004577 RID: 17783
		public float paddingTop;

		// Token: 0x04004578 RID: 17784
		public float paddingBottom;
	}

	// Token: 0x0200065C RID: 1628
	private enum Coordinates
	{
		// Token: 0x0400457A RID: 17786
		Pixels,
		// Token: 0x0400457B RID: 17787
		TexCoords
	}
}
