﻿using System;
using UnityEngine;

// Token: 0x0200049D RID: 1181
public class PathfindingTestScript : MonoBehaviour
{
	// Token: 0x06001E3F RID: 7743 RVA: 0x0017C448 File Offset: 0x0017A648
	private void Update()
	{
		if (Input.GetKeyDown("left"))
		{
			this.bytes = AstarPath.active.astarData.SerializeGraphs();
		}
		if (Input.GetKeyDown("right"))
		{
			AstarPath.active.astarData.DeserializeGraphs(this.bytes);
			AstarPath.active.Scan(null);
		}
	}

	// Token: 0x04003C73 RID: 15475
	private byte[] bytes;
}
