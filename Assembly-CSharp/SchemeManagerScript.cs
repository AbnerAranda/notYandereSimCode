using System;
using UnityEngine;

// Token: 0x020003CC RID: 972
public class SchemeManagerScript : MonoBehaviour
{
	// Token: 0x06001A5F RID: 6751 RVA: 0x001033EC File Offset: 0x001015EC
	private void Update()
	{
		if (this.Clock.HourTime > 15.5f)
		{
			SchemeGlobals.SetSchemeStage(SchemeGlobals.CurrentScheme, 100);
			this.Clock.Yandere.NotificationManager.CustomText = "Scheme failed! You were too slow.";
			this.Clock.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			this.Schemes.UpdateInstructions();
			base.enabled = false;
		}
		if (this.ClockCheck && this.Clock.HourTime > 8.25f)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Timer = 0f;
				if (SchemeGlobals.GetSchemeStage(5) == 1)
				{
					Debug.Log("It's past 8:15 AM, so we're advancing to Stage 2 of Scheme 5.");
					SchemeGlobals.SetSchemeStage(5, 2);
					this.Schemes.UpdateInstructions();
					this.ClockCheck = false;
				}
			}
		}
	}

	// Token: 0x040029DC RID: 10716
	public SchemesScript Schemes;

	// Token: 0x040029DD RID: 10717
	public ClockScript Clock;

	// Token: 0x040029DE RID: 10718
	public bool ClockCheck;

	// Token: 0x040029DF RID: 10719
	public float Timer;
}
