using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06000151 RID: 337 RVA: 0x000138F7 File Offset: 0x00011AF7
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00013905 File Offset: 0x00011B05
	private void Start()
	{
		this.mStarted = true;
		this.Refresh();
		EventDelegate.Add(this.mList.onChange, delegate()
		{
			Localization.language = UIPopupList.current.value;
		});
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00013944 File Offset: 0x00011B44
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Refresh();
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00013954 File Offset: 0x00011B54
	public void Refresh()
	{
		if (this.mList != null && Localization.knownLanguages != null)
		{
			this.mList.Clear();
			int i = 0;
			int num = Localization.knownLanguages.Length;
			while (i < num)
			{
				this.mList.items.Add(Localization.knownLanguages[i]);
				i++;
			}
			this.mList.value = Localization.language;
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000139BC File Offset: 0x00011BBC
	private void OnLocalize()
	{
		this.Refresh();
	}

	// Token: 0x040002E6 RID: 742
	private UIPopupList mList;

	// Token: 0x040002E7 RID: 743
	private bool mStarted;
}
