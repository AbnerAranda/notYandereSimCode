using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005BF RID: 1471
	public class VoxelArea
	{
		// Token: 0x060027C4 RID: 10180 RVA: 0x001B4990 File Offset: 0x001B2B90
		public void Reset()
		{
			this.ResetLinkedVoxelSpans();
			for (int i = 0; i < this.compactCells.Length; i++)
			{
				this.compactCells[i].count = 0U;
				this.compactCells[i].index = 0U;
			}
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x001B49DC File Offset: 0x001B2BDC
		private void ResetLinkedVoxelSpans()
		{
			int num = this.linkedSpans.Length;
			this.linkedSpanCount = this.width * this.depth;
			LinkedVoxelSpan linkedVoxelSpan = new LinkedVoxelSpan(uint.MaxValue, uint.MaxValue, -1, -1);
			for (int i = 0; i < num; i++)
			{
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
			}
			this.removedStackCount = 0;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x001B4B38 File Offset: 0x001B2D38
		public VoxelArea(int width, int depth)
		{
			this.width = width;
			this.depth = depth;
			int num = width * depth;
			this.compactCells = new CompactVoxelCell[num];
			this.linkedSpans = new LinkedVoxelSpan[(int)((float)num * 8f) + 15 & -16];
			this.ResetLinkedVoxelSpans();
			int[] array = new int[4];
			array[0] = -1;
			array[2] = 1;
			this.DirectionX = array;
			this.DirectionZ = new int[]
			{
				0,
				width,
				0,
				-width
			};
			this.VectorDirection = new Vector3[]
			{
				Vector3.left,
				Vector3.forward,
				Vector3.right,
				Vector3.back
			};
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x001B4C00 File Offset: 0x001B2E00
		public int GetSpanCountAll()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295U)
				{
					num++;
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x001B4C5C File Offset: 0x001B2E5C
		public int GetSpanCount()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295U)
				{
					if (this.linkedSpans[num3].area != 0)
					{
						num++;
					}
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x001B4CCC File Offset: 0x001B2ECC
		private void PushToSpanRemovedStack(int index)
		{
			if (this.removedStackCount == this.removedStack.Length)
			{
				int[] dst = new int[this.removedStackCount * 4];
				Buffer.BlockCopy(this.removedStack, 0, dst, 0, this.removedStackCount * 4);
				this.removedStack = dst;
			}
			this.removedStack[this.removedStackCount] = index;
			this.removedStackCount++;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x001B4D30 File Offset: 0x001B2F30
		public void AddLinkedSpan(int index, uint bottom, uint top, int area, int voxelWalkableClimb)
		{
			if (this.linkedSpans[index].bottom == 4294967295U)
			{
				this.linkedSpans[index] = new LinkedVoxelSpan(bottom, top, area);
				return;
			}
			int num = -1;
			int num2 = index;
			while (index != -1 && this.linkedSpans[index].bottom <= top)
			{
				if (this.linkedSpans[index].top < bottom)
				{
					num = index;
					index = this.linkedSpans[index].next;
				}
				else
				{
					bottom = Math.Min(this.linkedSpans[index].bottom, bottom);
					top = Math.Max(this.linkedSpans[index].top, top);
					if (Math.Abs((int)(top - this.linkedSpans[index].top)) <= voxelWalkableClimb)
					{
						area = Math.Max(area, this.linkedSpans[index].area);
					}
					int next = this.linkedSpans[index].next;
					if (num != -1)
					{
						this.linkedSpans[num].next = next;
						this.PushToSpanRemovedStack(index);
						index = next;
					}
					else
					{
						if (next == -1)
						{
							this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area);
							return;
						}
						this.linkedSpans[num2] = this.linkedSpans[next];
						this.PushToSpanRemovedStack(next);
					}
				}
			}
			if (this.linkedSpanCount >= this.linkedSpans.Length)
			{
				LinkedVoxelSpan[] array = this.linkedSpans;
				int num3 = this.linkedSpanCount;
				int num4 = this.removedStackCount;
				this.linkedSpans = new LinkedVoxelSpan[this.linkedSpans.Length * 2];
				this.ResetLinkedVoxelSpans();
				this.linkedSpanCount = num3;
				this.removedStackCount = num4;
				for (int i = 0; i < this.linkedSpanCount; i++)
				{
					this.linkedSpans[i] = array[i];
				}
				Debug.Log("Layer estimate too low, doubling size of buffer.\nThis message is harmless.");
			}
			int num5;
			if (this.removedStackCount > 0)
			{
				this.removedStackCount--;
				num5 = this.removedStack[this.removedStackCount];
			}
			else
			{
				num5 = this.linkedSpanCount;
				this.linkedSpanCount++;
			}
			if (num != -1)
			{
				this.linkedSpans[num5] = new LinkedVoxelSpan(bottom, top, area, this.linkedSpans[num].next);
				this.linkedSpans[num].next = num5;
				return;
			}
			this.linkedSpans[num5] = this.linkedSpans[num2];
			this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area, num5);
		}

		// Token: 0x040042BA RID: 17082
		public const uint MaxHeight = 65536U;

		// Token: 0x040042BB RID: 17083
		public const int MaxHeightInt = 65536;

		// Token: 0x040042BC RID: 17084
		public const uint InvalidSpanValue = 4294967295U;

		// Token: 0x040042BD RID: 17085
		public const float AvgSpanLayerCountEstimate = 8f;

		// Token: 0x040042BE RID: 17086
		public readonly int width;

		// Token: 0x040042BF RID: 17087
		public readonly int depth;

		// Token: 0x040042C0 RID: 17088
		public CompactVoxelSpan[] compactSpans;

		// Token: 0x040042C1 RID: 17089
		public CompactVoxelCell[] compactCells;

		// Token: 0x040042C2 RID: 17090
		public int compactSpanCount;

		// Token: 0x040042C3 RID: 17091
		public ushort[] tmpUShortArr;

		// Token: 0x040042C4 RID: 17092
		public int[] areaTypes;

		// Token: 0x040042C5 RID: 17093
		public ushort[] dist;

		// Token: 0x040042C6 RID: 17094
		public ushort maxDistance;

		// Token: 0x040042C7 RID: 17095
		public int maxRegions;

		// Token: 0x040042C8 RID: 17096
		public int[] DirectionX;

		// Token: 0x040042C9 RID: 17097
		public int[] DirectionZ;

		// Token: 0x040042CA RID: 17098
		public Vector3[] VectorDirection;

		// Token: 0x040042CB RID: 17099
		private int linkedSpanCount;

		// Token: 0x040042CC RID: 17100
		public LinkedVoxelSpan[] linkedSpans;

		// Token: 0x040042CD RID: 17101
		private int[] removedStack = new int[128];

		// Token: 0x040042CE RID: 17102
		private int removedStackCount;
	}
}
