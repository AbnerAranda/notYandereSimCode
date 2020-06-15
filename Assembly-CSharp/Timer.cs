using System;
using UnityEngine;

// Token: 0x0200045E RID: 1118
[Serializable]
public class Timer
{
	// Token: 0x06001D05 RID: 7429 RVA: 0x00159251 File Offset: 0x00157451
	public Timer(float targetSeconds)
	{
		this.currentSeconds = 0f;
		this.targetSeconds = targetSeconds;
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0015926B File Offset: 0x0015746B
	public float CurrentSeconds
	{
		get
		{
			return this.currentSeconds;
		}
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x06001D07 RID: 7431 RVA: 0x00159273 File Offset: 0x00157473
	public float TargetSeconds
	{
		get
		{
			return this.targetSeconds;
		}
	}

	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0015927B File Offset: 0x0015747B
	public bool IsDone
	{
		get
		{
			return this.currentSeconds >= this.targetSeconds;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0015928E File Offset: 0x0015748E
	public float Progress
	{
		get
		{
			return Mathf.Clamp01(this.currentSeconds / this.targetSeconds);
		}
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x001592A2 File Offset: 0x001574A2
	public void Reset()
	{
		this.currentSeconds = 0f;
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x001592AF File Offset: 0x001574AF
	public void SubtractTarget()
	{
		this.currentSeconds -= this.targetSeconds;
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x001592C4 File Offset: 0x001574C4
	public void Tick(float dt)
	{
		this.currentSeconds += dt;
	}

	// Token: 0x04003671 RID: 13937
	[SerializeField]
	private float currentSeconds;

	// Token: 0x04003672 RID: 13938
	[SerializeField]
	private float targetSeconds;
}
