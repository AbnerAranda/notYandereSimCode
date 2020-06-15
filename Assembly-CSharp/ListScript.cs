using System;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class ListScript : MonoBehaviour
{
	// Token: 0x0600180A RID: 6154 RVA: 0x000D4E68 File Offset: 0x000D3068
	public void Start()
	{
		if (this.AutoFill)
		{
			for (int i = 1; i < this.List.Length; i++)
			{
				this.List[i] = base.transform.GetChild(i - 1);
			}
		}
	}

	// Token: 0x040022A6 RID: 8870
	public Transform[] List;

	// Token: 0x040022A7 RID: 8871
	public bool AutoFill;
}
