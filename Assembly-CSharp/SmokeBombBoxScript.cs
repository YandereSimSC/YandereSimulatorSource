using System;
using UnityEngine;

// Token: 0x020003E2 RID: 994
public class SmokeBombBoxScript : MonoBehaviour
{
	// Token: 0x06001AA3 RID: 6819 RVA: 0x00109AD0 File Offset: 0x00107CD0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (!this.Amnesia)
			{
				this.Alphabet.RemainingBombs = 5;
				this.Alphabet.BombLabel.text = string.Concat(5);
			}
			else
			{
				this.Alphabet.RemainingBombs = 1;
				this.Alphabet.BombLabel.text = string.Concat(1);
			}
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Stink)
			{
				this.BombTexture.color = new Color(0f, 0.5f, 0f, 1f);
				this.Prompt.Yandere.Inventory.AmnesiaBomb = false;
				this.Prompt.Yandere.Inventory.SmokeBomb = false;
				this.Prompt.Yandere.Inventory.StinkBomb = true;
			}
			else if (this.Amnesia)
			{
				this.BombTexture.color = new Color(1f, 0.5f, 1f, 1f);
				this.Prompt.Yandere.Inventory.AmnesiaBomb = true;
				this.Prompt.Yandere.Inventory.SmokeBomb = false;
				this.Prompt.Yandere.Inventory.StinkBomb = false;
			}
			else
			{
				this.BombTexture.color = new Color(0.5f, 0.5f, 0.5f, 1f);
				this.Prompt.Yandere.Inventory.AmnesiaBomb = false;
				this.Prompt.Yandere.Inventory.StinkBomb = false;
				this.Prompt.Yandere.Inventory.SmokeBomb = true;
			}
			this.MyAudio.Play();
		}
	}

	// Token: 0x04002AD4 RID: 10964
	public AlphabetScript Alphabet;

	// Token: 0x04002AD5 RID: 10965
	public UITexture BombTexture;

	// Token: 0x04002AD6 RID: 10966
	public PromptScript Prompt;

	// Token: 0x04002AD7 RID: 10967
	public AudioSource MyAudio;

	// Token: 0x04002AD8 RID: 10968
	public bool Amnesia;

	// Token: 0x04002AD9 RID: 10969
	public bool Stink;
}
