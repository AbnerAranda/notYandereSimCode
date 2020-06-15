using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F5 RID: 1269
	public class CustomerSpawner : MonoBehaviour
	{
		// Token: 0x06001FE1 RID: 8161 RVA: 0x001886E4 File Offset: 0x001868E4
		private void Start()
		{
			this.spawnRate = GameController.Instance.activeDifficultyVariables.customerSpawnRate;
			this.spawnVariance = GameController.Instance.activeDifficultyVariables.customerSpawnVariance;
			this.isPaused = true;
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00188717 File Offset: 0x00186917
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00188739 File Offset: 0x00186939
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0018875B File Offset: 0x0018695B
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x00188764 File Offset: 0x00186964
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			if (this.timeTillSpawn <= 0f)
			{
				this.timeTillSpawn = this.spawnRate + UnityEngine.Random.Range(-this.spawnVariance, this.spawnVariance);
				this.SpawnCustomer();
				return;
			}
			this.timeTillSpawn -= Time.deltaTime;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x001887C0 File Offset: 0x001869C0
		private void SpawnCustomer()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.customerPrefabs[UnityEngine.Random.Range(0, this.customerPrefabs.Length)]);
			gameObject.transform.position = base.transform.position;
			AIController component = gameObject.GetComponent<AIController>();
			component.Init();
			component.leaveTarget = base.transform;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x00188813 File Offset: 0x00186A13
		public void OpenDoor()
		{
			base.transform.parent.GetComponent<Animator>().SetTrigger("DoorOpen");
		}

		// Token: 0x04003E07 RID: 15879
		public GameObject[] customerPrefabs;

		// Token: 0x04003E08 RID: 15880
		private float spawnRate = 10f;

		// Token: 0x04003E09 RID: 15881
		private float spawnVariance = 5f;

		// Token: 0x04003E0A RID: 15882
		private float timeTillSpawn;

		// Token: 0x04003E0B RID: 15883
		private int spawnedCustomers;

		// Token: 0x04003E0C RID: 15884
		private bool isPaused;
	}
}
