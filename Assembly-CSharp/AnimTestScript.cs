using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class AnimTestScript : MonoBehaviour
{
	// Token: 0x060009F5 RID: 2549 RVA: 0x0004F6ED File Offset: 0x0004D8ED
	private void Start()
	{
		Time.timeScale = 1f;
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0004F6FC File Offset: 0x0004D8FC
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 1;
			}
		}
		if (this.ID == 1)
		{
			this.CharacterB.transform.eulerAngles = new Vector3(0f, -90f, 0f);
			this.CharacterA.Play("f02_weightHighSanityA_00");
			this.CharacterB.Play("f02_weightHighSanityB_00");
			return;
		}
		if (this.ID == 2)
		{
			this.CharacterA.Play("f02_weightMedSanityA_00");
			this.CharacterB.Play("f02_weightMedSanityB_00");
			return;
		}
		if (this.ID == 3)
		{
			this.CharacterA.Play("f02_weightLowSanityA_00");
			this.CharacterB.Play("f02_weightLowSanityB_00");
			return;
		}
		if (this.ID == 4)
		{
			this.CharacterB.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.CharacterA.Play("f02_weightStealthA_00");
			this.CharacterB.Play("f02_weightStealthB_00");
		}
	}

	// Token: 0x040009D9 RID: 2521
	public Animation CharacterA;

	// Token: 0x040009DA RID: 2522
	public Animation CharacterB;

	// Token: 0x040009DB RID: 2523
	public int ID;
}
