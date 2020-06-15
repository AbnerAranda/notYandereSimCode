using System;
using UnityEngine;

// Token: 0x0200046D RID: 1133
public class WritheScript : MonoBehaviour
{
	// Token: 0x06001D51 RID: 7505 RVA: 0x0016048F File Offset: 0x0015E68F
	private void Start()
	{
		this.StartTime = Time.time;
		this.Duration = UnityEngine.Random.Range(1f, 5f);
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x001604B4 File Offset: 0x0015E6B4
	private void Update()
	{
		if (this.Rotation == this.EndValue)
		{
			this.StartValue = this.EndValue;
			this.EndValue = UnityEngine.Random.Range(-45f, 45f);
			this.StartTime = Time.time;
			this.Duration = UnityEngine.Random.Range(1f, 5f);
		}
		float t = (Time.time - this.StartTime) / this.Duration;
		this.Rotation = Mathf.SmoothStep(this.StartValue, this.EndValue, t);
		switch (this.ID)
		{
		case 1:
			base.transform.localEulerAngles = new Vector3(this.Rotation, base.transform.localEulerAngles.y, base.transform.localEulerAngles.z);
			return;
		case 2:
			if (this.SpecialCase)
			{
				this.Rotation += 180f;
			}
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
			return;
		case 3:
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, this.Rotation);
			return;
		default:
			return;
		}
	}

	// Token: 0x04003786 RID: 14214
	public float Rotation;

	// Token: 0x04003787 RID: 14215
	public float StartTime;

	// Token: 0x04003788 RID: 14216
	public float Duration;

	// Token: 0x04003789 RID: 14217
	public float StartValue;

	// Token: 0x0400378A RID: 14218
	public float EndValue;

	// Token: 0x0400378B RID: 14219
	public int ID;

	// Token: 0x0400378C RID: 14220
	public bool SpecialCase;
}
