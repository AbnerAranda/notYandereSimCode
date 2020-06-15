using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005EF RID: 1519
	public interface ITransform
	{
		// Token: 0x06002989 RID: 10633
		Vector3 Transform(Vector3 position);

		// Token: 0x0600298A RID: 10634
		Vector3 InverseTransform(Vector3 position);
	}
}
