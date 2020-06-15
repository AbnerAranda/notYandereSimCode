using System;

namespace Pathfinding
{
	// Token: 0x02000539 RID: 1337
	public class BinaryHeap
	{
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x00195895 File Offset: 0x00193A95
		public bool isEmpty
		{
			get
			{
				return this.numberOfItems <= 0;
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x001958A3 File Offset: 0x00193AA3
		private static int RoundUpToNextMultipleMod1(int v)
		{
			return v + (4 - (v - 1) % 4) % 4;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x001958B0 File Offset: 0x00193AB0
		public BinaryHeap(int capacity)
		{
			capacity = BinaryHeap.RoundUpToNextMultipleMod1(capacity);
			this.heap = new BinaryHeap.Tuple[capacity];
			this.numberOfItems = 0;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x001958E0 File Offset: 0x00193AE0
		public void Clear()
		{
			for (int i = 0; i < this.numberOfItems; i++)
			{
				this.heap[i].node.heapIndex = ushort.MaxValue;
			}
			this.numberOfItems = 0;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x00195920 File Offset: 0x00193B20
		internal PathNode GetNode(int i)
		{
			return this.heap[i].node;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x00195933 File Offset: 0x00193B33
		internal void SetF(int i, uint f)
		{
			this.heap[i].F = f;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x00195948 File Offset: 0x00193B48
		private void Expand()
		{
			int num = BinaryHeap.RoundUpToNextMultipleMod1(Math.Max(this.heap.Length + 4, Math.Min(65533, (int)Math.Round((double)((float)this.heap.Length * this.growthFactor)))));
			if (num > 65534)
			{
				throw new Exception("Binary Heap Size really large (>65534). A heap size this large is probably the cause of pathfinding running in an infinite loop. ");
			}
			BinaryHeap.Tuple[] array = new BinaryHeap.Tuple[num];
			this.heap.CopyTo(array, 0);
			this.heap = array;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x001959B8 File Offset: 0x00193BB8
		public void Add(PathNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.heapIndex != 65535)
			{
				this.DecreaseKey(this.heap[(int)node.heapIndex], node.heapIndex);
				return;
			}
			if (this.numberOfItems == this.heap.Length)
			{
				this.Expand();
			}
			this.DecreaseKey(new BinaryHeap.Tuple(0U, node), (ushort)this.numberOfItems);
			this.numberOfItems++;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00195A38 File Offset: 0x00193C38
		private void DecreaseKey(BinaryHeap.Tuple node, ushort index)
		{
			int num = (int)index;
			uint num2 = node.F = node.node.F;
			uint g = node.node.G;
			while (num != 0)
			{
				int num3 = (num - 1) / 4;
				if (num2 >= this.heap[num3].F && (num2 != this.heap[num3].F || g <= this.heap[num3].node.G))
				{
					break;
				}
				this.heap[num] = this.heap[num3];
				this.heap[num].node.heapIndex = (ushort)num;
				num = num3;
			}
			this.heap[num] = node;
			node.node.heapIndex = (ushort)num;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x00195B0C File Offset: 0x00193D0C
		public PathNode Remove()
		{
			PathNode node = this.heap[0].node;
			node.heapIndex = ushort.MaxValue;
			this.numberOfItems--;
			if (this.numberOfItems == 0)
			{
				return node;
			}
			BinaryHeap.Tuple tuple = this.heap[this.numberOfItems];
			uint g = tuple.node.G;
			int num = 0;
			for (;;)
			{
				int num2 = num;
				uint num3 = tuple.F;
				int num4 = num2 * 4 + 1;
				if (num4 <= this.numberOfItems)
				{
					uint f = this.heap[num4].F;
					uint f2 = this.heap[num4 + 1].F;
					uint f3 = this.heap[num4 + 2].F;
					uint f4 = this.heap[num4 + 3].F;
					if (num4 < this.numberOfItems && (f < num3 || (f == num3 && this.heap[num4].node.G < g)))
					{
						num3 = f;
						num = num4;
					}
					if (num4 + 1 < this.numberOfItems && (f2 < num3 || (f2 == num3 && this.heap[num4 + 1].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num3 = f2;
						num = num4 + 1;
					}
					if (num4 + 2 < this.numberOfItems && (f3 < num3 || (f3 == num3 && this.heap[num4 + 2].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num3 = f3;
						num = num4 + 2;
					}
					if (num4 + 3 < this.numberOfItems && (f4 < num3 || (f4 == num3 && this.heap[num4 + 3].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num = num4 + 3;
					}
				}
				if (num2 == num)
				{
					break;
				}
				this.heap[num2] = this.heap[num];
				this.heap[num2].node.heapIndex = (ushort)num2;
			}
			this.heap[num] = tuple;
			tuple.node.heapIndex = (ushort)num;
			return node;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x00195D80 File Offset: 0x00193F80
		private void Validate()
		{
			for (int i = 1; i < this.numberOfItems; i++)
			{
				int num = (i - 1) / 4;
				if (this.heap[num].F > this.heap[i].F)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Invalid state at ",
						i,
						":",
						num,
						" ( ",
						this.heap[num].F,
						" > ",
						this.heap[i].F,
						" ) "
					}));
				}
				if ((int)this.heap[i].node.heapIndex != i)
				{
					throw new Exception("Invalid heap index");
				}
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00195E70 File Offset: 0x00194070
		public void Rebuild()
		{
			for (int i = 2; i < this.numberOfItems; i++)
			{
				int num = i;
				BinaryHeap.Tuple tuple = this.heap[i];
				uint f = tuple.F;
				while (num != 1)
				{
					int num2 = num / 4;
					if (f >= this.heap[num2].F)
					{
						break;
					}
					this.heap[num] = this.heap[num2];
					this.heap[num].node.heapIndex = (ushort)num;
					this.heap[num2] = tuple;
					this.heap[num2].node.heapIndex = (ushort)num2;
					num = num2;
				}
			}
		}

		// Token: 0x04003FD5 RID: 16341
		public int numberOfItems;

		// Token: 0x04003FD6 RID: 16342
		public float growthFactor = 2f;

		// Token: 0x04003FD7 RID: 16343
		private const int D = 4;

		// Token: 0x04003FD8 RID: 16344
		private const bool SortGScores = true;

		// Token: 0x04003FD9 RID: 16345
		public const ushort NotInHeap = 65535;

		// Token: 0x04003FDA RID: 16346
		private BinaryHeap.Tuple[] heap;

		// Token: 0x02000731 RID: 1841
		private struct Tuple
		{
			// Token: 0x06002CF4 RID: 11508 RVA: 0x001CFCF3 File Offset: 0x001CDEF3
			public Tuple(uint f, PathNode node)
			{
				this.F = f;
				this.node = node;
			}

			// Token: 0x040049D7 RID: 18903
			public PathNode node;

			// Token: 0x040049D8 RID: 18904
			public uint F;
		}
	}
}
