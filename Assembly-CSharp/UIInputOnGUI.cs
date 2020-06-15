using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
[RequireComponent(typeof(UIInput))]
public class UIInputOnGUI : MonoBehaviour
{
	// Token: 0x060007B7 RID: 1975 RVA: 0x00040411 File Offset: 0x0003E611
	private void Awake()
	{
		this.mInput = base.GetComponent<UIInput>();
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0004041F File Offset: 0x0003E61F
	private void OnGUI()
	{
		if (Event.current.rawType == EventType.KeyDown)
		{
			this.mInput.ProcessEvent(Event.current);
		}
	}

	// Token: 0x040006FE RID: 1790
	[NonSerialized]
	private UIInput mInput;
}
