using System;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class LightSwitchScript : MonoBehaviour
{
	// Token: 0x06001803 RID: 6147 RVA: 0x000D479F File Offset: 0x000D299F
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x000D47B8 File Offset: 0x000D29B8
	private void Update()
	{
		if (this.Flicker)
		{
			this.FlickerTimer += Time.deltaTime;
			if (this.FlickerTimer > 0.1f)
			{
				this.FlickerTimer = 0f;
				this.BathroomLight.SetActive(!this.BathroomLight.activeInHierarchy);
			}
		}
		if (!this.Panel.useGravity)
		{
			if (this.Yandere.Armed)
			{
				this.Prompt.HideButton[3] = (this.Yandere.EquippedWeapon.WeaponID != 6);
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.BathroomLight.activeInHierarchy)
			{
				this.Prompt.Label[0].text = "     Turn On";
				this.BathroomLight.SetActive(false);
				component.clip = this.Flick[1];
				component.Play();
				if (this.ToiletEvent.EventActive && (this.ToiletEvent.EventPhase == 2 || this.ToiletEvent.EventPhase == 3))
				{
					this.ReactionID = UnityEngine.Random.Range(1, 4);
					AudioClipPlayer.Play(this.ReactionClips[this.ReactionID], this.ToiletEvent.EventStudent.transform.position, 5f, 10f, out this.ToiletEvent.VoiceClip);
					this.ToiletEvent.EventSubtitle.text = this.ReactionTexts[this.ReactionID];
					this.SubtitleTimer += Time.deltaTime;
				}
			}
			else
			{
				this.Prompt.Label[0].text = "     Turn Off";
				this.BathroomLight.SetActive(true);
				component.clip = this.Flick[0];
				component.Play();
			}
		}
		if (this.SubtitleTimer > 0f)
		{
			this.SubtitleTimer += Time.deltaTime;
			if (this.SubtitleTimer > 3f)
			{
				this.ToiletEvent.EventSubtitle.text = string.Empty;
				this.SubtitleTimer = 0f;
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.HideButton[3] = true;
			this.Wires.localScale = new Vector3(this.Wires.localScale.x, this.Wires.localScale.y, 1f);
			this.Panel.useGravity = true;
			this.Panel.AddForce(0f, 0f, 10f);
		}
	}

	// Token: 0x0400228D RID: 8845
	public ToiletEventScript ToiletEvent;

	// Token: 0x0400228E RID: 8846
	public YandereScript Yandere;

	// Token: 0x0400228F RID: 8847
	public PromptScript Prompt;

	// Token: 0x04002290 RID: 8848
	public Transform ElectrocutionSpot;

	// Token: 0x04002291 RID: 8849
	public GameObject BathroomLight;

	// Token: 0x04002292 RID: 8850
	public GameObject Electricity;

	// Token: 0x04002293 RID: 8851
	public Rigidbody Panel;

	// Token: 0x04002294 RID: 8852
	public Transform Wires;

	// Token: 0x04002295 RID: 8853
	public AudioClip[] ReactionClips;

	// Token: 0x04002296 RID: 8854
	public string[] ReactionTexts;

	// Token: 0x04002297 RID: 8855
	public AudioClip[] Flick;

	// Token: 0x04002298 RID: 8856
	public float SubtitleTimer;

	// Token: 0x04002299 RID: 8857
	public float FlickerTimer;

	// Token: 0x0400229A RID: 8858
	public int ReactionID;

	// Token: 0x0400229B RID: 8859
	public bool Flicker;
}
