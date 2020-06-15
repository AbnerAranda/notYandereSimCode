using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000516 RID: 1302
	public class InteractionMenu : MonoBehaviour
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0018B1A4 File Offset: 0x001893A4
		public static InteractionMenu Instance
		{
			get
			{
				if (InteractionMenu.instance == null)
				{
					InteractionMenu.instance = UnityEngine.Object.FindObjectOfType<InteractionMenu>();
				}
				return InteractionMenu.instance;
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0018B1C2 File Offset: 0x001893C2
		private void Awake()
		{
			InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
			InteractionMenu.SetBButton(false);
			InteractionMenu.SetADButton(true);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0018B1D8 File Offset: 0x001893D8
		public static void SetAButton(InteractionMenu.AButtonText text)
		{
			for (int i = 0; i < InteractionMenu.Instance.aButtonSprites.Length; i++)
			{
				if (i == (int)text)
				{
					InteractionMenu.Instance.aButtonSprites[i].gameObject.SetActive(true);
				}
				else
				{
					InteractionMenu.Instance.aButtonSprites[i].gameObject.SetActive(false);
				}
			}
			SpriteRenderer[] array = InteractionMenu.Instance.aButtons;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].gameObject.SetActive(text != InteractionMenu.AButtonText.None);
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0018B260 File Offset: 0x00189460
		public static void SetBButton(bool on)
		{
			SpriteRenderer[] array = InteractionMenu.Instance.backButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(on);
			}
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0018B294 File Offset: 0x00189494
		public static void SetADButton(bool on)
		{
			SpriteRenderer[] array = InteractionMenu.Instance.moveButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(on);
			}
		}

		// Token: 0x04003EA2 RID: 16034
		private static InteractionMenu instance;

		// Token: 0x04003EA3 RID: 16035
		public GameObject interactObject;

		// Token: 0x04003EA4 RID: 16036
		public GameObject backObject;

		// Token: 0x04003EA5 RID: 16037
		public GameObject moveObject;

		// Token: 0x04003EA6 RID: 16038
		public SpriteRenderer[] aButtons;

		// Token: 0x04003EA7 RID: 16039
		public SpriteRenderer[] aButtonSprites;

		// Token: 0x04003EA8 RID: 16040
		public SpriteRenderer[] backButtons;

		// Token: 0x04003EA9 RID: 16041
		public SpriteRenderer[] moveButtons;

		// Token: 0x02000721 RID: 1825
		public enum AButtonText
		{
			// Token: 0x04004992 RID: 18834
			ChoosePlate,
			// Token: 0x04004993 RID: 18835
			GrabPlate,
			// Token: 0x04004994 RID: 18836
			KitchenMenu,
			// Token: 0x04004995 RID: 18837
			PlaceOrder,
			// Token: 0x04004996 RID: 18838
			TakeOrder,
			// Token: 0x04004997 RID: 18839
			TossPlate,
			// Token: 0x04004998 RID: 18840
			GiveFood,
			// Token: 0x04004999 RID: 18841
			None
		}
	}
}
