using System;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class DanceMinigamePromptScript : MonoBehaviour
{
	// Token: 0x060012F4 RID: 4852 RVA: 0x000999CC File Offset: 0x00097BCC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.transform.position = this.PlayerLocation.position;
			this.Prompt.Yandere.transform.rotation = this.PlayerLocation.rotation;
			this.Prompt.Yandere.CharacterAnimation.Play("f02_danceMachineIdle_00");
			this.Prompt.Yandere.StudentManager.Clock.StopTime = true;
			this.Prompt.Yandere.MyController.enabled = false;
			this.Prompt.Yandere.HeartCamera.enabled = false;
			this.Prompt.Yandere.HUD.enabled = false;
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.enabled = false;
			this.Prompt.Yandere.Jukebox.LastVolume = this.Prompt.Yandere.Jukebox.Volume;
			this.Prompt.Yandere.Jukebox.Volume = 0f;
			this.Prompt.Yandere.HUD.transform.parent.gameObject.SetActive(false);
			this.Prompt.Yandere.MainCamera.gameObject.SetActive(false);
			this.OriginalRenderer.enabled = false;
			Physics.SyncTransforms();
			this.DanceMinigame.SetActive(true);
			this.DanceManager.BeginMinigame();
			this.StudentManager.DisableEveryone();
		}
	}

	// Token: 0x040018D8 RID: 6360
	public StudentManagerScript StudentManager;

	// Token: 0x040018D9 RID: 6361
	public Renderer OriginalRenderer;

	// Token: 0x040018DA RID: 6362
	public DDRManager DanceManager;

	// Token: 0x040018DB RID: 6363
	public PromptScript Prompt;

	// Token: 0x040018DC RID: 6364
	public ClockScript Clock;

	// Token: 0x040018DD RID: 6365
	public GameObject DanceMinigame;

	// Token: 0x040018DE RID: 6366
	public Transform PlayerLocation;
}
