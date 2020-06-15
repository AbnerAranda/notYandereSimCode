using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000513 RID: 1299
	public class ExitMaidMinigame : MonoBehaviour
	{
		// Token: 0x0600206D RID: 8301 RVA: 0x0018AE0C File Offset: 0x0018900C
		private void OnMouseOver()
		{
			if (Input.GetMouseButtonDown(0))
			{
				GameController.GoToExitScene(true);
			}
		}
	}
}
