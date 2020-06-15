using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003F2 RID: 1010
public class SponsorScript : MonoBehaviour
{
	// Token: 0x06001AEB RID: 6891 RVA: 0x0010F068 File Offset: 0x0010D268
	private void Start()
	{
		Time.timeScale = 1f;
		this.Set[1].SetActive(true);
		this.Set[2].SetActive(false);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x0010F0E0 File Offset: 0x0010D2E0
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer < 3.2f)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			return;
		}
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
		if (this.Darkness.color.a == 1f)
		{
			SceneManager.LoadScene("TitleScene");
		}
	}

	// Token: 0x04002BB0 RID: 11184
	public GameObject[] Set;

	// Token: 0x04002BB1 RID: 11185
	public UISprite Darkness;

	// Token: 0x04002BB2 RID: 11186
	public float Timer;

	// Token: 0x04002BB3 RID: 11187
	public int ID;
}
