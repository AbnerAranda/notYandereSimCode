using System;
using System.Collections;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000514 RID: 1300
	public class FailGame : MonoBehaviour
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x0018AE1C File Offset: 0x0018901C
		public static FailGame Instance
		{
			get
			{
				if (FailGame.instance == null)
				{
					FailGame.instance = UnityEngine.Object.FindObjectOfType<FailGame>();
				}
				return FailGame.instance;
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0018AE3C File Offset: 0x0018903C
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.textRenderer = base.transform.GetChild(0).GetComponent<SpriteRenderer>();
			this.targetTransitionTime = GameController.Instance.activeDifficultyVariables.transitionTime * this.fadeMultiplier;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0018AE88 File Offset: 0x00189088
		public static void GameFailed()
		{
			FailGame.Instance.StartCoroutine(FailGame.Instance.GameFailedRoutine());
			FailGame.Instance.StartCoroutine(FailGame.Instance.SlowPitch());
			SFXController.PlaySound(SFXController.Sounds.GameFail);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0018AEBA File Offset: 0x001890BA
		private IEnumerator GameFailedRoutine()
		{
			UnityEngine.Object.FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
			yield return null;
			this.textRenderer.color = Color.white;
			while (this.transitionTime < this.targetTransitionTime)
			{
				this.transitionTime += Time.deltaTime;
				yield return null;
			}
			base.transform.GetChild(1).gameObject.SetActive(true);
			while (!Input.anyKeyDown)
			{
				yield return null;
			}
			while (this.transitionTime < this.targetTransitionTime)
			{
				this.transitionTime += Time.deltaTime;
				float a = Mathf.Lerp(0f, 1f, this.transitionTime / this.targetTransitionTime);
				this.spriteRenderer.color = new Color(0f, 0f, 0f, a);
				yield return null;
			}
			GameController.GoToExitScene(false);
			yield break;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0018AEC9 File Offset: 0x001890C9
		private IEnumerator SlowPitch()
		{
			GameStarter starter = UnityEngine.Object.FindObjectOfType<GameStarter>();
			float timeToZero = 5f;
			while (timeToZero > 0f)
			{
				starter.SetAudioPitch(Mathf.Lerp(0f, 1f, timeToZero / 5f));
				timeToZero -= Time.deltaTime;
				yield return null;
			}
			starter.SetAudioPitch(0f);
			yield break;
		}

		// Token: 0x04003E91 RID: 16017
		private static FailGame instance;

		// Token: 0x04003E92 RID: 16018
		public float fadeMultiplier = 2f;

		// Token: 0x04003E93 RID: 16019
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003E94 RID: 16020
		private SpriteRenderer textRenderer;

		// Token: 0x04003E95 RID: 16021
		private float targetTransitionTime;

		// Token: 0x04003E96 RID: 16022
		private float transitionTime;
	}
}
