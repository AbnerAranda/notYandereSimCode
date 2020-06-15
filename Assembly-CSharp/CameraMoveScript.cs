using System;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public class CameraMoveScript : MonoBehaviour
{
	// Token: 0x06001E5D RID: 7773 RVA: 0x0017E118 File Offset: 0x0017C318
	private void Start()
	{
		base.transform.position = this.StartPos.position;
		base.transform.rotation = this.StartPos.rotation;
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x0017E148 File Offset: 0x0017C348
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			this.Timer += Time.deltaTime * this.Speed;
			if (this.Timer > 0.1f)
			{
				this.OpenDoors = true;
				if (this.LeftDoor != null)
				{
					this.LeftDoor.transform.localPosition = new Vector3(Mathf.Lerp(this.LeftDoor.transform.localPosition.x, 1f, Time.deltaTime), this.LeftDoor.transform.localPosition.y, this.LeftDoor.transform.localPosition.z);
					this.RightDoor.transform.localPosition = new Vector3(Mathf.Lerp(this.RightDoor.transform.localPosition.x, -1f, Time.deltaTime), this.RightDoor.transform.localPosition.y, this.RightDoor.transform.localPosition.z);
				}
			}
			base.transform.position = Vector3.Lerp(base.transform.position, this.EndPos.position, Time.deltaTime * this.Timer);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.EndPos.rotation, Time.deltaTime * this.Timer);
		}
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x0017E2DB File Offset: 0x0017C4DB
	private void LateUpdate()
	{
		if (this.Target != null)
		{
			base.transform.LookAt(this.Target);
		}
	}

	// Token: 0x04003CA0 RID: 15520
	public Transform StartPos;

	// Token: 0x04003CA1 RID: 15521
	public Transform EndPos;

	// Token: 0x04003CA2 RID: 15522
	public Transform RightDoor;

	// Token: 0x04003CA3 RID: 15523
	public Transform LeftDoor;

	// Token: 0x04003CA4 RID: 15524
	public Transform Target;

	// Token: 0x04003CA5 RID: 15525
	public bool OpenDoors;

	// Token: 0x04003CA6 RID: 15526
	public bool Begin;

	// Token: 0x04003CA7 RID: 15527
	public float Speed;

	// Token: 0x04003CA8 RID: 15528
	public float Timer;
}
