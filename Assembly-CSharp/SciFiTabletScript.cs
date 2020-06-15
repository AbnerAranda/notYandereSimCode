using System;
using UnityEngine;

// Token: 0x020003D0 RID: 976
public class SciFiTabletScript : MonoBehaviour
{
	// Token: 0x06001A6A RID: 6762 RVA: 0x00103F3C File Offset: 0x0010213C
	private void Start()
	{
		this.Holograms = this.Student.StudentManager.Holograms;
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x00103F54 File Offset: 0x00102154
	private void Update()
	{
		if ((double)Vector3.Distance(this.Finger.position, base.transform.position) < 0.1)
		{
			if (!this.Updated)
			{
				this.Holograms.UpdateHolograms();
				this.Updated = true;
				return;
			}
		}
		else
		{
			this.Updated = false;
		}
	}

	// Token: 0x04002A07 RID: 10759
	public StudentScript Student;

	// Token: 0x04002A08 RID: 10760
	public HologramScript Holograms;

	// Token: 0x04002A09 RID: 10761
	public Transform Finger;

	// Token: 0x04002A0A RID: 10762
	public bool Updated;
}
