using System;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class DumpScript : MonoBehaviour
{
	// Token: 0x06001377 RID: 4983 RVA: 0x000A7FC0 File Offset: 0x000A61C0
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 5f)
		{
			this.Incinerator.Corpses++;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001AAE RID: 6830
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04001AAF RID: 6831
	public IncineratorScript Incinerator;

	// Token: 0x04001AB0 RID: 6832
	public float Timer;
}
