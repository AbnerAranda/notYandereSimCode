using System;
using UnityEngine;

// Token: 0x02000437 RID: 1079
public class TributeScript : MonoBehaviour
{
	// Token: 0x06001C94 RID: 7316 RVA: 0x001571F0 File Offset: 0x001553F0
	private void Start()
	{
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode || GameGlobals.AlphabetMode)
		{
			base.enabled = false;
		}
		this.Rainey.SetActive(false);
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x0015721C File Offset: 0x0015541C
	private void Update()
	{
		if (this.RiggedAttacher.gameObject.activeInHierarchy)
		{
			this.RiggedAttacher.newRenderer.SetBlendShapeWeight(0, 100f);
			this.RiggedAttacher.newRenderer.SetBlendShapeWeight(1, 100f);
			base.enabled = false;
			return;
		}
		if (!this.Yandere.PauseScreen.Show && this.Yandere.CanMove)
		{
			if (Input.GetKeyDown(this.Letter[this.ID]))
			{
				this.ID++;
				if (this.ID == this.Letter.Length)
				{
					this.Rainey.SetActive(true);
					base.enabled = false;
				}
			}
			if (Input.GetKeyDown(this.AzurLane[this.AzurID]))
			{
				this.AzurID++;
				if (this.AzurID == this.AzurLane.Length)
				{
					this.Yandere.AzurLane();
					base.enabled = false;
				}
			}
			if (Input.GetKeyDown(this.NurseLetters[this.NurseID]))
			{
				this.NurseID++;
				if (this.NurseID == this.NurseLetters.Length)
				{
					this.RiggedAttacher.root = this.StudentManager.Students[90].Hips.parent.gameObject;
					this.RiggedAttacher.Student = this.StudentManager.Students[90];
					this.RiggedAttacher.gameObject.SetActive(true);
					this.StudentManager.Students[90].MyRenderer.enabled = false;
				}
			}
			if (this.Yandere.Armed && this.Yandere.EquippedWeapon.WeaponID == 14 && Input.GetKeyDown(this.MiyukiLetters[this.MiyukiID]))
			{
				this.MiyukiID++;
				if (this.MiyukiID == this.MiyukiLetters.Length)
				{
					this.Henshin.TransformYandere();
					this.Yandere.CanMove = false;
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x040035D5 RID: 13781
	public RiggedAccessoryAttacher RiggedAttacher;

	// Token: 0x040035D6 RID: 13782
	public StudentManagerScript StudentManager;

	// Token: 0x040035D7 RID: 13783
	public HenshinScript Henshin;

	// Token: 0x040035D8 RID: 13784
	public YandereScript Yandere;

	// Token: 0x040035D9 RID: 13785
	public GameObject Rainey;

	// Token: 0x040035DA RID: 13786
	public string[] MiyukiLetters;

	// Token: 0x040035DB RID: 13787
	public string[] NurseLetters;

	// Token: 0x040035DC RID: 13788
	public string[] AzurLane;

	// Token: 0x040035DD RID: 13789
	public string[] Letter;

	// Token: 0x040035DE RID: 13790
	public int MiyukiID;

	// Token: 0x040035DF RID: 13791
	public int NurseID;

	// Token: 0x040035E0 RID: 13792
	public int AzurID;

	// Token: 0x040035E1 RID: 13793
	public int ID;

	// Token: 0x040035E2 RID: 13794
	public Mesh ThiccMesh;
}
