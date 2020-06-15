using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000518 RID: 1304
	public class Timer : Meter
	{
		// Token: 0x06002088 RID: 8328 RVA: 0x0018B33B File Offset: 0x0018953B
		private void Awake()
		{
			this.gameTime = GameController.Instance.activeDifficultyVariables.gameTime;
			this.starter = UnityEngine.Object.FindObjectOfType<GameStarter>();
			this.isPaused = true;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0018B364 File Offset: 0x00189564
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0018B386 File Offset: 0x00189586
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0018B3A8 File Offset: 0x001895A8
		public void SetPause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0018B3B4 File Offset: 0x001895B4
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			this.gameTime -= Time.deltaTime;
			base.SetFill(this.gameTime / GameController.Instance.activeDifficultyVariables.gameTime);
			this.starter.SetGameTime(this.gameTime);
		}

		// Token: 0x04003EAD RID: 16045
		private GameStarter starter;

		// Token: 0x04003EAE RID: 16046
		private float gameTime;

		// Token: 0x04003EAF RID: 16047
		private bool isPaused;
	}
}
