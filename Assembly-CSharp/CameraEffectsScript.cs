using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x02000226 RID: 550
public class CameraEffectsScript : MonoBehaviour
{
	// Token: 0x06001217 RID: 4631 RVA: 0x000804DC File Offset: 0x0007E6DC
	private void Start()
	{
		this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, 0f);
		this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, 0f);
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x00080574 File Offset: 0x0007E774
	private void Update()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0f, Time.deltaTime);
			if (this.VibrationTimer == 0f)
			{
				GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
				this.VibrationCheck = false;
			}
		}
		if (this.Streaks.color.a > 0f)
		{
			this.AlarmBloom.bloomIntensity -= Time.deltaTime;
			this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, this.Streaks.color.a - Time.deltaTime);
			if (this.Streaks.color.a <= 0f)
			{
				this.AlarmBloom.enabled = false;
			}
		}
		if (this.MurderStreaks.color.a > 0f)
		{
			this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, this.MurderStreaks.color.a - Time.deltaTime);
		}
		this.EffectStrength = 1f - this.Yandere.Sanity * 0.01f;
		this.Vignette.intensity = Mathf.Lerp(this.Vignette.intensity, this.EffectStrength * 5f, Time.deltaTime);
		this.Vignette.blur = Mathf.Lerp(this.Vignette.blur, this.EffectStrength, Time.deltaTime);
		this.Vignette.chromaticAberration = Mathf.Lerp(this.Vignette.chromaticAberration, this.EffectStrength * 5f, Time.deltaTime);
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x0008077C File Offset: 0x0007E97C
	public void Alarm()
	{
		GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
		this.VibrationCheck = true;
		this.VibrationTimer = 0.1f;
		this.AlarmBloom.bloomIntensity = 1f;
		this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, 1f);
		this.AlarmBloom.enabled = true;
		this.Yandere.Jukebox.SFX.PlayOneShot(this.Noticed);
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x00080828 File Offset: 0x0007EA28
	public void MurderWitnessed()
	{
		GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
		this.VibrationCheck = true;
		this.VibrationTimer = 0.1f;
		this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, 1f);
		this.Yandere.Jukebox.SFX.PlayOneShot(this.Yandere.Noticed ? this.SenpaiNoticed : this.MurderNoticed);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000808CC File Offset: 0x0007EACC
	public void DisableCamera()
	{
		if (!this.OneCamera)
		{
			this.OneCamera = true;
			return;
		}
		this.OneCamera = false;
	}

	// Token: 0x0400154C RID: 5452
	public YandereScript Yandere;

	// Token: 0x0400154D RID: 5453
	public Vignetting Vignette;

	// Token: 0x0400154E RID: 5454
	public UITexture MurderStreaks;

	// Token: 0x0400154F RID: 5455
	public UITexture Streaks;

	// Token: 0x04001550 RID: 5456
	public Bloom AlarmBloom;

	// Token: 0x04001551 RID: 5457
	public float EffectStrength;

	// Token: 0x04001552 RID: 5458
	public float VibrationTimer;

	// Token: 0x04001553 RID: 5459
	public Bloom QualityBloom;

	// Token: 0x04001554 RID: 5460
	public Vignetting QualityVignetting;

	// Token: 0x04001555 RID: 5461
	public AntialiasingAsPostEffect QualityAntialiasingAsPostEffect;

	// Token: 0x04001556 RID: 5462
	public bool VibrationCheck;

	// Token: 0x04001557 RID: 5463
	public bool OneCamera;

	// Token: 0x04001558 RID: 5464
	public AudioClip MurderNoticed;

	// Token: 0x04001559 RID: 5465
	public AudioClip SenpaiNoticed;

	// Token: 0x0400155A RID: 5466
	public AudioClip Noticed;
}
