using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class InfoChanWindowScript : MonoBehaviour
{
	// Token: 0x0600178A RID: 6026 RVA: 0x000CC984 File Offset: 0x000CAB84
	private void Update()
	{
		if (this.Drop)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, this.Drop ? -90f : 0f, Time.deltaTime * 10f);
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				if ((float)this.Orders > 0f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.Drops[this.ItemsToDrop[this.Orders]], this.DropPoint.position, Quaternion.identity);
					this.Timer = 0f;
					this.Orders--;
				}
				else
				{
					this.Open = false;
					if (this.Timer > 3f)
					{
						base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
						this.Drop = false;
					}
				}
			}
		}
		if (this.Test)
		{
			this.DropObject();
		}
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000CCAD5 File Offset: 0x000CACD5
	public void DropObject()
	{
		this.Rotation = 0f;
		this.Timer = 0f;
		this.Dropped = false;
		this.Test = false;
		this.Drop = true;
		this.Open = true;
	}

	// Token: 0x040020E3 RID: 8419
	public Transform DropPoint;

	// Token: 0x040020E4 RID: 8420
	public GameObject[] Drops;

	// Token: 0x040020E5 RID: 8421
	public int[] ItemsToDrop;

	// Token: 0x040020E6 RID: 8422
	public int Orders;

	// Token: 0x040020E7 RID: 8423
	public int ID;

	// Token: 0x040020E8 RID: 8424
	public float Rotation;

	// Token: 0x040020E9 RID: 8425
	public float Timer;

	// Token: 0x040020EA RID: 8426
	public bool Dropped;

	// Token: 0x040020EB RID: 8427
	public bool Drop;

	// Token: 0x040020EC RID: 8428
	public bool Open = true;

	// Token: 0x040020ED RID: 8429
	public bool Test;
}
