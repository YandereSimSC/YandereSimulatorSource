using System;

// Token: 0x020000D2 RID: 210
[Serializable]
public class ArrayLayout
{
	// Token: 0x04000A3B RID: 2619
	public ArrayLayout.rowData[] rows = new ArrayLayout.rowData[6];

	// Token: 0x02000696 RID: 1686
	[Serializable]
	public struct rowData
	{
		// Token: 0x04004616 RID: 17942
		public bool[] row;
	}
}
