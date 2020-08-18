using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057B RID: 1403
	public class RecastBBTree
	{
		// Token: 0x06002626 RID: 9766 RVA: 0x001A52FC File Offset: 0x001A34FC
		public void QueryInBounds(Rect bounds, List<RecastMeshObj> buffer)
		{
			if (this.root == null)
			{
				return;
			}
			this.QueryBoxInBounds(this.root, bounds, buffer);
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x001A5318 File Offset: 0x001A3518
		private void QueryBoxInBounds(RecastBBTreeBox box, Rect bounds, List<RecastMeshObj> boxes)
		{
			if (box.mesh != null)
			{
				if (RecastBBTree.RectIntersectsRect(box.rect, bounds))
				{
					boxes.Add(box.mesh);
					return;
				}
			}
			else
			{
				if (RecastBBTree.RectIntersectsRect(box.c1.rect, bounds))
				{
					this.QueryBoxInBounds(box.c1, bounds, boxes);
				}
				if (RecastBBTree.RectIntersectsRect(box.c2.rect, bounds))
				{
					this.QueryBoxInBounds(box.c2, bounds, boxes);
				}
			}
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x001A5390 File Offset: 0x001A3590
		public bool Remove(RecastMeshObj mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (this.root == null)
			{
				return false;
			}
			bool result = false;
			Bounds bounds = mesh.GetBounds();
			Rect bounds2 = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			this.root = this.RemoveBox(this.root, mesh, bounds2, ref result);
			return result;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x001A5414 File Offset: 0x001A3614
		private RecastBBTreeBox RemoveBox(RecastBBTreeBox c, RecastMeshObj mesh, Rect bounds, ref bool found)
		{
			if (!RecastBBTree.RectIntersectsRect(c.rect, bounds))
			{
				return c;
			}
			if (c.mesh == mesh)
			{
				found = true;
				return null;
			}
			if (c.mesh == null && !found)
			{
				c.c1 = this.RemoveBox(c.c1, mesh, bounds, ref found);
				if (c.c1 == null)
				{
					return c.c2;
				}
				if (!found)
				{
					c.c2 = this.RemoveBox(c.c2, mesh, bounds, ref found);
					if (c.c2 == null)
					{
						return c.c1;
					}
				}
				if (found)
				{
					c.rect = RecastBBTree.ExpandToContain(c.c1.rect, c.c2.rect);
				}
			}
			return c;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x001A54D0 File Offset: 0x001A36D0
		public void Insert(RecastMeshObj mesh)
		{
			RecastBBTreeBox recastBBTreeBox = new RecastBBTreeBox(mesh);
			if (this.root == null)
			{
				this.root = recastBBTreeBox;
				return;
			}
			RecastBBTreeBox recastBBTreeBox2 = this.root;
			for (;;)
			{
				recastBBTreeBox2.rect = RecastBBTree.ExpandToContain(recastBBTreeBox2.rect, recastBBTreeBox.rect);
				if (recastBBTreeBox2.mesh != null)
				{
					break;
				}
				float num = RecastBBTree.ExpansionRequired(recastBBTreeBox2.c1.rect, recastBBTreeBox.rect);
				float num2 = RecastBBTree.ExpansionRequired(recastBBTreeBox2.c2.rect, recastBBTreeBox.rect);
				if (num < num2)
				{
					recastBBTreeBox2 = recastBBTreeBox2.c1;
				}
				else if (num2 < num)
				{
					recastBBTreeBox2 = recastBBTreeBox2.c2;
				}
				else
				{
					recastBBTreeBox2 = ((RecastBBTree.RectArea(recastBBTreeBox2.c1.rect) < RecastBBTree.RectArea(recastBBTreeBox2.c2.rect)) ? recastBBTreeBox2.c1 : recastBBTreeBox2.c2);
				}
			}
			recastBBTreeBox2.c1 = recastBBTreeBox;
			RecastBBTreeBox c = new RecastBBTreeBox(recastBBTreeBox2.mesh);
			recastBBTreeBox2.c2 = c;
			recastBBTreeBox2.mesh = null;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x001A55C8 File Offset: 0x001A37C8
		private static bool RectIntersectsRect(Rect r, Rect r2)
		{
			return r.xMax > r2.xMin && r.yMax > r2.yMin && r2.xMax > r.xMin && r2.yMax > r.yMin;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x001A5618 File Offset: 0x001A3818
		private static float ExpansionRequired(Rect r, Rect r2)
		{
			float num = Mathf.Min(r.xMin, r2.xMin);
			float num2 = Mathf.Max(r.xMax, r2.xMax);
			float num3 = Mathf.Min(r.yMin, r2.yMin);
			float num4 = Mathf.Max(r.yMax, r2.yMax);
			return (num2 - num) * (num4 - num3) - RecastBBTree.RectArea(r);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x001A5684 File Offset: 0x001A3884
		private static Rect ExpandToContain(Rect r, Rect r2)
		{
			float xmin = Mathf.Min(r.xMin, r2.xMin);
			float xmax = Mathf.Max(r.xMax, r2.xMax);
			float ymin = Mathf.Min(r.yMin, r2.yMin);
			float ymax = Mathf.Max(r.yMax, r2.yMax);
			return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x001A56E8 File Offset: 0x001A38E8
		private static float RectArea(Rect r)
		{
			return r.width * r.height;
		}

		// Token: 0x04004119 RID: 16665
		private RecastBBTreeBox root;
	}
}
