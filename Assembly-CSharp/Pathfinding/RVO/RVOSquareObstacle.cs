using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005E1 RID: 1505
	[AddComponentMenu("Pathfinding/Local Avoidance/Square Obstacle")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_square_obstacle.php")]
	public class RVOSquareObstacle : RVOObstacle
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x0002D199 File Offset: 0x0002B399
		protected override bool StaticObstacle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x00022944 File Offset: 0x00020B44
		protected override bool ExecuteInEditor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x00022944 File Offset: 0x00020B44
		protected override bool LocalCoordinates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x001BF184 File Offset: 0x001BD384
		protected override float Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0002D199 File Offset: 0x0002B399
		protected override bool AreGizmosDirty()
		{
			return false;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x001BF18C File Offset: 0x001BD38C
		protected override void CreateObstacles()
		{
			this.size.x = Mathf.Abs(this.size.x);
			this.size.y = Mathf.Abs(this.size.y);
			this.height = Mathf.Abs(this.height);
			Vector3[] array = new Vector3[]
			{
				new Vector3(1f, 0f, -1f),
				new Vector3(1f, 0f, 1f),
				new Vector3(-1f, 0f, 1f),
				new Vector3(-1f, 0f, -1f)
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Scale(new Vector3(this.size.x * 0.5f, 0f, this.size.y * 0.5f));
				array[i] += new Vector3(this.center.x, 0f, this.center.y);
			}
			base.AddObstacle(array, this.height);
		}

		// Token: 0x040043A2 RID: 17314
		public float height = 1f;

		// Token: 0x040043A3 RID: 17315
		public Vector2 size = Vector3.one;

		// Token: 0x040043A4 RID: 17316
		public Vector2 center = Vector3.zero;
	}
}
