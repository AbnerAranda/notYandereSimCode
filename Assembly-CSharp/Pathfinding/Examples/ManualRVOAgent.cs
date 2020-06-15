using System;
using Pathfinding.RVO;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200060A RID: 1546
	[RequireComponent(typeof(RVOController))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_manual_r_v_o_agent.php")]
	public class ManualRVOAgent : MonoBehaviour
	{
		// Token: 0x06002A4C RID: 10828 RVA: 0x001C81F8 File Offset: 0x001C63F8
		private void Awake()
		{
			this.rvo = base.GetComponent<RVOController>();
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x001C8208 File Offset: 0x001C6408
		private void Update()
		{
			float axis = Input.GetAxis("Horizontal");
			float axis2 = Input.GetAxis("Vertical");
			Vector3 vector = new Vector3(axis, 0f, axis2) * this.speed;
			this.rvo.velocity = vector;
			base.transform.position += vector * Time.deltaTime;
		}

		// Token: 0x0400449F RID: 17567
		private RVOController rvo;

		// Token: 0x040044A0 RID: 17568
		public float speed = 1f;
	}
}
