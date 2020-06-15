using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class BodyHidingLockerScript : MonoBehaviour
{
	// Token: 0x06000A6B RID: 2667 RVA: 0x000563C8 File Offset: 0x000545C8
	private void Update()
	{
		if (this.Rotation != 0f)
		{
			this.Speed += Time.deltaTime * 100f;
			this.Rotation = Mathf.MoveTowards(this.Rotation, 0f, this.Speed * Time.deltaTime);
			if (this.Rotation > -1f)
			{
				AudioSource.PlayClipAtPoint(this.LockerClose, this.Prompt.Yandere.MainCamera.transform.position);
				this.Corpse.gameObject.SetActive(false);
				base.enabled = false;
				this.Rotation = 0f;
				this.Speed = 0f;
			}
			this.Door.transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
		}
		if (this.Prompt.Yandere.Carrying || this.Prompt.Yandere.Dragging)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				AudioSource.PlayClipAtPoint(this.LockerOpen, this.Prompt.Yandere.MainCamera.transform.position);
				if (this.Prompt.Yandere.Carrying)
				{
					this.Corpse = this.Prompt.Yandere.CurrentRagdoll;
				}
				else
				{
					this.Corpse = this.Prompt.Yandere.Ragdoll.GetComponent<RagdollScript>();
				}
				this.Prompt.Yandere.EmptyHands();
				this.Prompt.Yandere.NearBodies = 0;
				this.Prompt.Yandere.NearestCorpseID = 0;
				this.Prompt.Yandere.CorpseWarning = false;
				this.Prompt.Yandere.StudentManager.UpdateStudents(0);
				this.Corpse.Student.CharacterAnimation.Play("f02_lockerPose_00");
				this.Corpse.transform.parent = base.transform;
				this.Corpse.transform.position = base.transform.position + new Vector3(0f, 0.1f, 0f);
				this.Corpse.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
				this.Corpse.DisableRigidbodies();
				this.Corpse.enabled = false;
				this.Corpse.Hidden = true;
				this.Rotation = -180f;
				return;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04000AD0 RID: 2768
	public RagdollScript Corpse;

	// Token: 0x04000AD1 RID: 2769
	public PromptScript Prompt;

	// Token: 0x04000AD2 RID: 2770
	public AudioClip LockerClose;

	// Token: 0x04000AD3 RID: 2771
	public AudioClip LockerOpen;

	// Token: 0x04000AD4 RID: 2772
	public float Rotation;

	// Token: 0x04000AD5 RID: 2773
	public float Speed;

	// Token: 0x04000AD6 RID: 2774
	public Transform Door;
}
