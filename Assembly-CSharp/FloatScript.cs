using System;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class FloatScript : MonoBehaviour
{
	// Token: 0x06001418 RID: 5144 RVA: 0x000B0EB0 File Offset: 0x000AF0B0
	private void Update()
	{
		if (!this.Down)
		{
			this.Float += Time.deltaTime * this.Speed;
			if (this.Float > this.Limit)
			{
				this.Down = true;
			}
		}
		else
		{
			this.Float -= Time.deltaTime * this.Speed;
			if (this.Float < -1f * this.Limit)
			{
				this.Down = false;
			}
		}
		base.transform.localPosition += new Vector3(0f, this.Float * Time.deltaTime, 0f);
		if (base.transform.localPosition.y > this.UpLimit)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.UpLimit, base.transform.localPosition.z);
		}
		if (base.transform.localPosition.y < this.DownLimit)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.DownLimit, base.transform.localPosition.z);
		}
	}

	// Token: 0x04001C66 RID: 7270
	public bool Down;

	// Token: 0x04001C67 RID: 7271
	public float Float;

	// Token: 0x04001C68 RID: 7272
	public float Speed;

	// Token: 0x04001C69 RID: 7273
	public float Limit;

	// Token: 0x04001C6A RID: 7274
	public float DownLimit;

	// Token: 0x04001C6B RID: 7275
	public float UpLimit;
}
