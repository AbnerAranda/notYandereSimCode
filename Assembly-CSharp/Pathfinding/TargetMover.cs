using System;
using System.Linq;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000575 RID: 1397
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
	public class TargetMover : MonoBehaviour
	{
		// Token: 0x060024A5 RID: 9381 RVA: 0x0019CDCC File Offset: 0x0019AFCC
		public void Start()
		{
			this.cam = Camera.main;
			this.ais = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray<IAstarAI>();
			base.useGUILayout = false;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0019CDF5 File Offset: 0x0019AFF5
		public void OnGUI()
		{
			if (this.onlyOnDoubleClick && this.cam != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0019CE2C File Offset: 0x0019B02C
		private void Update()
		{
			if (!this.onlyOnDoubleClick && this.cam != null)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0019CE4C File Offset: 0x0019B04C
		public void UpdateTargetPosition()
		{
			Vector3 vector = Vector3.zero;
			bool flag = false;
			RaycastHit raycastHit;
			if (this.use2D)
			{
				vector = this.cam.ScreenToWorldPoint(Input.mousePosition);
				vector.z = 0f;
				flag = true;
			}
			else if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity, this.mask))
			{
				vector = raycastHit.point;
				flag = true;
			}
			if (flag && vector != this.target.position)
			{
				this.target.position = vector;
				if (this.onlyOnDoubleClick)
				{
					for (int i = 0; i < this.ais.Length; i++)
					{
						if (this.ais[i] != null)
						{
							this.ais[i].SearchPath();
						}
					}
				}
			}
		}

		// Token: 0x0400410C RID: 16652
		public LayerMask mask;

		// Token: 0x0400410D RID: 16653
		public Transform target;

		// Token: 0x0400410E RID: 16654
		private IAstarAI[] ais;

		// Token: 0x0400410F RID: 16655
		public bool onlyOnDoubleClick;

		// Token: 0x04004110 RID: 16656
		public bool use2D;

		// Token: 0x04004111 RID: 16657
		private Camera cam;
	}
}
