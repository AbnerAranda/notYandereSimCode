using System;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public class StudentPortraitScript : MonoBehaviour
{
	// Token: 0x06001B8C RID: 7052 RVA: 0x0011DAF4 File Offset: 0x0011BCF4
	private void Start()
	{
		this.DeathShadow.SetActive(false);
		this.PrisonBars.SetActive(false);
		this.Panties.SetActive(false);
		this.Friend.SetActive(false);
	}

	// Token: 0x04002E36 RID: 11830
	public GameObject DeathShadow;

	// Token: 0x04002E37 RID: 11831
	public GameObject PrisonBars;

	// Token: 0x04002E38 RID: 11832
	public GameObject Panties;

	// Token: 0x04002E39 RID: 11833
	public GameObject Friend;

	// Token: 0x04002E3A RID: 11834
	public UITexture Portrait;
}
