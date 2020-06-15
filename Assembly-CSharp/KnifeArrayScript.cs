using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class KnifeArrayScript : MonoBehaviour
{
	// Token: 0x060017F1 RID: 6129 RVA: 0x000D3688 File Offset: 0x000D1888
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.ID < 10)
		{
			if (this.Timer > this.SpawnTimes[this.ID] && this.GlobalKnifeArray.ID < 1000)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Knife, base.transform.position, Quaternion.identity);
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0.5f, 2f), UnityEngine.Random.Range(-0.75f, -1.75f));
				gameObject.transform.parent = null;
				gameObject.transform.LookAt(this.KnifeTarget);
				this.GlobalKnifeArray.Knives[this.GlobalKnifeArray.ID] = gameObject.GetComponent<TimeStopKnifeScript>();
				this.GlobalKnifeArray.ID++;
				this.ID++;
				return;
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002255 RID: 8789
	public GlobalKnifeArrayScript GlobalKnifeArray;

	// Token: 0x04002256 RID: 8790
	public Transform KnifeTarget;

	// Token: 0x04002257 RID: 8791
	public float[] SpawnTimes;

	// Token: 0x04002258 RID: 8792
	public GameObject Knife;

	// Token: 0x04002259 RID: 8793
	public float Timer;

	// Token: 0x0400225A RID: 8794
	public int ID;
}
