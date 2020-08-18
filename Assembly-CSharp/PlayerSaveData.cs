using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B8 RID: 952
[Serializable]
public class PlayerSaveData
{
	// Token: 0x06001A07 RID: 6663 RVA: 0x000FDC0C File Offset: 0x000FBE0C
	public static PlayerSaveData ReadFromGlobals()
	{
		PlayerSaveData playerSaveData = new PlayerSaveData();
		playerSaveData.alerts = PlayerGlobals.Alerts;
		playerSaveData.enlightenment = PlayerGlobals.Enlightenment;
		playerSaveData.enlightenmentBonus = PlayerGlobals.EnlightenmentBonus;
		playerSaveData.headset = PlayerGlobals.Headset;
		playerSaveData.kills = PlayerGlobals.Kills;
		playerSaveData.numbness = PlayerGlobals.Numbness;
		playerSaveData.numbnessBonus = PlayerGlobals.NumbnessBonus;
		playerSaveData.pantiesEquipped = PlayerGlobals.PantiesEquipped;
		playerSaveData.pantyShots = PlayerGlobals.PantyShots;
		foreach (int num in PlayerGlobals.KeysOfPhoto())
		{
			if (PlayerGlobals.GetPhoto(num))
			{
				playerSaveData.photo.Add(num);
			}
		}
		foreach (int num2 in PlayerGlobals.KeysOfPhotoOnCorkboard())
		{
			if (PlayerGlobals.GetPhotoOnCorkboard(num2))
			{
				playerSaveData.photoOnCorkboard.Add(num2);
			}
		}
		foreach (int num3 in PlayerGlobals.KeysOfPhotoPosition())
		{
			playerSaveData.photoPosition.Add(num3, PlayerGlobals.GetPhotoPosition(num3));
		}
		foreach (int num4 in PlayerGlobals.KeysOfPhotoRotation())
		{
			playerSaveData.photoRotation.Add(num4, PlayerGlobals.GetPhotoRotation(num4));
		}
		playerSaveData.reputation = PlayerGlobals.Reputation;
		playerSaveData.seduction = PlayerGlobals.Seduction;
		playerSaveData.seductionBonus = PlayerGlobals.SeductionBonus;
		foreach (int num5 in PlayerGlobals.KeysOfSenpaiPhoto())
		{
			if (PlayerGlobals.GetSenpaiPhoto(num5))
			{
				playerSaveData.senpaiPhoto.Add(num5);
			}
		}
		playerSaveData.senpaiShots = PlayerGlobals.SenpaiShots;
		playerSaveData.socialBonus = PlayerGlobals.SocialBonus;
		playerSaveData.speedBonus = PlayerGlobals.SpeedBonus;
		playerSaveData.stealthBonus = PlayerGlobals.StealthBonus;
		foreach (int num6 in PlayerGlobals.KeysOfStudentFriend())
		{
			if (PlayerGlobals.GetStudentFriend(num6))
			{
				playerSaveData.studentFriend.Add(num6);
			}
		}
		foreach (string text in PlayerGlobals.KeysOfStudentPantyShot())
		{
			if (PlayerGlobals.GetStudentPantyShot(text))
			{
				playerSaveData.studentPantyShot.Add(text);
			}
		}
		return playerSaveData;
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x000FDE1C File Offset: 0x000FC01C
	public static void WriteToGlobals(PlayerSaveData data)
	{
		PlayerGlobals.Alerts = data.alerts;
		PlayerGlobals.Enlightenment = data.enlightenment;
		PlayerGlobals.EnlightenmentBonus = data.enlightenmentBonus;
		PlayerGlobals.Headset = data.headset;
		PlayerGlobals.Kills = data.kills;
		PlayerGlobals.Numbness = data.numbness;
		PlayerGlobals.NumbnessBonus = data.numbnessBonus;
		PlayerGlobals.PantiesEquipped = data.pantiesEquipped;
		PlayerGlobals.PantyShots = data.pantyShots;
		Debug.Log("Is this being called anywhere?");
		foreach (int photoID in data.photo)
		{
			PlayerGlobals.SetPhoto(photoID, true);
		}
		foreach (int photoID2 in data.photoOnCorkboard)
		{
			PlayerGlobals.SetPhotoOnCorkboard(photoID2, true);
		}
		foreach (KeyValuePair<int, Vector2> keyValuePair in data.photoPosition)
		{
			PlayerGlobals.SetPhotoPosition(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (KeyValuePair<int, float> keyValuePair2 in data.photoRotation)
		{
			PlayerGlobals.SetPhotoRotation(keyValuePair2.Key, keyValuePair2.Value);
		}
		PlayerGlobals.Reputation = data.reputation;
		PlayerGlobals.Seduction = data.seduction;
		PlayerGlobals.SeductionBonus = data.seductionBonus;
		foreach (int photoID3 in data.senpaiPhoto)
		{
			PlayerGlobals.SetSenpaiPhoto(photoID3, true);
		}
		PlayerGlobals.SenpaiShots = data.senpaiShots;
		PlayerGlobals.SocialBonus = data.socialBonus;
		PlayerGlobals.SpeedBonus = data.speedBonus;
		PlayerGlobals.StealthBonus = data.stealthBonus;
		foreach (int studentID in data.studentFriend)
		{
			PlayerGlobals.SetStudentFriend(studentID, true);
		}
		foreach (string studentName in data.studentPantyShot)
		{
			PlayerGlobals.SetStudentPantyShot(studentName, true);
		}
	}

	// Token: 0x040028C4 RID: 10436
	public int alerts;

	// Token: 0x040028C5 RID: 10437
	public int enlightenment;

	// Token: 0x040028C6 RID: 10438
	public int enlightenmentBonus;

	// Token: 0x040028C7 RID: 10439
	public bool headset;

	// Token: 0x040028C8 RID: 10440
	public int kills;

	// Token: 0x040028C9 RID: 10441
	public int numbness;

	// Token: 0x040028CA RID: 10442
	public int numbnessBonus;

	// Token: 0x040028CB RID: 10443
	public int pantiesEquipped;

	// Token: 0x040028CC RID: 10444
	public int pantyShots;

	// Token: 0x040028CD RID: 10445
	public IntHashSet photo = new IntHashSet();

	// Token: 0x040028CE RID: 10446
	public IntHashSet photoOnCorkboard = new IntHashSet();

	// Token: 0x040028CF RID: 10447
	public IntAndVector2Dictionary photoPosition = new IntAndVector2Dictionary();

	// Token: 0x040028D0 RID: 10448
	public IntAndFloatDictionary photoRotation = new IntAndFloatDictionary();

	// Token: 0x040028D1 RID: 10449
	public float reputation;

	// Token: 0x040028D2 RID: 10450
	public int seduction;

	// Token: 0x040028D3 RID: 10451
	public int seductionBonus;

	// Token: 0x040028D4 RID: 10452
	public IntHashSet senpaiPhoto = new IntHashSet();

	// Token: 0x040028D5 RID: 10453
	public int senpaiShots;

	// Token: 0x040028D6 RID: 10454
	public int socialBonus;

	// Token: 0x040028D7 RID: 10455
	public int speedBonus;

	// Token: 0x040028D8 RID: 10456
	public int stealthBonus;

	// Token: 0x040028D9 RID: 10457
	public IntHashSet studentFriend = new IntHashSet();

	// Token: 0x040028DA RID: 10458
	public StringHashSet studentPantyShot = new StringHashSet();
}
