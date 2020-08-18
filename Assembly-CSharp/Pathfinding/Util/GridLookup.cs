using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005DB RID: 1499
	public class GridLookup<T> where T : class
	{
		// Token: 0x06002947 RID: 10567 RVA: 0x001BC6DC File Offset: 0x001BA8DC
		public GridLookup(Int2 size)
		{
			this.size = size;
			this.cells = new GridLookup<T>.Item[size.x * size.y];
			for (int i = 0; i < this.cells.Length; i++)
			{
				this.cells[i] = new GridLookup<T>.Item();
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x001BC74F File Offset: 0x001BA94F
		public GridLookup<T>.Root AllItems
		{
			get
			{
				return this.all.next;
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x001BC75C File Offset: 0x001BA95C
		public GridLookup<T>.Root GetRoot(T item)
		{
			GridLookup<T>.Root result;
			this.rootLookup.TryGetValue(item, out result);
			return result;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x001BC77C File Offset: 0x001BA97C
		public GridLookup<T>.Root Add(T item, IntRect bounds)
		{
			GridLookup<T>.Root root = new GridLookup<T>.Root
			{
				obj = item,
				prev = this.all,
				next = this.all.next
			};
			this.all.next = root;
			if (root.next != null)
			{
				root.next.prev = root;
			}
			this.rootLookup.Add(item, root);
			this.Move(item, bounds);
			return root;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x001BC7EC File Offset: 0x001BA9EC
		public void Remove(T item)
		{
			GridLookup<T>.Root root;
			if (!this.rootLookup.TryGetValue(item, out root))
			{
				return;
			}
			this.Move(item, new IntRect(0, 0, -1, -1));
			this.rootLookup.Remove(item);
			root.prev.next = root.next;
			if (root.next != null)
			{
				root.next.prev = root.prev;
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x001BC854 File Offset: 0x001BAA54
		public void Move(T item, IntRect bounds)
		{
			GridLookup<T>.Root root;
			if (!this.rootLookup.TryGetValue(item, out root))
			{
				throw new ArgumentException("The item has not been added to this object");
			}
			if (root.previousBounds == bounds)
			{
				return;
			}
			for (int i = 0; i < root.items.Count; i++)
			{
				GridLookup<T>.Item item2 = root.items[i];
				item2.prev.next = item2.next;
				if (item2.next != null)
				{
					item2.next.prev = item2.prev;
				}
			}
			root.previousBounds = bounds;
			int num = 0;
			for (int j = bounds.ymin; j <= bounds.ymax; j++)
			{
				for (int k = bounds.xmin; k <= bounds.xmax; k++)
				{
					GridLookup<T>.Item item3;
					if (num < root.items.Count)
					{
						item3 = root.items[num];
					}
					else
					{
						item3 = ((this.itemPool.Count > 0) ? this.itemPool.Pop() : new GridLookup<T>.Item());
						item3.root = root;
						root.items.Add(item3);
					}
					num++;
					item3.prev = this.cells[k + j * this.size.x];
					item3.next = item3.prev.next;
					item3.prev.next = item3;
					if (item3.next != null)
					{
						item3.next.prev = item3;
					}
				}
			}
			for (int l = root.items.Count - 1; l >= num; l--)
			{
				GridLookup<T>.Item item4 = root.items[l];
				item4.root = null;
				item4.next = null;
				item4.prev = null;
				root.items.RemoveAt(l);
				this.itemPool.Push(item4);
			}
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x001BCA30 File Offset: 0x001BAC30
		public List<U> QueryRect<U>(IntRect r) where U : class, T
		{
			List<U> list = ListPool<U>.Claim();
			for (int i = r.ymin; i <= r.ymax; i++)
			{
				int num = i * this.size.x;
				for (int j = r.xmin; j <= r.xmax; j++)
				{
					GridLookup<T>.Item item = this.cells[j + num];
					while (item.next != null)
					{
						item = item.next;
						U u = item.root.obj as U;
						if (!item.root.flag && u != null)
						{
							item.root.flag = true;
							list.Add(u);
						}
					}
				}
			}
			for (int k = r.ymin; k <= r.ymax; k++)
			{
				int num2 = k * this.size.x;
				for (int l = r.xmin; l <= r.xmax; l++)
				{
					GridLookup<T>.Item item2 = this.cells[l + num2];
					while (item2.next != null)
					{
						item2 = item2.next;
						item2.root.flag = false;
					}
				}
			}
			return list;
		}

		// Token: 0x04004318 RID: 17176
		private Int2 size;

		// Token: 0x04004319 RID: 17177
		private GridLookup<T>.Item[] cells;

		// Token: 0x0400431A RID: 17178
		private GridLookup<T>.Root all = new GridLookup<T>.Root();

		// Token: 0x0400431B RID: 17179
		private Dictionary<T, GridLookup<T>.Root> rootLookup = new Dictionary<T, GridLookup<T>.Root>();

		// Token: 0x0400431C RID: 17180
		private Stack<GridLookup<T>.Item> itemPool = new Stack<GridLookup<T>.Item>();

		// Token: 0x02000772 RID: 1906
		internal class Item
		{
			// Token: 0x04004A70 RID: 19056
			public GridLookup<T>.Root root;

			// Token: 0x04004A71 RID: 19057
			public GridLookup<T>.Item prev;

			// Token: 0x04004A72 RID: 19058
			public GridLookup<T>.Item next;
		}

		// Token: 0x02000773 RID: 1907
		public class Root
		{
			// Token: 0x04004A73 RID: 19059
			public T obj;

			// Token: 0x04004A74 RID: 19060
			public GridLookup<T>.Root next;

			// Token: 0x04004A75 RID: 19061
			internal GridLookup<T>.Root prev;

			// Token: 0x04004A76 RID: 19062
			internal IntRect previousBounds = new IntRect(0, 0, -1, -1);

			// Token: 0x04004A77 RID: 19063
			internal List<GridLookup<T>.Item> items = new List<GridLookup<T>.Item>();

			// Token: 0x04004A78 RID: 19064
			internal bool flag;
		}
	}
}
