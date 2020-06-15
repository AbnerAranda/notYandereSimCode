using System;
using UnityEngine;

// Token: 0x0200048B RID: 1163
public class YanvaniaTripleFireballScript : MonoBehaviour
{
	// Token: 0x06001DFF RID: 7679 RVA: 0x00177CEC File Offset: 0x00175EEC
	private void Start()
	{
		this.Direction = ((this.Dracula.position.x > base.transform.position.x) ? -1 : 1);
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x00177D1C File Offset: 0x00175F1C
	private void Update()
	{
		Transform transform = this.Fireballs[1];
		Transform transform2 = this.Fireballs[2];
		Transform transform3 = this.Fireballs[3];
		if (transform != null)
		{
			transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, 7.5f, Time.deltaTime * this.Speed), transform.position.z);
		}
		if (transform2 != null)
		{
			transform2.position = new Vector3(transform2.position.x, Mathf.MoveTowards(transform2.position.y, 7.16666f, Time.deltaTime * this.Speed), transform2.position.z);
		}
		if (transform3 != null)
		{
			transform3.position = new Vector3(transform3.position.x, Mathf.MoveTowards(transform3.position.y, 6.83333f, Time.deltaTime * this.Speed), transform3.position.z);
		}
		for (int i = 1; i < 4; i++)
		{
			Transform transform4 = this.Fireballs[i];
			if (transform4 != null)
			{
				transform4.position = new Vector3(transform4.position.x + (float)this.Direction * Time.deltaTime * this.Speed, transform4.position.y, transform4.position.z);
			}
		}
		this.Timer += Time.deltaTime;
		if (this.Timer > 10f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003BA0 RID: 15264
	public Transform[] Fireballs;

	// Token: 0x04003BA1 RID: 15265
	public Transform Dracula;

	// Token: 0x04003BA2 RID: 15266
	public int Direction;

	// Token: 0x04003BA3 RID: 15267
	public float Speed;

	// Token: 0x04003BA4 RID: 15268
	public float Timer;
}
