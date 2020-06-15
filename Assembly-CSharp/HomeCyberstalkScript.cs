using System;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class HomeCyberstalkScript : MonoBehaviour
{
	// Token: 0x06001730 RID: 5936 RVA: 0x000C4FDC File Offset: 0x000C31DC
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
			this.HomeDarkness.Cyberstalking = true;
			this.HomeDarkness.FadeOut = true;
			base.gameObject.SetActive(false);
			for (int i = 1; i < 26; i++)
			{
				ConversationGlobals.SetTopicLearnedByStudent(i, this.HomeDarkness.HomeCamera.HomeInternet.Student, true);
				ConversationGlobals.SetTopicDiscovered(i, true);
			}
		}
		if (Input.GetButtonDown("B"))
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001F86 RID: 8070
	public HomeDarknessScript HomeDarkness;
}
