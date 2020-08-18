using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200053F RID: 1343
	[Serializable]
	public class AstarColor
	{
		// Token: 0x060023E1 RID: 9185 RVA: 0x00195AF4 File Offset: 0x00193CF4
		public static Color GetAreaColor(uint area)
		{
			if (AstarColor.AreaColors == null || (ulong)area >= (ulong)((long)AstarColor.AreaColors.Length))
			{
				return AstarMath.IntToColor((int)area, 1f);
			}
			return AstarColor.AreaColors[(int)area];
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00195B20 File Offset: 0x00193D20
		public void OnEnable()
		{
			AstarColor.NodeConnection = this._NodeConnection;
			AstarColor.UnwalkableNode = this._UnwalkableNode;
			AstarColor.BoundsHandles = this._BoundsHandles;
			AstarColor.ConnectionLowLerp = this._ConnectionLowLerp;
			AstarColor.ConnectionHighLerp = this._ConnectionHighLerp;
			AstarColor.MeshEdgeColor = this._MeshEdgeColor;
			AstarColor.AreaColors = this._AreaColors;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00195B7C File Offset: 0x00193D7C
		public AstarColor()
		{
			this._NodeConnection = new Color(1f, 1f, 1f, 0.9f);
			this._UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);
			this._BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);
			this._ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);
			this._ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);
			this._MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);
		}

		// Token: 0x04003FA0 RID: 16288
		public Color _NodeConnection;

		// Token: 0x04003FA1 RID: 16289
		public Color _UnwalkableNode;

		// Token: 0x04003FA2 RID: 16290
		public Color _BoundsHandles;

		// Token: 0x04003FA3 RID: 16291
		public Color _ConnectionLowLerp;

		// Token: 0x04003FA4 RID: 16292
		public Color _ConnectionHighLerp;

		// Token: 0x04003FA5 RID: 16293
		public Color _MeshEdgeColor;

		// Token: 0x04003FA6 RID: 16294
		public Color[] _AreaColors;

		// Token: 0x04003FA7 RID: 16295
		public static Color NodeConnection = new Color(1f, 1f, 1f, 0.9f);

		// Token: 0x04003FA8 RID: 16296
		public static Color UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04003FA9 RID: 16297
		public static Color BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);

		// Token: 0x04003FAA RID: 16298
		public static Color ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);

		// Token: 0x04003FAB RID: 16299
		public static Color ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04003FAC RID: 16300
		public static Color MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04003FAD RID: 16301
		private static Color[] AreaColors;
	}
}
