using System;
using Bayat.SaveSystem;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class StalkerYandereScript : MonoBehaviour
{
	// Token: 0x06001AF7 RID: 6903 RVA: 0x0010FA76 File Offset: 0x0010DC76
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x0010FA84 File Offset: 0x0010DC84
	private void Update()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (Input.GetKeyDown("=") && Time.timeScale < 10f)
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown("-") && Time.timeScale > 1f)
		{
			Time.timeScale -= 1f;
		}
		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money += 1f;
			if (this.Jukebox != null)
			{
				if (this.Jukebox.isPlaying)
				{
					this.Jukebox.Stop();
				}
				else
				{
					this.Jukebox.Play();
				}
			}
		}
		if (this.CanMove)
		{
			if (this.CameraTarget != null)
			{
				this.CameraTarget.localPosition = new Vector3(0f, 1f + (this.RPGCamera.distanceMax - this.RPGCamera.distance) * 0.2f, 0f);
			}
			this.UpdateMovement();
		}
		else if (this.CameraTarget != null && this.Climbing)
		{
			if (this.ClimbPhase == 1)
			{
				if (this.MyAnimation["f02_climbTrellis_00"].time < this.MyAnimation["f02_climbTrellis_00"].length - 1f)
				{
					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, this.Hips.position + new Vector3(0f, 0.103729f, 0.003539f), Time.deltaTime);
				}
				else
				{
					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, new Vector3(-9.5f, 5f, -2.5f), Time.deltaTime);
				}
				this.MoveTowardsTarget(this.TrellisClimbSpot.position);
				this.SpinTowardsTarget(this.TrellisClimbSpot.rotation);
				if (this.MyAnimation["f02_climbTrellis_00"].time > 7.5f)
				{
					this.RPGCamera.transform.position = this.EntryPOV.position;
					this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
					this.RPGCamera.enabled = false;
					RenderSettings.ambientIntensity = 8f;
					this.ClimbPhase++;
				}
			}
			else
			{
				this.RPGCamera.transform.position = this.EntryPOV.position;
				this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
				if (this.MyAnimation["f02_climbTrellis_00"].time > 11f)
				{
					base.transform.position = Vector3.MoveTowards(base.transform.position, this.TrellisClimbSpot.position + new Vector3(0.4f, 0f, 0f), Time.deltaTime * 0.5f);
				}
			}
			if (this.MyAnimation["f02_climbTrellis_00"].time > this.MyAnimation["f02_climbTrellis_00"].length)
			{
				this.MyAnimation.Play("f02_idleShort_00");
				base.transform.position = new Vector3(-9.1f, 4f, -2.5f);
				this.CameraTarget.position = base.transform.position + new Vector3(0f, 1f, 0f);
				this.RPGCamera.enabled = true;
				this.Climbing = false;
				this.CanMove = true;
				Physics.SyncTransforms();
			}
		}
		if (this.Street && base.transform.position.x < -16f)
		{
			base.transform.position = new Vector3(-16f, 0f, base.transform.position.z);
		}
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x0010FEA0 File Offset: 0x0010E0A0
	private void UpdateMovement()
	{
		if (!OptionGlobals.ToggleRun)
		{
			this.Running = false;
			if (Input.GetButton("LB"))
			{
				this.Running = true;
			}
		}
		else if (Input.GetButtonDown("LB"))
		{
			this.Running = !this.Running;
		}
		this.MyController.Move(Physics.gravity * Time.deltaTime);
		float axis = Input.GetAxis("Vertical");
		float axis2 = Input.GetAxis("Horizontal");
		Vector3 vector = this.MainCamera.transform.TransformDirection(Vector3.forward);
		vector.y = 0f;
		vector = vector.normalized;
		Vector3 a = new Vector3(vector.z, 0f, -vector.x);
		Vector3 vector2 = axis2 * a + axis * vector;
		Quaternion b = Quaternion.identity;
		if (vector2 != Vector3.zero)
		{
			b = Quaternion.LookRotation(vector2);
		}
		if (vector2 != Vector3.zero)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 10f);
		}
		else
		{
			b = new Quaternion(0f, 0f, 0f, 0f);
		}
		if (!this.Street)
		{
			if (this.Stance.Current == StanceType.Standing)
			{
				if (Input.GetButtonDown("RS"))
				{
					this.Stance.Current = StanceType.Crouching;
					this.MyController.center = new Vector3(0f, 0.5f, 0f);
					this.MyController.height = 1f;
				}
			}
			else if (Input.GetButtonDown("RS"))
			{
				this.Stance.Current = StanceType.Standing;
				this.MyController.center = new Vector3(0f, 0.75f, 0f);
				this.MyController.height = 1.5f;
			}
		}
		if (axis != 0f || axis2 != 0f)
		{
			if (this.Running)
			{
				if (this.Stance.Current == StanceType.Crouching)
				{
					this.MyAnimation.CrossFade(this.CrouchRunAnim);
					this.MyController.Move(base.transform.forward * this.CrouchRunSpeed * Time.deltaTime);
					return;
				}
				this.MyAnimation.CrossFade(this.RunAnim);
				this.MyController.Move(base.transform.forward * this.RunSpeed * Time.deltaTime);
				return;
			}
			else
			{
				if (this.Stance.Current == StanceType.Crouching)
				{
					this.MyAnimation.CrossFade(this.CrouchWalkAnim);
					this.MyController.Move(base.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime));
					return;
				}
				this.MyAnimation.CrossFade(this.WalkAnim);
				this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime));
				return;
			}
		}
		else
		{
			if (this.Stance.Current == StanceType.Crouching)
			{
				this.MyAnimation.CrossFade(this.CrouchIdleAnim);
				return;
			}
			this.MyAnimation.CrossFade(this.IdleAnim);
			return;
		}
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x001101EC File Offset: 0x0010E3EC
	private void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		this.MyController.Move(a * (Time.deltaTime * 10f));
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x0010E5F4 File Offset: 0x0010C7F4
	private void SpinTowardsTarget(Quaternion target)
	{
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, target, Time.deltaTime * 10f);
	}

	// Token: 0x04002BCE RID: 11214
	public CharacterController MyController;

	// Token: 0x04002BCF RID: 11215
	public AutoSaveManager SaveManager;

	// Token: 0x04002BD0 RID: 11216
	public Transform TrellisClimbSpot;

	// Token: 0x04002BD1 RID: 11217
	public Transform CameraTarget;

	// Token: 0x04002BD2 RID: 11218
	public Transform EntryPOV;

	// Token: 0x04002BD3 RID: 11219
	public Transform Hips;

	// Token: 0x04002BD4 RID: 11220
	public RPG_Camera RPGCamera;

	// Token: 0x04002BD5 RID: 11221
	public Animation MyAnimation;

	// Token: 0x04002BD6 RID: 11222
	public AudioSource Jukebox;

	// Token: 0x04002BD7 RID: 11223
	public Camera MainCamera;

	// Token: 0x04002BD8 RID: 11224
	public bool Climbing;

	// Token: 0x04002BD9 RID: 11225
	public bool Running;

	// Token: 0x04002BDA RID: 11226
	public bool CanMove;

	// Token: 0x04002BDB RID: 11227
	public bool Street;

	// Token: 0x04002BDC RID: 11228
	public Stance Stance = new Stance(StanceType.Standing);

	// Token: 0x04002BDD RID: 11229
	public string IdleAnim;

	// Token: 0x04002BDE RID: 11230
	public string WalkAnim;

	// Token: 0x04002BDF RID: 11231
	public string RunAnim;

	// Token: 0x04002BE0 RID: 11232
	public string CrouchIdleAnim;

	// Token: 0x04002BE1 RID: 11233
	public string CrouchWalkAnim;

	// Token: 0x04002BE2 RID: 11234
	public string CrouchRunAnim;

	// Token: 0x04002BE3 RID: 11235
	public float WalkSpeed;

	// Token: 0x04002BE4 RID: 11236
	public float RunSpeed;

	// Token: 0x04002BE5 RID: 11237
	public float CrouchWalkSpeed;

	// Token: 0x04002BE6 RID: 11238
	public float CrouchRunSpeed;

	// Token: 0x04002BE7 RID: 11239
	public int ClimbPhase;

	// Token: 0x04002BE8 RID: 11240
	public int Frame;
}
