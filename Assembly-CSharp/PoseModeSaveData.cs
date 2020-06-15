using System;
using UnityEngine;

// Token: 0x020003BE RID: 958
[Serializable]
public class PoseModeSaveData
{
	// Token: 0x06001A2B RID: 6699 RVA: 0x0010082C File Offset: 0x000FEA2C
	public static PoseModeSaveData ReadFromGlobals()
	{
		return new PoseModeSaveData
		{
			posePosition = PoseModeGlobals.PosePosition,
			poseRotation = PoseModeGlobals.PoseRotation,
			poseScale = PoseModeGlobals.PoseScale
		};
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x00100854 File Offset: 0x000FEA54
	public static void WriteToGlobals(PoseModeSaveData data)
	{
		PoseModeGlobals.PosePosition = data.posePosition;
		PoseModeGlobals.PoseRotation = data.poseRotation;
		PoseModeGlobals.PoseScale = data.poseScale;
	}

	// Token: 0x04002938 RID: 10552
	public Vector3 posePosition;

	// Token: 0x04002939 RID: 10553
	public Vector3 poseRotation;

	// Token: 0x0400293A RID: 10554
	public Vector3 poseScale;
}
