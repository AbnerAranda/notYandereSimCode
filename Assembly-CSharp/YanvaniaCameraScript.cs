using System;
using UnityEngine;

// Token: 0x0200047D RID: 1149
public class YanvaniaCameraScript : MonoBehaviour
{
	// Token: 0x06001DD2 RID: 7634 RVA: 0x001746AE File Offset: 0x001728AE
	private void Start()
	{
		base.transform.position = this.Yanmont.transform.position + new Vector3(0f, 1.5f, -5.85f);
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x001746E4 File Offset: 0x001728E4
	private void FixedUpdate()
	{
		this.TargetZoom += Input.GetAxis("Mouse ScrollWheel");
		if (this.TargetZoom < 0f)
		{
			this.TargetZoom = 0f;
		}
		if (this.TargetZoom > 3.85f)
		{
			this.TargetZoom = 3.85f;
		}
		this.Zoom = Mathf.Lerp(this.Zoom, this.TargetZoom, Time.deltaTime);
		if (!this.Cutscene)
		{
			base.transform.position = this.Yanmont.transform.position + new Vector3(0f, 1.5f, -5.85f + this.Zoom);
			if (base.transform.position.x > 47.9f)
			{
				base.transform.position = new Vector3(47.9f, base.transform.position.y, base.transform.position.z);
				return;
			}
		}
		else
		{
			if (this.StopMusic)
			{
				AudioSource component = this.Jukebox.GetComponent<AudioSource>();
				component.volume -= Time.deltaTime * ((this.Yanmont.Health > 0f) ? 0.2f : 0.025f);
				if (component.volume <= 0f)
				{
					this.StopMusic = false;
				}
			}
			base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34.675f, Time.deltaTime * this.Yanmont.walkSpeed), 8f, -5.85f + this.Zoom);
		}
	}

	// Token: 0x04003B13 RID: 15123
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003B14 RID: 15124
	public GameObject Jukebox;

	// Token: 0x04003B15 RID: 15125
	public bool Cutscene;

	// Token: 0x04003B16 RID: 15126
	public bool StopMusic = true;

	// Token: 0x04003B17 RID: 15127
	public float TargetZoom;

	// Token: 0x04003B18 RID: 15128
	public float Zoom;
}
