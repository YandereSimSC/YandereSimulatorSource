using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005D9 RID: 1497
	public interface ITransform
	{
		// Token: 0x06002933 RID: 10547
		Vector3 Transform(Vector3 position);

		// Token: 0x06002934 RID: 10548
		Vector3 InverseTransform(Vector3 position);
	}
}
