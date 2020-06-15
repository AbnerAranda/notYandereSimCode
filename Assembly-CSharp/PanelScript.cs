using System;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class PanelScript : MonoBehaviour
{
	// Token: 0x060018BE RID: 6334 RVA: 0x000E357C File Offset: 0x000E177C
	private void Update()
	{
		if (this.Player.position.z > this.StairsZ || this.Player.position.z < -this.StairsZ)
		{
			this.Floor = "Stairs";
		}
		else if (this.Player.position.y < this.Floor1Height)
		{
			this.Floor = "First Floor";
		}
		else if (this.Player.position.y > this.Floor1Height && this.Player.position.y < this.Floor2Height)
		{
			this.Floor = "Second Floor";
		}
		else if (this.Player.position.y > this.Floor2Height && this.Player.position.y < this.Floor3Height)
		{
			this.Floor = "Third Floor";
		}
		else
		{
			this.Floor = "Rooftop";
		}
		if (this.Player.position.z < this.PracticeBuildingZ)
		{
			this.BuildingLabel.text = "Practice Building, " + this.Floor;
		}
		else
		{
			this.BuildingLabel.text = "Classroom Building, " + this.Floor;
		}
		this.DoorBox.Show = false;
	}

	// Token: 0x04002493 RID: 9363
	public UILabel BuildingLabel;

	// Token: 0x04002494 RID: 9364
	public DoorBoxScript DoorBox;

	// Token: 0x04002495 RID: 9365
	public Transform Player;

	// Token: 0x04002496 RID: 9366
	public string Floor = string.Empty;

	// Token: 0x04002497 RID: 9367
	public float PracticeBuildingZ;

	// Token: 0x04002498 RID: 9368
	public float StairsZ;

	// Token: 0x04002499 RID: 9369
	public float Floor1Height;

	// Token: 0x0400249A RID: 9370
	public float Floor2Height;

	// Token: 0x0400249B RID: 9371
	public float Floor3Height;
}
