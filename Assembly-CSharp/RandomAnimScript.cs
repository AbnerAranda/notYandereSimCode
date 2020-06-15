using System;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class RandomAnimScript : MonoBehaviour
{
	// Token: 0x060019A3 RID: 6563 RVA: 0x000FAF69 File Offset: 0x000F9169
	private void Start()
	{
		this.PickRandomAnim();
		base.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x000FAF84 File Offset: 0x000F9184
	private void Update()
	{
		AnimationState animationState = base.GetComponent<Animation>()[this.CurrentAnim];
		if (animationState.time >= animationState.length)
		{
			this.PickRandomAnim();
		}
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x000FAFB7 File Offset: 0x000F91B7
	private void PickRandomAnim()
	{
		this.CurrentAnim = this.AnimationNames[UnityEngine.Random.Range(0, this.AnimationNames.Length)];
		base.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}

	// Token: 0x040027B3 RID: 10163
	public string[] AnimationNames;

	// Token: 0x040027B4 RID: 10164
	public string CurrentAnim = string.Empty;
}
