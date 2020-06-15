using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200050F RID: 1295
	[RequireComponent(typeof(SpriteRenderer))]
	public class MenuButton : MonoBehaviour
	{
		// Token: 0x06002060 RID: 8288 RVA: 0x0018AB47 File Offset: 0x00188D47
		public void Init()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0018AB55 File Offset: 0x00188D55
		private void OnMouseEnter()
		{
			this.menu.SetActiveMenuButton(this.index);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0018AB68 File Offset: 0x00188D68
		public void DoClick()
		{
			switch (this.buttonType)
			{
			case MenuButton.ButtonType.Start:
				this.menu.flipBook.FlipToPage(2);
				return;
			case MenuButton.ButtonType.Controls:
				this.menu.flipBook.FlipToPage(3);
				return;
			case MenuButton.ButtonType.Credits:
				this.menu.flipBook.FlipToPage(4);
				return;
			case MenuButton.ButtonType.Exit:
				this.menu.StopInputs();
				GameController.GoToExitScene(true);
				return;
			case MenuButton.ButtonType.Easy:
				this.menu.StopInputs();
				GameController.Instance.activeDifficultyVariables = GameController.Instance.easyVariables;
				GameController.Instance.LoadScene(this.targetScene);
				SFXController.PlaySound(SFXController.Sounds.MenuConfirm);
				return;
			case MenuButton.ButtonType.Medium:
				this.menu.StopInputs();
				GameController.Instance.activeDifficultyVariables = GameController.Instance.mediumVariables;
				GameController.Instance.LoadScene(this.targetScene);
				SFXController.PlaySound(SFXController.Sounds.MenuConfirm);
				return;
			case MenuButton.ButtonType.Hard:
				this.menu.StopInputs();
				GameController.Instance.activeDifficultyVariables = GameController.Instance.hardVariables;
				GameController.Instance.LoadScene(this.targetScene);
				SFXController.PlaySound(SFXController.Sounds.MenuConfirm);
				return;
			default:
				return;
			}
		}

		// Token: 0x04003E8A RID: 16010
		public MenuButton.ButtonType buttonType;

		// Token: 0x04003E8B RID: 16011
		public SceneObject targetScene;

		// Token: 0x04003E8C RID: 16012
		[HideInInspector]
		public int index;

		// Token: 0x04003E8D RID: 16013
		[HideInInspector]
		public Menu menu;

		// Token: 0x04003E8E RID: 16014
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x0200071E RID: 1822
		public enum ButtonType
		{
			// Token: 0x04004983 RID: 18819
			Start,
			// Token: 0x04004984 RID: 18820
			Controls,
			// Token: 0x04004985 RID: 18821
			Credits,
			// Token: 0x04004986 RID: 18822
			Exit,
			// Token: 0x04004987 RID: 18823
			Easy,
			// Token: 0x04004988 RID: 18824
			Medium,
			// Token: 0x04004989 RID: 18825
			Hard
		}
	}
}
