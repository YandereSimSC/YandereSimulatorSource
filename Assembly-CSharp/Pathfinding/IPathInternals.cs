using System;

namespace Pathfinding
{
	// Token: 0x0200053C RID: 1340
	internal interface IPathInternals
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060023C0 RID: 9152
		PathHandler PathHandler { get; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060023C1 RID: 9153
		// (set) Token: 0x060023C2 RID: 9154
		bool Pooled { get; set; }

		// Token: 0x060023C3 RID: 9155
		void AdvanceState(PathState s);

		// Token: 0x060023C4 RID: 9156
		void OnEnterPool();

		// Token: 0x060023C5 RID: 9157
		void Reset();

		// Token: 0x060023C6 RID: 9158
		void ReturnPath();

		// Token: 0x060023C7 RID: 9159
		void PrepareBase(PathHandler handler);

		// Token: 0x060023C8 RID: 9160
		void Prepare();

		// Token: 0x060023C9 RID: 9161
		void Initialize();

		// Token: 0x060023CA RID: 9162
		void Cleanup();

		// Token: 0x060023CB RID: 9163
		void CalculateStep(long targetTick);
	}
}
