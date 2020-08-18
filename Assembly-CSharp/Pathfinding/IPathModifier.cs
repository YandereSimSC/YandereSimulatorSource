using System;

namespace Pathfinding
{
	// Token: 0x02000581 RID: 1409
	public interface IPathModifier
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600264A RID: 9802
		int Order { get; }

		// Token: 0x0600264B RID: 9803
		void Apply(Path path);

		// Token: 0x0600264C RID: 9804
		void PreProcess(Path path);
	}
}
