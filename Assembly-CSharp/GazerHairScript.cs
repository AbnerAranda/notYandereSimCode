using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class GazerHairScript : MonoBehaviour
{
	// Token: 0x0600144A RID: 5194 RVA: 0x000B4544 File Offset: 0x000B2744
	private void Update()
	{
		this.ID = 0;
		while (this.ID < this.Weight.Length)
		{
			this.Weight[this.ID] = Mathf.MoveTowards(this.Weight[this.ID], this.TargetWeight[this.ID], Time.deltaTime * this.Strength);
			if (this.Weight[this.ID] == this.TargetWeight[this.ID])
			{
				this.TargetWeight[this.ID] = UnityEngine.Random.Range(0f, 100f);
			}
			this.MyMesh.SetBlendShapeWeight(this.ID, this.Weight[this.ID]);
			this.ID++;
		}
	}

	// Token: 0x04001D0C RID: 7436
	public SkinnedMeshRenderer MyMesh;

	// Token: 0x04001D0D RID: 7437
	public float[] TargetWeight;

	// Token: 0x04001D0E RID: 7438
	public float[] Weight;

	// Token: 0x04001D0F RID: 7439
	public float Strength = 100f;

	// Token: 0x04001D10 RID: 7440
	public int ID;
}
