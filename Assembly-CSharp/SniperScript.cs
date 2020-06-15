using System;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class SniperScript : MonoBehaviour
{
	// Token: 0x06001AD8 RID: 6872 RVA: 0x0010EB24 File Offset: 0x0010CD24
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 10f)
		{
			if (this.StudentManager.Students[10] != null)
			{
				this.StudentManager.Students[10].BecomeRagdoll();
			}
			if (this.StudentManager.Students[11] != null)
			{
				this.StudentManager.Students[11].BecomeRagdoll();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002B99 RID: 11161
	public StudentManagerScript StudentManager;

	// Token: 0x04002B9A RID: 11162
	public float Timer;
}
