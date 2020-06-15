using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x06000112 RID: 274 RVA: 0x000129B4 File Offset: 0x00010BB4
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		this.mInput.label.maxLineCount = 1;
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 == 0) ? "[FFFFFF]" : "[AAAAAA]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00012A44 File Offset: 0x00010C44
	public void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUIText.StripSymbols(this.mInput.value);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.value = "";
				this.mInput.isSelected = false;
			}
		}
	}

	// Token: 0x040002A7 RID: 679
	public UITextList textList;

	// Token: 0x040002A8 RID: 680
	public bool fillWithDummyData;

	// Token: 0x040002A9 RID: 681
	private UIInput mInput;
}
