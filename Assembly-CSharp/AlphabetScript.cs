using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000C2 RID: 194
public class AlphabetScript : MonoBehaviour
{
	// Token: 0x060009EC RID: 2540 RVA: 0x0004E224 File Offset: 0x0004C424
	private void Start()
	{
		if (GameGlobals.AlphabetMode)
		{
			this.TargetLabel.transform.parent.gameObject.SetActive(true);
			this.StudentManager.Yandere.NoDebug = true;
			this.BodyHidingLockers.SetActive(true);
			this.AmnesiaBombBox.SetActive(true);
			this.SmokeBombBox.SetActive(true);
			this.StinkBombBox.SetActive(true);
			this.SuperRobot.SetActive(true);
			this.PuzzleCube.SetActive(true);
			this.WeaponBag.SetActive(true);
			ClassGlobals.PhysicalGrade = 5;
			this.UpdateText();
			return;
		}
		this.TargetLabel.transform.parent.gameObject.SetActive(false);
		this.BombLabel.transform.parent.gameObject.SetActive(false);
		UnityEngine.Object.Destroy(this.Music[1].transform.parent.gameObject);
		base.gameObject.SetActive(false);
		base.enabled = false;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0004E330 File Offset: 0x0004C530
	private void Update()
	{
		if (this.CurrentTarget < this.IDs.Length)
		{
			if (this.StudentManager.Yandere.CanMove && (Input.GetButtonDown("LS") || Input.GetKeyDown(KeyCode.T)))
			{
				if (this.StudentManager.Yandere.Inventory.SmokeBomb)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.SmokeBomb, this.Yandere.position, Quaternion.identity);
					this.RemainingBombs--;
					this.BombLabel.text = string.Concat(this.RemainingBombs);
					if (this.RemainingBombs == 0)
					{
						this.StudentManager.Yandere.Inventory.SmokeBomb = false;
					}
				}
				else if (this.StudentManager.Yandere.Inventory.StinkBomb)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.StinkBomb, this.Yandere.position, Quaternion.identity);
					this.RemainingBombs--;
					this.BombLabel.text = string.Concat(this.RemainingBombs);
					if (this.RemainingBombs == 0)
					{
						this.StudentManager.Yandere.Inventory.StinkBomb = false;
					}
				}
				else if (this.StudentManager.Yandere.Inventory.AmnesiaBomb)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.AmnesiaBomb, this.Yandere.position, Quaternion.identity);
					this.RemainingBombs--;
					this.BombLabel.text = string.Concat(this.RemainingBombs);
					if (this.RemainingBombs == 0)
					{
						this.StudentManager.Yandere.Inventory.AmnesiaBomb = false;
					}
				}
			}
			this.LocalArrow.LookAt(this.StudentManager.Students[this.IDs[this.CurrentTarget]].transform.position);
			base.transform.eulerAngles = this.LocalArrow.eulerAngles - new Vector3(0f, this.StudentManager.MainCamera.transform.eulerAngles.y, 0f);
			if ((this.StudentManager.Yandere.Attacking && this.StudentManager.Yandere.TargetStudent.StudentID != this.IDs[this.CurrentTarget]) || (this.StudentManager.Yandere.Struggling && this.StudentManager.Yandere.TargetStudent.StudentID != this.IDs[this.CurrentTarget]) || this.StudentManager.Police.Show)
			{
				this.ChallengeFailed.enabled = true;
			}
			if (!this.StudentManager.Students[this.IDs[this.CurrentTarget]].Alive)
			{
				this.CurrentTarget++;
				int num = this.CurrentTarget + 1;
				int num2 = this.Music.Length;
				if (this.CurrentTarget > 77)
				{
					this.TargetLabel.text = "Challenge Complete!";
					SceneManager.LoadScene("OsanaJokeScene");
				}
				else
				{
					this.UpdateText();
				}
			}
			if (this.ChallengeFailed.enabled)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 3f)
				{
					SceneManager.LoadScene("LoadingScene");
				}
			}
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0004E698 File Offset: 0x0004C898
	public void UpdateText()
	{
		this.TargetLabel.text = string.Concat(new object[]
		{
			"(",
			this.CurrentTarget,
			"/77) Current Target: ",
			this.StudentManager.JSON.Students[this.IDs[this.CurrentTarget]].Name
		});
		if (this.RemainingBombs > 0)
		{
			this.BombLabel.transform.parent.gameObject.SetActive(true);
			if (this.BombTexture.color.a < 1f)
			{
				if (this.Inventory.StinkBomb)
				{
					this.BombTexture.color = new Color(0f, 0.5f, 0f, 1f);
					return;
				}
				if (this.Inventory.AmnesiaBomb)
				{
					this.BombTexture.color = new Color(1f, 0.5f, 1f, 1f);
					return;
				}
				this.BombTexture.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}
	}

	// Token: 0x04000876 RID: 2166
	public StudentManagerScript StudentManager;

	// Token: 0x04000877 RID: 2167
	public InventoryScript Inventory;

	// Token: 0x04000878 RID: 2168
	public GameObject BodyHidingLockers;

	// Token: 0x04000879 RID: 2169
	public GameObject AmnesiaBombBox;

	// Token: 0x0400087A RID: 2170
	public GameObject SmokeBombBox;

	// Token: 0x0400087B RID: 2171
	public GameObject StinkBombBox;

	// Token: 0x0400087C RID: 2172
	public GameObject AmnesiaBomb;

	// Token: 0x0400087D RID: 2173
	public GameObject PuzzleCube;

	// Token: 0x0400087E RID: 2174
	public GameObject SuperRobot;

	// Token: 0x0400087F RID: 2175
	public GameObject SmokeBomb;

	// Token: 0x04000880 RID: 2176
	public GameObject StinkBomb;

	// Token: 0x04000881 RID: 2177
	public GameObject WeaponBag;

	// Token: 0x04000882 RID: 2178
	public UILabel ChallengeFailed;

	// Token: 0x04000883 RID: 2179
	public UILabel TargetLabel;

	// Token: 0x04000884 RID: 2180
	public UILabel BombLabel;

	// Token: 0x04000885 RID: 2181
	public UITexture BombTexture;

	// Token: 0x04000886 RID: 2182
	public Transform LocalArrow;

	// Token: 0x04000887 RID: 2183
	public Transform Yandere;

	// Token: 0x04000888 RID: 2184
	public int RemainingBombs;

	// Token: 0x04000889 RID: 2185
	public int CurrentTarget;

	// Token: 0x0400088A RID: 2186
	public float Timer;

	// Token: 0x0400088B RID: 2187
	public int[] IDs;

	// Token: 0x0400088C RID: 2188
	public AudioSource[] Music;
}
