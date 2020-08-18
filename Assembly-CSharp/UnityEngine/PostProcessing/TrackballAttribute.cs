using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004A9 RID: 1193
	public sealed class TrackballAttribute : PropertyAttribute
	{
		// Token: 0x06001E40 RID: 7744 RVA: 0x0017A27B File Offset: 0x0017847B
		public TrackballAttribute(string method)
		{
			this.method = method;
		}

		// Token: 0x04003C47 RID: 15431
		public readonly string method;
	}
}
