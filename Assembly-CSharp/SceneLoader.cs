using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003CA RID: 970
public class SceneLoader : MonoBehaviour
{
	// Token: 0x06001A5B RID: 6747 RVA: 0x001031E8 File Offset: 0x001013E8
	private void Start()
	{
		Time.timeScale = 1f;
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
			PlayerGlobals.Money = 10f;
		}
		if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = new Color(0f, 0f, 0f, 1f);
			this.loadingText.color = new Color(1f, 0f, 0f, 1f);
			this.crashText.color = new Color(1f, 0f, 0f, 1f);
			this.KeyboardGraphic.color = new Color(1f, 0f, 0f, 1f);
			this.ControllerLines.color = new Color(1f, 0f, 0f, 1f);
			this.LightAnimation.SetActive(false);
			this.DarkAnimation.SetActive(true);
			for (int i = 1; i < this.ControllerText.Length; i++)
			{
				this.ControllerText[i].color = new Color(1f, 0f, 0f, 1f);
			}
			for (int i = 1; i < this.KeyboardText.Length; i++)
			{
				this.KeyboardText[i].color = new Color(1f, 0f, 0f, 1f);
			}
		}
		if (PlayerGlobals.UsingGamepad)
		{
			this.Keyboard.SetActive(false);
			this.Gamepad.SetActive(true);
		}
		if (!this.Debugging)
		{
			base.StartCoroutine(this.LoadNewScene());
		}
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x001033A7 File Offset: 0x001015A7
	private void Update()
	{
		if (this.Debugging)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 10f)
			{
				this.Debugging = false;
				base.StartCoroutine(this.LoadNewScene());
			}
		}
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x001033E4 File Offset: 0x001015E4
	private IEnumerator LoadNewScene()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("SchoolScene");
		while (!async.isDone)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x040029A8 RID: 10664
	[SerializeField]
	private UILabel loadingText;

	// Token: 0x040029A9 RID: 10665
	[SerializeField]
	private UILabel crashText;

	// Token: 0x040029AA RID: 10666
	private float timer;

	// Token: 0x040029AB RID: 10667
	public UILabel[] ControllerText;

	// Token: 0x040029AC RID: 10668
	public UILabel[] KeyboardText;

	// Token: 0x040029AD RID: 10669
	public GameObject LightAnimation;

	// Token: 0x040029AE RID: 10670
	public GameObject DarkAnimation;

	// Token: 0x040029AF RID: 10671
	public GameObject Keyboard;

	// Token: 0x040029B0 RID: 10672
	public GameObject Gamepad;

	// Token: 0x040029B1 RID: 10673
	public UITexture ControllerLines;

	// Token: 0x040029B2 RID: 10674
	public UITexture KeyboardGraphic;

	// Token: 0x040029B3 RID: 10675
	public bool Debugging;

	// Token: 0x040029B4 RID: 10676
	public float Timer;
}
