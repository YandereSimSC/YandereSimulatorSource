using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004FE RID: 1278
	public class Menu : MonoBehaviour
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x00185430 File Offset: 0x00183630
		private void Start()
		{
			for (int i = 0; i < this.mainMenuButtons.Count; i++)
			{
				int index = i;
				this.mainMenuButtons[i].Init();
				this.mainMenuButtons[i].index = index;
				this.mainMenuButtons[i].spriteRenderer.enabled = false;
				this.mainMenuButtons[i].menu = this;
			}
			this.flipBook = FlipBook.Instance;
			this.SetActiveMenuButton(0);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x001854B4 File Offset: 0x001836B4
		private void Update()
		{
			if (this.cancelInputs)
			{
				return;
			}
			if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("A")) && this.activeMenuButton != null)
			{
				this.activeMenuButton.DoClick();
			}
			float num = Input.GetAxisRaw("Vertical") * -1f;
			if (Input.GetKeyDown("up") || Input.GetAxis("DpadY") > 0.5f)
			{
				num = -1f;
			}
			else if (Input.GetKeyDown("down") || Input.GetAxis("DpadY") < -0.5f)
			{
				num = 1f;
			}
			if (num != 0f && this.PreviousFrameVertical == 0f)
			{
				SFXController.PlaySound(SFXController.Sounds.MenuSelect);
			}
			this.PreviousFrameVertical = num;
			if (num != 0f)
			{
				if (!this.prevVertical)
				{
					this.prevVertical = true;
					if (num >= 0f)
					{
						int num2 = this.mainMenuButtons.IndexOf(this.activeMenuButton);
						this.SetActiveMenuButton((num2 + 1) % this.mainMenuButtons.Count);
						return;
					}
					int num3 = this.mainMenuButtons.IndexOf(this.activeMenuButton);
					if (num3 == 0)
					{
						this.SetActiveMenuButton(this.mainMenuButtons.Count - 1);
						return;
					}
					this.SetActiveMenuButton(num3 - 1);
					return;
				}
			}
			else
			{
				this.prevVertical = false;
			}
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x001855F4 File Offset: 0x001837F4
		public void SetActiveMenuButton(int index)
		{
			if (this.activeMenuButton != null)
			{
				this.activeMenuButton.spriteRenderer.enabled = false;
			}
			this.activeMenuButton = this.mainMenuButtons[index];
			this.activeMenuButton.spriteRenderer.enabled = true;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00185643 File Offset: 0x00183843
		public void StopInputs()
		{
			this.cancelInputs = true;
			this.flipBook.StopInputs();
		}

		// Token: 0x04003DD4 RID: 15828
		public List<MenuButton> mainMenuButtons;

		// Token: 0x04003DD5 RID: 15829
		[HideInInspector]
		public FlipBook flipBook;

		// Token: 0x04003DD6 RID: 15830
		private MenuButton activeMenuButton;

		// Token: 0x04003DD7 RID: 15831
		private bool prevVertical;

		// Token: 0x04003DD8 RID: 15832
		private bool cancelInputs;

		// Token: 0x04003DD9 RID: 15833
		private float PreviousFrameVertical;
	}
}
