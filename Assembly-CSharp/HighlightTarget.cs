using System;
using UnityEngine;

// Token: 0x02000495 RID: 1173
[Serializable]
public struct HighlightTarget
{
	// Token: 0x04003C5D RID: 15453
	public Color TargetColor;

	// Token: 0x04003C5E RID: 15454
	[ColorUsage(true, true, 0f, 3f, 0f, 3f)]
	public Color ReplacementColor;

	// Token: 0x04003C5F RID: 15455
	[Range(0f, 1f)]
	public float Threshold;

	// Token: 0x04003C60 RID: 15456
	[Range(0f, 1f)]
	public float SmoothColorInterpolation;
}
