using System;

// Token: 0x020000D2 RID: 210
[Serializable]
public class ArrayLayout
{
	// Token: 0x04000A50 RID: 2640
	public ArrayLayout.rowData[] rows = new ArrayLayout.rowData[6];

	// Token: 0x020006AC RID: 1708
	[Serializable]
	public struct rowData
	{
		// Token: 0x040046F4 RID: 18164
		public bool[] row;
	}
}
