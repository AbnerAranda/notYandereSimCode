using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class SplashSpawnerScript : MonoBehaviour
{
	// Token: 0x06001AE8 RID: 6888 RVA: 0x0010EF34 File Offset: 0x0010D134
	private void Update()
	{
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Yandere.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
				return;
			}
		}
		else if (base.transform.position.y < this.Yandere.transform.position.y + this.DownThreshold)
		{
			this.FootUp = false;
			if (this.Bloody)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodSplash, new Vector3(base.transform.position.x, this.Yandere.position.y, base.transform.position.z), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(-90f, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
				this.Bloody = false;
			}
		}
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x0010F047 File Offset: 0x0010D247
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.Bloody = true;
		}
	}

	// Token: 0x04002BA9 RID: 11177
	public GameObject BloodSplash;

	// Token: 0x04002BAA RID: 11178
	public Transform Yandere;

	// Token: 0x04002BAB RID: 11179
	public bool Bloody;

	// Token: 0x04002BAC RID: 11180
	public bool FootUp;

	// Token: 0x04002BAD RID: 11181
	public float DownThreshold;

	// Token: 0x04002BAE RID: 11182
	public float UpThreshold;

	// Token: 0x04002BAF RID: 11183
	public float Height;
}
