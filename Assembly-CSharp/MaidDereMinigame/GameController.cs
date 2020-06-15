using System;
using System.Collections;
using System.Collections.Generic;
using MaidDereMinigame.Malee;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidDereMinigame
{
	// Token: 0x02000503 RID: 1283
	public class GameController : MonoBehaviour
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0018A28E File Offset: 0x0018848E
		public static GameController Instance
		{
			get
			{
				if (GameController.instance == null)
				{
					GameController.instance = UnityEngine.Object.FindObjectOfType<GameController>();
				}
				return GameController.instance;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x0018A2AC File Offset: 0x001884AC
		public static SceneWrapper Scenes
		{
			get
			{
				return GameController.Instance.scenes;
			}
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0018A2B8 File Offset: 0x001884B8
		public static void GoToExitScene(bool fadeOut = true)
		{
			GameController.Instance.StartCoroutine(GameController.Instance.FadeWithAction(delegate
			{
				PlayerGlobals.Money += GameController.Instance.totalPayout;
				if (SceneManager.GetActiveScene().name == "MaidMenuScene")
				{
					SceneManager.LoadScene("StreetScene");
					return;
				}
				SceneManager.LoadScene("CalendarScene");
			}, fadeOut, true));
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0018A2F0 File Offset: 0x001884F0
		private void Awake()
		{
			if (GameController.Instance != this)
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
				return;
			}
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0018A322 File Offset: 0x00188522
		public static void SetPause(bool toPause)
		{
			if (GameController.PauseGame != null)
			{
				GameController.PauseGame(toPause);
			}
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0018A336 File Offset: 0x00188536
		public void LoadScene(SceneObject scene)
		{
			base.StartCoroutine(this.FadeWithAction(delegate
			{
				SceneManager.LoadScene("MaidGameScene");
			}, true, false));
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0018A366 File Offset: 0x00188566
		private IEnumerator FadeWithAction(Action PostFadeAction, bool doFadeOut = true, bool destroyGameController = false)
		{
			float timeToFade = 0f;
			if (doFadeOut)
			{
				while (timeToFade <= this.activeDifficultyVariables.transitionTime)
				{
					this.spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, timeToFade / this.activeDifficultyVariables.transitionTime));
					timeToFade += Time.deltaTime;
					yield return null;
				}
				this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				timeToFade = this.activeDifficultyVariables.transitionTime;
			}
			PostFadeAction();
			if (destroyGameController)
			{
				if (GameController.Instance.whiteFadeOutPost != null && doFadeOut)
				{
					GameController.Instance.whiteFadeOutPost.color = Color.white;
				}
				UnityEngine.Object.Destroy(GameController.Instance.gameObject);
				Camera.main.farClipPlane = 0f;
				GameController.instance = null;
			}
			else
			{
				while (timeToFade >= 0f)
				{
					this.spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, timeToFade / this.activeDifficultyVariables.transitionTime));
					timeToFade -= Time.deltaTime;
					yield return null;
				}
				this.spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
			}
			yield break;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0018A38A File Offset: 0x0018858A
		public static void TimeUp()
		{
			GameController.SetPause(true);
			GameController.Instance.tipPage.Init();
			GameController.Instance.tipPage.DisplayTips(GameController.Instance.tips);
			UnityEngine.Object.FindObjectOfType<GameStarter>().GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0018A3CC File Offset: 0x001885CC
		public static void AddTip(float tip)
		{
			if (GameController.Instance.tips == null)
			{
				GameController.Instance.tips = new List<float>();
			}
			tip = Mathf.Floor(tip * 100f) / 100f;
			GameController.Instance.tips.Add(tip);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0018A418 File Offset: 0x00188618
		public static float GetTotalDollars()
		{
			float num = 0f;
			foreach (float num2 in GameController.Instance.tips)
			{
				num += Mathf.Floor(num2 * 100f) / 100f;
			}
			return num + GameController.Instance.activeDifficultyVariables.basePay;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0018A494 File Offset: 0x00188694
		public static void AddAngryCustomer()
		{
			GameController.Instance.angryCustomers++;
			if (GameController.Instance.angryCustomers >= GameController.Instance.activeDifficultyVariables.failQuantity)
			{
				FailGame.GameFailed();
				GameController.SetPause(true);
			}
		}

		// Token: 0x04003E53 RID: 15955
		private static GameController instance;

		// Token: 0x04003E54 RID: 15956
		[Reorderable]
		public Sprites numbers;

		// Token: 0x04003E55 RID: 15957
		public SceneWrapper scenes;

		// Token: 0x04003E56 RID: 15958
		[Tooltip("Scene Object Reference to return to when the game ends.")]
		public SceneObject returnScene;

		// Token: 0x04003E57 RID: 15959
		public SetupVariables activeDifficultyVariables;

		// Token: 0x04003E58 RID: 15960
		public SetupVariables easyVariables;

		// Token: 0x04003E59 RID: 15961
		public SetupVariables mediumVariables;

		// Token: 0x04003E5A RID: 15962
		public SetupVariables hardVariables;

		// Token: 0x04003E5B RID: 15963
		private List<float> tips;

		// Token: 0x04003E5C RID: 15964
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003E5D RID: 15965
		private int angryCustomers;

		// Token: 0x04003E5E RID: 15966
		[HideInInspector]
		public TipPage tipPage;

		// Token: 0x04003E5F RID: 15967
		[HideInInspector]
		public float totalPayout;

		// Token: 0x04003E60 RID: 15968
		[HideInInspector]
		public SpriteRenderer whiteFadeOutPost;

		// Token: 0x04003E61 RID: 15969
		public static BoolParameterEvent PauseGame;
	}
}
