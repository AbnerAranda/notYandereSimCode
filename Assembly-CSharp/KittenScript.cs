using System;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class KittenScript : MonoBehaviour
{
	// Token: 0x060017EC RID: 6124 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Start()
	{
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void PickRandomAnim()
	{
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000D352C File Offset: 0x000D172C
	private void LateUpdate()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			if (!this.Yandere.Aiming)
			{
				Vector3 b = (this.Yandere.Head.transform.position.x < base.transform.position.x) ? this.Yandere.Head.transform.position : (base.transform.position + base.transform.forward + base.transform.up * 0.139854f);
				this.Target.position = Vector3.Lerp(this.Target.position, b, Time.deltaTime * 5f);
				this.Head.transform.LookAt(this.Target);
				return;
			}
			this.Head.transform.LookAt(this.Yandere.transform.position + Vector3.up * this.Head.position.y);
		}
	}

	// Token: 0x0400224C RID: 8780
	public YandereScript Yandere;

	// Token: 0x0400224D RID: 8781
	public GameObject Character;

	// Token: 0x0400224E RID: 8782
	public string[] AnimationNames;

	// Token: 0x0400224F RID: 8783
	public Transform Target;

	// Token: 0x04002250 RID: 8784
	public Transform Head;

	// Token: 0x04002251 RID: 8785
	public string CurrentAnim = string.Empty;

	// Token: 0x04002252 RID: 8786
	public string IdleAnim = string.Empty;

	// Token: 0x04002253 RID: 8787
	public bool Wait;

	// Token: 0x04002254 RID: 8788
	public float Timer;
}
