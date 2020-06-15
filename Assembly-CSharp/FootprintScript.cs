﻿using System;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class FootprintScript : MonoBehaviour
{
	// Token: 0x06001421 RID: 5153 RVA: 0x000B1438 File Offset: 0x000AF638
	private void Start()
	{
		if (this.Yandere.Schoolwear == 0 || this.Yandere.Schoolwear == 2 || (this.Yandere.ClubAttire && this.Yandere.Club == ClubType.MartialArts) || this.Yandere.Hungry || this.Yandere.LucyHelmet.activeInHierarchy)
		{
			base.GetComponent<Renderer>().material.mainTexture = this.Footprint;
		}
		if (GameGlobals.CensorBlood)
		{
			base.GetComponent<Renderer>().material.mainTexture = this.Flower;
			base.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x04001C7F RID: 7295
	public YandereScript Yandere;

	// Token: 0x04001C80 RID: 7296
	public Texture Footprint;

	// Token: 0x04001C81 RID: 7297
	public Texture Flower;
}
