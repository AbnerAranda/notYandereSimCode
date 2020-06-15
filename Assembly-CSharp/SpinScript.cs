using System;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class SpinScript : MonoBehaviour
{
	// Token: 0x06001AE3 RID: 6883 RVA: 0x0010ECFC File Offset: 0x0010CEFC
	private void Update()
	{
		this.RotationX += this.X * Time.deltaTime;
		this.RotationY += this.Y * Time.deltaTime;
		this.RotationZ += this.Z * Time.deltaTime;
		base.transform.localEulerAngles = new Vector3(this.RotationX, this.RotationY, this.RotationZ);
	}

	// Token: 0x04002BA0 RID: 11168
	public float X;

	// Token: 0x04002BA1 RID: 11169
	public float Y;

	// Token: 0x04002BA2 RID: 11170
	public float Z;

	// Token: 0x04002BA3 RID: 11171
	private float RotationX;

	// Token: 0x04002BA4 RID: 11172
	private float RotationY;

	// Token: 0x04002BA5 RID: 11173
	private float RotationZ;
}
