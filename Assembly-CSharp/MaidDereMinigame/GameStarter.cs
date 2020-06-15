﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000506 RID: 1286
	public class GameStarter : MonoBehaviour
	{
		// Token: 0x0600203C RID: 8252 RVA: 0x0018A578 File Offset: 0x00188778
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.audioSource = base.GetComponent<AudioSource>();
			base.StartCoroutine(this.CountdownToStart());
			GameController.Instance.tipPage = this.tipPage;
			GameController.Instance.whiteFadeOutPost = this.whiteFadeOutPost;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0018A5CC File Offset: 0x001887CC
		public void SetGameTime(float gameTime)
		{
			int num = Mathf.CeilToInt(gameTime);
			if ((float)num == 10f)
			{
				SFXController.PlaySound(SFXController.Sounds.ClockTick);
			}
			if (gameTime > 3f)
			{
				return;
			}
			if (num - 1 <= 2)
			{
				this.spriteRenderer.sprite = this.numbers[num];
				return;
			}
			this.EndGame();
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0018A61C File Offset: 0x0018881C
		public void EndGame()
		{
			base.StartCoroutine(this.EndGameRoutine());
			SFXController.PlaySound(SFXController.Sounds.GameSuccess);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0018A631 File Offset: 0x00188831
		private IEnumerator CountdownToStart()
		{
			yield return new WaitForSeconds(GameController.Instance.activeDifficultyVariables.transitionTime);
			SFXController.PlaySound(SFXController.Sounds.Countdown);
			while (this.timeToStart > 0)
			{
				yield return new WaitForSeconds(1f);
				this.timeToStart--;
				this.spriteRenderer.sprite = this.numbers[this.timeToStart];
			}
			yield return new WaitForSeconds(1f);
			GameController.SetPause(false);
			this.spriteRenderer.sprite = null;
			yield break;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0018A640 File Offset: 0x00188840
		private IEnumerator EndGameRoutine()
		{
			GameController.SetPause(true);
			this.spriteRenderer.sprite = this.timeUp;
			yield return new WaitForSeconds(1f);
			UnityEngine.Object.FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
			GameController.TimeUp();
			yield break;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0018A64F File Offset: 0x0018884F
		public void SetAudioPitch(float value)
		{
			this.audioSource.pitch = value;
		}

		// Token: 0x04003E6F RID: 15983
		public List<Sprite> numbers;

		// Token: 0x04003E70 RID: 15984
		public SpriteRenderer whiteFadeOutPost;

		// Token: 0x04003E71 RID: 15985
		public Sprite timeUp;

		// Token: 0x04003E72 RID: 15986
		public TipPage tipPage;

		// Token: 0x04003E73 RID: 15987
		private AudioSource audioSource;

		// Token: 0x04003E74 RID: 15988
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003E75 RID: 15989
		private int timeToStart = 3;
	}
}
