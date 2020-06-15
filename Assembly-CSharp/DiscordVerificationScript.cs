using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200026B RID: 619
public class DiscordVerificationScript : MonoBehaviour
{
	// Token: 0x06001350 RID: 4944 RVA: 0x000A5153 File Offset: 0x000A3353
	private void Update()
	{
		if (Input.GetKeyDown("q"))
		{
			SceneManager.LoadScene("MissionModeScene");
		}
	}
}
