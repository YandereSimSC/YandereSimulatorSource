using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000545 RID: 1349
	public struct Progress
	{
		// Token: 0x060023F5 RID: 9205 RVA: 0x00195F29 File Offset: 0x00194129
		public Progress(float progress, string description)
		{
			this.progress = progress;
			this.description = description;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00195F39 File Offset: 0x00194139
		public Progress MapTo(float min, float max, string prefix = null)
		{
			return new Progress(Mathf.Lerp(min, max, this.progress), prefix + this.description);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00195F5C File Offset: 0x0019415C
		public override string ToString()
		{
			return this.progress.ToString("0.0") + " " + this.description;
		}

		// Token: 0x04003FC2 RID: 16322
		public readonly float progress;

		// Token: 0x04003FC3 RID: 16323
		public readonly string description;
	}
}
