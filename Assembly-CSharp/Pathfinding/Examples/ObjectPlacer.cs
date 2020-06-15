using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200060D RID: 1549
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_object_placer.php")]
	public class ObjectPlacer : MonoBehaviour
	{
		// Token: 0x06002A55 RID: 10837 RVA: 0x001C84A4 File Offset: 0x001C66A4
		private void Update()
		{
			if (Input.GetKeyDown("p"))
			{
				this.PlaceObject();
			}
			if (Input.GetKeyDown("r"))
			{
				this.RemoveObject();
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x001C84CC File Offset: 0x001C66CC
		public void PlaceObject()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity))
			{
				Vector3 point = raycastHit.point;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.go, point, Quaternion.identity);
				if (this.issueGUOs)
				{
					GraphUpdateObject ob = new GraphUpdateObject(gameObject.GetComponent<Collider>().bounds);
					AstarPath.active.UpdateGraphs(ob);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
			}
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x001C8548 File Offset: 0x001C6748
		public void RemoveObject()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity))
			{
				if (raycastHit.collider.isTrigger || raycastHit.transform.gameObject.name == "Ground")
				{
					return;
				}
				Bounds bounds = raycastHit.collider.bounds;
				UnityEngine.Object.Destroy(raycastHit.collider);
				UnityEngine.Object.Destroy(raycastHit.collider.gameObject);
				if (this.issueGUOs)
				{
					GraphUpdateObject ob = new GraphUpdateObject(bounds);
					AstarPath.active.UpdateGraphs(ob);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
			}
		}

		// Token: 0x040044AD RID: 17581
		public GameObject go;

		// Token: 0x040044AE RID: 17582
		public bool direct;

		// Token: 0x040044AF RID: 17583
		public bool issueGUOs = true;
	}
}
