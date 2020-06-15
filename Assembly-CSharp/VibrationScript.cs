using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x02000461 RID: 1121
public class VibrationScript : MonoBehaviour
{
	// Token: 0x06001D13 RID: 7443 RVA: 0x0015952B File Offset: 0x0015772B
	private void Start()
	{
		GamePad.SetVibration(PlayerIndex.One, this.Strength1, this.Strength2);
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0015953F File Offset: 0x0015773F
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.TimeLimit)
		{
			GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
			base.enabled = false;
		}
	}

	// Token: 0x0400367A RID: 13946
	public float Strength1;

	// Token: 0x0400367B RID: 13947
	public float Strength2;

	// Token: 0x0400367C RID: 13948
	public float TimeLimit;

	// Token: 0x0400367D RID: 13949
	public float Timer;
}
