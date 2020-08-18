using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000580 RID: 1408
	[AddComponentMenu("Pathfinding/Modifiers/Funnel")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_funnel_modifier.php")]
	[Serializable]
	public class FunnelModifier : MonoModifier
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x001A5BF7 File Offset: 0x001A3DF7
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x001A5D98 File Offset: 0x001A3F98
		public override void Apply(Path p)
		{
			if (p.path == null || p.path.Count == 0 || p.vectorPath == null || p.vectorPath.Count == 0)
			{
				return;
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			List<Funnel.PathPart> list2 = Funnel.SplitIntoParts(p);
			if (list2.Count == 0)
			{
				return;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				Funnel.PathPart pathPart = list2[i];
				if (!pathPart.isLink)
				{
					Funnel.FunnelPortals funnel = Funnel.ConstructFunnelPortals(p.path, pathPart);
					List<Vector3> collection = Funnel.Calculate(funnel, this.unwrap, this.splitAtEveryPortal);
					list.AddRange(collection);
					ListPool<Vector3>.Release(ref funnel.left);
					ListPool<Vector3>.Release(ref funnel.right);
					ListPool<Vector3>.Release(ref collection);
				}
				else
				{
					if (i == 0 || list2[i - 1].isLink)
					{
						list.Add(pathPart.startPoint);
					}
					if (i == list2.Count - 1 || list2[i + 1].isLink)
					{
						list.Add(pathPart.endPoint);
					}
				}
			}
			ListPool<Funnel.PathPart>.Release(ref list2);
			ListPool<Vector3>.Release(ref p.vectorPath);
			p.vectorPath = list;
		}

		// Token: 0x0400412E RID: 16686
		public bool unwrap = true;

		// Token: 0x0400412F RID: 16687
		public bool splitAtEveryPortal;
	}
}
