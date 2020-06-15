using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000237 RID: 567
public class ChoiceScript : MonoBehaviour
{
	// Token: 0x06001243 RID: 4675 RVA: 0x00081E44 File Offset: 0x00080044
	private void Start()
	{
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x00081E6C File Offset: 0x0008006C
	private void Update()
	{
		this.Highlight.transform.localPosition = Vector3.Lerp(this.Highlight.transform.localPosition, new Vector3((float)(-360 + 720 * this.Selected), this.Highlight.transform.localPosition.y, this.Highlight.transform.localPosition.z), Time.deltaTime * 10f);
		if (this.Phase == 0)
		{
			this.Darkness.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 0f)
			{
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 1)
		{
			if (this.InputManager.TappedLeft)
			{
				this.Darkness.color = new Color(1f, 1f, 1f, 0f);
				this.Selected = 0;
			}
			else if (this.InputManager.TappedRight)
			{
				this.Darkness.color = new Color(0f, 0f, 0f, 0f);
				this.Selected = 1;
			}
			if (Input.GetButtonDown("A"))
			{
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 1f)
			{
				GameGlobals.LoveSick = (this.Selected == 1);
				SceneManager.LoadScene("TitleScene");
			}
		}
	}

	// Token: 0x040015A5 RID: 5541
	public InputManagerScript InputManager;

	// Token: 0x040015A6 RID: 5542
	public PromptBarScript PromptBar;

	// Token: 0x040015A7 RID: 5543
	public Transform Highlight;

	// Token: 0x040015A8 RID: 5544
	public UISprite Darkness;

	// Token: 0x040015A9 RID: 5545
	public int Selected;

	// Token: 0x040015AA RID: 5546
	public int Phase;
}
