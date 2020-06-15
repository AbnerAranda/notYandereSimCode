using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000555 RID: 1365
	[Serializable]
	public class AstarColor
	{
		// Token: 0x06002437 RID: 9271 RVA: 0x0019B6E0 File Offset: 0x001998E0
		public static Color GetAreaColor(uint area)
		{
			if (AstarColor.AreaColors == null || (ulong)area >= (ulong)((long)AstarColor.AreaColors.Length))
			{
				return AstarMath.IntToColor((int)area, 1f);
			}
			return AstarColor.AreaColors[(int)area];
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0019B70C File Offset: 0x0019990C
		public void OnEnable()
		{
			AstarColor.NodeConnection = this._NodeConnection;
			AstarColor.UnwalkableNode = this._UnwalkableNode;
			AstarColor.BoundsHandles = this._BoundsHandles;
			AstarColor.ConnectionLowLerp = this._ConnectionLowLerp;
			AstarColor.ConnectionHighLerp = this._ConnectionHighLerp;
			AstarColor.MeshEdgeColor = this._MeshEdgeColor;
			AstarColor.AreaColors = this._AreaColors;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x0019B768 File Offset: 0x00199968
		public AstarColor()
		{
			this._NodeConnection = new Color(1f, 1f, 1f, 0.9f);
			this._UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);
			this._BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);
			this._ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);
			this._ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);
			this._MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);
		}

		// Token: 0x0400407E RID: 16510
		public Color _NodeConnection;

		// Token: 0x0400407F RID: 16511
		public Color _UnwalkableNode;

		// Token: 0x04004080 RID: 16512
		public Color _BoundsHandles;

		// Token: 0x04004081 RID: 16513
		public Color _ConnectionLowLerp;

		// Token: 0x04004082 RID: 16514
		public Color _ConnectionHighLerp;

		// Token: 0x04004083 RID: 16515
		public Color _MeshEdgeColor;

		// Token: 0x04004084 RID: 16516
		public Color[] _AreaColors;

		// Token: 0x04004085 RID: 16517
		public static Color NodeConnection = new Color(1f, 1f, 1f, 0.9f);

		// Token: 0x04004086 RID: 16518
		public static Color UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04004087 RID: 16519
		public static Color BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);

		// Token: 0x04004088 RID: 16520
		public static Color ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);

		// Token: 0x04004089 RID: 16521
		public static Color ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x0400408A RID: 16522
		public static Color MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x0400408B RID: 16523
		private static Color[] AreaColors;
	}
}
