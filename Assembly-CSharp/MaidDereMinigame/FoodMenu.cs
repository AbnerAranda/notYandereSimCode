using System;
using System.Collections.Generic;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000515 RID: 1301
	public class FoodMenu : MonoBehaviour
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0018AEE4 File Offset: 0x001890E4
		public static FoodMenu Instance
		{
			get
			{
				if (FoodMenu.instance == null)
				{
					FoodMenu.instance = UnityEngine.Object.FindObjectOfType<FoodMenu>();
				}
				return FoodMenu.instance;
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0018AF04 File Offset: 0x00189104
		private void Awake()
		{
			this.SetMenuIcons();
			this.menuSelectorTarget = this.menuSlots[0].position.x;
			this.startY = this.menuSelector.position.y;
			this.startZ = this.menuSelector.position.z;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0018AF60 File Offset: 0x00189160
		public void SetMenuIcons()
		{
			this.menuSlots = new List<Transform>();
			for (int i = 0; i < this.menuSlotParent.childCount; i++)
			{
				Transform child = this.menuSlotParent.GetChild(i);
				this.menuSlots.Add(child);
				if (this.foodItems.Count >= i)
				{
					child.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.foodItems[i].largeSprite;
				}
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0018AFD7 File Offset: 0x001891D7
		public void SetActive(int index)
		{
			this.menuSelectorTarget = this.menuSlots[index].position.x;
			this.interpolator = 0f;
			this.activeIndex = index;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0018B007 File Offset: 0x00189207
		public Food GetActiveFood()
		{
			Food food = UnityEngine.Object.Instantiate<Food>(this.foodItems[this.activeIndex]);
			food.name = this.foodItems[this.activeIndex].name;
			return food;
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0018B03C File Offset: 0x0018923C
		public Food GetRandomFood()
		{
			int index = UnityEngine.Random.Range(0, this.foodItems.Count);
			Food food = UnityEngine.Object.Instantiate<Food>(this.foodItems[index]);
			food.name = this.foodItems[index].name;
			return food;
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0018B084 File Offset: 0x00189284
		private void Update()
		{
			if (this.interpolator < 1f)
			{
				float x = Mathf.Lerp(this.menuSelector.position.x, this.menuSelectorTarget, this.interpolator);
				this.menuSelector.position = new Vector3(x, this.startY, this.startZ);
				this.interpolator += Time.deltaTime * this.selectorMoveSpeed;
			}
			else
			{
				this.menuSelector.transform.position = new Vector3(this.menuSelectorTarget, this.startY, this.startZ);
			}
			if (YandereController.rightButton)
			{
				this.IncrementSelection();
				return;
			}
			if (YandereController.leftButton)
			{
				this.DecrementSelection();
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0018B13A File Offset: 0x0018933A
		private void IncrementSelection()
		{
			this.SetActive((this.activeIndex + 1) % this.menuSlots.Count);
			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0018B15D File Offset: 0x0018935D
		private void DecrementSelection()
		{
			if (this.activeIndex == 0)
			{
				this.SetActive(this.menuSlots.Count - 1);
			}
			else
			{
				this.SetActive(this.activeIndex - 1);
			}
			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
		}

		// Token: 0x04003E97 RID: 16023
		private static FoodMenu instance;

		// Token: 0x04003E98 RID: 16024
		[Reorderable]
		public Foods foodItems;

		// Token: 0x04003E99 RID: 16025
		public Transform menuSelector;

		// Token: 0x04003E9A RID: 16026
		public Transform menuSlotParent;

		// Token: 0x04003E9B RID: 16027
		public float selectorMoveSpeed = 3f;

		// Token: 0x04003E9C RID: 16028
		private List<Transform> menuSlots;

		// Token: 0x04003E9D RID: 16029
		private float menuSelectorTarget;

		// Token: 0x04003E9E RID: 16030
		private float startY;

		// Token: 0x04003E9F RID: 16031
		private float startZ;

		// Token: 0x04003EA0 RID: 16032
		private float interpolator;

		// Token: 0x04003EA1 RID: 16033
		private int activeIndex;
	}
}
