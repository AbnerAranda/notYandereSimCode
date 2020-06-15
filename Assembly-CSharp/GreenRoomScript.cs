using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class GreenRoomScript : MonoBehaviour
{
	// Token: 0x060016EE RID: 5870 RVA: 0x000BE965 File Offset: 0x000BCB65
	private void Start()
	{
		this.QualityManager.Obscurance.enabled = false;
		this.UpdateColor();
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x000BE97E File Offset: 0x000BCB7E
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.UpdateColor();
		}
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000BE994 File Offset: 0x000BCB94
	private void UpdateColor()
	{
		this.ID++;
		if (this.ID > 7)
		{
			this.ID = 0;
		}
		this.Renderers[0].material.color = this.Colors[this.ID];
		this.Renderers[1].material.color = this.Colors[this.ID];
	}

	// Token: 0x04001E60 RID: 7776
	public QualityManagerScript QualityManager;

	// Token: 0x04001E61 RID: 7777
	public Color[] Colors;

	// Token: 0x04001E62 RID: 7778
	public Renderer[] Renderers;

	// Token: 0x04001E63 RID: 7779
	public int ID;
}
