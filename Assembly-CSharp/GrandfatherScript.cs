using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class GrandfatherScript : MonoBehaviour
{
	// Token: 0x060016EA RID: 5866 RVA: 0x000BE7B0 File Offset: 0x000BC9B0
	private void Update()
	{
		if (!this.Flip)
		{
			if ((double)this.Force < 0.1)
			{
				this.Force += Time.deltaTime * 0.1f * this.Speed;
			}
		}
		else if ((double)this.Force > -0.1)
		{
			this.Force -= Time.deltaTime * 0.1f * this.Speed;
		}
		this.Rotation += this.Force;
		if (this.Rotation > 1f)
		{
			this.Flip = true;
		}
		else if (this.Rotation < -1f)
		{
			this.Flip = false;
		}
		if (this.Rotation > 5f)
		{
			this.Rotation = 5f;
		}
		else if (this.Rotation < -5f)
		{
			this.Rotation = -5f;
		}
		this.Pendulum.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		this.MinuteHand.localEulerAngles = new Vector3(this.MinuteHand.localEulerAngles.x, this.MinuteHand.localEulerAngles.y, this.Clock.Minute * 6f);
		this.HourHand.localEulerAngles = new Vector3(this.HourHand.localEulerAngles.x, this.HourHand.localEulerAngles.y, this.Clock.Hour * 30f);
	}

	// Token: 0x04001E56 RID: 7766
	public ClockScript Clock;

	// Token: 0x04001E57 RID: 7767
	public Transform MinuteHand;

	// Token: 0x04001E58 RID: 7768
	public Transform HourHand;

	// Token: 0x04001E59 RID: 7769
	public Transform Pendulum;

	// Token: 0x04001E5A RID: 7770
	public float Rotation;

	// Token: 0x04001E5B RID: 7771
	public float Force;

	// Token: 0x04001E5C RID: 7772
	public float Speed;

	// Token: 0x04001E5D RID: 7773
	public bool Flip;
}
