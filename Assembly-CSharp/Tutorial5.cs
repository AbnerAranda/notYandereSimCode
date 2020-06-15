using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class Tutorial5 : MonoBehaviour
{
	// Token: 0x06000141 RID: 321 RVA: 0x000134C4 File Offset: 0x000116C4
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].duration = Mathf.Lerp(2f, 0.5f, UIProgressBar.current.value);
		}
	}
}
