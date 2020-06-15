using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000573 RID: 1395
	[AddComponentMenu("Pathfinding/Navmesh/RecastTileUpdate")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_tile_update.php")]
	public class RecastTileUpdate : MonoBehaviour
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06002498 RID: 9368 RVA: 0x0019CA34 File Offset: 0x0019AC34
		// (remove) Token: 0x06002499 RID: 9369 RVA: 0x0019CA68 File Offset: 0x0019AC68
		public static event Action<Bounds> OnNeedUpdates;

		// Token: 0x0600249A RID: 9370 RVA: 0x0019CA9B File Offset: 0x0019AC9B
		private void Start()
		{
			this.ScheduleUpdate();
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0019CA9B File Offset: 0x0019AC9B
		private void OnDestroy()
		{
			this.ScheduleUpdate();
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0019CAA4 File Offset: 0x0019ACA4
		public void ScheduleUpdate()
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				if (RecastTileUpdate.OnNeedUpdates != null)
				{
					RecastTileUpdate.OnNeedUpdates(component.bounds);
					return;
				}
			}
			else if (RecastTileUpdate.OnNeedUpdates != null)
			{
				RecastTileUpdate.OnNeedUpdates(new Bounds(base.transform.position, Vector3.zero));
			}
		}
	}
}
