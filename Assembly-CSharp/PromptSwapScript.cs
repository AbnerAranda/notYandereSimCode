using System;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class PromptSwapScript : MonoBehaviour
{
	// Token: 0x0600197C RID: 6524 RVA: 0x000F7062 File Offset: 0x000F5262
	private void Awake()
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = UnityEngine.Object.FindObjectOfType<InputDeviceScript>();
		}
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x000F7080 File Offset: 0x000F5280
	public void UpdateSpriteType(InputDeviceType deviceType)
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = UnityEngine.Object.FindObjectOfType<InputDeviceScript>();
		}
		if (deviceType == InputDeviceType.Gamepad)
		{
			this.MySprite.spriteName = this.GamepadName;
			if (this.MyLetter != null)
			{
				this.MyLetter.text = "";
				return;
			}
		}
		else
		{
			this.MySprite.spriteName = this.KeyboardName;
			if (this.MyLetter != null)
			{
				this.MyLetter.text = this.KeyboardLetter;
			}
		}
	}

	// Token: 0x04002723 RID: 10019
	public InputDeviceScript InputDevice;

	// Token: 0x04002724 RID: 10020
	public UISprite MySprite;

	// Token: 0x04002725 RID: 10021
	public UILabel MyLetter;

	// Token: 0x04002726 RID: 10022
	public string KeyboardLetter = string.Empty;

	// Token: 0x04002727 RID: 10023
	public string KeyboardName = string.Empty;

	// Token: 0x04002728 RID: 10024
	public string GamepadName = string.Empty;
}
