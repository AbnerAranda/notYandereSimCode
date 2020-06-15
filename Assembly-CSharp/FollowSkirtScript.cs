using System;
using UnityEngine;

// Token: 0x020002A6 RID: 678
public class FollowSkirtScript : MonoBehaviour
{
	// Token: 0x0600141D RID: 5149 RVA: 0x000B12E0 File Offset: 0x000AF4E0
	private void LateUpdate()
	{
		this.SkirtHips.position = this.TargetSkirtHips.position;
		for (int i = 0; i < 3; i++)
		{
			this.SkirtFront[i].position = this.TargetSkirtFront[i].position;
			this.SkirtFront[i].rotation = this.TargetSkirtFront[i].rotation;
			this.SkirtBack[i].position = this.TargetSkirtBack[i].position;
			this.SkirtBack[i].rotation = this.TargetSkirtBack[i].rotation;
			this.SkirtRight[i].position = this.TargetSkirtRight[i].position;
			this.SkirtRight[i].rotation = this.TargetSkirtRight[i].rotation;
			this.SkirtLeft[i].position = this.TargetSkirtLeft[i].position;
			this.SkirtLeft[i].rotation = this.TargetSkirtLeft[i].rotation;
		}
	}

	// Token: 0x04001C74 RID: 7284
	public Transform[] TargetSkirtFront;

	// Token: 0x04001C75 RID: 7285
	public Transform[] TargetSkirtBack;

	// Token: 0x04001C76 RID: 7286
	public Transform[] TargetSkirtRight;

	// Token: 0x04001C77 RID: 7287
	public Transform[] TargetSkirtLeft;

	// Token: 0x04001C78 RID: 7288
	public Transform[] SkirtFront;

	// Token: 0x04001C79 RID: 7289
	public Transform[] SkirtBack;

	// Token: 0x04001C7A RID: 7290
	public Transform[] SkirtRight;

	// Token: 0x04001C7B RID: 7291
	public Transform[] SkirtLeft;

	// Token: 0x04001C7C RID: 7292
	public Transform TargetSkirtHips;

	// Token: 0x04001C7D RID: 7293
	public Transform SkirtHips;
}
