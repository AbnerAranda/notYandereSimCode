using System;
using UnityEngine;

// Token: 0x02000431 RID: 1073
public class TranqCaseScript : MonoBehaviour
{
	// Token: 0x06001C80 RID: 7296 RVA: 0x00156255 File Offset: 0x00154455
	private void Start()
	{
		this.Prompt.enabled = false;
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x00156264 File Offset: 0x00154464
	private void Update()
	{
		if (this.Yandere.transform.position.x > base.transform.position.x && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
		{
			if (this.Yandere.Dragging)
			{
				if (this.Ragdoll == null)
				{
					this.Ragdoll = this.Yandere.Ragdoll.GetComponent<RagdollScript>();
				}
				if (this.Ragdoll.Tranquil)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.enabled = true;
					}
				}
				else if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Prompt.enabled && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TranquilHiding = true;
				this.Yandere.CanMove = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				this.Ragdoll.TranqCase = this;
				this.VictimClubType = this.Ragdoll.Student.Club;
				this.VictimID = this.Ragdoll.StudentID;
				this.Door.Prompt.enabled = true;
				this.Door.enabled = true;
				this.Occupied = true;
				this.Animate = true;
				this.Open = true;
			}
		}
		if (this.Animate)
		{
			if (this.Open)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 105f, Time.deltaTime * 10f);
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairL.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairL.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairR.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairR.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				if (this.Rotation < 1f)
				{
					this.Animate = false;
					this.Rotation = 0f;
				}
			}
			this.Hinge.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		}
	}

	// Token: 0x040035AA RID: 13738
	public YandereScript Yandere;

	// Token: 0x040035AB RID: 13739
	public RagdollScript Ragdoll;

	// Token: 0x040035AC RID: 13740
	public PromptScript Prompt;

	// Token: 0x040035AD RID: 13741
	public DoorScript Door;

	// Token: 0x040035AE RID: 13742
	public Transform Hinge;

	// Token: 0x040035AF RID: 13743
	public bool Occupied;

	// Token: 0x040035B0 RID: 13744
	public bool Open;

	// Token: 0x040035B1 RID: 13745
	public int VictimID;

	// Token: 0x040035B2 RID: 13746
	public ClubType VictimClubType;

	// Token: 0x040035B3 RID: 13747
	public float Rotation;

	// Token: 0x040035B4 RID: 13748
	public bool Animate;
}
