using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000322 RID: 802
public class LivingRoomCutsceneScript : MonoBehaviour
{
	// Token: 0x0600180C RID: 6156 RVA: 0x000D4EA8 File Offset: 0x000D30A8
	private void Start()
	{
		this.YandereCosmetic.SetFemaleUniform();
		this.YandereCosmetic.RightWristband.SetActive(false);
		this.YandereCosmetic.LeftWristband.SetActive(false);
		this.YandereCosmetic.ThickBrows.SetActive(false);
		this.ID = 0;
		while (this.ID < this.YandereCosmetic.FemaleHair.Length)
		{
			GameObject gameObject = this.YandereCosmetic.FemaleHair[this.ID];
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.YandereCosmetic.TeacherHair.Length)
		{
			GameObject gameObject2 = this.YandereCosmetic.TeacherHair[this.ID];
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.YandereCosmetic.FemaleAccessories.Length)
		{
			GameObject gameObject3 = this.YandereCosmetic.FemaleAccessories[this.ID];
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.YandereCosmetic.TeacherAccessories.Length)
		{
			GameObject gameObject4 = this.YandereCosmetic.TeacherAccessories[this.ID];
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.YandereCosmetic.ClubAccessories.Length)
		{
			GameObject gameObject5 = this.YandereCosmetic.ClubAccessories[this.ID];
			if (gameObject5 != null)
			{
				gameObject5.SetActive(false);
			}
			this.ID++;
		}
		foreach (GameObject gameObject6 in this.YandereCosmetic.Scanners)
		{
			if (gameObject6 != null)
			{
				gameObject6.SetActive(false);
			}
		}
		foreach (GameObject gameObject7 in this.YandereCosmetic.Flowers)
		{
			if (gameObject7 != null)
			{
				gameObject7.SetActive(false);
			}
		}
		foreach (GameObject gameObject8 in this.YandereCosmetic.PunkAccessories)
		{
			if (gameObject8 != null)
			{
				gameObject8.SetActive(false);
			}
		}
		foreach (GameObject gameObject9 in this.YandereCosmetic.RedCloth)
		{
			if (gameObject9 != null)
			{
				gameObject9.SetActive(false);
			}
		}
		foreach (GameObject gameObject10 in this.YandereCosmetic.Kerchiefs)
		{
			if (gameObject10 != null)
			{
				gameObject10.SetActive(false);
			}
		}
		for (int j = 0; j < 10; j++)
		{
			this.YandereCosmetic.Fingernails[j].gameObject.SetActive(false);
		}
		this.ID = 0;
		this.YandereCosmetic.FemaleHair[1].SetActive(true);
		this.YandereCosmetic.MyRenderer.materials[2].mainTexture = this.YandereCosmetic.DefaultFaceTexture;
		this.Subtitle.text = string.Empty;
		this.RightEyeRenderer.material.color = new Color(0.33f, 0.33f, 0.33f, 1f);
		this.LeftEyeRenderer.material.color = new Color(0.33f, 0.33f, 0.33f, 1f);
		this.RightEyeOrigin = this.RightEye.localPosition;
		this.LeftEyeOrigin = this.LeftEye.localPosition;
		this.EliminationPanel.alpha = 0f;
		this.Panel.alpha = 1f;
		this.ColorCorrection.saturation = 1f;
		this.Noise.intensityMultiplier = 0f;
		this.Obscurance.intensity = 0f;
		this.Vignette.enabled = false;
		this.Vignette.intensity = 1f;
		this.Vignette.blur = 1f;
		this.Vignette.chromaticAberration = 1f;
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000D5314 File Offset: 0x000D3514
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Phase == 1)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f && Input.GetButtonDown("A"))
				{
					this.Timer = 0f;
					this.Phase++;
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				base.transform.parent = this.LivingRoomCamera;
				base.transform.localPosition = new Vector3(-0.65f, 0f, 0f);
				base.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
				this.Vignette.enabled = true;
				this.Prologue.SetActive(false);
				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0f, Time.deltaTime);
				if (this.Panel.alpha == 0f)
				{
					this.Yandere.GetComponent<Animation>()["FriendshipYandere"].time = 0f;
					this.Rival.GetComponent<Animation>()["FriendshipRival"].time = 0f;
					this.LivingRoomCamera.gameObject.GetComponent<Animation>().Play();
					this.Timer = 0f;
					this.Phase++;
				}
			}
		}
		else if (this.Phase == 4)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 10f)
			{
				base.transform.parent = this.FriendshipCamera;
				base.transform.localPosition = new Vector3(-0.65f, 0f, 0f);
				base.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
				this.FriendshipCamera.gameObject.GetComponent<Animation>().Play();
				component.Play();
				this.Subtitle.text = this.Lines[0];
				this.Timer = 0f;
				this.Phase++;
			}
		}
		else if (this.Phase == 5)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.Timer += 10f;
				component.time += 10f;
				this.FriendshipCamera.gameObject.GetComponent<Animation>()["FriendshipCameraFlat"].time += 10f;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer < 166f)
			{
				this.Yandere.GetComponent<Animation>()["FriendshipYandere"].time = component.time + this.AnimOffset;
				this.Rival.GetComponent<Animation>()["FriendshipRival"].time = component.time + this.AnimOffset;
			}
			if (this.ID < this.Times.Length && component.time > this.Times[this.ID])
			{
				this.Subtitle.text = this.Lines[this.ID];
				this.ID++;
			}
			if (component.time > 54f)
			{
				this.Jukebox.volume = Mathf.MoveTowards(this.Jukebox.volume, 0f, Time.deltaTime / 5f);
				component.volume = Mathf.MoveTowards(component.volume, 0.2f, Time.deltaTime * 0.1f / 5f);
				this.Vignette.intensity = Mathf.MoveTowards(this.Vignette.intensity, 1f, Time.deltaTime * 4f / 5f);
				this.Vignette.blur = this.Vignette.intensity;
				this.Vignette.chromaticAberration = this.Vignette.intensity;
				this.ColorCorrection.saturation = Mathf.MoveTowards(this.ColorCorrection.saturation, 1f, Time.deltaTime / 5f);
				this.Noise.intensityMultiplier = Mathf.MoveTowards(this.Noise.intensityMultiplier, 0f, Time.deltaTime * 3f / 5f);
				this.Obscurance.intensity = Mathf.MoveTowards(this.Obscurance.intensity, 0f, Time.deltaTime * 3f / 5f);
				this.ShakeStrength = Mathf.MoveTowards(this.ShakeStrength, 0f, Time.deltaTime * 0.01f / 5f);
				this.EliminationPanel.alpha = Mathf.MoveTowards(this.EliminationPanel.alpha, 0f, Time.deltaTime);
				this.EyeShrink = Mathf.MoveTowards(this.EyeShrink, 0f, Time.deltaTime);
			}
			else if (component.time > 42f)
			{
				if (!this.Jukebox.isPlaying)
				{
					this.Jukebox.Play();
				}
				this.Jukebox.volume = Mathf.MoveTowards(this.Jukebox.volume, 1f, Time.deltaTime / 10f);
				component.volume = Mathf.MoveTowards(component.volume, 0.1f, Time.deltaTime * 0.1f / 10f);
				this.Vignette.intensity = Mathf.MoveTowards(this.Vignette.intensity, 5f, Time.deltaTime * 4f / 10f);
				this.Vignette.blur = this.Vignette.intensity;
				this.Vignette.chromaticAberration = this.Vignette.intensity;
				this.ColorCorrection.saturation = Mathf.MoveTowards(this.ColorCorrection.saturation, 0f, Time.deltaTime / 10f);
				this.Noise.intensityMultiplier = Mathf.MoveTowards(this.Noise.intensityMultiplier, 3f, Time.deltaTime * 3f / 10f);
				this.Obscurance.intensity = Mathf.MoveTowards(this.Obscurance.intensity, 3f, Time.deltaTime * 3f / 10f);
				this.ShakeStrength = Mathf.MoveTowards(this.ShakeStrength, 0.01f, Time.deltaTime * 0.01f / 10f);
				this.EyeShrink = Mathf.MoveTowards(this.EyeShrink, 0.9f, Time.deltaTime);
				if (component.time > 45f)
				{
					if (component.time > 54f)
					{
						this.EliminationPanel.alpha = Mathf.MoveTowards(this.EliminationPanel.alpha, 0f, Time.deltaTime);
					}
					else
					{
						this.EliminationPanel.alpha = Mathf.MoveTowards(this.EliminationPanel.alpha, 1f, Time.deltaTime);
						if (Input.GetButtonDown("X"))
						{
							component.clip = this.RivalProtest;
							component.volume = 1f;
							component.Play();
							this.Jukebox.gameObject.SetActive(false);
							this.Subtitle.text = "Wait, what are you doing?! That's not funny! Stop! Let me go! ...n...NO!!!";
							this.SubDarknessBG.color = new Color(this.SubDarknessBG.color.r, this.SubDarknessBG.color.g, this.SubDarknessBG.color.b, 1f);
							this.Phase++;
						}
					}
				}
			}
			if (this.Timer > 167f)
			{
				Animation component2 = this.Yandere.GetComponent<Animation>();
				component2["FriendshipYandere"].speed = -0.2f;
				component2.Play("FriendshipYandere");
				component2["FriendshipYandere"].time = component2["FriendshipYandere"].length;
				this.Subtitle.text = string.Empty;
				this.Phase = 10;
			}
		}
		else if (this.Phase == 6)
		{
			if (!component.isPlaying)
			{
				component.clip = this.DramaticBoom;
				component.Play();
				this.Subtitle.text = string.Empty;
				this.Phase++;
			}
		}
		else if (this.Phase == 7)
		{
			if (!component.isPlaying)
			{
				StudentGlobals.SetStudentKidnapped(81, false);
				StudentGlobals.SetStudentBroken(81, true);
				StudentGlobals.SetStudentKidnapped(30, true);
				StudentGlobals.SetStudentSanity(30, 100f);
				SchoolGlobals.KidnapVictim = 30;
				HomeGlobals.StartInBasement = true;
				SceneManager.LoadScene("CalendarScene");
			}
		}
		else if (this.Phase == 10)
		{
			this.SubDarkness.color = new Color(this.SubDarkness.color.r, this.SubDarkness.color.g, this.SubDarkness.color.b, Mathf.MoveTowards(this.SubDarkness.color.a, 1f, Time.deltaTime * 0.2f));
			if (this.SubDarkness.color.a == 1f)
			{
				StudentGlobals.SetStudentKidnapped(81, false);
				StudentGlobals.SetStudentBroken(81, true);
				SchoolGlobals.KidnapVictim = 0;
				SceneManager.LoadScene("CalendarScene");
			}
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 1f;
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 1f;
		}
		component.pitch = Time.timeScale;
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x000D5E24 File Offset: 0x000D4024
	private void LateUpdate()
	{
		if (this.Phase > 2)
		{
			base.transform.localPosition = new Vector3(-0.65f + this.ShakeStrength * UnityEngine.Random.Range(-1f, 1f), this.ShakeStrength * UnityEngine.Random.Range(-1f, 1f), this.ShakeStrength * UnityEngine.Random.Range(-1f, 1f));
			this.CutsceneCamera.position = new Vector3(this.CutsceneCamera.position.x + this.xOffset, this.CutsceneCamera.position.y, this.CutsceneCamera.position.z + this.zOffset);
			this.LeftEye.localPosition = new Vector3(this.LeftEye.localPosition.x, this.LeftEye.localPosition.y, this.LeftEyeOrigin.z - this.EyeShrink * 0.01f);
			this.RightEye.localPosition = new Vector3(this.RightEye.localPosition.x, this.RightEye.localPosition.y, this.RightEyeOrigin.z + this.EyeShrink * 0.01f);
			this.LeftEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.LeftEye.localScale.z);
			this.RightEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.RightEye.localScale.z);
		}
	}

	// Token: 0x040022A8 RID: 8872
	public ColorCorrectionCurves ColorCorrection;

	// Token: 0x040022A9 RID: 8873
	public CosmeticScript YandereCosmetic;

	// Token: 0x040022AA RID: 8874
	public AmbientObscurance Obscurance;

	// Token: 0x040022AB RID: 8875
	public Vignetting Vignette;

	// Token: 0x040022AC RID: 8876
	public NoiseAndGrain Noise;

	// Token: 0x040022AD RID: 8877
	public SkinnedMeshRenderer YandereRenderer;

	// Token: 0x040022AE RID: 8878
	public Renderer RightEyeRenderer;

	// Token: 0x040022AF RID: 8879
	public Renderer LeftEyeRenderer;

	// Token: 0x040022B0 RID: 8880
	public Transform FriendshipCamera;

	// Token: 0x040022B1 RID: 8881
	public Transform LivingRoomCamera;

	// Token: 0x040022B2 RID: 8882
	public Transform CutsceneCamera;

	// Token: 0x040022B3 RID: 8883
	public UIPanel EliminationPanel;

	// Token: 0x040022B4 RID: 8884
	public UISprite SubDarknessBG;

	// Token: 0x040022B5 RID: 8885
	public UISprite SubDarkness;

	// Token: 0x040022B6 RID: 8886
	public UISprite Darkness;

	// Token: 0x040022B7 RID: 8887
	public UILabel Subtitle;

	// Token: 0x040022B8 RID: 8888
	public UIPanel Panel;

	// Token: 0x040022B9 RID: 8889
	public Vector3 RightEyeOrigin;

	// Token: 0x040022BA RID: 8890
	public Vector3 LeftEyeOrigin;

	// Token: 0x040022BB RID: 8891
	public AudioClip DramaticBoom;

	// Token: 0x040022BC RID: 8892
	public AudioClip RivalProtest;

	// Token: 0x040022BD RID: 8893
	public AudioSource Jukebox;

	// Token: 0x040022BE RID: 8894
	public GameObject Prologue;

	// Token: 0x040022BF RID: 8895
	public GameObject Yandere;

	// Token: 0x040022C0 RID: 8896
	public GameObject Rival;

	// Token: 0x040022C1 RID: 8897
	public Transform RightEye;

	// Token: 0x040022C2 RID: 8898
	public Transform LeftEye;

	// Token: 0x040022C3 RID: 8899
	public float ShakeStrength;

	// Token: 0x040022C4 RID: 8900
	public float AnimOffset;

	// Token: 0x040022C5 RID: 8901
	public float EyeShrink;

	// Token: 0x040022C6 RID: 8902
	public float xOffset;

	// Token: 0x040022C7 RID: 8903
	public float zOffset;

	// Token: 0x040022C8 RID: 8904
	public float Timer;

	// Token: 0x040022C9 RID: 8905
	public string[] Lines;

	// Token: 0x040022CA RID: 8906
	public float[] Times;

	// Token: 0x040022CB RID: 8907
	public int Phase = 1;

	// Token: 0x040022CC RID: 8908
	public int ID = 1;

	// Token: 0x040022CD RID: 8909
	public Texture ZTR;

	// Token: 0x040022CE RID: 8910
	public int ZTRID;
}
