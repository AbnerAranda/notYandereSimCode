using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200048C RID: 1164
public class YanvaniaTryAgainScript : MonoBehaviour
{
	// Token: 0x06001E02 RID: 7682 RVA: 0x00177EB4 File Offset: 0x001760B4
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x00177EC8 File Offset: 0x001760C8
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (base.transform.localScale.x > 0.9f)
			{
				if (this.InputManager.TappedLeft)
				{
					this.Selected = 1;
				}
				if (this.InputManager.TappedRight)
				{
					this.Selected = 2;
				}
				if (this.Selected == 1)
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, -100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, -1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				else
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, 100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, 1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				if (Input.GetButtonDown("A") || Input.GetKeyDown("z") || Input.GetKeyDown("x"))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.Highlight.position, Quaternion.identity);
					gameObject.transform.parent = this.Highlight;
					gameObject.transform.localPosition = Vector3.zero;
					this.FadeOut = true;
					return;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Selected == 1)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					return;
				}
				SceneManager.LoadScene("YanvaniaTitleScene");
			}
		}
	}

	// Token: 0x04003BA5 RID: 15269
	public InputManagerScript InputManager;

	// Token: 0x04003BA6 RID: 15270
	public GameObject ButtonEffect;

	// Token: 0x04003BA7 RID: 15271
	public Transform Highlight;

	// Token: 0x04003BA8 RID: 15272
	public UISprite Darkness;

	// Token: 0x04003BA9 RID: 15273
	public bool FadeOut;

	// Token: 0x04003BAA RID: 15274
	public int Selected = 1;
}
