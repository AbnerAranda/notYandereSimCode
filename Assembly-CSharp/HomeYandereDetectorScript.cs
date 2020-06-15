using System;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class HomeYandereDetectorScript : MonoBehaviour
{
	// Token: 0x06001770 RID: 6000 RVA: 0x000CAE5D File Offset: 0x000C905D
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = true;
		}
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x000CAE78 File Offset: 0x000C9078
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = false;
		}
	}

	// Token: 0x04002096 RID: 8342
	public bool YandereDetected;
}
