using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	// Token: 0x060001D1 RID: 465 RVA: 0x000165EC File Offset: 0x000147EC
	private void OnEnable()
	{
		UIDragDropRoot.root = base.transform;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x000165F9 File Offset: 0x000147F9
	private void OnDisable()
	{
		if (UIDragDropRoot.root == base.transform)
		{
			UIDragDropRoot.root = null;
		}
	}

	// Token: 0x0400034B RID: 843
	public static Transform root;
}
