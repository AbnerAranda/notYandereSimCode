using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000609 RID: 1545
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_door_controller.php")]
	public class DoorController : MonoBehaviour
	{
		// Token: 0x06002A48 RID: 10824 RVA: 0x001C80E9 File Offset: 0x001C62E9
		public void Start()
		{
			this.bounds = base.GetComponent<Collider>().bounds;
			this.SetState(this.open);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x001C8108 File Offset: 0x001C6308
		private void OnGUI()
		{
			if (GUI.Button(new Rect(5f, this.yOffset, 100f, 22f), "Toggle Door"))
			{
				this.SetState(!this.open);
			}
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x001C8140 File Offset: 0x001C6340
		public void SetState(bool open)
		{
			this.open = open;
			if (this.updateGraphsWithGUO)
			{
				GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.bounds);
				int num = open ? this.opentag : this.closedtag;
				if (num > 31)
				{
					Debug.LogError("tag > 31");
					return;
				}
				graphUpdateObject.modifyTag = true;
				graphUpdateObject.setTag = num;
				graphUpdateObject.updatePhysics = false;
				AstarPath.active.UpdateGraphs(graphUpdateObject);
			}
			if (open)
			{
				base.GetComponent<Animation>().Play("Open");
				return;
			}
			base.GetComponent<Animation>().Play("Close");
		}

		// Token: 0x04004499 RID: 17561
		private bool open;

		// Token: 0x0400449A RID: 17562
		public int opentag = 1;

		// Token: 0x0400449B RID: 17563
		public int closedtag = 1;

		// Token: 0x0400449C RID: 17564
		public bool updateGraphsWithGUO = true;

		// Token: 0x0400449D RID: 17565
		public float yOffset = 5f;

		// Token: 0x0400449E RID: 17566
		private Bounds bounds;
	}
}
