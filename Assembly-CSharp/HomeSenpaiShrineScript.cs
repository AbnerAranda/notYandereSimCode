using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class HomeSenpaiShrineScript : MonoBehaviour
{
	// Token: 0x06001759 RID: 5977 RVA: 0x000CA064 File Offset: 0x000C8264
	private void Start()
	{
		this.UpdateText(this.GetCurrentIndex());
		for (int i = 1; i < 11; i++)
		{
			if (PlayerGlobals.GetShrineCollectible(i))
			{
				this.Collectibles[i].SetActive(true);
			}
		}
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000CA0A0 File Offset: 0x000C82A0
	private bool InUpperHalf()
	{
		return this.Y < 2;
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000CA0AB File Offset: 0x000C82AB
	private int GetCurrentIndex()
	{
		if (this.InUpperHalf())
		{
			return this.Y;
		}
		return 2 + (this.X + (this.Y - 2) * this.Columns);
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000CA0D4 File Offset: 0x000C82D4
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.PauseScreen.Show)
		{
			if (this.HomeCamera.ID == 6)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 135f, Time.deltaTime * 10f);
				this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
				this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, -this.Rotation, this.LeftDoor.localEulerAngles.z);
				if (this.InputManager.TappedUp)
				{
					this.Y = ((this.Y > 0) ? (this.Y - 1) : (this.Rows - 1));
				}
				if (this.InputManager.TappedDown)
				{
					this.Y = ((this.Y < this.Rows - 1) ? (this.Y + 1) : 0);
				}
				if (this.InputManager.TappedRight && !this.InUpperHalf())
				{
					this.X = ((this.X < this.Columns - 1) ? (this.X + 1) : 0);
				}
				if (this.InputManager.TappedLeft && !this.InUpperHalf())
				{
					this.X = ((this.X > 0) ? (this.X - 1) : (this.Columns - 1));
				}
				if (this.InUpperHalf())
				{
					this.X = 1;
				}
				int currentIndex = this.GetCurrentIndex();
				this.HomeCamera.Destination = this.Destinations[currentIndex];
				this.HomeCamera.Target = this.Targets[currentIndex];
				if (this.InputManager.TappedUp || this.InputManager.TappedDown || this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.UpdateText(currentIndex - 1);
				}
				if (Input.GetButtonDown("B"))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeYandere.gameObject.SetActive(true);
					this.HomeWindow.Show = false;
					return;
				}
			}
		}
		else
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
			this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, this.Rotation, this.LeftDoor.localEulerAngles.z);
		}
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000CA3DC File Offset: 0x000C85DC
	private void UpdateText(int newIndex)
	{
		if (newIndex == -1)
		{
			newIndex = 10;
		}
		if (newIndex == 0)
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
			return;
		}
		if (PlayerGlobals.GetShrineCollectible(newIndex))
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
			return;
		}
		this.NameLabel.text = "???";
		this.DescLabel.text = "I'd like to find something that Senpai touched and keep it here...";
	}

	// Token: 0x04002062 RID: 8290
	public InputManagerScript InputManager;

	// Token: 0x04002063 RID: 8291
	public PauseScreenScript PauseScreen;

	// Token: 0x04002064 RID: 8292
	public HomeYandereScript HomeYandere;

	// Token: 0x04002065 RID: 8293
	public HomeCameraScript HomeCamera;

	// Token: 0x04002066 RID: 8294
	public HomeWindowScript HomeWindow;

	// Token: 0x04002067 RID: 8295
	public GameObject[] Collectibles;

	// Token: 0x04002068 RID: 8296
	public Transform[] Destinations;

	// Token: 0x04002069 RID: 8297
	public Transform[] Targets;

	// Token: 0x0400206A RID: 8298
	public Transform RightDoor;

	// Token: 0x0400206B RID: 8299
	public Transform LeftDoor;

	// Token: 0x0400206C RID: 8300
	public UILabel NameLabel;

	// Token: 0x0400206D RID: 8301
	public UILabel DescLabel;

	// Token: 0x0400206E RID: 8302
	public string[] Names;

	// Token: 0x0400206F RID: 8303
	public string[] Descs;

	// Token: 0x04002070 RID: 8304
	public float Rotation;

	// Token: 0x04002071 RID: 8305
	private int Rows = 5;

	// Token: 0x04002072 RID: 8306
	private int Columns = 3;

	// Token: 0x04002073 RID: 8307
	private int X = 1;

	// Token: 0x04002074 RID: 8308
	private int Y = 3;
}
