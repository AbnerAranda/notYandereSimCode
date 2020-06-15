using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F3 RID: 1267
	[RequireComponent(typeof(Animator))]
	public class Chef : MonoBehaviour
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x00188398 File Offset: 0x00186598
		public static Chef Instance
		{
			get
			{
				if (Chef.instance == null)
				{
					Chef.instance = UnityEngine.Object.FindObjectOfType<Chef>();
				}
				return Chef.instance;
			}
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x001883B6 File Offset: 0x001865B6
		private void Awake()
		{
			this.cookQueue = new Foods();
			this.animator = base.GetComponent<Animator>();
			this.cookMeter.gameObject.SetActive(false);
			this.isPaused = true;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x001883E7 File Offset: 0x001865E7
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x00188409 File Offset: 0x00186609
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0018842B File Offset: 0x0018662B
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
			this.animator.speed = (float)(this.isPaused ? 0 : 1);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0018844C File Offset: 0x0018664C
		public static void AddToQueue(Food foodItem)
		{
			Chef.Instance.cookQueue.Add(foodItem);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0018845E File Offset: 0x0018665E
		public static Food GrabFromQueue()
		{
			Food result = Chef.Instance.cookQueue[0];
			Chef.Instance.cookQueue.RemoveAt(0);
			return result;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00188480 File Offset: 0x00186680
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			Chef.ChefState chefState = this.state;
			if (chefState != Chef.ChefState.Queueing)
			{
				if (chefState != Chef.ChefState.Cooking)
				{
					return;
				}
				if (this.timeToFinishDish <= 0f)
				{
					this.state = Chef.ChefState.Delivering;
					this.animator.SetTrigger("PlateCooked");
					this.cookMeter.gameObject.SetActive(false);
					return;
				}
				this.timeToFinishDish -= Time.deltaTime;
				this.cookMeter.SetFill(1f - this.timeToFinishDish / (this.currentPlate.cookTimeMultiplier * this.cookTime));
			}
			else if (this.cookQueue.Count > 0)
			{
				this.currentPlate = Chef.GrabFromQueue();
				this.timeToFinishDish = this.currentPlate.cookTimeMultiplier * this.cookTime;
				this.state = Chef.ChefState.Cooking;
				this.cookMeter.gameObject.SetActive(true);
				return;
			}
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00188564 File Offset: 0x00186764
		public void Deliver()
		{
			UnityEngine.Object.FindObjectOfType<ServingCounter>().AddPlate(this.currentPlate);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00188576 File Offset: 0x00186776
		public void Queue()
		{
			this.state = Chef.ChefState.Queueing;
		}

		// Token: 0x04003DFB RID: 15867
		private static Chef instance;

		// Token: 0x04003DFC RID: 15868
		[Reorderable]
		public Foods cookQueue;

		// Token: 0x04003DFD RID: 15869
		public FoodMenu foodMenu;

		// Token: 0x04003DFE RID: 15870
		public Meter cookMeter;

		// Token: 0x04003DFF RID: 15871
		public float cookTime = 3f;

		// Token: 0x04003E00 RID: 15872
		private Chef.ChefState state;

		// Token: 0x04003E01 RID: 15873
		private Food currentPlate;

		// Token: 0x04003E02 RID: 15874
		private Animator animator;

		// Token: 0x04003E03 RID: 15875
		private float timeToFinishDish;

		// Token: 0x04003E04 RID: 15876
		private bool isPaused;

		// Token: 0x02000714 RID: 1812
		public enum ChefState
		{
			// Token: 0x04004949 RID: 18761
			Queueing,
			// Token: 0x0400494A RID: 18762
			Cooking,
			// Token: 0x0400494B RID: 18763
			Delivering
		}
	}
}
