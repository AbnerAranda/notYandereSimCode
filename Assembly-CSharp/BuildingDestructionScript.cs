using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class BuildingDestructionScript : MonoBehaviour
{
	// Token: 0x06000AA0 RID: 2720 RVA: 0x000588F8 File Offset: 0x00056AF8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Phase++;
			this.Sink = true;
		}
		if (this.Sink)
		{
			if (this.Phase == 1)
			{
				base.transform.position = new Vector3(UnityEngine.Random.Range(-1f, 1f), base.transform.position.y - Time.deltaTime * 10f, UnityEngine.Random.Range(-19f, -21f));
				return;
			}
			if (this.NewSchool.position.y != 0f)
			{
				this.NewSchool.position = new Vector3(this.NewSchool.position.x, Mathf.MoveTowards(this.NewSchool.position.y, 0f, Time.deltaTime * 10f), this.NewSchool.position.z);
				base.transform.position = new Vector3(UnityEngine.Random.Range(-1f, 1f), base.transform.position.y, UnityEngine.Random.Range(13f, 15f));
				return;
			}
			base.transform.position = new Vector3(0f, base.transform.position.y, 14f);
		}
	}

	// Token: 0x04000B52 RID: 2898
	public Transform NewSchool;

	// Token: 0x04000B53 RID: 2899
	public bool Sink;

	// Token: 0x04000B54 RID: 2900
	public int Phase;
}
