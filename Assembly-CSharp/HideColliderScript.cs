using System;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class HideColliderScript : MonoBehaviour
{
	// Token: 0x06001716 RID: 5910 RVA: 0x000C3BC0 File Offset: 0x000C1DC0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 11)
		{
			GameObject gameObject = other.gameObject.transform.root.gameObject;
			if (!gameObject.GetComponent<StudentScript>().Alive)
			{
				this.Corpse = gameObject.GetComponent<RagdollScript>();
				if (!this.Corpse.Hidden)
				{
					this.Corpse.HideCollider = this.MyCollider;
					this.Corpse.Police.HiddenCorpses++;
					this.Corpse.Hidden = true;
				}
			}
		}
	}

	// Token: 0x04001F39 RID: 7993
	public RagdollScript Corpse;

	// Token: 0x04001F3A RID: 7994
	public Collider MyCollider;
}
