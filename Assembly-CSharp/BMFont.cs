using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006D RID: 109
[Serializable]
public class BMFont
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000347 RID: 839 RVA: 0x000204B2 File Offset: 0x0001E6B2
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000348 RID: 840 RVA: 0x000204C2 File Offset: 0x0001E6C2
	// (set) Token: 0x06000349 RID: 841 RVA: 0x000204CA File Offset: 0x0001E6CA
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x0600034A RID: 842 RVA: 0x000204D3 File Offset: 0x0001E6D3
	// (set) Token: 0x0600034B RID: 843 RVA: 0x000204DB File Offset: 0x0001E6DB
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600034C RID: 844 RVA: 0x000204E4 File Offset: 0x0001E6E4
	// (set) Token: 0x0600034D RID: 845 RVA: 0x000204EC File Offset: 0x0001E6EC
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600034E RID: 846 RVA: 0x000204F5 File Offset: 0x0001E6F5
	// (set) Token: 0x0600034F RID: 847 RVA: 0x000204FD File Offset: 0x0001E6FD
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000350 RID: 848 RVA: 0x00020506 File Offset: 0x0001E706
	public int glyphCount
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			return this.mSaved.Count;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000351 RID: 849 RVA: 0x0002051D File Offset: 0x0001E71D
	// (set) Token: 0x06000352 RID: 850 RVA: 0x00020525 File Offset: 0x0001E725
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000353 RID: 851 RVA: 0x0002052E File Offset: 0x0001E72E
	public List<BMGlyph> glyphs
	{
		get
		{
			return this.mSaved;
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00020538 File Offset: 0x0001E738
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph2 = this.mSaved[i];
				this.mDict.Add(bmglyph2.index, bmglyph2);
				i++;
			}
		}
		if (!this.mDict.TryGetValue(index, out bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x000205C7 File Offset: 0x0001E7C7
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x06000356 RID: 854 RVA: 0x000205D1 File Offset: 0x0001E7D1
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x06000357 RID: 855 RVA: 0x000205EC File Offset: 0x0001E7EC
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x04000497 RID: 1175
	[HideInInspector]
	[SerializeField]
	private int mSize = 16;

	// Token: 0x04000498 RID: 1176
	[HideInInspector]
	[SerializeField]
	private int mBase;

	// Token: 0x04000499 RID: 1177
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x0400049A RID: 1178
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x0400049B RID: 1179
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x0400049C RID: 1180
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x0400049D RID: 1181
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
