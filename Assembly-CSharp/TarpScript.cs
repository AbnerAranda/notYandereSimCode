using System;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class TarpScript : MonoBehaviour
{
	// Token: 0x06001C2C RID: 7212 RVA: 0x00151CC2 File Offset: 0x0014FEC2
	private void Start()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x00151CE4 File Offset: 0x0014FEE4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			AudioSource.PlayClipAtPoint(this.Tarp, base.transform.position);
			this.Unwrap = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Mecha.enabled = true;
			this.Mecha.Prompt.enabled = true;
		}
		if (this.Unwrap)
		{
			this.Speed += Time.deltaTime * 10f;
			base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, new Vector3(90f, 90f, 0f), Time.deltaTime * this.Speed);
			if (base.transform.localEulerAngles.x > 45f)
			{
				if (this.PreviousSpeed == 0f)
				{
					this.PreviousSpeed = this.Speed;
				}
				this.Speed += Time.deltaTime * 10f;
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 0.0001f), (this.Speed - this.PreviousSpeed) * Time.deltaTime);
			}
		}
	}

	// Token: 0x040034CB RID: 13515
	public PromptScript Prompt;

	// Token: 0x040034CC RID: 13516
	public MechaScript Mecha;

	// Token: 0x040034CD RID: 13517
	public AudioClip Tarp;

	// Token: 0x040034CE RID: 13518
	public float PreviousSpeed;

	// Token: 0x040034CF RID: 13519
	public float Speed;

	// Token: 0x040034D0 RID: 13520
	public bool Unwrap;
}
