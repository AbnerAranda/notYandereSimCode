using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005F1 RID: 1521
	public class GridLookup<T> where T : class
	{
		// Token: 0x0600299D RID: 10653 RVA: 0x001C22C8 File Offset: 0x001C04C8
		public GridLookup(Int2 size)
		{
			this.size = size;
			this.cells = new GridLookup<T>.Item[size.x * size.y];
			for (int i = 0; i < this.cells.Length; i++)
			{
				this.cells[i] = new GridLookup<T>.Item();
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x001C233B File Offset: 0x001C053B
		public GridLookup<T>.Root AllItems
		{
			get
			{
				return this.all.next;
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x001C2348 File Offset: 0x001C0548
		public GridLookup<T>.Root GetRoot(T item)
		{
			GridLookup<T>.Root result;
			this.rootLookup.TryGetValue(item, out result);
			return result;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x001C2368 File Offset: 0x001C0568
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

		// Token: 0x060029A1 RID: 10657 RVA: 0x001C23D8 File Offset: 0x001C05D8
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

		// Token: 0x060029A2 RID: 10658 RVA: 0x001C2440 File Offset: 0x001C0640
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

		// Token: 0x060029A3 RID: 10659 RVA: 0x001C261C File Offset: 0x001C081C
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

		// Token: 0x040043F6 RID: 17398
		private Int2 size;

		// Token: 0x040043F7 RID: 17399
		private GridLookup<T>.Item[] cells;

		// Token: 0x040043F8 RID: 17400
		private GridLookup<T>.Root all = new GridLookup<T>.Root();

		// Token: 0x040043F9 RID: 17401
		private Dictionary<T, GridLookup<T>.Root> rootLookup = new Dictionary<T, GridLookup<T>.Root>();

		// Token: 0x040043FA RID: 17402
		private Stack<GridLookup<T>.Item> itemPool = new Stack<GridLookup<T>.Item>();

		// Token: 0x0200078C RID: 1932
		internal class Item
		{
			// Token: 0x04004B5B RID: 19291
			public GridLookup<T>.Root root;

			// Token: 0x04004B5C RID: 19292
			public GridLookup<T>.Item prev;

			// Token: 0x04004B5D RID: 19293
			public GridLookup<T>.Item next;
		}

		// Token: 0x0200078D RID: 1933
		public class Root
		{
			// Token: 0x04004B5E RID: 19294
			public T obj;

			// Token: 0x04004B5F RID: 19295
			public GridLookup<T>.Root next;

			// Token: 0x04004B60 RID: 19296
			internal GridLookup<T>.Root prev;

			// Token: 0x04004B61 RID: 19297
			internal IntRect previousBounds = new IntRect(0, 0, -1, -1);

			// Token: 0x04004B62 RID: 19298
			internal List<GridLookup<T>.Item> items = new List<GridLookup<T>.Item>();

			// Token: 0x04004B63 RID: 19299
			internal bool flag;
		}
	}
}
