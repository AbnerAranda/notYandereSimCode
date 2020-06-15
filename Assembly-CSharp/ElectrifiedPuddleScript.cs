using System;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class ElectrifiedPuddleScript : MonoBehaviour
{
	// Token: 0x06001399 RID: 5017 RVA: 0x000A93F8 File Offset: 0x000A75F8
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && !component.Electrified)
			{
				component.Yandere.GazerEyes.ElectrocuteStudent(component);
				base.gameObject.SetActive(false);
				this.PowerSwitch.On = false;
			}
		}
		if (other.gameObject.layer == 13)
		{
			YandereScript component2 = other.gameObject.GetComponent<YandereScript>();
			if (component2 != null)
			{
				component2.StudentManager.Headmaster.Taze();
				component2.StudentManager.Headmaster.Heartbroken.Headmaster = false;
			}
		}
	}

	// Token: 0x04001AE2 RID: 6882
	public PowerSwitchScript PowerSwitch;
}
