using System;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class PickUpScript : MonoBehaviour
{
	// Token: 0x0600190C RID: 6412 RVA: 0x000EA930 File Offset: 0x000E8B30
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.Clock = GameObject.Find("Clock").GetComponent<ClockScript>();
		if (!this.CanCollide)
		{
			Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
		}
		if (this.Outline.Length != 0)
		{
			this.OriginalColor = this.Outline[0].color;
		}
		this.OriginalScale = base.transform.localScale;
		if (this.MyRigidbody == null)
		{
			this.MyRigidbody = base.GetComponent<Rigidbody>();
		}
		if (this.DisableAtStart)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x000EA9E0 File Offset: 0x000E8BE0
	private void LateUpdate()
	{
		if (this.CleaningProduct)
		{
			if (this.Clock.Period == 5)
			{
				this.Suspicious = false;
			}
			else
			{
				this.Suspicious = true;
			}
		}
		if (this.Weight)
		{
			if (this.Period < this.Clock.Period)
			{
				this.Strength = ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus;
				this.Period++;
			}
			if (this.Strength == 0)
			{
				this.Prompt.Label[3].text = "     Physical Stat Too Low";
				this.Prompt.Circle[3].fillAmount = 1f;
			}
			else
			{
				this.Prompt.Label[3].text = "     Carry";
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Circle[3].fillAmount = 1f;
			if (this.Weight)
			{
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Yandere.PickUp != null)
					{
						this.Yandere.CharacterAnimation[this.Yandere.CarryAnims[this.Yandere.PickUp.CarryAnimID]].weight = 0f;
					}
					if (this.Yandere.Armed)
					{
						this.Yandere.CharacterAnimation[this.Yandere.ArmedAnims[this.Yandere.EquippedWeapon.AnimID]].weight = 0f;
					}
					this.Yandere.targetRotation = Quaternion.LookRotation(new Vector3(base.transform.position.x, this.Yandere.transform.position.y, base.transform.position.z) - this.Yandere.transform.position);
					this.Yandere.transform.rotation = this.Yandere.targetRotation;
					this.Yandere.EmptyHands();
					base.transform.parent = this.Yandere.transform;
					base.transform.localPosition = new Vector3(0f, 0f, 0.79184f);
					base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.Yandere.Character.GetComponent<Animation>().Play("f02_heavyWeightLift_00");
					this.Yandere.HeavyWeight = true;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;
					this.MyAnimation.Play("Weight_liftUp_00");
					this.MyRigidbody.isKinematic = true;
					this.BeingLifted = true;
				}
			}
			else
			{
				this.BePickedUp();
			}
		}
		if (this.Yandere.PickUp == this)
		{
			base.transform.localPosition = this.HoldPosition;
			base.transform.localEulerAngles = this.HoldRotation;
			if (this.Garbage && !this.Yandere.StudentManager.IncineratorArea.bounds.Contains(this.Yandere.transform.position))
			{
				this.Drop();
				base.transform.position = new Vector3(-40f, 0f, 24f);
			}
		}
		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;
			if (this.DumpTimer > 1f)
			{
				if (this.Clothing)
				{
					this.Yandere.Incinerator.BloodyClothing++;
				}
				else if (this.BodyPart)
				{
					this.Yandere.Incinerator.BodyParts++;
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (this.Yandere.PickUp != this && !this.MyRigidbody.isKinematic)
		{
			this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5f, Time.deltaTime);
			if (this.KinematicTimer == 5f)
			{
				this.MyRigidbody.isKinematic = true;
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -71f && base.transform.position.x < -61f && base.transform.position.z > -37.5f && base.transform.position.z < -27.5f)
			{
				base.transform.position = new Vector3(-63f, 1f, -26.5f);
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -46f && base.transform.position.x < -18f && base.transform.position.z > 66f && base.transform.position.z < 78f)
			{
				base.transform.position = new Vector3(-16f, 5f, 72f);
				this.KinematicTimer = 0f;
			}
		}
		if (this.Weight && this.BeingLifted)
		{
			if (this.Yandere.Lifting)
			{
				if (this.Yandere.StudentManager.Stop)
				{
					this.Drop();
					return;
				}
			}
			else
			{
				this.BePickedUp();
			}
		}
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x000EAF84 File Offset: 0x000E9184
	public void BePickedUp()
	{
		if (this.Radio && SchemeGlobals.GetSchemeStage(5) == 2)
		{
			SchemeGlobals.SetSchemeStage(5, 3);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.Salty && SchemeGlobals.GetSchemeStage(4) == 4)
		{
			SchemeGlobals.SetSchemeStage(4, 5);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.OpenBook;
			this.Yandere.LifeNotePen.SetActive(true);
		}
		if (this.MyAnimation != null)
		{
			this.MyAnimation.Stop();
		}
		this.Prompt.Circle[3].fillAmount = 1f;
		this.BeingLifted = false;
		if (this.Yandere.PickUp != null)
		{
			this.Yandere.PickUp.Drop();
		}
		if (this.Yandere.Equipped == 3)
		{
			this.Yandere.Weapon[3].Drop();
		}
		else if (this.Yandere.Equipped > 0)
		{
			this.Yandere.Unequip();
		}
		if (this.Yandere.Dragging)
		{
			this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}
		if (this.Yandere.Carrying)
		{
			this.Yandere.StopCarrying();
		}
		if (!this.LeftHand)
		{
			base.transform.parent = this.Yandere.ItemParent;
		}
		else
		{
			base.transform.parent = this.Yandere.LeftItemParent;
		}
		if (base.GetComponent<RadioScript>() != null && base.GetComponent<RadioScript>().On)
		{
			base.GetComponent<RadioScript>().TurnOff();
		}
		this.MyCollider.enabled = false;
		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		if (!this.Usable)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.NearestPrompt = null;
		}
		else
		{
			this.Prompt.Carried = true;
		}
		this.Yandere.PickUp = this;
		this.Yandere.CarryAnimID = this.CarryAnimID;
		OutlineScript[] outline = this.Outline;
		for (int i = 0; i < outline.Length; i++)
		{
			outline[i].color = new Color(0f, 0f, 0f, 1f);
		}
		if (this.BodyPart)
		{
			this.Yandere.NearBodies++;
		}
		this.Yandere.StudentManager.UpdateStudents(0);
		this.MyRigidbody.isKinematic = true;
		this.KinematicTimer = 0f;
	}

	// Token: 0x0600190F RID: 6415 RVA: 0x000EB240 File Offset: 0x000E9440
	public void Drop()
	{
		if (this.Salty && SchemeGlobals.GetSchemeStage(4) == 5)
		{
			SchemeGlobals.SetSchemeStage(4, 4);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.TrashCan)
		{
			this.Yandere.MyController.radius = 0.2f;
		}
		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.ClosedBook;
			this.Yandere.LifeNotePen.SetActive(false);
		}
		if (this.Weight)
		{
			this.Yandere.IdleAnim = this.Yandere.OriginalIdleAnim;
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = this.Yandere.OriginalRunAnim;
		}
		if (this.BloodCleaner != null)
		{
			this.BloodCleaner.enabled = true;
			this.BloodCleaner.Pathfinding.enabled = true;
		}
		this.Yandere.PickUp = null;
		if (this.BodyPart)
		{
			base.transform.parent = this.Yandere.LimbParent;
		}
		else
		{
			base.transform.parent = null;
		}
		if (this.LockRotation)
		{
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		}
		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = this.OriginalConstraints;
			this.MyRigidbody.isKinematic = false;
			this.MyRigidbody.useGravity = true;
		}
		if (this.Dumped)
		{
			base.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;
			this.MyCollider.isTrigger = false;
			if (!this.CanCollide)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}
		this.Prompt.Carried = false;
		OutlineScript[] outline = this.Outline;
		for (int i = 0; i < outline.Length; i++)
		{
			outline[i].color = (this.Evidence ? this.EvidenceColor : this.OriginalColor);
		}
		base.transform.localScale = this.OriginalScale;
		if (this.BodyPart)
		{
			this.Yandere.NearBodies--;
		}
		this.Yandere.StudentManager.UpdateStudents(0);
		if (this.Clothing && this.Evidence)
		{
			base.transform.parent = this.Yandere.Police.BloodParent;
		}
	}

	// Token: 0x04002586 RID: 9606
	public RigidbodyConstraints OriginalConstraints;

	// Token: 0x04002587 RID: 9607
	public BloodCleanerScript BloodCleaner;

	// Token: 0x04002588 RID: 9608
	public IncineratorScript Incinerator;

	// Token: 0x04002589 RID: 9609
	public BodyPartScript BodyPart;

	// Token: 0x0400258A RID: 9610
	public TrashCanScript TrashCan;

	// Token: 0x0400258B RID: 9611
	public OutlineScript[] Outline;

	// Token: 0x0400258C RID: 9612
	public YandereScript Yandere;

	// Token: 0x0400258D RID: 9613
	public Animation MyAnimation;

	// Token: 0x0400258E RID: 9614
	public BucketScript Bucket;

	// Token: 0x0400258F RID: 9615
	public PromptScript Prompt;

	// Token: 0x04002590 RID: 9616
	public ClockScript Clock;

	// Token: 0x04002591 RID: 9617
	public MopScript Mop;

	// Token: 0x04002592 RID: 9618
	public Mesh ClosedBook;

	// Token: 0x04002593 RID: 9619
	public Mesh OpenBook;

	// Token: 0x04002594 RID: 9620
	public Rigidbody MyRigidbody;

	// Token: 0x04002595 RID: 9621
	public Collider MyCollider;

	// Token: 0x04002596 RID: 9622
	public MeshFilter MyRenderer;

	// Token: 0x04002597 RID: 9623
	public Vector3 TrashPosition;

	// Token: 0x04002598 RID: 9624
	public Vector3 TrashRotation;

	// Token: 0x04002599 RID: 9625
	public Vector3 OriginalScale;

	// Token: 0x0400259A RID: 9626
	public Vector3 HoldPosition;

	// Token: 0x0400259B RID: 9627
	public Vector3 HoldRotation;

	// Token: 0x0400259C RID: 9628
	public Color EvidenceColor;

	// Token: 0x0400259D RID: 9629
	public Color OriginalColor;

	// Token: 0x0400259E RID: 9630
	public bool CleaningProduct;

	// Token: 0x0400259F RID: 9631
	public bool DisableAtStart;

	// Token: 0x040025A0 RID: 9632
	public bool LockRotation;

	// Token: 0x040025A1 RID: 9633
	public bool BeingLifted;

	// Token: 0x040025A2 RID: 9634
	public bool CanCollide;

	// Token: 0x040025A3 RID: 9635
	public bool Electronic;

	// Token: 0x040025A4 RID: 9636
	public bool Flashlight;

	// Token: 0x040025A5 RID: 9637
	public bool PuzzleCube;

	// Token: 0x040025A6 RID: 9638
	public bool Suspicious;

	// Token: 0x040025A7 RID: 9639
	public bool Blowtorch;

	// Token: 0x040025A8 RID: 9640
	public bool Clothing;

	// Token: 0x040025A9 RID: 9641
	public bool Evidence;

	// Token: 0x040025AA RID: 9642
	public bool JerryCan;

	// Token: 0x040025AB RID: 9643
	public bool LeftHand;

	// Token: 0x040025AC RID: 9644
	public bool RedPaint;

	// Token: 0x040025AD RID: 9645
	public bool Garbage;

	// Token: 0x040025AE RID: 9646
	public bool Bleach;

	// Token: 0x040025AF RID: 9647
	public bool Dumped;

	// Token: 0x040025B0 RID: 9648
	public bool Usable;

	// Token: 0x040025B1 RID: 9649
	public bool Weight;

	// Token: 0x040025B2 RID: 9650
	public bool Radio;

	// Token: 0x040025B3 RID: 9651
	public bool Salty;

	// Token: 0x040025B4 RID: 9652
	public int CarryAnimID;

	// Token: 0x040025B5 RID: 9653
	public int Strength;

	// Token: 0x040025B6 RID: 9654
	public int Period;

	// Token: 0x040025B7 RID: 9655
	public int Food;

	// Token: 0x040025B8 RID: 9656
	public float KinematicTimer;

	// Token: 0x040025B9 RID: 9657
	public float DumpTimer;

	// Token: 0x040025BA RID: 9658
	public bool Empty = true;

	// Token: 0x040025BB RID: 9659
	public GameObject[] FoodPieces;

	// Token: 0x040025BC RID: 9660
	public WeaponScript StuckBoxCutter;
}
