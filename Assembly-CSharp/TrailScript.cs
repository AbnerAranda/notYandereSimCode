using System;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class TrailScript : MonoBehaviour
{
	// Token: 0x06001C7E RID: 7294 RVA: 0x00156233 File Offset: 0x00154433
	private void Start()
	{
		Physics.IgnoreCollision(GameObject.Find("YandereChan").GetComponent<Collider>(), base.GetComponent<Collider>());
		UnityEngine.Object.Destroy(this);
	}
}
