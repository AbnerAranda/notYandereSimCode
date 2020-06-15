using System;
using UnityEngine;

// Token: 0x020003F0 RID: 1008
public class SplashCameraScript : MonoBehaviour
{
	// Token: 0x06001AE5 RID: 6885 RVA: 0x0010ED76 File Offset: 0x0010CF76
	private void Start()
	{
		this.MyCamera.enabled = false;
		this.MyCamera.rect = new Rect(0f, 0.219f, 0f, 0f);
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x0010EDA8 File Offset: 0x0010CFA8
	private void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0.4f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0.71104f, Time.deltaTime * 10f));
			this.Timer += Time.deltaTime;
			if (this.Timer > 15f)
			{
				this.Show = false;
				this.Timer = 0f;
				return;
			}
		}
		else
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0f, Time.deltaTime * 10f));
			if (this.MyCamera.enabled && this.MyCamera.rect.width < 0.1f)
			{
				this.MyCamera.enabled = false;
			}
		}
	}

	// Token: 0x04002BA6 RID: 11174
	public Camera MyCamera;

	// Token: 0x04002BA7 RID: 11175
	public bool Show;

	// Token: 0x04002BA8 RID: 11176
	public float Timer;
}
