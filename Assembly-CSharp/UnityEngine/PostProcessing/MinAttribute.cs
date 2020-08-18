using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004A8 RID: 1192
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x0017A26C File Offset: 0x0017846C
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04003C46 RID: 15430
		public readonly float min;
	}
}
