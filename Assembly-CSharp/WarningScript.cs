using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000464 RID: 1124
public class WarningScript : MonoBehaviour
{
	// Token: 0x06001D21 RID: 7457 RVA: 0x0015B59C File Offset: 0x0015979C
	private void Start()
	{
		this.WarningLabel.gameObject.SetActive(false);
		this.Label.text = string.Empty;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x0015B610 File Offset: 0x00159810
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (this.Timer == 0f)
				{
					this.WarningLabel.gameObject.SetActive(true);
					component.Play();
				}
				this.Timer += Time.deltaTime;
				if (this.ID < this.Triggers.Length && this.Timer > this.Triggers[this.ID])
				{
					this.Label.text = this.Text[this.ID];
					this.ID++;
				}
			}
		}
		else
		{
			component.volume = Mathf.MoveTowards(component.volume, 0f, Time.deltaTime);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				SceneManager.LoadScene("SponsorScene");
			}
		}
		if (Input.anyKey)
		{
			this.FadeOut = true;
		}
	}

	// Token: 0x040036D1 RID: 14033
	public float[] Triggers;

	// Token: 0x040036D2 RID: 14034
	public string[] Text;

	// Token: 0x040036D3 RID: 14035
	public UILabel WarningLabel;

	// Token: 0x040036D4 RID: 14036
	public UISprite Darkness;

	// Token: 0x040036D5 RID: 14037
	public UILabel Label;

	// Token: 0x040036D6 RID: 14038
	public bool FadeOut;

	// Token: 0x040036D7 RID: 14039
	public float Timer;

	// Token: 0x040036D8 RID: 14040
	public int ID;
}
