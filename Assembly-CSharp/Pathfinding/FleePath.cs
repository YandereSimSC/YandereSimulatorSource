using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000594 RID: 1428
	public class FleePath : RandomPath
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x001AA398 File Offset: 0x001A8598
		public static FleePath Construct(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback = null)
		{
			FleePath path = PathPool.GetPath<FleePath>();
			path.Setup(start, avoid, searchLength, callback);
			return path;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x001AA3AC File Offset: 0x001A85AC
		protected void Setup(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback)
		{
			base.Setup(start, searchLength, callback);
			this.aim = avoid - start;
			this.aim *= 10f;
			this.aim = start - this.aim;
		}
	}
}
