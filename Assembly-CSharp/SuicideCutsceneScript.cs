using System;
using UnityEngine;

// Token: 0x02000413 RID: 1043
public class SuicideCutsceneScript : MonoBehaviour
{
	// Token: 0x06001C0E RID: 7182 RVA: 0x00149E5C File Offset: 0x0014805C
	private void Start()
	{
		this.PointLight.color = new Color(0.1f, 0.1f, 0.1f, 1f);
		this.Door.eulerAngles = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x00149EAC File Offset: 0x001480AC
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 2f)
		{
			this.Speed += Time.deltaTime;
			this.Rotation = Mathf.Lerp(this.Rotation, -45f, Time.deltaTime * this.Speed);
			this.PointLight.color = new Color(0.1f + this.Rotation / -45f * 0.9f, 0.1f + this.Rotation / -45f * 0.9f, 0.1f + this.Rotation / -45f * 0.9f, 1f);
			this.Door.eulerAngles = new Vector3(0f, this.Rotation, 0f);
		}
	}

	// Token: 0x04003441 RID: 13377
	public Light PointLight;

	// Token: 0x04003442 RID: 13378
	public Transform Door;

	// Token: 0x04003443 RID: 13379
	public float Timer;

	// Token: 0x04003444 RID: 13380
	public float Rotation;

	// Token: 0x04003445 RID: 13381
	public float Speed;
}
