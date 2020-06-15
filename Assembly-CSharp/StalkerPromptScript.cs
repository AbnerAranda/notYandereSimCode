using System;
using UnityEngine;

// Token: 0x020003F5 RID: 1013
public class StalkerPromptScript : MonoBehaviour
{
	// Token: 0x06001AF5 RID: 6901 RVA: 0x0010F948 File Offset: 0x0010DB48
	private void Update()
	{
		base.transform.LookAt(this.Yandere.MainCamera.transform);
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
			if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 2f && Input.GetButtonDown("A") && this.ID == 1)
			{
				this.Yandere.MyAnimation.CrossFade("f02_climbTrellis_00");
				this.Yandere.Climbing = true;
				this.Yandere.CanMove = false;
				UnityEngine.Object.Destroy(base.gameObject);
				UnityEngine.Object.Destroy(this.MySprite);
			}
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
		}
		this.MySprite.color = new Color(1f, 1f, 1f, this.Alpha);
	}

	// Token: 0x04002BCA RID: 11210
	public StalkerYandereScript Yandere;

	// Token: 0x04002BCB RID: 11211
	public UISprite MySprite;

	// Token: 0x04002BCC RID: 11212
	public float Alpha;

	// Token: 0x04002BCD RID: 11213
	public int ID;
}
