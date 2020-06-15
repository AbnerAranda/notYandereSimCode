using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class HomePrisonerChanScript : MonoBehaviour
{
	// Token: 0x06001751 RID: 5969 RVA: 0x000C8A00 File Offset: 0x000C6C00
	private void Start()
	{
		if (SchoolGlobals.KidnapVictim > 0)
		{
			this.StudentID = SchoolGlobals.KidnapVictim;
			if (StudentGlobals.GetStudentSanity(this.StudentID) == 100f)
			{
				this.AnkleRopes.SetActive(false);
			}
			this.PermanentAngleR = this.TwintailR.eulerAngles;
			this.PermanentAngleL = this.TwintailL.eulerAngles;
			if (StudentGlobals.GetStudentArrested(this.StudentID) || StudentGlobals.GetStudentDead(this.StudentID))
			{
				SchoolGlobals.KidnapVictim = 0;
				base.gameObject.SetActive(false);
				return;
			}
			this.Cosmetic.StudentID = this.StudentID;
			this.Cosmetic.enabled = true;
			this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;
			this.RightEyeRotOrigin = this.RightEye.localEulerAngles;
			this.LeftEyeRotOrigin = this.LeftEye.localEulerAngles;
			this.RightEyeOrigin = this.RightEye.localPosition;
			this.LeftEyeOrigin = this.LeftEye.localPosition;
			this.UpdateSanity();
			this.TwintailR.transform.localEulerAngles = new Vector3(0f, 180f, -90f);
			this.TwintailL.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			this.Blindfold.SetActive(false);
			this.Tripod.SetActive(false);
			if (this.StudentID == 81 && !StudentGlobals.GetStudentBroken(81) && SchemeGlobals.HelpingKokona)
			{
				this.Blindfold.SetActive(true);
				this.Tripod.SetActive(true);
				return;
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000C8BBC File Offset: 0x000C6DBC
	private void LateUpdate()
	{
		this.Skirt.transform.localPosition = new Vector3(0f, -0.135f, 0.01f);
		this.Skirt.transform.localScale = new Vector3(this.Skirt.transform.localScale.x, 1.2f, this.Skirt.transform.localScale.z);
		if (!this.Tortured)
		{
			if (this.Sanity > 0f)
			{
				if (this.LookAhead)
				{
					this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
				}
				else if (this.YandereDetector.YandereDetected && Vector3.Distance(base.transform.position, this.HomeYandere.position) < 2f)
				{
					Quaternion b;
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						b = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot1, Time.deltaTime * 2f);
					}
					else
					{
						b = Quaternion.LookRotation(this.HomeYandere.position + Vector3.up * 1.5f - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot2, Time.deltaTime * 2f);
					}
					this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
				else
				{
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						Quaternion b2 = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot3, Time.deltaTime * 2f);
					}
					else
					{
						Quaternion b2 = Quaternion.LookRotation(base.transform.position + base.transform.forward - this.Neck.position);
						this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b2, Time.deltaTime * 2f);
					}
					this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot4, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
			}
			else
			{
				this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
			}
		}
		this.LastRotation = this.Neck.rotation;
		if (!this.Tortured && this.Sanity < 100f && this.Sanity > 0f)
		{
			this.TwitchTimer += Time.deltaTime;
			if (this.TwitchTimer > this.NextTwitch)
			{
				this.Twitch = new Vector3((1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f));
				this.NextTwitch = UnityEngine.Random.Range(0f, 1f);
				this.TwitchTimer = 0f;
			}
			this.Twitch = Vector3.Lerp(this.Twitch, Vector3.zero, Time.deltaTime * 10f);
			this.Neck.localEulerAngles += this.Twitch;
		}
		if (this.Tortured)
		{
			this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot5, Time.deltaTime * 2f);
			this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
			this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
		}
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000C916C File Offset: 0x000C736C
	public void UpdateSanity()
	{
		this.Sanity = StudentGlobals.GetStudentSanity(this.StudentID);
		bool active = this.Sanity == 0f;
		this.RightMindbrokenEye.SetActive(active);
		this.LeftMindbrokenEye.SetActive(active);
	}

	// Token: 0x04002012 RID: 8210
	public HomeYandereDetectorScript YandereDetector;

	// Token: 0x04002013 RID: 8211
	public HomeCameraScript HomeCamera;

	// Token: 0x04002014 RID: 8212
	public CosmeticScript Cosmetic;

	// Token: 0x04002015 RID: 8213
	public JsonScript JSON;

	// Token: 0x04002016 RID: 8214
	public Vector3 RightEyeRotOrigin;

	// Token: 0x04002017 RID: 8215
	public Vector3 LeftEyeRotOrigin;

	// Token: 0x04002018 RID: 8216
	public Vector3 PermanentAngleR;

	// Token: 0x04002019 RID: 8217
	public Vector3 PermanentAngleL;

	// Token: 0x0400201A RID: 8218
	public Vector3 RightEyeOrigin;

	// Token: 0x0400201B RID: 8219
	public Vector3 LeftEyeOrigin;

	// Token: 0x0400201C RID: 8220
	public Vector3 Twitch;

	// Token: 0x0400201D RID: 8221
	public Quaternion LastRotation;

	// Token: 0x0400201E RID: 8222
	public Transform HomeYandere;

	// Token: 0x0400201F RID: 8223
	public Transform RightBreast;

	// Token: 0x04002020 RID: 8224
	public Transform LeftBreast;

	// Token: 0x04002021 RID: 8225
	public Transform TwintailR;

	// Token: 0x04002022 RID: 8226
	public Transform TwintailL;

	// Token: 0x04002023 RID: 8227
	public Transform RightEye;

	// Token: 0x04002024 RID: 8228
	public Transform LeftEye;

	// Token: 0x04002025 RID: 8229
	public Transform Skirt;

	// Token: 0x04002026 RID: 8230
	public Transform Neck;

	// Token: 0x04002027 RID: 8231
	public GameObject RightMindbrokenEye;

	// Token: 0x04002028 RID: 8232
	public GameObject LeftMindbrokenEye;

	// Token: 0x04002029 RID: 8233
	public GameObject AnkleRopes;

	// Token: 0x0400202A RID: 8234
	public GameObject Blindfold;

	// Token: 0x0400202B RID: 8235
	public GameObject Character;

	// Token: 0x0400202C RID: 8236
	public GameObject Tripod;

	// Token: 0x0400202D RID: 8237
	public float HairRotation;

	// Token: 0x0400202E RID: 8238
	public float TwitchTimer;

	// Token: 0x0400202F RID: 8239
	public float NextTwitch;

	// Token: 0x04002030 RID: 8240
	public float BreastSize;

	// Token: 0x04002031 RID: 8241
	public float EyeShrink;

	// Token: 0x04002032 RID: 8242
	public float Sanity;

	// Token: 0x04002033 RID: 8243
	public float HairRot1;

	// Token: 0x04002034 RID: 8244
	public float HairRot2;

	// Token: 0x04002035 RID: 8245
	public float HairRot3;

	// Token: 0x04002036 RID: 8246
	public float HairRot4;

	// Token: 0x04002037 RID: 8247
	public float HairRot5;

	// Token: 0x04002038 RID: 8248
	public bool LookAhead;

	// Token: 0x04002039 RID: 8249
	public bool Tortured;

	// Token: 0x0400203A RID: 8250
	public bool Male;

	// Token: 0x0400203B RID: 8251
	public int StudentID;
}
