using System;
using Pathfinding;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class NyanDroidScript : MonoBehaviour
{
	// Token: 0x0600189F RID: 6303 RVA: 0x000E215C File Offset: 0x000E035C
	private void Start()
	{
		this.OriginalPosition = base.transform.position;
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000E2170 File Offset: 0x000E0370
	private void Update()
	{
		if (!this.Pathfinding.canSearch)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Stop";
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				return;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Timer = 0f;
				base.transform.position += new Vector3(0f, 0.0001f, 0f);
				if (base.transform.position.y < 0f)
				{
					base.transform.position = new Vector3(base.transform.position.x, 0.001f, base.transform.position.z);
				}
				Physics.SyncTransforms();
			}
			if (Input.GetButtonDown("RB"))
			{
				base.transform.position = this.OriginalPosition;
			}
			if (Vector3.Distance(base.transform.position, this.Pathfinding.target.position) <= 1f)
			{
				this.Character.CrossFade(this.Prefix + "_Idle");
				this.Pathfinding.speed = 0f;
			}
			else if (Vector3.Distance(base.transform.position, this.Pathfinding.target.position) <= 2f)
			{
				this.Character.CrossFade(this.Prefix + "_Walk");
				this.Pathfinding.speed = 0.5f;
			}
			else
			{
				this.Character.CrossFade(this.Prefix + "_Run");
				this.Pathfinding.speed = 5f;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Follow";
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Character.CrossFade(this.Prefix + "_Idle");
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
			}
		}
	}

	// Token: 0x04002447 RID: 9287
	public Animation Character;

	// Token: 0x04002448 RID: 9288
	public PromptScript Prompt;

	// Token: 0x04002449 RID: 9289
	public AIPath Pathfinding;

	// Token: 0x0400244A RID: 9290
	public Vector3 OriginalPosition;

	// Token: 0x0400244B RID: 9291
	public string Prefix;

	// Token: 0x0400244C RID: 9292
	public float Timer;
}
