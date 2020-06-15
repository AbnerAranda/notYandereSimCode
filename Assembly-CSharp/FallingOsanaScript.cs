using System;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class FallingOsanaScript : MonoBehaviour
{
	// Token: 0x06001407 RID: 5127 RVA: 0x000AFAFC File Offset: 0x000ADCFC
	private void Update()
	{
		if (base.transform.parent.position.y > 0f)
		{
			this.Osana.CharacterAnimation.Play(this.Osana.IdleAnim);
			base.transform.parent.position += new Vector3(0f, -1.0001f, 0f);
		}
		if (base.transform.parent.position.y < 0f)
		{
			base.transform.parent.position = new Vector3(base.transform.parent.position.x, 0f, base.transform.parent.position.z);
			UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.parent.position, Quaternion.identity);
		}
	}

	// Token: 0x04001C2B RID: 7211
	public StudentScript Osana;

	// Token: 0x04001C2C RID: 7212
	public GameObject GroundImpact;
}
