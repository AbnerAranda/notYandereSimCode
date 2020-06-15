using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class PaintingMidoriScript : MonoBehaviour
{
	// Token: 0x060018BC RID: 6332 RVA: 0x000E34A4 File Offset: 0x000E16A4
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.ID++;
		}
		if (this.ID == 0)
		{
			this.Anim.CrossFade("f02_painting_00");
		}
		else if (this.ID == 1)
		{
			this.Anim.CrossFade("f02_shock_00");
			this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * 10f);
		}
		else if (this.ID == 2)
		{
			base.transform.position -= new Vector3(Time.deltaTime * 2f, 0f, 0f);
		}
		base.transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
	}

	// Token: 0x04002490 RID: 9360
	public Animation Anim;

	// Token: 0x04002491 RID: 9361
	public float Rotation;

	// Token: 0x04002492 RID: 9362
	public int ID;
}
