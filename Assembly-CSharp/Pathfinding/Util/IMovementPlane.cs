using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005EE RID: 1518
	public interface IMovementPlane
	{
		// Token: 0x06002986 RID: 10630
		Vector2 ToPlane(Vector3 p);

		// Token: 0x06002987 RID: 10631
		Vector2 ToPlane(Vector3 p, out float elevation);

		// Token: 0x06002988 RID: 10632
		Vector3 ToWorld(Vector2 p, float elevation = 0f);
	}
}
