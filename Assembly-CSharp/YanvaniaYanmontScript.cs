using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200048F RID: 1167
[RequireComponent(typeof(CharacterController))]
public class YanvaniaYanmontScript : MonoBehaviour
{
	// Token: 0x06001E0A RID: 7690 RVA: 0x001785B0 File Offset: 0x001767B0
	private void Awake()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["f02_yanvaniaDeath_00"].speed = 0.25f;
		component["f02_yanvaniaAttack_00"].speed = 2f;
		component["f02_yanvaniaCrouchAttack_00"].speed = 2f;
		component["f02_yanvaniaWalk_00"].speed = 0.6666667f;
		component["f02_yanvaniaWhip_Neutral"].speed = 0f;
		component["f02_yanvaniaWhip_Up"].speed = 0f;
		component["f02_yanvaniaWhip_Right"].speed = 0f;
		component["f02_yanvaniaWhip_Down"].speed = 0f;
		component["f02_yanvaniaWhip_Left"].speed = 0f;
		component["f02_yanvaniaCrouchPose_00"].layer = 1;
		component.Play("f02_yanvaniaCrouchPose_00");
		component["f02_yanvaniaCrouchPose_00"].weight = 0f;
		Physics.IgnoreLayerCollision(19, 13, true);
		Physics.IgnoreLayerCollision(19, 19, true);
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x001786CC File Offset: 0x001768CC
	private void Start()
	{
		this.WhipChain[0].transform.localScale = Vector3.zero;
		this.Character.GetComponent<Animation>().Play("f02_yanvaniaIdle_00");
		this.controller = base.GetComponent<CharacterController>();
		this.myTransform = base.transform;
		this.speed = this.walkSpeed;
		this.rayDistance = this.controller.height * 0.5f + this.controller.radius;
		this.slideLimit = this.controller.slopeLimit - 0.1f;
		this.jumpTimer = this.antiBunnyHopFactor;
		this.originalThreshold = this.fallingDamageThreshold;
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x0017877C File Offset: 0x0017697C
	private void FixedUpdate()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (this.CanMove)
		{
			if (!this.Injured)
			{
				if (!this.Cutscene)
				{
					if (this.grounded)
					{
						if (!this.Attacking)
						{
							if (!this.Crouching)
							{
								if (Input.GetAxis("VaniaHorizontal") > 0f)
								{
									this.inputX = 1f;
								}
								else if (Input.GetAxis("VaniaHorizontal") < 0f)
								{
									this.inputX = -1f;
								}
								else
								{
									this.inputX = 0f;
								}
							}
						}
						else if (this.grounded)
						{
							this.fallingDamageThreshold = 100f;
							this.moveDirection.x = 0f;
							this.inputX = 0f;
							this.speed = 0f;
						}
					}
					else if (Input.GetAxis("VaniaHorizontal") != 0f)
					{
						if (Input.GetAxis("VaniaHorizontal") > 0f)
						{
							this.inputX = 1f;
						}
						else if (Input.GetAxis("VaniaHorizontal") < 0f)
						{
							this.inputX = -1f;
						}
						else
						{
							this.inputX = 0f;
						}
					}
					else
					{
						this.inputX = Mathf.MoveTowards(this.inputX, 0f, Time.deltaTime * 10f);
					}
					float num = 0f;
					float num2 = (this.inputX != 0f && num != 0f && this.limitDiagonalSpeed) ? 0.707106769f : 1f;
					if (!this.Attacking)
					{
						if (Input.GetAxis("VaniaHorizontal") < 0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
						}
						else if (Input.GetAxis("VaniaHorizontal") > 0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, 90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(-1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
						}
					}
					if (this.grounded)
					{
						if (!this.Attacking && !this.Dangling)
						{
							if (Input.GetAxis("VaniaVertical") < -0.5f)
							{
								this.MyController.center = new Vector3(this.MyController.center.x, 0.5f, this.MyController.center.z);
								this.MyController.height = 1f;
								this.Crouching = true;
								this.IdleTimer = 10f;
								this.inputX = 0f;
							}
							if (this.Crouching)
							{
								component.CrossFade("f02_yanvaniaCrouch_00", 0.1f);
								if (!this.Attacking)
								{
									if (!this.Dangling)
									{
										if (Input.GetAxis("VaniaVertical") > -0.5f)
										{
											component["f02_yanvaniaCrouchPose_00"].weight = 0f;
											this.MyController.center = new Vector3(this.MyController.center.x, 0.75f, this.MyController.center.z);
											this.MyController.height = 1.5f;
											this.Crouching = false;
										}
									}
									else if (Input.GetAxis("VaniaVertical") > -0.5f && Input.GetButton("X"))
									{
										component["f02_yanvaniaCrouchPose_00"].weight = 0f;
										this.MyController.center = new Vector3(this.MyController.center.x, 0.75f, this.MyController.center.z);
										this.MyController.height = 1.5f;
										this.Crouching = false;
									}
								}
							}
							else if (this.inputX == 0f)
							{
								if (this.IdleTimer > 0f)
								{
									component.CrossFade("f02_yanvaniaIdle_00", 0.1f);
									component["f02_yanvaniaIdle_00"].speed = this.IdleTimer / 10f;
								}
								else
								{
									component.CrossFade("f02_yanvaniaDramaticIdle_00", 1f);
								}
								this.IdleTimer -= Time.deltaTime;
							}
							else
							{
								this.IdleTimer = 10f;
								component.CrossFade((this.speed == this.walkSpeed) ? "f02_yanvaniaWalk_00" : "f02_yanvaniaRun_00", 0.1f);
							}
						}
						bool flag = false;
						if (Physics.Raycast(this.myTransform.position, -Vector3.up, out this.hit, this.rayDistance))
						{
							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								flag = true;
							}
						}
						else
						{
							Physics.Raycast(this.contactPoint + Vector3.up, -Vector3.up, out this.hit);
							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								flag = true;
							}
						}
						if (this.falling)
						{
							this.falling = false;
							if (this.myTransform.position.y < this.fallStartLevel - this.fallingDamageThreshold)
							{
								this.FallingDamageAlert(this.fallStartLevel - this.myTransform.position.y);
							}
							this.fallingDamageThreshold = this.originalThreshold;
						}
						if (!this.toggleRun)
						{
							this.speed = (Input.GetKey(KeyCode.LeftShift) ? this.runSpeed : this.walkSpeed);
						}
						if ((flag && this.slideWhenOverSlopeLimit) || (this.slideOnTaggedObjects && this.hit.collider.tag == "Slide"))
						{
							Vector3 normal = this.hit.normal;
							this.moveDirection = new Vector3(normal.x, -normal.y, normal.z);
							Vector3.OrthoNormalize(ref normal, ref this.moveDirection);
							this.moveDirection *= this.slideSpeed;
							this.playerControl = false;
						}
						else
						{
							this.moveDirection = new Vector3(this.inputX * num2, -this.antiBumpFactor, num * num2);
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection) * this.speed;
							this.playerControl = true;
						}
						if (!Input.GetButton("VaniaJump"))
						{
							this.jumpTimer++;
						}
						else if (this.jumpTimer >= this.antiBunnyHopFactor && !this.Attacking)
						{
							this.Crouching = false;
							this.fallingDamageThreshold = 0f;
							this.moveDirection.y = this.jumpSpeed;
							this.IdleTimer = 10f;
							this.jumpTimer = 0;
							AudioSource component2 = base.GetComponent<AudioSource>();
							component2.clip = this.Voices[UnityEngine.Random.Range(0, this.Voices.Length)];
							component2.Play();
						}
					}
					else
					{
						if (!this.Attacking)
						{
							component.CrossFade((base.transform.position.y > this.PreviousY) ? "f02_yanvaniaJump_00" : "f02_yanvaniaFall_00", 0.4f);
						}
						this.PreviousY = base.transform.position.y;
						if (!this.falling)
						{
							this.falling = true;
							this.fallStartLevel = this.myTransform.position.y;
						}
						if (this.airControl && this.playerControl)
						{
							this.moveDirection.x = this.inputX * this.speed * num2;
							this.moveDirection.z = num * this.speed * num2;
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection);
						}
					}
				}
				else
				{
					this.moveDirection.x = 0f;
					if (this.grounded)
					{
						if (base.transform.position.x > -34f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34f, Time.deltaTime * this.walkSpeed), base.transform.position.y, base.transform.position.z);
							component.CrossFade("f02_yanvaniaWalk_00");
						}
						else if (base.transform.position.x < -34f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, 90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(-1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34f, Time.deltaTime * this.walkSpeed), base.transform.position.y, base.transform.position.z);
							component.CrossFade("f02_yanvaniaWalk_00");
						}
						else
						{
							component.CrossFade("f02_yanvaniaDramaticIdle_00", 1f);
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							this.WhipChain[0].transform.localScale = Vector3.zero;
							this.fallingDamageThreshold = 100f;
							this.TextBox.SetActive(true);
							this.Attacking = false;
							base.enabled = false;
						}
					}
					Physics.SyncTransforms();
				}
			}
			else
			{
				component.CrossFade("f02_damage_25");
				this.RecoveryTimer += Time.deltaTime;
				if (this.RecoveryTimer > 1f)
				{
					this.RecoveryTimer = 0f;
					this.Injured = false;
				}
			}
			this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
			this.grounded = ((this.controller.Move(this.moveDirection * Time.deltaTime) & CollisionFlags.Below) > CollisionFlags.None);
			if (this.grounded && this.EnterCutscene)
			{
				this.YanvaniaCamera.Cutscene = true;
				this.Cutscene = true;
			}
			if ((this.controller.collisionFlags & CollisionFlags.Above) != CollisionFlags.None && this.moveDirection.y > 0f)
			{
				this.moveDirection.y = 0f;
				return;
			}
		}
		else if (this.Health == 0f)
		{
			this.DeathTimer += Time.deltaTime;
			if (this.DeathTimer > 5f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime * 0.2f);
				if (this.Darkness.color.a >= 1f)
				{
					if (this.Darkness.gameObject.activeInHierarchy)
					{
						this.HealthBar.parent.gameObject.SetActive(false);
						this.EXPBar.parent.gameObject.SetActive(false);
						this.Darkness.gameObject.SetActive(false);
						this.BossHealthBar.SetActive(false);
						this.BlackBG.SetActive(true);
					}
					this.TryAgainWindow.transform.localScale = Vector3.Lerp(this.TryAgainWindow.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
		}
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x00179538 File Offset: 0x00177738
	private void Update()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (!this.Injured && this.CanMove && !this.Cutscene)
		{
			if (this.grounded)
			{
				if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.TapTimer = 0.25f;
					this.Taps++;
				}
				if (this.Taps > 1)
				{
					this.speed = this.runSpeed;
				}
			}
			if (this.inputX == 0f)
			{
				this.speed = this.walkSpeed;
			}
			this.TapTimer -= Time.deltaTime;
			if (this.TapTimer < 0f)
			{
				this.Taps = 0;
			}
			if (Input.GetButtonDown("VaniaAttack") && !this.Attacking)
			{
				AudioSource.PlayClipAtPoint(this.WhipSound, base.transform.position);
				AudioSource component2 = base.GetComponent<AudioSource>();
				component2.clip = this.Voices[UnityEngine.Random.Range(0, this.Voices.Length)];
				component2.Play();
				this.WhipChain[0].transform.localScale = Vector3.zero;
				this.Attacking = true;
				this.IdleTimer = 10f;
				if (this.Crouching)
				{
					component["f02_yanvaniaCrouchAttack_00"].time = 0f;
					component.Play("f02_yanvaniaCrouchAttack_00");
				}
				else
				{
					component["f02_yanvaniaAttack_00"].time = 0f;
					component.Play("f02_yanvaniaAttack_00");
				}
				if (this.grounded)
				{
					this.moveDirection.x = 0f;
					this.inputX = 0f;
					this.speed = 0f;
				}
			}
			if (this.Attacking)
			{
				if (!this.Dangling)
				{
					this.WhipChain[0].transform.localScale = Vector3.MoveTowards(this.WhipChain[0].transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 5f);
					this.StraightenWhip();
				}
				else
				{
					this.LoosenWhip();
					if (Input.GetAxis("VaniaHorizontal") > -0.5f && Input.GetAxis("VaniaHorizontal") < 0.5f && Input.GetAxis("VaniaVertical") > -0.5f && Input.GetAxis("VaniaVertical") < 0.5f)
					{
						component.CrossFade("f02_yanvaniaWhip_Neutral");
						if (this.Crouching)
						{
							component["f02_yanvaniaCrouchPose_00"].weight = 1f;
						}
						this.SpunUp = false;
						this.SpunDown = false;
						this.SpunRight = false;
						this.SpunLeft = false;
					}
					else
					{
						if (Input.GetAxis("VaniaVertical") > 0.5f)
						{
							if (!this.SpunUp)
							{
								AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
								this.StraightenWhip();
								this.TargetRotation = -360f;
								this.Rotation = 0f;
								this.SpunUp = true;
							}
							component.CrossFade("f02_yanvaniaWhip_Up", 0.1f);
						}
						else
						{
							this.SpunUp = false;
						}
						if (Input.GetAxis("VaniaVertical") < -0.5f)
						{
							if (!this.SpunDown)
							{
								AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
								this.StraightenWhip();
								this.TargetRotation = 360f;
								this.Rotation = 0f;
								this.SpunDown = true;
							}
							component.CrossFade("f02_yanvaniaWhip_Down", 0.1f);
						}
						else
						{
							this.SpunDown = false;
						}
						if (Input.GetAxis("VaniaHorizontal") > 0.5f)
						{
							if (this.Character.transform.localScale.x == 1f)
							{
								this.SpinRight();
							}
							else
							{
								this.SpinLeft();
							}
						}
						else if (this.Character.transform.localScale.x == 1f)
						{
							this.SpunRight = false;
						}
						else
						{
							this.SpunLeft = false;
						}
						if (Input.GetAxis("VaniaHorizontal") < -0.5f)
						{
							if (this.Character.transform.localScale.x == 1f)
							{
								this.SpinLeft();
							}
							else
							{
								this.SpinRight();
							}
						}
						else if (this.Character.transform.localScale.x == 1f)
						{
							this.SpunLeft = false;
						}
						else
						{
							this.SpunRight = false;
						}
					}
					this.Rotation = Mathf.MoveTowards(this.Rotation, this.TargetRotation, Time.deltaTime * 3600f * 0.5f);
					this.WhipChain[1].transform.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
					if (!Input.GetButton("VaniaAttack"))
					{
						this.StopAttacking();
					}
				}
			}
			else
			{
				if (this.WhipCollider[1].enabled)
				{
					for (int i = 1; i < this.WhipChain.Length; i++)
					{
						this.SphereCollider[i].enabled = false;
						this.WhipCollider[i].enabled = false;
					}
				}
				this.WhipChain[0].transform.localScale = Vector3.MoveTowards(this.WhipChain[0].transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			if ((!this.Crouching && component["f02_yanvaniaAttack_00"].time >= component["f02_yanvaniaAttack_00"].length) || (this.Crouching && component["f02_yanvaniaCrouchAttack_00"].time >= component["f02_yanvaniaCrouchAttack_00"].length))
			{
				if (Input.GetButton("VaniaAttack"))
				{
					if (this.Crouching)
					{
						component["f02_yanvaniaCrouchPose_00"].weight = 1f;
					}
					this.Dangling = true;
				}
				else
				{
					this.StopAttacking();
				}
			}
		}
		if (this.FlashTimer > 0f)
		{
			this.FlashTimer -= Time.deltaTime;
			if (!this.Red)
			{
				Material[] materials = this.MyRenderer.materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].color = new Color(1f, 0f, 0f, 1f);
				}
				this.Frames++;
				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = true;
				}
			}
			else
			{
				Material[] materials = this.MyRenderer.materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].color = new Color(1f, 1f, 1f, 1f);
				}
				this.Frames++;
				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = false;
				}
			}
		}
		else
		{
			this.FlashTimer = 0f;
			if (this.MyRenderer.materials[0].color != new Color(1f, 1f, 1f, 1f))
			{
				Material[] materials = this.MyRenderer.materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].color = new Color(1f, 1f, 1f, 1f);
				}
			}
		}
		this.HealthBar.localScale = new Vector3(this.HealthBar.localScale.x, Mathf.Lerp(this.HealthBar.localScale.y, this.Health / this.MaxHealth, Time.deltaTime * 10f), this.HealthBar.localScale.z);
		if (this.Health > 0f)
		{
			if (this.EXP >= 100f)
			{
				this.Level++;
				if (this.Level >= 99)
				{
					this.Level = 99;
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(this.LevelUpEffect, this.LevelLabel.transform.position, Quaternion.identity).transform.parent = this.LevelLabel.transform;
					this.MaxHealth += 20f;
					this.Health = this.MaxHealth;
					this.EXP -= 100f;
				}
				this.LevelLabel.text = this.Level.ToString();
			}
			this.EXPBar.localScale = new Vector3(this.EXPBar.localScale.x, Mathf.Lerp(this.EXPBar.localScale.y, this.EXP / 100f, Time.deltaTime * 10f), this.EXPBar.localScale.z);
		}
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			base.transform.position = new Vector3(-31.75f, 6.51f, 0f);
			Physics.SyncTransforms();
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			this.Level = 5;
			this.LevelLabel.text = this.Level.ToString();
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10f;
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10f;
			if (Time.timeScale < 0f)
			{
				Time.timeScale = 1f;
			}
		}
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void LateUpdate()
	{
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x00179EF0 File Offset: 0x001780F0
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		this.contactPoint = this.hit.point;
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x00179F04 File Offset: 0x00178104
	private void FallingDamageAlert(float fallDistance)
	{
		AudioClipPlayer.Play2D(this.LandSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
		this.Character.GetComponent<Animation>().Play("f02_yanvaniaCrouch_00");
		this.fallingDamageThreshold = this.originalThreshold;
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x00179F58 File Offset: 0x00178158
	private void SpinRight()
	{
		if (!this.SpunRight)
		{
			AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
			this.StraightenWhip();
			this.TargetRotation = 360f;
			this.Rotation = 0f;
			this.SpunRight = true;
		}
		this.Character.GetComponent<Animation>().CrossFade("f02_yanvaniaWhip_Right", 0.1f);
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x00179FD0 File Offset: 0x001781D0
	private void SpinLeft()
	{
		if (!this.SpunLeft)
		{
			AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
			this.StraightenWhip();
			this.TargetRotation = -360f;
			this.Rotation = 0f;
			this.SpunLeft = true;
		}
		this.Character.GetComponent<Animation>().CrossFade("f02_yanvaniaWhip_Left", 0.1f);
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x0017A048 File Offset: 0x00178248
	private void StraightenWhip()
	{
		for (int i = 1; i < this.WhipChain.Length; i++)
		{
			this.WhipCollider[i].enabled = true;
			this.WhipChain[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
			Transform transform = this.WhipChain[i].transform;
			transform.localPosition = new Vector3(0f, -0.03f, 0f);
			transform.localEulerAngles = Vector3.zero;
		}
		this.WhipChain[1].transform.localPosition = new Vector3(0f, -0.1f, 0f);
		this.WhipTimer = 0f;
		this.Loose = false;
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x0017A0F8 File Offset: 0x001782F8
	private void LoosenWhip()
	{
		if (!this.Loose)
		{
			this.WhipTimer += Time.deltaTime;
			if (this.WhipTimer > 0.25f)
			{
				for (int i = 1; i < this.WhipChain.Length; i++)
				{
					this.WhipChain[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
					this.SphereCollider[i].enabled = true;
				}
				this.Loose = true;
			}
		}
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x0017A16C File Offset: 0x0017836C
	private void StopAttacking()
	{
		this.Character.GetComponent<Animation>()["f02_yanvaniaCrouchPose_00"].weight = 0f;
		this.TargetRotation = 0f;
		this.Rotation = 0f;
		this.Attacking = false;
		this.Dangling = false;
		this.SpunUp = false;
		this.SpunDown = false;
		this.SpunRight = false;
		this.SpunLeft = false;
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x0017A1D8 File Offset: 0x001783D8
	public void TakeDamage(int Damage)
	{
		if (this.WhipCollider[1].enabled)
		{
			for (int i = 1; i < this.WhipChain.Length; i++)
			{
				this.SphereCollider[i].enabled = false;
				this.WhipCollider[i].enabled = false;
			}
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Injuries[UnityEngine.Random.Range(0, this.Injuries.Length)];
		component.Play();
		this.WhipChain[0].transform.localScale = Vector3.zero;
		Animation component2 = this.Character.GetComponent<Animation>();
		component2["f02_damage_25"].time = 0f;
		this.fallingDamageThreshold = 100f;
		this.moveDirection.x = 0f;
		this.RecoveryTimer = 0f;
		this.FlashTimer = 2f;
		this.Injured = true;
		this.StopAttacking();
		this.Health -= (float)Damage;
		if (this.Dracula.Health <= 0f)
		{
			this.Health = 1f;
		}
		if (this.Dracula.Health > 0f && this.Health <= 0f)
		{
			if (this.NewBlood == null)
			{
				this.MyController.enabled = false;
				this.YanvaniaCamera.StopMusic = true;
				component.clip = this.DeathSound;
				component.Play();
				this.NewBlood = UnityEngine.Object.Instantiate<GameObject>(this.DeathBlood, base.transform.position, Quaternion.identity);
				this.NewBlood.transform.parent = this.Hips;
				this.NewBlood.transform.localPosition = Vector3.zero;
				component2.CrossFade("f02_yanvaniaDeath_00");
				this.CanMove = false;
			}
			this.Health = 0f;
		}
	}

	// Token: 0x04003BBA RID: 15290
	private GameObject NewBlood;

	// Token: 0x04003BBB RID: 15291
	public YanvaniaCameraScript YanvaniaCamera;

	// Token: 0x04003BBC RID: 15292
	public InputManagerScript InputManager;

	// Token: 0x04003BBD RID: 15293
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003BBE RID: 15294
	public CharacterController MyController;

	// Token: 0x04003BBF RID: 15295
	public GameObject BossHealthBar;

	// Token: 0x04003BC0 RID: 15296
	public GameObject LevelUpEffect;

	// Token: 0x04003BC1 RID: 15297
	public GameObject DeathBlood;

	// Token: 0x04003BC2 RID: 15298
	public GameObject Character;

	// Token: 0x04003BC3 RID: 15299
	public GameObject BlackBG;

	// Token: 0x04003BC4 RID: 15300
	public GameObject TextBox;

	// Token: 0x04003BC5 RID: 15301
	public Renderer MyRenderer;

	// Token: 0x04003BC6 RID: 15302
	public Transform TryAgainWindow;

	// Token: 0x04003BC7 RID: 15303
	public Transform HealthBar;

	// Token: 0x04003BC8 RID: 15304
	public Transform EXPBar;

	// Token: 0x04003BC9 RID: 15305
	public Transform Hips;

	// Token: 0x04003BCA RID: 15306
	public Transform TrailStart;

	// Token: 0x04003BCB RID: 15307
	public Transform TrailEnd;

	// Token: 0x04003BCC RID: 15308
	public UITexture Photograph;

	// Token: 0x04003BCD RID: 15309
	public UILabel LevelLabel;

	// Token: 0x04003BCE RID: 15310
	public UISprite Darkness;

	// Token: 0x04003BCF RID: 15311
	public Collider[] SphereCollider;

	// Token: 0x04003BD0 RID: 15312
	public Collider[] WhipCollider;

	// Token: 0x04003BD1 RID: 15313
	public Transform[] WhipChain;

	// Token: 0x04003BD2 RID: 15314
	public AudioClip[] Voices;

	// Token: 0x04003BD3 RID: 15315
	public AudioClip[] Injuries;

	// Token: 0x04003BD4 RID: 15316
	public AudioClip DeathSound;

	// Token: 0x04003BD5 RID: 15317
	public AudioClip LandSound;

	// Token: 0x04003BD6 RID: 15318
	public AudioClip WhipSound;

	// Token: 0x04003BD7 RID: 15319
	public bool Attacking;

	// Token: 0x04003BD8 RID: 15320
	public bool Crouching;

	// Token: 0x04003BD9 RID: 15321
	public bool Dangling;

	// Token: 0x04003BDA RID: 15322
	public bool EnterCutscene;

	// Token: 0x04003BDB RID: 15323
	public bool Cutscene;

	// Token: 0x04003BDC RID: 15324
	public bool CanMove;

	// Token: 0x04003BDD RID: 15325
	public bool Injured;

	// Token: 0x04003BDE RID: 15326
	public bool Loose;

	// Token: 0x04003BDF RID: 15327
	public bool Red;

	// Token: 0x04003BE0 RID: 15328
	public bool SpunUp;

	// Token: 0x04003BE1 RID: 15329
	public bool SpunDown;

	// Token: 0x04003BE2 RID: 15330
	public bool SpunRight;

	// Token: 0x04003BE3 RID: 15331
	public bool SpunLeft;

	// Token: 0x04003BE4 RID: 15332
	public float TargetRotation;

	// Token: 0x04003BE5 RID: 15333
	public float Rotation;

	// Token: 0x04003BE6 RID: 15334
	public float RecoveryTimer;

	// Token: 0x04003BE7 RID: 15335
	public float DeathTimer;

	// Token: 0x04003BE8 RID: 15336
	public float FlashTimer;

	// Token: 0x04003BE9 RID: 15337
	public float IdleTimer;

	// Token: 0x04003BEA RID: 15338
	public float WhipTimer;

	// Token: 0x04003BEB RID: 15339
	public float TapTimer;

	// Token: 0x04003BEC RID: 15340
	public float PreviousY;

	// Token: 0x04003BED RID: 15341
	public float MaxHealth = 100f;

	// Token: 0x04003BEE RID: 15342
	public float Health = 100f;

	// Token: 0x04003BEF RID: 15343
	public float EXP;

	// Token: 0x04003BF0 RID: 15344
	public int Frames;

	// Token: 0x04003BF1 RID: 15345
	public int Level;

	// Token: 0x04003BF2 RID: 15346
	public int Taps;

	// Token: 0x04003BF3 RID: 15347
	public float walkSpeed = 6f;

	// Token: 0x04003BF4 RID: 15348
	public float runSpeed = 11f;

	// Token: 0x04003BF5 RID: 15349
	public bool limitDiagonalSpeed = true;

	// Token: 0x04003BF6 RID: 15350
	public bool toggleRun;

	// Token: 0x04003BF7 RID: 15351
	public float jumpSpeed = 8f;

	// Token: 0x04003BF8 RID: 15352
	public float gravity = 20f;

	// Token: 0x04003BF9 RID: 15353
	public float fallingDamageThreshold = 10f;

	// Token: 0x04003BFA RID: 15354
	public bool slideWhenOverSlopeLimit;

	// Token: 0x04003BFB RID: 15355
	public bool slideOnTaggedObjects;

	// Token: 0x04003BFC RID: 15356
	public float slideSpeed = 12f;

	// Token: 0x04003BFD RID: 15357
	public bool airControl;

	// Token: 0x04003BFE RID: 15358
	public float antiBumpFactor = 0.75f;

	// Token: 0x04003BFF RID: 15359
	public int antiBunnyHopFactor = 1;

	// Token: 0x04003C00 RID: 15360
	private Vector3 moveDirection = Vector3.zero;

	// Token: 0x04003C01 RID: 15361
	public bool grounded;

	// Token: 0x04003C02 RID: 15362
	private CharacterController controller;

	// Token: 0x04003C03 RID: 15363
	private Transform myTransform;

	// Token: 0x04003C04 RID: 15364
	private float speed;

	// Token: 0x04003C05 RID: 15365
	private RaycastHit hit;

	// Token: 0x04003C06 RID: 15366
	private float fallStartLevel;

	// Token: 0x04003C07 RID: 15367
	private bool falling;

	// Token: 0x04003C08 RID: 15368
	private float slideLimit;

	// Token: 0x04003C09 RID: 15369
	private float rayDistance;

	// Token: 0x04003C0A RID: 15370
	private Vector3 contactPoint;

	// Token: 0x04003C0B RID: 15371
	private bool playerControl;

	// Token: 0x04003C0C RID: 15372
	private int jumpTimer;

	// Token: 0x04003C0D RID: 15373
	private float originalThreshold;

	// Token: 0x04003C0E RID: 15374
	public float inputX;
}
