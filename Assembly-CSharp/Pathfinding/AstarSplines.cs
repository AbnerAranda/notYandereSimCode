﻿using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000531 RID: 1329
	internal static class AstarSplines
	{
		// Token: 0x060021FD RID: 8701 RVA: 0x001910CC File Offset: 0x0018F2CC
		public static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime)
		{
			float num = elapsedTime * elapsedTime;
			float num2 = num * elapsedTime;
			return previous * (-0.5f * num2 + num - 0.5f * elapsedTime) + start * (1.5f * num2 + -2.5f * num + 1f) + end * (-1.5f * num2 + 2f * num + 0.5f * elapsedTime) + next * (0.5f * num2 - 0.5f * num);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00191157 File Offset: 0x0018F357
		[Obsolete("Use CatmullRom")]
		public static Vector3 CatmullRomOLD(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime)
		{
			return AstarSplines.CatmullRom(previous, start, end, next, elapsedTime);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00191164 File Offset: 0x0018F364
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x001911D0 File Offset: 0x0018F3D0
		public static Vector3 CubicBezierDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return 3f * num * num * (p1 - p0) + 6f * num * t * (p2 - p1) + 3f * t * t * (p3 - p2);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0019123C File Offset: 0x0018F43C
		public static Vector3 CubicBezierSecondDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return 6f * num * (p2 - 2f * p1 + p0) + 6f * t * (p3 - 2f * p2 + p1);
		}
	}
}
