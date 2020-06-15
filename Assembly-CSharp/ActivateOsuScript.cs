using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class ActivateOsuScript : MonoBehaviour
{
	// Token: 0x060018B1 RID: 6321 RVA: 0x000E314C File Offset: 0x000E134C
	private void Start()
	{
		this.OsuScripts = this.Osu.GetComponents<OsuScript>();
		this.OriginalMouseRotation = this.Mouse.transform.eulerAngles;
		this.OriginalMousePosition = this.Mouse.transform.position;
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x000E318C File Offset: 0x000E138C
	private void Update()
	{
		if (this.Student == null)
		{
			this.Student = this.StudentManager.Students[this.PlayerID];
			return;
		}
		if (!this.Osu.activeInHierarchy)
		{
			if (Vector3.Distance(base.transform.position, this.Student.transform.position) < 0.1f && this.Student.Routine && this.Student.CurrentDestination == this.Student.Destinations[this.Student.Phase] && this.Student.Actions[this.Student.Phase] == StudentActionType.Gaming)
			{
				this.ActivateOsu();
				return;
			}
		}
		else
		{
			this.Mouse.transform.eulerAngles = this.OriginalMouseRotation;
			if (!this.Student.Routine || this.Student.CurrentDestination != this.Student.Destinations[this.Student.Phase] || this.Student.Actions[this.Student.Phase] != StudentActionType.Gaming)
			{
				this.DeactivateOsu();
			}
		}
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x000E32C4 File Offset: 0x000E14C4
	private void ActivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(true);
		this.Student.SmartPhone.SetActive(false);
		this.Music.SetActive(true);
		this.Mouse.parent = this.Student.RightHand;
		this.Mouse.transform.localPosition = Vector3.zero;
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x000E3334 File Offset: 0x000E1534
	private void DeactivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(false);
		this.Music.SetActive(false);
		OsuScript[] osuScripts = this.OsuScripts;
		for (int i = 0; i < osuScripts.Length; i++)
		{
			osuScripts[i].Timer = 0f;
		}
		this.Mouse.parent = base.transform.parent;
		this.Mouse.transform.position = this.OriginalMousePosition;
	}

	// Token: 0x04002478 RID: 9336
	public StudentManagerScript StudentManager;

	// Token: 0x04002479 RID: 9337
	public OsuScript[] OsuScripts;

	// Token: 0x0400247A RID: 9338
	public StudentScript Student;

	// Token: 0x0400247B RID: 9339
	public ClockScript Clock;

	// Token: 0x0400247C RID: 9340
	public GameObject Music;

	// Token: 0x0400247D RID: 9341
	public Transform Mouse;

	// Token: 0x0400247E RID: 9342
	public GameObject Osu;

	// Token: 0x0400247F RID: 9343
	public int PlayerID;

	// Token: 0x04002480 RID: 9344
	public Vector3 OriginalMousePosition;

	// Token: 0x04002481 RID: 9345
	public Vector3 OriginalMouseRotation;
}
