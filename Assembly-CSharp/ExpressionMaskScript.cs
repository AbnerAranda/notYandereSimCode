﻿using System;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class ExpressionMaskScript : MonoBehaviour
{
	// Token: 0x060013F6 RID: 5110 RVA: 0x000AE974 File Offset: 0x000ACB74
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if (this.ID < 3)
			{
				this.ID++;
			}
			else
			{
				this.ID = 0;
			}
			switch (this.ID)
			{
			case 0:
				this.Mask.material.mainTextureOffset = Vector2.zero;
				return;
			case 1:
				this.Mask.material.mainTextureOffset = new Vector2(0f, 0.5f);
				return;
			case 2:
				this.Mask.material.mainTextureOffset = new Vector2(0.5f, 0f);
				return;
			case 3:
				this.Mask.material.mainTextureOffset = new Vector2(0.5f, 0.5f);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x04001BE7 RID: 7143
	public Renderer Mask;

	// Token: 0x04001BE8 RID: 7144
	public int ID;
}
