using System;
using UnityEngine;

// Token: 0x0200042A RID: 1066
public class TitleSaveFilesScript : MonoBehaviour
{
	// Token: 0x06001C63 RID: 7267 RVA: 0x00154FF6 File Offset: 0x001531F6
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.UpdateHighlight();
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x00155034 File Offset: 0x00153234
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			return;
		}
		base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		if (!this.ConfirmationWindow.activeInHierarchy)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				if (this.ID > 3)
				{
					this.ID = 1;
				}
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				if (this.ID < 1)
				{
					this.ID = 3;
				}
				this.UpdateHighlight();
			}
		}
		if (base.transform.localPosition.x < 50f)
		{
			if (!this.ConfirmationWindow.activeInHierarchy)
			{
				if (Input.GetButtonDown("A"))
				{
					GameGlobals.Profile = this.ID;
					Globals.DeleteAll();
					GameGlobals.Profile = this.ID;
					this.Menu.FadeOut = true;
					this.Menu.Fading = true;
					return;
				}
				if (Input.GetButtonDown("X"))
				{
					this.ConfirmationWindow.SetActive(true);
					return;
				}
			}
			else
			{
				if (Input.GetButtonDown("A"))
				{
					PlayerPrefs.SetInt("ProfileCreated_" + this.ID, 0);
					this.ConfirmationWindow.SetActive(false);
					this.SaveDatas[this.ID].Start();
					return;
				}
				if (Input.GetButtonDown("B"))
				{
					this.ConfirmationWindow.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x00155241 File Offset: 0x00153441
	private void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(0f, 700f - 350f * (float)this.ID, 0f);
	}

	// Token: 0x04003573 RID: 13683
	public InputManagerScript InputManager;

	// Token: 0x04003574 RID: 13684
	public TitleSaveDataScript[] SaveDatas;

	// Token: 0x04003575 RID: 13685
	public GameObject ConfirmationWindow;

	// Token: 0x04003576 RID: 13686
	public TitleMenuScript Menu;

	// Token: 0x04003577 RID: 13687
	public Transform Highlight;

	// Token: 0x04003578 RID: 13688
	public bool Show;

	// Token: 0x04003579 RID: 13689
	public int ID = 1;
}
