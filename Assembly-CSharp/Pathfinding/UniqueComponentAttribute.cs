using System;

namespace Pathfinding
{
	// Token: 0x0200058E RID: 1422
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UniqueComponentAttribute : Attribute
	{
		// Token: 0x04004186 RID: 16774
		public string tag;
	}
}
