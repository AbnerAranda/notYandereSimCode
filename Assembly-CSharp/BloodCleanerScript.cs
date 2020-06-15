using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class BloodCleanerScript : MonoBehaviour
{
	// Token: 0x06000A51 RID: 2641 RVA: 0x00055508 File Offset: 0x00053708
	private void Start()
	{
		Physics.IgnoreLayerCollision(11, 15, true);
		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0005552C File Offset: 0x0005372C
	private void Update()
	{
		if (this.Blood < 100f)
		{
			if (this.BloodParent.childCount > 0)
			{
				this.Pathfinding.target = this.BloodParent.GetChild(0);
				this.Pathfinding.speed = 4f;
				if (this.Pathfinding.target.position.y < 4f)
				{
					this.Label.text = "1";
				}
				else if (this.Pathfinding.target.position.y < 8f)
				{
					this.Label.text = "2";
				}
				else if (this.Pathfinding.target.position.y < 12f)
				{
					this.Label.text = "3";
				}
				else
				{
					this.Label.text = "R";
				}
				if (this.Pathfinding.target != null)
				{
					this.Distance = Vector3.Distance(base.transform.position, this.Pathfinding.target.position);
					if (this.Distance >= 1f)
					{
						this.Pathfinding.speed = 4f;
						return;
					}
					this.Pathfinding.speed = 0f;
					Transform child = this.BloodParent.GetChild(0);
					if (!(child.GetComponent("BloodPoolScript") != null))
					{
						UnityEngine.Object.Destroy(child.gameObject);
						return;
					}
					child.localScale = new Vector3(child.localScale.x - Time.deltaTime, child.localScale.y - Time.deltaTime, child.localScale.z);
					this.Blood += Time.deltaTime;
					if (this.Blood >= 100f)
					{
						this.Lens.SetActive(true);
					}
					if (child.transform.localScale.x < 0.1f)
					{
						UnityEngine.Object.Destroy(child.gameObject);
						return;
					}
				}
			}
			else if (this.Super)
			{
				this.Pathfinding.target = this.Prompt.Yandere.transform;
				this.Pathfinding.speed = 4f;
			}
		}
	}

	// Token: 0x04000A9F RID: 2719
	public Transform BloodParent;

	// Token: 0x04000AA0 RID: 2720
	public PromptScript Prompt;

	// Token: 0x04000AA1 RID: 2721
	public AIPath Pathfinding;

	// Token: 0x04000AA2 RID: 2722
	public GameObject Lens;

	// Token: 0x04000AA3 RID: 2723
	public UILabel Label;

	// Token: 0x04000AA4 RID: 2724
	public float Distance;

	// Token: 0x04000AA5 RID: 2725
	public float Blood;

	// Token: 0x04000AA6 RID: 2726
	public bool Super;
}
