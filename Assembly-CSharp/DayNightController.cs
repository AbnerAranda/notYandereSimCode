using System;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class DayNightController : MonoBehaviour
{
	// Token: 0x0600130D RID: 4877 RVA: 0x0009C574 File Offset: 0x0009A774
	private void Initialize()
	{
		this.quarterDay = this.dayCycleLength * 0.25f;
		this.dawnTime = 0f;
		this.dayTime = this.dawnTime + this.quarterDay;
		this.duskTime = this.dayTime + this.quarterDay;
		this.nightTime = this.duskTime + this.quarterDay;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			this.lightIntensity = component.intensity;
		}
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x0009C5F4 File Offset: 0x0009A7F4
	private void Reset()
	{
		this.dayCycleLength = 120f;
		this.hoursPerDay = 24f;
		this.dawnTimeOffset = 3f;
		this.fullDark = new Color(0.1254902f, 0.109803922f, 0.180392161f);
		this.fullLight = new Color(0.992156863f, 0.972549f, 0.8745098f);
		this.dawnDuskFog = new Color(0.521568656f, 0.4862745f, 0.4f);
		this.dayFog = new Color(0.7058824f, 0.8156863f, 0.819607854f);
		this.nightFog = new Color(0.0470588244f, 0.05882353f, 0.356862754f);
		foreach (Skybox skybox in Resources.FindObjectsOfTypeAll<Skybox>())
		{
			if (skybox.name == "DawnDusk Skybox")
			{
				this.dawnDuskSkybox = skybox.material;
			}
			else if (skybox.name == "StarryNight Skybox")
			{
				this.nightSkybox = skybox.material;
			}
			else if (skybox.name == "Sunny2 Skybox")
			{
				this.daySkybox = skybox.material;
			}
		}
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0009C71A File Offset: 0x0009A91A
	private void Start()
	{
		this.Initialize();
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x0009C724 File Offset: 0x0009A924
	private void Update()
	{
		if (this.currentCycleTime > this.nightTime && this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			this.SetNight();
		}
		else if (this.currentCycleTime > this.duskTime && this.currentPhase == DayNightController.DayPhase.Day)
		{
			this.SetDusk();
		}
		else if (this.currentCycleTime > this.dayTime && this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			this.SetDay();
		}
		else if (this.currentCycleTime > this.dawnTime && this.currentCycleTime < this.dayTime && this.currentPhase == DayNightController.DayPhase.Night)
		{
			this.SetDawn();
		}
		this.UpdateWorldTime();
		this.UpdateDaylight();
		this.UpdateFog();
		this.currentCycleTime += Time.deltaTime;
		this.currentCycleTime %= this.dayCycleLength;
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x0009C7F0 File Offset: 0x0009A9F0
	public void SetDawn()
	{
		RenderSettings.skybox = this.dawnDuskSkybox;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.enabled = true;
		}
		this.currentPhase = DayNightController.DayPhase.Dawn;
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x0009C828 File Offset: 0x0009AA28
	public void SetDay()
	{
		RenderSettings.skybox = this.daySkybox;
		RenderSettings.ambientLight = this.fullLight;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.intensity = this.lightIntensity;
		}
		this.currentPhase = DayNightController.DayPhase.Day;
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x0009C86E File Offset: 0x0009AA6E
	public void SetDusk()
	{
		RenderSettings.skybox = this.dawnDuskSkybox;
		this.currentPhase = DayNightController.DayPhase.Dusk;
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x0009C884 File Offset: 0x0009AA84
	public void SetNight()
	{
		RenderSettings.skybox = this.nightSkybox;
		RenderSettings.ambientLight = this.fullDark;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.enabled = false;
		}
		this.currentPhase = DayNightController.DayPhase.Night;
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x0009C8C8 File Offset: 0x0009AAC8
	private void UpdateDaylight()
	{
		if (this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			float num = this.currentCycleTime - this.dawnTime;
			RenderSettings.ambientLight = Color.Lerp(this.fullDark, this.fullLight, num / this.quarterDay);
			Light component = base.GetComponent<Light>();
			if (component != null)
			{
				component.intensity = this.lightIntensity * (num / this.quarterDay);
			}
		}
		else if (this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			float num2 = this.currentCycleTime - this.duskTime;
			RenderSettings.ambientLight = Color.Lerp(this.fullLight, this.fullDark, num2 / this.quarterDay);
			Light component2 = base.GetComponent<Light>();
			if (component2 != null)
			{
				component2.intensity = this.lightIntensity * ((this.quarterDay - num2) / this.quarterDay);
			}
		}
		base.transform.Rotate(Vector3.up * (Time.deltaTime / this.dayCycleLength * 360f), Space.Self);
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x0009C9BC File Offset: 0x0009ABBC
	private void UpdateFog()
	{
		if (this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			float num = this.currentCycleTime - this.dawnTime;
			RenderSettings.fogColor = Color.Lerp(this.dawnDuskFog, this.dayFog, num / this.quarterDay);
			return;
		}
		if (this.currentPhase == DayNightController.DayPhase.Day)
		{
			float num2 = this.currentCycleTime - this.dayTime;
			RenderSettings.fogColor = Color.Lerp(this.dayFog, this.dawnDuskFog, num2 / this.quarterDay);
			return;
		}
		if (this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			float num3 = this.currentCycleTime - this.duskTime;
			RenderSettings.fogColor = Color.Lerp(this.dawnDuskFog, this.nightFog, num3 / this.quarterDay);
			return;
		}
		if (this.currentPhase == DayNightController.DayPhase.Night)
		{
			float num4 = this.currentCycleTime - this.nightTime;
			RenderSettings.fogColor = Color.Lerp(this.nightFog, this.dawnDuskFog, num4 / this.quarterDay);
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x0009CA9F File Offset: 0x0009AC9F
	private void UpdateWorldTime()
	{
		this.worldTimeHour = (int)((Mathf.Ceil(this.currentCycleTime / this.dayCycleLength * this.hoursPerDay) + this.dawnTimeOffset) % this.hoursPerDay) + 1;
	}

	// Token: 0x04001951 RID: 6481
	public float dayCycleLength;

	// Token: 0x04001952 RID: 6482
	public float currentCycleTime;

	// Token: 0x04001953 RID: 6483
	public DayNightController.DayPhase currentPhase;

	// Token: 0x04001954 RID: 6484
	public float hoursPerDay;

	// Token: 0x04001955 RID: 6485
	public float dawnTimeOffset;

	// Token: 0x04001956 RID: 6486
	public int worldTimeHour;

	// Token: 0x04001957 RID: 6487
	public Color fullLight;

	// Token: 0x04001958 RID: 6488
	public Color fullDark;

	// Token: 0x04001959 RID: 6489
	public Material dawnDuskSkybox;

	// Token: 0x0400195A RID: 6490
	public Color dawnDuskFog;

	// Token: 0x0400195B RID: 6491
	public Material daySkybox;

	// Token: 0x0400195C RID: 6492
	public Color dayFog;

	// Token: 0x0400195D RID: 6493
	public Material nightSkybox;

	// Token: 0x0400195E RID: 6494
	public Color nightFog;

	// Token: 0x0400195F RID: 6495
	private float dawnTime;

	// Token: 0x04001960 RID: 6496
	private float dayTime;

	// Token: 0x04001961 RID: 6497
	private float duskTime;

	// Token: 0x04001962 RID: 6498
	private float nightTime;

	// Token: 0x04001963 RID: 6499
	private float quarterDay;

	// Token: 0x04001964 RID: 6500
	private float lightIntensity;

	// Token: 0x020006B3 RID: 1715
	public enum DayPhase
	{
		// Token: 0x0400474C RID: 18252
		Night,
		// Token: 0x0400474D RID: 18253
		Dawn,
		// Token: 0x0400474E RID: 18254
		Day,
		// Token: 0x0400474F RID: 18255
		Dusk
	}
}
