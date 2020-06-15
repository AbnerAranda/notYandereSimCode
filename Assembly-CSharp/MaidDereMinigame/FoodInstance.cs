using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000500 RID: 1280
	[RequireComponent(typeof(SpriteRenderer))]
	public class FoodInstance : MonoBehaviour
	{
		// Token: 0x06002023 RID: 8227 RVA: 0x0018A107 File Offset: 0x00188307
		private void Start()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.spriteRenderer.sprite = this.food.smallSprite;
			this.heat = this.timeToCool;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0018A137 File Offset: 0x00188337
		private void Update()
		{
			this.heat -= Time.deltaTime;
			this.warmthMeter.SetFill(this.heat / this.timeToCool);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0018A163 File Offset: 0x00188363
		public void SetHeat(float newHeat)
		{
			this.heat = newHeat;
		}

		// Token: 0x04003E4C RID: 15948
		public Food food;

		// Token: 0x04003E4D RID: 15949
		public Meter warmthMeter;

		// Token: 0x04003E4E RID: 15950
		public float timeToCool = 30f;

		// Token: 0x04003E4F RID: 15951
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x04003E50 RID: 15952
		private float heat;
	}
}
