using System;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class WeaponManagerScript : MonoBehaviour
{
	// Token: 0x06001CFF RID: 7423 RVA: 0x001582C4 File Offset: 0x001564C4
	public void Start()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			this.Weapons[i].GlobalID = i;
			if (WeaponGlobals.GetWeaponStatus(i) == 1)
			{
				this.Weapons[i].gameObject.SetActive(false);
			}
		}
		this.ChangeBloodTexture();
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x00158314 File Offset: 0x00156514
	public void UpdateLabels()
	{
		WeaponScript[] weapons = this.Weapons;
		for (int i = 0; i < weapons.Length; i++)
		{
			weapons[i].UpdateLabel();
		}
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x00158340 File Offset: 0x00156540
	public void CheckWeapons()
	{
		this.MurderWeapons = 0;
		this.Fingerprints = 0;
		for (int i = 0; i < this.Victims.Length; i++)
		{
			this.Victims[i] = 0;
		}
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null && weaponScript.Blood.enabled && !weaponScript.AlreadyExamined)
			{
				this.MurderWeapons++;
				if (weaponScript.FingerprintID > 0)
				{
					this.Fingerprints++;
					for (int k = 0; k < weaponScript.Victims.Length; k++)
					{
						if (weaponScript.Victims[k])
						{
							this.Victims[k] = weaponScript.FingerprintID;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x00158408 File Offset: 0x00156608
	public void CleanWeapons()
	{
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null)
			{
				weaponScript.Blood.enabled = false;
				weaponScript.FingerprintID = 0;
			}
		}
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x0015844C File Offset: 0x0015664C
	public void ChangeBloodTexture()
	{
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null)
			{
				if (!GameGlobals.CensorBlood)
				{
					weaponScript.Blood.material.mainTexture = this.Blood;
					weaponScript.Blood.material.SetColor("_TintColor", new Color(0.25f, 0.25f, 0.25f, 0.5f));
				}
				else
				{
					weaponScript.Blood.material.mainTexture = this.Flower;
					weaponScript.Blood.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
				}
			}
		}
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x00158518 File Offset: 0x00156718
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			this.CheckWeapons();
			for (int i = 0; i < this.Victims.Length; i++)
			{
				if (this.Victims[i] != 0)
				{
					if (this.Victims[i] == 100)
					{
						Debug.Log("The student named " + this.JSON.Students[i].Name + " was killed by Yandere-chan!");
					}
					else
					{
						Debug.Log(string.Concat(new string[]
						{
							"The student named ",
							this.JSON.Students[i].Name,
							" was killed by ",
							this.JSON.Students[this.Victims[i]].Name,
							"!"
						}));
					}
				}
			}
		}
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x001585EC File Offset: 0x001567EC
	public void TrackDumpedWeapons()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			if (this.Weapons[i] == null)
			{
				Debug.Log("Weapon #" + i + " was destroyed! Setting status to 1!");
			}
		}
	}

	// Token: 0x04003664 RID: 13924
	public WeaponScript[] DelinquentWeapons;

	// Token: 0x04003665 RID: 13925
	public WeaponScript[] Weapons;

	// Token: 0x04003666 RID: 13926
	public JsonScript JSON;

	// Token: 0x04003667 RID: 13927
	public int[] Victims;

	// Token: 0x04003668 RID: 13928
	public int MisplacedWeapons;

	// Token: 0x04003669 RID: 13929
	public int MurderWeapons;

	// Token: 0x0400366A RID: 13930
	public int Fingerprints;

	// Token: 0x0400366B RID: 13931
	public Texture Flower;

	// Token: 0x0400366C RID: 13932
	public Texture Blood;

	// Token: 0x0400366D RID: 13933
	public bool YandereGuilty;
}
