using System;
using UnityEngine;

// Token: 0x02000330 RID: 816
public class MemeManagerScript : MonoBehaviour
{
	// Token: 0x06001836 RID: 6198 RVA: 0x000D9210 File Offset: 0x000D7410
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			GameObject[] memes = this.Memes;
			for (int i = 0; i < memes.Length; i++)
			{
				memes[i].SetActive(false);
			}
		}
	}

	// Token: 0x04002333 RID: 9011
	[SerializeField]
	private GameObject[] Memes;
}
