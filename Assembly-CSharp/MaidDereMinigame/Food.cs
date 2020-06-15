using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004FE RID: 1278
	[CreateAssetMenu(fileName = "New Food Item", menuName = "Food")]
	public class Food : ScriptableObject
	{
		// Token: 0x04003E49 RID: 15945
		public Sprite largeSprite;

		// Token: 0x04003E4A RID: 15946
		public Sprite smallSprite;

		// Token: 0x04003E4B RID: 15947
		public float cookTimeMultiplier = 1f;
	}
}
