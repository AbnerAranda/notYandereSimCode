using System;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public class SentenceScript : MonoBehaviour
{
	// Token: 0x06001A7F RID: 6783 RVA: 0x001045D1 File Offset: 0x001027D1
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.Sentence.text = this.Words[this.ID];
			this.ID++;
		}
	}

	// Token: 0x04002A22 RID: 10786
	public UILabel Sentence;

	// Token: 0x04002A23 RID: 10787
	public string[] Words;

	// Token: 0x04002A24 RID: 10788
	public int ID;
}
