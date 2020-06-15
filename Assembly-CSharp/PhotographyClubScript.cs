using System;
using UnityEngine;

// Token: 0x02000366 RID: 870
public class PhotographyClubScript : MonoBehaviour
{
	// Token: 0x06001908 RID: 6408 RVA: 0x000EA80C File Offset: 0x000E8A0C
	private void Start()
	{
		if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
		{
			this.InvestigationPhotos.SetActive(true);
			this.ArtsyPhotos.SetActive(false);
			this.CrimeScene.SetActive(true);
			this.StraightTables.SetActive(true);
			this.CrookedTables.SetActive(false);
			return;
		}
		this.InvestigationPhotos.SetActive(false);
		this.ArtsyPhotos.SetActive(true);
		this.CrimeScene.SetActive(false);
		this.StraightTables.SetActive(false);
		this.CrookedTables.SetActive(true);
	}

	// Token: 0x0400257E RID: 9598
	public GameObject CrimeScene;

	// Token: 0x0400257F RID: 9599
	public GameObject InvestigationPhotos;

	// Token: 0x04002580 RID: 9600
	public GameObject ArtsyPhotos;

	// Token: 0x04002581 RID: 9601
	public GameObject StraightTables;

	// Token: 0x04002582 RID: 9602
	public GameObject CrookedTables;
}
