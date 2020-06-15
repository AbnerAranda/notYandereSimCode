using System;
using UnityEngine;

// Token: 0x020003DB RID: 987
public class ShadowScript : MonoBehaviour
{
	// Token: 0x06001A90 RID: 6800 RVA: 0x00105EE8 File Offset: 0x001040E8
	private void Update()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = this.Foot.position;
		position.x = position2.x;
		position.z = position2.z;
		base.transform.position = position;
	}

	// Token: 0x04002A68 RID: 10856
	public Transform Foot;
}
