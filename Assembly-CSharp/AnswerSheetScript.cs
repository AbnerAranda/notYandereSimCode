using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class AnswerSheetScript : MonoBehaviour
{
	// Token: 0x06000A00 RID: 2560 RVA: 0x0004F95B File Offset: 0x0004DB5B
	private void Start()
	{
		this.OriginalMesh = this.MyMesh.mesh;
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0004F990 File Offset: 0x0004DB90
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Phase == 1)
			{
				SchemeGlobals.SetSchemeStage(5, 5);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.AnswerSheet = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.DoorGap.Prompt.enabled = true;
				this.MyMesh.mesh = null;
				this.Phase++;
				return;
			}
			SchemeGlobals.SetSchemeStage(5, 8);
			this.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.AnswerSheet = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.MyMesh.mesh = this.OriginalMesh;
			this.Phase++;
		}
	}

	// Token: 0x040009EA RID: 2538
	public SchemesScript Schemes;

	// Token: 0x040009EB RID: 2539
	public DoorGapScript DoorGap;

	// Token: 0x040009EC RID: 2540
	public PromptScript Prompt;

	// Token: 0x040009ED RID: 2541
	public ClockScript Clock;

	// Token: 0x040009EE RID: 2542
	public Mesh OriginalMesh;

	// Token: 0x040009EF RID: 2543
	public MeshFilter MyMesh;

	// Token: 0x040009F0 RID: 2544
	public int Phase = 1;
}
