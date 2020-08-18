using System;

// Token: 0x020000AC RID: 172
[Serializable]
public class UISpriteData
{
	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00047219 File Offset: 0x00045419
	public bool hasBorder
	{
		get
		{
			return (this.borderLeft | this.borderRight | this.borderTop | this.borderBottom) != 0;
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00047239 File Offset: 0x00045439
	public bool hasPadding
	{
		get
		{
			return (this.paddingLeft | this.paddingRight | this.paddingTop | this.paddingBottom) != 0;
		}
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00047259 File Offset: 0x00045459
	public void SetRect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00047278 File Offset: 0x00045478
	public void SetPadding(int left, int bottom, int right, int top)
	{
		this.paddingLeft = left;
		this.paddingBottom = bottom;
		this.paddingRight = right;
		this.paddingTop = top;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00047297 File Offset: 0x00045497
	public void SetBorder(int left, int bottom, int right, int top)
	{
		this.borderLeft = left;
		this.borderBottom = bottom;
		this.borderRight = right;
		this.borderTop = top;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x000472B8 File Offset: 0x000454B8
	public void CopyFrom(UISpriteData sd)
	{
		this.name = sd.name;
		this.x = sd.x;
		this.y = sd.y;
		this.width = sd.width;
		this.height = sd.height;
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
		this.paddingLeft = sd.paddingLeft;
		this.paddingRight = sd.paddingRight;
		this.paddingTop = sd.paddingTop;
		this.paddingBottom = sd.paddingBottom;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00047361 File Offset: 0x00045561
	public void CopyBorderFrom(UISpriteData sd)
	{
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
	}

	// Token: 0x04000784 RID: 1924
	public string name = "Sprite";

	// Token: 0x04000785 RID: 1925
	public int x;

	// Token: 0x04000786 RID: 1926
	public int y;

	// Token: 0x04000787 RID: 1927
	public int width;

	// Token: 0x04000788 RID: 1928
	public int height;

	// Token: 0x04000789 RID: 1929
	public int borderLeft;

	// Token: 0x0400078A RID: 1930
	public int borderRight;

	// Token: 0x0400078B RID: 1931
	public int borderTop;

	// Token: 0x0400078C RID: 1932
	public int borderBottom;

	// Token: 0x0400078D RID: 1933
	public int paddingLeft;

	// Token: 0x0400078E RID: 1934
	public int paddingRight;

	// Token: 0x0400078F RID: 1935
	public int paddingTop;

	// Token: 0x04000790 RID: 1936
	public int paddingBottom;
}
