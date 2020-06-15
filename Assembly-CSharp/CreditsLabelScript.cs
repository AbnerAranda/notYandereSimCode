using System;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class CreditsLabelScript : MonoBehaviour
{
	// Token: 0x060012C3 RID: 4803 RVA: 0x00096AD0 File Offset: 0x00094CD0
	private void Start()
	{
		this.Rotation = -90f;
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x00096B20 File Offset: 0x00094D20
	private void Update()
	{
		this.Rotation += Time.deltaTime * this.RotationSpeed;
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y + Time.deltaTime * this.MovementSpeed, base.transform.localPosition.z);
		if (this.Rotation > 90f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001871 RID: 6257
	public float RotationSpeed;

	// Token: 0x04001872 RID: 6258
	public float MovementSpeed;

	// Token: 0x04001873 RID: 6259
	public float Rotation;
}
