using System;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public class StringScript : MonoBehaviour
{
	// Token: 0x06001B28 RID: 6952 RVA: 0x00113510 File Offset: 0x00111710
	private void Start()
	{
		if (this.ArrayID == 0)
		{
			this.Target.position = this.Origin.position;
		}
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x00113530 File Offset: 0x00111730
	private void Update()
	{
		this.String.position = this.Origin.position;
		this.String.LookAt(this.Target);
		this.String.localScale = new Vector3(this.String.localScale.x, this.String.localScale.y, Vector3.Distance(this.Origin.position, this.Target.position) * 0.5f);
	}

	// Token: 0x04002C7E RID: 11390
	public Transform Origin;

	// Token: 0x04002C7F RID: 11391
	public Transform Target;

	// Token: 0x04002C80 RID: 11392
	public Transform String;

	// Token: 0x04002C81 RID: 11393
	public int ArrayID;
}
