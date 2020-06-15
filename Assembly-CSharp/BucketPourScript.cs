using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class BucketPourScript : MonoBehaviour
{
	// Token: 0x06000A94 RID: 2708 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Start()
	{
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x00056EE4 File Offset: 0x000550E4
	private void Update()
	{
		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket != null)
			{
				if (this.Yandere.PickUp.Bucket.Full)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.Label[0].text = "     Pour";
						this.Prompt.enabled = true;
					}
				}
				else if (this.Yandere.PickUp.Bucket.Dumbbells == 5)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.Label[0].text = "     Drop";
						this.Prompt.enabled = true;
					}
				}
				else if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Prompt.Circle[0] != null && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				if (this.Prompt.Label[0].text == "     Pour")
				{
					this.Yandere.Stool = base.transform;
					this.Yandere.CanMove = false;
					this.Yandere.Pouring = true;
					this.Yandere.PourDistance = this.PourDistance;
					this.Yandere.PourHeight = this.PourHeight;
					this.Yandere.PourTime = this.PourTime;
				}
				else
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_bucketDrop_00");
					this.Yandere.MyController.radius = 0f;
					this.Yandere.BucketDropping = true;
					this.Yandere.DropSpot = base.transform;
					this.Yandere.CanMove = false;
				}
			}
		}
		if (this.Yandere.Pouring)
		{
			if (this.PourHeight == "Low" && Input.GetButtonDown("B") && this.Prompt.DistanceSqr < 1f)
			{
				this.SplashCamera.Show = true;
				this.SplashCamera.MyCamera.enabled = true;
				if (this.ID == 1)
				{
					this.SplashCamera.transform.position = new Vector3(32.1f, 0.8f, 26.9f);
					this.SplashCamera.transform.eulerAngles = new Vector3(0f, -45f, 0f);
					return;
				}
				this.SplashCamera.transform.position = new Vector3(1.1f, 0.8f, 32.1f);
				this.SplashCamera.transform.eulerAngles = new Vector3(0f, -135f, 0f);
				return;
			}
		}
		else if (this.Yandere.BucketDropping && Input.GetButtonDown("B") && this.Prompt.DistanceSqr < 1f)
		{
			this.SplashCamera.Show = true;
			this.SplashCamera.MyCamera.enabled = true;
			if (this.ID == 1)
			{
				this.SplashCamera.transform.position = new Vector3(32.1f, 0.8f, 26.9f);
				this.SplashCamera.transform.eulerAngles = new Vector3(0f, -45f, 0f);
				return;
			}
			this.SplashCamera.transform.position = new Vector3(1.1f, 0.8f, 32.1f);
			this.SplashCamera.transform.eulerAngles = new Vector3(0f, -135f, 0f);
		}
	}

	// Token: 0x04000B27 RID: 2855
	public SplashCameraScript SplashCamera;

	// Token: 0x04000B28 RID: 2856
	public YandereScript Yandere;

	// Token: 0x04000B29 RID: 2857
	public PromptScript Prompt;

	// Token: 0x04000B2A RID: 2858
	public string PourHeight = string.Empty;

	// Token: 0x04000B2B RID: 2859
	public float PourDistance;

	// Token: 0x04000B2C RID: 2860
	public float PourTime;

	// Token: 0x04000B2D RID: 2861
	public int ID;
}
