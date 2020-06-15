using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055F RID: 1375
	public interface IRaycastableGraph
	{
		// Token: 0x06002458 RID: 9304
		bool Linecast(Vector3 start, Vector3 end);

		// Token: 0x06002459 RID: 9305
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint);

		// Token: 0x0600245A RID: 9306
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit);

		// Token: 0x0600245B RID: 9307
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace);
	}
}
