using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000525 RID: 1317
	[ExecuteInEditMode]
	public abstract class GraphModifier : VersionedMonoBehaviour
	{
		// Token: 0x06002275 RID: 8821 RVA: 0x0019033C File Offset: 0x0018E53C
		protected static List<T> GetModifiersOfType<T>() where T : GraphModifier
		{
			GraphModifier graphModifier = GraphModifier.root;
			List<T> list = new List<T>();
			while (graphModifier != null)
			{
				T t = graphModifier as T;
				if (t != null)
				{
					list.Add(t);
				}
				graphModifier = graphModifier.next;
			}
			return list;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0019038C File Offset: 0x0018E58C
		public static void FindAllModifiers()
		{
			GraphModifier[] array = UnityEngine.Object.FindObjectsOfType(typeof(GraphModifier)) as GraphModifier[];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].enabled)
				{
					array[i].OnEnable();
				}
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x001903D0 File Offset: 0x0018E5D0
		public static void TriggerEvent(GraphModifier.EventType type)
		{
			if (!Application.isPlaying)
			{
				GraphModifier.FindAllModifiers();
			}
			GraphModifier graphModifier = GraphModifier.root;
			if (type <= GraphModifier.EventType.PreUpdate)
			{
				switch (type)
				{
				case GraphModifier.EventType.PostScan:
					while (graphModifier != null)
					{
						graphModifier.OnPostScan();
						graphModifier = graphModifier.next;
					}
					return;
				case GraphModifier.EventType.PreScan:
					while (graphModifier != null)
					{
						graphModifier.OnPreScan();
						graphModifier = graphModifier.next;
					}
					return;
				case (GraphModifier.EventType)3:
					break;
				case GraphModifier.EventType.LatePostScan:
					while (graphModifier != null)
					{
						graphModifier.OnLatePostScan();
						graphModifier = graphModifier.next;
					}
					return;
				default:
					if (type != GraphModifier.EventType.PreUpdate)
					{
						return;
					}
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPreUpdate();
						graphModifier = graphModifier.next;
					}
					return;
				}
			}
			else
			{
				if (type == GraphModifier.EventType.PostUpdate)
				{
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPostUpdate();
						graphModifier = graphModifier.next;
					}
					return;
				}
				if (type != GraphModifier.EventType.PostCacheLoad)
				{
					return;
				}
				while (graphModifier != null)
				{
					graphModifier.OnPostCacheLoad();
					graphModifier = graphModifier.next;
				}
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x001904A7 File Offset: 0x0018E6A7
		protected virtual void OnEnable()
		{
			this.RemoveFromLinkedList();
			this.AddToLinkedList();
			this.ConfigureUniqueID();
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x001904BB File Offset: 0x0018E6BB
		protected virtual void OnDisable()
		{
			this.RemoveFromLinkedList();
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x001904C3 File Offset: 0x0018E6C3
		protected override void Awake()
		{
			base.Awake();
			this.ConfigureUniqueID();
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x001904D4 File Offset: 0x0018E6D4
		private void ConfigureUniqueID()
		{
			GraphModifier x;
			if (GraphModifier.usedIDs.TryGetValue(this.uniqueID, out x) && x != this)
			{
				this.Reset();
			}
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00190515 File Offset: 0x0018E715
		private void AddToLinkedList()
		{
			if (GraphModifier.root == null)
			{
				GraphModifier.root = this;
				return;
			}
			this.next = GraphModifier.root;
			GraphModifier.root.prev = this;
			GraphModifier.root = this;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00190548 File Offset: 0x0018E748
		private void RemoveFromLinkedList()
		{
			if (GraphModifier.root == this)
			{
				GraphModifier.root = this.next;
				if (GraphModifier.root != null)
				{
					GraphModifier.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x001905D3 File Offset: 0x0018E7D3
		protected virtual void OnDestroy()
		{
			GraphModifier.usedIDs.Remove(this.uniqueID);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnPostScan()
		{
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnPreScan()
		{
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnLatePostScan()
		{
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnPostCacheLoad()
		{
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnGraphsPreUpdate()
		{
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnGraphsPostUpdate()
		{
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x001905E8 File Offset: 0x0018E7E8
		private void Reset()
		{
			ulong num = (ulong)((long)UnityEngine.Random.Range(0, int.MaxValue));
			ulong num2 = (ulong)((ulong)((long)UnityEngine.Random.Range(0, int.MaxValue)) << 32);
			this.uniqueID = (num | num2);
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x04003EFE RID: 16126
		private static GraphModifier root;

		// Token: 0x04003EFF RID: 16127
		private GraphModifier prev;

		// Token: 0x04003F00 RID: 16128
		private GraphModifier next;

		// Token: 0x04003F01 RID: 16129
		[SerializeField]
		[HideInInspector]
		protected ulong uniqueID;

		// Token: 0x04003F02 RID: 16130
		protected static Dictionary<ulong, GraphModifier> usedIDs = new Dictionary<ulong, GraphModifier>();

		// Token: 0x02000718 RID: 1816
		public enum EventType
		{
			// Token: 0x040048EF RID: 18671
			PostScan = 1,
			// Token: 0x040048F0 RID: 18672
			PreScan,
			// Token: 0x040048F1 RID: 18673
			LatePostScan = 4,
			// Token: 0x040048F2 RID: 18674
			PreUpdate = 8,
			// Token: 0x040048F3 RID: 18675
			PostUpdate = 16,
			// Token: 0x040048F4 RID: 18676
			PostCacheLoad = 32
		}
	}
}
