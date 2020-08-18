using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005EA RID: 1514
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_local_space_rich_a_i.php")]
	public class LocalSpaceRichAI : RichAI
	{
		// Token: 0x060029BF RID: 10687 RVA: 0x001C1456 File Offset: 0x001BF656
		private void RefreshTransform()
		{
			this.graph.Refresh();
			this.richPath.transform = this.graph.transformation;
			this.movementPlane = this.graph.transformation;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x001C148A File Offset: 0x001BF68A
		protected override void Start()
		{
			this.RefreshTransform();
			base.Start();
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x001C1498 File Offset: 0x001BF698
		protected override void CalculatePathRequestEndpoints(out Vector3 start, out Vector3 end)
		{
			this.RefreshTransform();
			base.CalculatePathRequestEndpoints(out start, out end);
			start = this.graph.transformation.InverseTransform(start);
			end = this.graph.transformation.InverseTransform(end);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x001C14EB File Offset: 0x001BF6EB
		protected override void Update()
		{
			this.RefreshTransform();
			base.Update();
		}

		// Token: 0x0400438D RID: 17293
		public LocalSpaceGraph graph;
	}
}
