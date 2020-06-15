using System;
using UnityEngine;

// Token: 0x020003AD RID: 941
public class RuntimeAnimRetarget : MonoBehaviour
{
	// Token: 0x060019F8 RID: 6648 RVA: 0x000FF016 File Offset: 0x000FD216
	private void Start()
	{
		Debug.Log(this.Source.name);
		this.SourceSkelNodes = this.Source.GetComponentsInChildren<Component>();
		this.TargetSkelNodes = this.Target.GetComponentsInChildren<Component>();
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x000FF04C File Offset: 0x000FD24C
	private void LateUpdate()
	{
		this.TargetSkelNodes[1].transform.localPosition = this.SourceSkelNodes[1].transform.localPosition;
		for (int i = 0; i < 154; i++)
		{
			this.TargetSkelNodes[i].transform.localRotation = this.SourceSkelNodes[i].transform.localRotation;
		}
	}

	// Token: 0x040028CB RID: 10443
	public GameObject Source;

	// Token: 0x040028CC RID: 10444
	public GameObject Target;

	// Token: 0x040028CD RID: 10445
	private Component[] SourceSkelNodes;

	// Token: 0x040028CE RID: 10446
	private Component[] TargetSkelNodes;
}
