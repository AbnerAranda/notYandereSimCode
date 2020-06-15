using System;
using UnityEngine;

// Token: 0x0200047A RID: 1146
public class YanvaniaBlackHoleAttackScript : MonoBehaviour
{
	// Token: 0x06001DC9 RID: 7625 RVA: 0x0017446D File Offset: 0x0017266D
	private void Start()
	{
		this.Yanmont = GameObject.Find("YanmontChan").GetComponent<YanvaniaYanmontScript>();
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x00174484 File Offset: 0x00172684
	private void Update()
	{
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Yanmont.transform.position + Vector3.up, Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.Yanmont.transform.position) > 10f || this.Yanmont.EnterCutscene)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x0017450C File Offset: 0x0017270C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackExplosion, base.transform.position, Quaternion.identity);
			this.Yanmont.TakeDamage(20);
		}
		if (other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackExplosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B0A RID: 15114
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003B0B RID: 15115
	public GameObject BlackExplosion;
}
