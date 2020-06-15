using System;
using UnityEngine;

// Token: 0x020003D2 RID: 978
public class SciFiToolScript : MonoBehaviour
{
	// Token: 0x06001A70 RID: 6768 RVA: 0x0010408F File Offset: 0x0010228F
	private void Start()
	{
		this.Target = this.Student.StudentManager.ToolTarget;
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x001040A7 File Offset: 0x001022A7
	private void Update()
	{
		if ((double)Vector3.Distance(this.Tip.position, this.Target.position) < 0.1)
		{
			this.Sparks.Play();
			return;
		}
		this.Sparks.Stop();
	}

	// Token: 0x04002A0F RID: 10767
	public StudentScript Student;

	// Token: 0x04002A10 RID: 10768
	public ParticleSystem Sparks;

	// Token: 0x04002A11 RID: 10769
	public Transform Target;

	// Token: 0x04002A12 RID: 10770
	public Transform Tip;
}
