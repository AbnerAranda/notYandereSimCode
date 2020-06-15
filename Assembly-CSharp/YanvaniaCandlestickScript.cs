using System;
using UnityEngine;

// Token: 0x0200047F RID: 1151
public class YanvaniaCandlestickScript : MonoBehaviour
{
	// Token: 0x06001DD8 RID: 7640 RVA: 0x00174980 File Offset: 0x00172B80
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19 && !this.Destroyed)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.DestroyedCandlestick, base.transform.position, Quaternion.identity).transform.localScale = base.transform.localScale;
			this.Destroyed = true;
			AudioClipPlayer.Play2D(this.Break, base.transform.position);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B1C RID: 15132
	public GameObject DestroyedCandlestick;

	// Token: 0x04003B1D RID: 15133
	public bool Destroyed;

	// Token: 0x04003B1E RID: 15134
	public AudioClip Break;
}
