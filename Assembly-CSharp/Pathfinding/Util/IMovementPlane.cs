using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005D8 RID: 1496
	public interface IMovementPlane
	{
		// Token: 0x06002930 RID: 10544
		Vector2 ToPlane(Vector3 p);

		// Token: 0x06002931 RID: 10545
		Vector2 ToPlane(Vector3 p, out float elevation);

		// Token: 0x06002932 RID: 10546
		Vector3 ToWorld(Vector2 p, float elevation = 0f);
	}
}
