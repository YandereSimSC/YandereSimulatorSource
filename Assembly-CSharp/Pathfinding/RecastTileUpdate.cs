using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055D RID: 1373
	[AddComponentMenu("Pathfinding/Navmesh/RecastTileUpdate")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_tile_update.php")]
	public class RecastTileUpdate : MonoBehaviour
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06002442 RID: 9282 RVA: 0x00196E48 File Offset: 0x00195048
		// (remove) Token: 0x06002443 RID: 9283 RVA: 0x00196E7C File Offset: 0x0019507C
		public static event Action<Bounds> OnNeedUpdates;

		// Token: 0x06002444 RID: 9284 RVA: 0x00196EAF File Offset: 0x001950AF
		private void Start()
		{
			this.ScheduleUpdate();
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00196EAF File Offset: 0x001950AF
		private void OnDestroy()
		{
			this.ScheduleUpdate();
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00196EB8 File Offset: 0x001950B8
		public void ScheduleUpdate()
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				if (RecastTileUpdate.OnNeedUpdates != null)
				{
					RecastTileUpdate.OnNeedUpdates(component.bounds);
					return;
				}
			}
			else if (RecastTileUpdate.OnNeedUpdates != null)
			{
				RecastTileUpdate.OnNeedUpdates(new Bounds(base.transform.position, Vector3.zero));
			}
		}
	}
}
