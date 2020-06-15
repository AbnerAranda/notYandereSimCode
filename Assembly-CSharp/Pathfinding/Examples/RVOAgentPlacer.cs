using System;
using System.Collections;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005FC RID: 1532
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_r_v_o_agent_placer.php")]
	public class RVOAgentPlacer : MonoBehaviour
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x001C642F File Offset: 0x001C462F
		private IEnumerator Start()
		{
			yield return null;
			for (int i = 0; i < this.agents; i++)
			{
				float num = (float)i / (float)this.agents * 3.14159274f * 2f;
				Vector3 vector = new Vector3((float)Math.Cos((double)num), 0f, (float)Math.Sin((double)num)) * this.ringSize;
				Vector3 target = -vector + this.goalOffset;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, Vector3.zero, Quaternion.Euler(0f, num + 180f, 0f));
				RVOExampleAgent component = gameObject.GetComponent<RVOExampleAgent>();
				if (component == null)
				{
					Debug.LogError("Prefab does not have an RVOExampleAgent component attached");
					yield break;
				}
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = vector;
				component.repathRate = this.repathRate;
				component.SetTarget(target);
				component.SetColor(this.GetColor(num));
			}
			yield break;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x001C555D File Offset: 0x001C375D
		public Color GetColor(float angle)
		{
			return AstarMath.HSVToRGB(angle * 57.2957764f, 0.8f, 0.6f);
		}

		// Token: 0x0400444A RID: 17482
		public int agents = 100;

		// Token: 0x0400444B RID: 17483
		public float ringSize = 100f;

		// Token: 0x0400444C RID: 17484
		public LayerMask mask;

		// Token: 0x0400444D RID: 17485
		public GameObject prefab;

		// Token: 0x0400444E RID: 17486
		public Vector3 goalOffset;

		// Token: 0x0400444F RID: 17487
		public float repathRate = 1f;

		// Token: 0x04004450 RID: 17488
		private const float rad2Deg = 57.2957764f;
	}
}
