using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004FF RID: 1279
	[RequireComponent(typeof(SpriteRenderer))]
	public class MenuButton : MonoBehaviour
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x00185657 File Offset: 0x00183857
		public void Init()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00185665 File Offset: 0x00183865
		private void OnMouseEnter()
		{
			this.menu.SetActiveMenuButton(this.index);
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00185678 File Offset: 0x00183878
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

		// Token: 0x04003DDA RID: 15834
		public MenuButton.ButtonType buttonType;

		// Token: 0x04003DDB RID: 15835
		public SceneObject targetScene;

		// Token: 0x04003DDC RID: 15836
		[HideInInspector]
		public int index;

		// Token: 0x04003DDD RID: 15837
		[HideInInspector]
		public Menu menu;

		// Token: 0x04003DDE RID: 15838
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x02000705 RID: 1797
		public enum ButtonType
		{
			// Token: 0x0400489A RID: 18586
			Start,
			// Token: 0x0400489B RID: 18587
			Controls,
			// Token: 0x0400489C RID: 18588
			Credits,
			// Token: 0x0400489D RID: 18589
			Exit,
			// Token: 0x0400489E RID: 18590
			Easy,
			// Token: 0x0400489F RID: 18591
			Medium,
			// Token: 0x040048A0 RID: 18592
			Hard
		}
	}
}
