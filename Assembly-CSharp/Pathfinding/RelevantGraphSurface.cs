using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005A1 RID: 1441
	[AddComponentMenu("Pathfinding/Navmesh/RelevantGraphSurface")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_relevant_graph_surface.php")]
	public class RelevantGraphSurface : VersionedMonoBehaviour
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x001AE6B0 File Offset: 0x001AC8B0
		public Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x001AE6B8 File Offset: 0x001AC8B8
		public RelevantGraphSurface Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x001AE6C0 File Offset: 0x001AC8C0
		public RelevantGraphSurface Prev
		{
			get
			{
				return this.prev;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x001AE6C8 File Offset: 0x001AC8C8
		public static RelevantGraphSurface Root
		{
			get
			{
				return RelevantGraphSurface.root;
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x001AE6CF File Offset: 0x001AC8CF
		public void UpdatePosition()
		{
			this.position = base.transform.position;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x001AE6E2 File Offset: 0x001AC8E2
		private void OnEnable()
		{
			this.UpdatePosition();
			if (RelevantGraphSurface.root == null)
			{
				RelevantGraphSurface.root = this;
				return;
			}
			this.next = RelevantGraphSurface.root;
			RelevantGraphSurface.root.prev = this;
			RelevantGraphSurface.root = this;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x001AE71C File Offset: 0x001AC91C
		private void OnDisable()
		{
			if (RelevantGraphSurface.root == this)
			{
				RelevantGraphSurface.root = this.next;
				if (RelevantGraphSurface.root != null)
				{
					RelevantGraphSurface.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x001AE7A8 File Offset: 0x001AC9A8
		public static void UpdateAllPositions()
		{
			RelevantGraphSurface relevantGraphSurface = RelevantGraphSurface.root;
			while (relevantGraphSurface != null)
			{
				relevantGraphSurface.UpdatePosition();
				relevantGraphSurface = relevantGraphSurface.Next;
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x001AE7D4 File Offset: 0x001AC9D4
		public static void FindAllGraphSurfaces()
		{
			RelevantGraphSurface[] array = UnityEngine.Object.FindObjectsOfType(typeof(RelevantGraphSurface)) as RelevantGraphSurface[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnDisable();
				array[i].OnEnable();
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x001AE814 File Offset: 0x001ACA14
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(0.223529413f, 0.827451f, 0.180392161f, 0.4f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x001AE884 File Offset: 0x001ACA84
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.223529413f, 0.827451f, 0.180392161f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x0400425B RID: 16987
		private static RelevantGraphSurface root;

		// Token: 0x0400425C RID: 16988
		public float maxRange = 1f;

		// Token: 0x0400425D RID: 16989
		private RelevantGraphSurface prev;

		// Token: 0x0400425E RID: 16990
		private RelevantGraphSurface next;

		// Token: 0x0400425F RID: 16991
		private Vector3 position;
	}
}
