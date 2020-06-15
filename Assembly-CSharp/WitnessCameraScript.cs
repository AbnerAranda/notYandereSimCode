using System;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class WitnessCameraScript : MonoBehaviour
{
	// Token: 0x06001D4B RID: 7499 RVA: 0x0015FBDC File Offset: 0x0015DDDC
	private void Start()
	{
		this.MyCamera.enabled = false;
		this.MyCamera.rect = new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0015FC10 File Offset: 0x0015DE10
	private void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0.25f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0.444444448f, Time.deltaTime * 10f));
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z + Time.deltaTime * 0.09f);
			this.WitnessTimer += Time.deltaTime;
			if (this.WitnessTimer > 5f)
			{
				this.WitnessTimer = 0f;
				this.Show = false;
			}
			if (this.Yandere.Struggling)
			{
				this.WitnessTimer = 0f;
				this.Show = false;
				return;
			}
		}
		else
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0f, Time.deltaTime * 10f));
			if (this.MyCamera.enabled && this.MyCamera.rect.width < 0.1f)
			{
				this.MyCamera.enabled = false;
				base.transform.parent = null;
			}
		}
	}

	// Token: 0x0400376D RID: 14189
	public YandereScript Yandere;

	// Token: 0x0400376E RID: 14190
	public Transform WitnessPOV;

	// Token: 0x0400376F RID: 14191
	public float WitnessTimer;

	// Token: 0x04003770 RID: 14192
	public Camera MyCamera;

	// Token: 0x04003771 RID: 14193
	public bool Show;
}
