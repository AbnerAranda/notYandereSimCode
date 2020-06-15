using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public class StopAnimationScript : MonoBehaviour
{
	// Token: 0x06001B10 RID: 6928 RVA: 0x0011184C File Offset: 0x0010FA4C
	private void Start()
	{
		this.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
		this.Anim = base.GetComponent<Animation>();
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x00111870 File Offset: 0x0010FA70
	private void Update()
	{
		if (this.StudentManager.DisableFarAnims)
		{
			if (Vector3.Distance(this.Yandere.position, base.transform.position) > 15f)
			{
				if (this.Anim.enabled)
				{
					this.Anim.enabled = false;
					return;
				}
			}
			else if (!this.Anim.enabled)
			{
				this.Anim.enabled = true;
				return;
			}
		}
		else if (!this.Anim.enabled)
		{
			this.Anim.enabled = true;
		}
	}

	// Token: 0x04002C13 RID: 11283
	public StudentManagerScript StudentManager;

	// Token: 0x04002C14 RID: 11284
	public Transform Yandere;

	// Token: 0x04002C15 RID: 11285
	private Animation Anim;
}
