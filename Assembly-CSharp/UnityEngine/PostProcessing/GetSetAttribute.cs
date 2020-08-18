using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004A7 RID: 1191
	public sealed class GetSetAttribute : PropertyAttribute
	{
		// Token: 0x06001E3E RID: 7742 RVA: 0x0017A25D File Offset: 0x0017845D
		public GetSetAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x04003C44 RID: 15428
		public readonly string name;

		// Token: 0x04003C45 RID: 15429
		public bool dirty;
	}
}
