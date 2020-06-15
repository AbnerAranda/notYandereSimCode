using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class OpenURLOnClick : MonoBehaviour
{
	// Token: 0x0600012D RID: 301 RVA: 0x00012EAC File Offset: 0x000110AC
	private void OnClick()
	{
		UILabel component = base.GetComponent<UILabel>();
		if (component != null)
		{
			string urlAtPosition = component.GetUrlAtPosition(UICamera.lastWorldPosition);
			if (!string.IsNullOrEmpty(urlAtPosition))
			{
				Application.OpenURL(urlAtPosition);
			}
		}
	}
}
