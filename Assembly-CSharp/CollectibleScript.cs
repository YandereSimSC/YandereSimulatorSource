using System;
using UnityEngine;

// Token: 0x02000241 RID: 577
public class CollectibleScript : MonoBehaviour
{
	// Token: 0x0600126C RID: 4716 RVA: 0x00086A20 File Offset: 0x00084C20
	private void Start()
	{
		if ((this.CollectibleType == CollectibleType.BasementTape && CollectibleGlobals.GetBasementTapeCollected(this.ID)) || (this.CollectibleType == CollectibleType.Manga && CollectibleGlobals.GetMangaCollected(this.ID)) || (this.CollectibleType == CollectibleType.Tape && CollectibleGlobals.GetTapeCollected(this.ID)) || (this.CollectibleType == CollectibleType.Panty && CollectibleGlobals.GetPantyPurchased(11)))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x0600126D RID: 4717 RVA: 0x00086AAC File Offset: 0x00084CAC
	public CollectibleType CollectibleType
	{
		get
		{
			if (this.Name == "HeadmasterTape")
			{
				return CollectibleType.HeadmasterTape;
			}
			if (this.Name == "BasementTape")
			{
				return CollectibleType.BasementTape;
			}
			if (this.Name == "Manga")
			{
				return CollectibleType.Manga;
			}
			if (this.Name == "Tape")
			{
				return CollectibleType.Tape;
			}
			if (this.Type == 5)
			{
				return CollectibleType.Key;
			}
			if (this.Type == 6)
			{
				return CollectibleType.Panty;
			}
			Debug.LogError("Unrecognized collectible \"" + this.Name + "\".", base.gameObject);
			return CollectibleType.Tape;
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x00086B40 File Offset: 0x00084D40
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.CollectibleType == CollectibleType.HeadmasterTape)
			{
				CollectibleGlobals.SetHeadmasterTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.BasementTape)
			{
				CollectibleGlobals.SetBasementTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Manga)
			{
				CollectibleGlobals.SetMangaCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Tape)
			{
				CollectibleGlobals.SetTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Key)
			{
				this.Prompt.Yandere.Inventory.MysteriousKeys++;
			}
			else if (this.CollectibleType == CollectibleType.Panty)
			{
				CollectibleGlobals.SetPantyPurchased(11, true);
			}
			else
			{
				Debug.LogError("Collectible \"" + this.Name + "\" not implemented.", base.gameObject);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400165A RID: 5722
	public PromptScript Prompt;

	// Token: 0x0400165B RID: 5723
	public string Name = string.Empty;

	// Token: 0x0400165C RID: 5724
	public int Type;

	// Token: 0x0400165D RID: 5725
	public int ID;
}
