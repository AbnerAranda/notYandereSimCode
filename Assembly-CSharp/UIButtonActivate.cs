using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x0600016D RID: 365 RVA: 0x0001448D File Offset: 0x0001268D
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x04000306 RID: 774
	public GameObject target;

	// Token: 0x04000307 RID: 775
	public bool state = true;
}
