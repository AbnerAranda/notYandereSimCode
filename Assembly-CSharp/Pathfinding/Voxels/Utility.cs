using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005CC RID: 1484
	public class Utility
	{
		// Token: 0x06002817 RID: 10263 RVA: 0x001B9E13 File Offset: 0x001B8013
		public static float Min(float a, float b, float c)
		{
			a = ((a < b) ? a : b);
			if (a >= c)
			{
				return c;
			}
			return a;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x001B9E26 File Offset: 0x001B8026
		public static float Max(float a, float b, float c)
		{
			a = ((a > b) ? a : b);
			if (a <= c)
			{
				return c;
			}
			return a;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x001B9E39 File Offset: 0x001B8039
		public static int Max(int a, int b, int c, int d)
		{
			a = ((a > b) ? a : b);
			a = ((a > c) ? a : c);
			if (a <= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x001B9E56 File Offset: 0x001B8056
		public static int Min(int a, int b, int c, int d)
		{
			a = ((a < b) ? a : b);
			a = ((a < c) ? a : c);
			if (a >= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x001B9E39 File Offset: 0x001B8039
		public static float Max(float a, float b, float c, float d)
		{
			a = ((a > b) ? a : b);
			a = ((a > c) ? a : c);
			if (a <= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x001B9E56 File Offset: 0x001B8056
		public static float Min(float a, float b, float c, float d)
		{
			a = ((a < b) ? a : b);
			a = ((a < c) ? a : c);
			if (a >= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x001B9E73 File Offset: 0x001B8073
		public static void CopyVector(float[] a, int i, Vector3 v)
		{
			a[i] = v.x;
			a[i + 1] = v.y;
			a[i + 2] = v.z;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x001B9E94 File Offset: 0x001B8094
		public static Int3[] RemoveDuplicateVertices(Int3[] vertices, int[] triangles)
		{
			Dictionary<Int3, int> dictionary = ObjectPoolSimple<Dictionary<Int3, int>>.Claim();
			dictionary.Clear();
			int[] array = new int[vertices.Length];
			int num = 0;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (!dictionary.ContainsKey(vertices[i]))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = dictionary[vertices[i]];
				}
			}
			dictionary.Clear();
			ObjectPoolSimple<Dictionary<Int3, int>>.Release(ref dictionary);
			for (int j = 0; j < triangles.Length; j++)
			{
				triangles[j] = array[triangles[j]];
			}
			Int3[] array2 = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				array2[k] = vertices[k];
			}
			return array2;
		}
	}
}
