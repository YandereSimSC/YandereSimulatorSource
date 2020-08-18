using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000589 RID: 1417
	public abstract class NavmeshClipper : VersionedMonoBehaviour
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x001A7F5E File Offset: 0x001A615E
		public NavmeshClipper()
		{
			this.node = new LinkedListNode<NavmeshClipper>(this);
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x001A7F74 File Offset: 0x001A6174
		public static void AddEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnDisableCallback, onDisable);
			for (LinkedListNode<NavmeshClipper> linkedListNode = NavmeshClipper.all.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				onEnable(linkedListNode.Value);
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x001A7FD0 File Offset: 0x001A61D0
		public static void RemoveEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnDisableCallback, onDisable);
			for (LinkedListNode<NavmeshClipper> linkedListNode = NavmeshClipper.all.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				onDisable(linkedListNode.Value);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x001A802A File Offset: 0x001A622A
		public static bool AnyEnableListeners
		{
			get
			{
				return NavmeshClipper.OnEnableCallback != null;
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x001A8034 File Offset: 0x001A6234
		protected virtual void OnEnable()
		{
			NavmeshClipper.all.AddFirst(this.node);
			if (NavmeshClipper.OnEnableCallback != null)
			{
				NavmeshClipper.OnEnableCallback(this);
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x001A8058 File Offset: 0x001A6258
		protected virtual void OnDisable()
		{
			if (NavmeshClipper.OnDisableCallback != null)
			{
				NavmeshClipper.OnDisableCallback(this);
			}
			NavmeshClipper.all.Remove(this.node);
		}

		// Token: 0x06002685 RID: 9861
		internal abstract void NotifyUpdated();

		// Token: 0x06002686 RID: 9862
		internal abstract Rect GetBounds(GraphTransform transform);

		// Token: 0x06002687 RID: 9863
		public abstract bool RequiresUpdate();

		// Token: 0x06002688 RID: 9864
		public abstract void ForceUpdate();

		// Token: 0x04004164 RID: 16740
		private static Action<NavmeshClipper> OnEnableCallback;

		// Token: 0x04004165 RID: 16741
		private static Action<NavmeshClipper> OnDisableCallback;

		// Token: 0x04004166 RID: 16742
		private static readonly LinkedList<NavmeshClipper> all = new LinkedList<NavmeshClipper>();

		// Token: 0x04004167 RID: 16743
		private readonly LinkedListNode<NavmeshClipper> node;
	}
}
