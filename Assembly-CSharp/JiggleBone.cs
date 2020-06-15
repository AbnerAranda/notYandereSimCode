using System;
using UnityEngine;

// Token: 0x02000310 RID: 784
public class JiggleBone : MonoBehaviour
{
	// Token: 0x060017AB RID: 6059 RVA: 0x000D16C4 File Offset: 0x000CF8C4
	private void Awake()
	{
		Vector3 vector = base.transform.position + base.transform.TransformDirection(this.boneAxis * this.targetDistance);
		this.dynamicPos = vector;
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x000D1708 File Offset: 0x000CF908
	private void LateUpdate()
	{
		base.transform.rotation = default(Quaternion);
		Vector3 dir = base.transform.TransformDirection(this.boneAxis * this.targetDistance);
		Vector3 vector = base.transform.TransformDirection(new Vector3(0f, 1f, 0f));
		Vector3 vector2 = base.transform.position + base.transform.TransformDirection(this.boneAxis * this.targetDistance);
		this.force.x = (vector2.x - this.dynamicPos.x) * this.bStiffness;
		this.acc.x = this.force.x / this.bMass;
		this.vel.x = this.vel.x + this.acc.x * (1f - this.bDamping);
		this.force.y = (vector2.y - this.dynamicPos.y) * this.bStiffness;
		this.force.y = this.force.y - this.bGravity / 10f;
		this.acc.y = this.force.y / this.bMass;
		this.vel.y = this.vel.y + this.acc.y * (1f - this.bDamping);
		this.force.z = (vector2.z - this.dynamicPos.z) * this.bStiffness;
		this.acc.z = this.force.z / this.bMass;
		this.vel.z = this.vel.z + this.acc.z * (1f - this.bDamping);
		this.dynamicPos += this.vel + this.force;
		base.transform.LookAt(this.dynamicPos, vector);
		if (this.SquashAndStretch)
		{
			float magnitude = (this.dynamicPos - vector2).magnitude;
			if (this.boneAxis.x != 0f)
			{
				float num = this.frontStretch;
			}
			else
			{
				float num2 = this.sideStretch;
			}
			if (this.boneAxis.y != 0f)
			{
				float num3 = this.frontStretch;
			}
			else
			{
				float num4 = this.sideStretch;
			}
			if (this.boneAxis.z != 0f)
			{
				float num5 = this.frontStretch;
			}
			else
			{
				float num6 = this.sideStretch;
			}
		}
		if (this.debugMode)
		{
			Debug.DrawRay(base.transform.position, dir, Color.blue);
			Debug.DrawRay(base.transform.position, vector, Color.green);
			Debug.DrawRay(vector2, Vector3.up * 0.2f, Color.yellow);
			Debug.DrawRay(this.dynamicPos, Vector3.up * 0.2f, Color.red);
		}
	}

	// Token: 0x040021D8 RID: 8664
	public bool debugMode = true;

	// Token: 0x040021D9 RID: 8665
	private Vector3 dynamicPos;

	// Token: 0x040021DA RID: 8666
	public Vector3 boneAxis = new Vector3(0f, 0f, 1f);

	// Token: 0x040021DB RID: 8667
	public float targetDistance = 2f;

	// Token: 0x040021DC RID: 8668
	public float bStiffness = 0.1f;

	// Token: 0x040021DD RID: 8669
	public float bMass = 0.9f;

	// Token: 0x040021DE RID: 8670
	public float bDamping = 0.75f;

	// Token: 0x040021DF RID: 8671
	public float bGravity = 0.75f;

	// Token: 0x040021E0 RID: 8672
	private Vector3 force;

	// Token: 0x040021E1 RID: 8673
	private Vector3 acc;

	// Token: 0x040021E2 RID: 8674
	private Vector3 vel;

	// Token: 0x040021E3 RID: 8675
	public bool SquashAndStretch = true;

	// Token: 0x040021E4 RID: 8676
	public float sideStretch = 0.15f;

	// Token: 0x040021E5 RID: 8677
	public float frontStretch = 0.2f;
}
