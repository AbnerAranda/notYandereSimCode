using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public class StreetCivilianScript : MonoBehaviour
{
	// Token: 0x06001B13 RID: 6931 RVA: 0x001118F9 File Offset: 0x0010FAF9
	private void Start()
	{
		this.Pathfinding.target = this.Destinations[0];
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x00111910 File Offset: 0x0010FB10
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Destinations[this.ID].position) < 0.55f)
		{
			this.MoveTowardsTarget(this.Destinations[this.ID].position);
			this.MyAnimation.CrossFade("f02_idle_00");
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
			this.Timer += Time.deltaTime;
			if (this.Timer > 13.5f)
			{
				this.MyAnimation.CrossFade("f02_newWalk_00");
				this.ID++;
				if (this.ID == this.Destinations.Length)
				{
					this.ID = 0;
				}
				this.Pathfinding.target = this.Destinations[this.ID];
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x00111A18 File Offset: 0x0010FC18
	public void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		if (a.sqrMagnitude > 1E-06f)
		{
			this.MyController.Move(a * (Time.deltaTime * 1f / Time.timeScale));
		}
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Destinations[this.ID].rotation, 10f * Time.deltaTime);
	}

	// Token: 0x04002C16 RID: 11286
	public CharacterController MyController;

	// Token: 0x04002C17 RID: 11287
	public Animation MyAnimation;

	// Token: 0x04002C18 RID: 11288
	public AIPath Pathfinding;

	// Token: 0x04002C19 RID: 11289
	public Transform[] Destinations;

	// Token: 0x04002C1A RID: 11290
	public float Timer;

	// Token: 0x04002C1B RID: 11291
	public int ID;
}
