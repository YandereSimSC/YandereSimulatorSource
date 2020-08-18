using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057D RID: 1405
	[AddComponentMenu("Pathfinding/Navmesh/RecastMeshObj")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_mesh_obj.php")]
	public class RecastMeshObj : VersionedMonoBehaviour
	{
		// Token: 0x06002632 RID: 9778 RVA: 0x001A5760 File Offset: 0x001A3960
		public static void GetAllInBounds(List<RecastMeshObj> buffer, Bounds bounds)
		{
			if (!Application.isPlaying)
			{
				RecastMeshObj[] array = UnityEngine.Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].RecalculateBounds();
					if (array[i].GetBounds().Intersects(bounds))
					{
						buffer.Add(array[i]);
					}
				}
				return;
			}
			if (Time.timeSinceLevelLoad == 0f)
			{
				RecastMeshObj[] array2 = UnityEngine.Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Register();
				}
			}
			for (int k = 0; k < RecastMeshObj.dynamicMeshObjs.Count; k++)
			{
				if (RecastMeshObj.dynamicMeshObjs[k].GetBounds().Intersects(bounds))
				{
					buffer.Add(RecastMeshObj.dynamicMeshObjs[k]);
				}
			}
			Rect rect = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			RecastMeshObj.tree.QueryInBounds(rect, buffer);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x001A5884 File Offset: 0x001A3A84
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x001A588C File Offset: 0x001A3A8C
		private void Register()
		{
			if (this.registered)
			{
				return;
			}
			this.registered = true;
			this.area = Mathf.Clamp(this.area, -1, 33554432);
			Renderer component = base.GetComponent<Renderer>();
			Collider component2 = base.GetComponent<Collider>();
			if (component == null && component2 == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component3 = base.GetComponent<MeshFilter>();
			if (component != null && component3 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			this.bounds = ((component != null) ? component.bounds : component2.bounds);
			this._dynamic = this.dynamic;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Add(this);
				return;
			}
			RecastMeshObj.tree.Insert(this);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x001A5958 File Offset: 0x001A3B58
		private void RecalculateBounds()
		{
			Renderer component = base.GetComponent<Renderer>();
			Collider collider = this.GetCollider();
			if (component == null && collider == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component2 = base.GetComponent<MeshFilter>();
			if (component != null && component2 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			this.bounds = ((component != null) ? component.bounds : collider.bounds);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x001A59D1 File Offset: 0x001A3BD1
		public Bounds GetBounds()
		{
			if (this._dynamic)
			{
				this.RecalculateBounds();
			}
			return this.bounds;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x001A59E7 File Offset: 0x001A3BE7
		public MeshFilter GetMeshFilter()
		{
			return base.GetComponent<MeshFilter>();
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x001A59EF File Offset: 0x001A3BEF
		public Collider GetCollider()
		{
			return base.GetComponent<Collider>();
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x001A59F8 File Offset: 0x001A3BF8
		private void OnDisable()
		{
			this.registered = false;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Remove(this);
			}
			else if (!RecastMeshObj.tree.Remove(this))
			{
				throw new Exception("Could not remove RecastMeshObj from tree even though it should exist in it. Has the object moved without being marked as dynamic?");
			}
			this._dynamic = this.dynamic;
		}

		// Token: 0x0400411E RID: 16670
		protected static RecastBBTree tree = new RecastBBTree();

		// Token: 0x0400411F RID: 16671
		protected static List<RecastMeshObj> dynamicMeshObjs = new List<RecastMeshObj>();

		// Token: 0x04004120 RID: 16672
		[HideInInspector]
		public Bounds bounds;

		// Token: 0x04004121 RID: 16673
		public bool dynamic = true;

		// Token: 0x04004122 RID: 16674
		public int area;

		// Token: 0x04004123 RID: 16675
		private bool _dynamic;

		// Token: 0x04004124 RID: 16676
		private bool registered;
	}
}
