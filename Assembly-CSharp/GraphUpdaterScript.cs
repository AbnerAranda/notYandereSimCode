using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class GraphUpdaterScript : MonoBehaviour
{
	// Token: 0x060016EC RID: 5868 RVA: 0x000BE93A File Offset: 0x000BCB3A
	private void Update()
	{
		if (this.Frames > 0)
		{
			this.Graph.Scan(null);
			UnityEngine.Object.Destroy(this);
		}
		this.Frames++;
	}

	// Token: 0x04001E5E RID: 7774
	public AstarPath Graph;

	// Token: 0x04001E5F RID: 7775
	public int Frames;
}
