using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class RPG_Animation_CharacterFadeOnly : MonoBehaviour
{
	// Token: 0x060009C5 RID: 2501 RVA: 0x0004BB2E File Offset: 0x00049D2E
	private void Awake()
	{
		RPG_Animation_CharacterFadeOnly.instance = this;
	}

	// Token: 0x04000821 RID: 2081
	public static RPG_Animation_CharacterFadeOnly instance;
}
