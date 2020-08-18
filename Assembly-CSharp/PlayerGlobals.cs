using System;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public static class PlayerGlobals
{
	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06001560 RID: 5472 RVA: 0x000B728B File Offset: 0x000B548B
	// (set) Token: 0x06001561 RID: 5473 RVA: 0x000B72AB File Offset: 0x000B54AB
	public static float Money
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Money");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Money", value);
		}
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06001562 RID: 5474 RVA: 0x000B72CC File Offset: 0x000B54CC
	// (set) Token: 0x06001563 RID: 5475 RVA: 0x000B72EC File Offset: 0x000B54EC
	public static int Alerts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Alerts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Alerts", value);
		}
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001564 RID: 5476 RVA: 0x000B730D File Offset: 0x000B550D
	// (set) Token: 0x06001565 RID: 5477 RVA: 0x000B732D File Offset: 0x000B552D
	public static int Enlightenment
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Enlightenment");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Enlightenment", value);
		}
	}

	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06001566 RID: 5478 RVA: 0x000B734E File Offset: 0x000B554E
	// (set) Token: 0x06001567 RID: 5479 RVA: 0x000B736E File Offset: 0x000B556E
	public static int EnlightenmentBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus", value);
		}
	}

	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06001568 RID: 5480 RVA: 0x000B738F File Offset: 0x000B558F
	// (set) Token: 0x06001569 RID: 5481 RVA: 0x000B73AF File Offset: 0x000B55AF
	public static int Friends
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Friends");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Friends", value);
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x0600156A RID: 5482 RVA: 0x000B73D0 File Offset: 0x000B55D0
	// (set) Token: 0x0600156B RID: 5483 RVA: 0x000B73F0 File Offset: 0x000B55F0
	public static bool Headset
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Headset");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Headset", value);
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x0600156C RID: 5484 RVA: 0x000B7411 File Offset: 0x000B5611
	// (set) Token: 0x0600156D RID: 5485 RVA: 0x000B7431 File Offset: 0x000B5631
	public static bool FakeID
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_FakeID");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_FakeID", value);
		}
	}

	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x0600156E RID: 5486 RVA: 0x000B7452 File Offset: 0x000B5652
	// (set) Token: 0x0600156F RID: 5487 RVA: 0x000B7472 File Offset: 0x000B5672
	public static bool RaibaruLoner
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RaibaruLoner");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RaibaruLoner", value);
		}
	}

	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06001570 RID: 5488 RVA: 0x000B7493 File Offset: 0x000B5693
	// (set) Token: 0x06001571 RID: 5489 RVA: 0x000B74B3 File Offset: 0x000B56B3
	public static int Kills
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Kills");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Kills", value);
		}
	}

	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06001572 RID: 5490 RVA: 0x000B74D4 File Offset: 0x000B56D4
	// (set) Token: 0x06001573 RID: 5491 RVA: 0x000B74F4 File Offset: 0x000B56F4
	public static int Numbness
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Numbness");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Numbness", value);
		}
	}

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x06001574 RID: 5492 RVA: 0x000B7515 File Offset: 0x000B5715
	// (set) Token: 0x06001575 RID: 5493 RVA: 0x000B7535 File Offset: 0x000B5735
	public static int NumbnessBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_NumbnessBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_NumbnessBonus", value);
		}
	}

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x06001576 RID: 5494 RVA: 0x000B7556 File Offset: 0x000B5756
	// (set) Token: 0x06001577 RID: 5495 RVA: 0x000B7576 File Offset: 0x000B5776
	public static int PantiesEquipped
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PantiesEquipped");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PantiesEquipped", value);
		}
	}

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x06001578 RID: 5496 RVA: 0x000B7597 File Offset: 0x000B5797
	// (set) Token: 0x06001579 RID: 5497 RVA: 0x000B75B7 File Offset: 0x000B57B7
	public static int PantyShots
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PantyShots");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PantyShots", value);
		}
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000B75D8 File Offset: 0x000B57D8
	public static bool GetPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Photo_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x000B7614 File Offset: 0x000B5814
	public static void SetPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_Photo_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Photo_",
			text
		}), value);
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000B767A File Offset: 0x000B587A
	public static int[] KeysOfPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_Photo_");
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x000B769A File Offset: 0x000B589A
	public static bool GetPhotoOnCorkboard(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoOnCorkboard_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000B76D4 File Offset: 0x000B58D4
	public static void SetPhotoOnCorkboard(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoOnCorkboard_",
			text
		}), value);
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x000B773A File Offset: 0x000B593A
	public static int[] KeysOfPhotoOnCorkboard()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_");
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000B775A File Offset: 0x000B595A
	public static Vector2 GetPhotoPosition(int photoID)
	{
		return GlobalsHelper.GetVector2(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoPosition_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000B7794 File Offset: 0x000B5994
	public static void SetPhotoPosition(int photoID, Vector2 value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoPosition_", text);
		GlobalsHelper.SetVector2(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoPosition_",
			text
		}), value);
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000B77FA File Offset: 0x000B59FA
	public static int[] KeysOfPhotoPosition()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoPosition_");
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000B781A File Offset: 0x000B5A1A
	public static float GetPhotoRotation(int photoID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoRotation_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000B7854 File Offset: 0x000B5A54
	public static void SetPhotoRotation(int photoID, float value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoRotation_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoRotation_",
			text
		}), value);
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x000B78BA File Offset: 0x000B5ABA
	public static int[] KeysOfPhotoRotation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoRotation_");
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x06001586 RID: 5510 RVA: 0x000B78DA File Offset: 0x000B5ADA
	// (set) Token: 0x06001587 RID: 5511 RVA: 0x000B78FA File Offset: 0x000B5AFA
	public static float Reputation
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Reputation");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Reputation", value);
		}
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x06001588 RID: 5512 RVA: 0x000B791B File Offset: 0x000B5B1B
	// (set) Token: 0x06001589 RID: 5513 RVA: 0x000B793B File Offset: 0x000B5B3B
	public static int Seduction
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Seduction");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Seduction", value);
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x0600158A RID: 5514 RVA: 0x000B795C File Offset: 0x000B5B5C
	// (set) Token: 0x0600158B RID: 5515 RVA: 0x000B797C File Offset: 0x000B5B7C
	public static int SeductionBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SeductionBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SeductionBonus", value);
		}
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x000B799D File Offset: 0x000B5B9D
	public static bool GetSenpaiPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SenpaiPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x000B79D8 File Offset: 0x000B5BD8
	public static void SetSenpaiPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SenpaiPhoto_",
			text
		}), value);
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x000B7A3E File Offset: 0x000B5C3E
	public static int GetBullyPhoto(int photoID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BullyPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x000B7A77 File Offset: 0x000B5C77
	public static void SetBullyPhoto(int photoID, int value)
	{
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BullyPhoto_",
			photoID.ToString()
		}), value);
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x000B7AB1 File Offset: 0x000B5CB1
	public static int[] KeysOfSenpaiPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_");
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06001591 RID: 5521 RVA: 0x000B7AD1 File Offset: 0x000B5CD1
	// (set) Token: 0x06001592 RID: 5522 RVA: 0x000B7AF1 File Offset: 0x000B5CF1
	public static int SenpaiShots
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiShots");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiShots", value);
		}
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x06001593 RID: 5523 RVA: 0x000B7B12 File Offset: 0x000B5D12
	// (set) Token: 0x06001594 RID: 5524 RVA: 0x000B7B32 File Offset: 0x000B5D32
	public static int SocialBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SocialBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SocialBonus", value);
		}
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x06001595 RID: 5525 RVA: 0x000B7B53 File Offset: 0x000B5D53
	// (set) Token: 0x06001596 RID: 5526 RVA: 0x000B7B73 File Offset: 0x000B5D73
	public static int SpeedBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SpeedBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SpeedBonus", value);
		}
	}

	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x06001597 RID: 5527 RVA: 0x000B7B94 File Offset: 0x000B5D94
	// (set) Token: 0x06001598 RID: 5528 RVA: 0x000B7BB4 File Offset: 0x000B5DB4
	public static int StealthBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StealthBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StealthBonus", value);
		}
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000B7BD5 File Offset: 0x000B5DD5
	public static bool GetStudentFriend(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentFriend_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000B7C10 File Offset: 0x000B5E10
	public static void SetStudentFriend(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentFriend_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentFriend_",
			text
		}), value);
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x000B7C76 File Offset: 0x000B5E76
	public static int[] KeysOfStudentFriend()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentFriend_");
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x000B7C96 File Offset: 0x000B5E96
	public static bool GetStudentPantyShot(string studentName)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPantyShot_",
			studentName
		}));
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x000B7CCC File Offset: 0x000B5ECC
	public static void SetStudentPantyShot(string studentName, bool value)
	{
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPantyShot_", studentName);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPantyShot_",
			studentName
		}), value);
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x000B7D2A File Offset: 0x000B5F2A
	public static string[] KeysOfStudentPantyShot()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_StudentPantyShot_");
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000B7D4A File Offset: 0x000B5F4A
	public static string[] KeysOfShrineCollectible()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_ShrineCollectible");
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x000B7D6A File Offset: 0x000B5F6A
	public static bool GetShrineCollectible(int ID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ShrineCollectible",
			ID.ToString()
		}));
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000B7DA4 File Offset: 0x000B5FA4
	public static void SetShrineCollectible(int ID, bool value)
	{
		string text = ID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ShrineCollectible", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ShrineCollectible",
			text
		}), value);
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000B7E0A File Offset: 0x000B600A
	// (set) Token: 0x060015A3 RID: 5539 RVA: 0x000B7E2A File Offset: 0x000B602A
	public static bool UsingGamepad
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_UsingGamepad");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_UsingGamepad", value);
		}
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x000B7E4C File Offset: 0x000B604C
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Money");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Alerts");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Enlightenment");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Friends");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Headset");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FakeID");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RaibaruLoner");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Kills");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Numbness");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_NumbnessBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PantiesEquipped");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PantyShots");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_Photo_", PlayerGlobals.KeysOfPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_", PlayerGlobals.KeysOfPhotoOnCorkboard());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoPosition_", PlayerGlobals.KeysOfPhotoPosition());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoRotation_", PlayerGlobals.KeysOfPhotoRotation());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Reputation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Seduction");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SeductionBonus");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_", PlayerGlobals.KeysOfSenpaiPhoto());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiShots");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SocialBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SpeedBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StealthBonus");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentFriend_", PlayerGlobals.KeysOfStudentFriend());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPantyShot_", PlayerGlobals.KeysOfStudentPantyShot());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ShrineCollectible", PlayerGlobals.KeysOfShrineCollectible());
	}

	// Token: 0x04001D5D RID: 7517
	private const string Str_Money = "Money";

	// Token: 0x04001D5E RID: 7518
	private const string Str_Alerts = "Alerts";

	// Token: 0x04001D5F RID: 7519
	private const string Str_BullyPhoto = "BullyPhoto_";

	// Token: 0x04001D60 RID: 7520
	private const string Str_Enlightenment = "Enlightenment";

	// Token: 0x04001D61 RID: 7521
	private const string Str_EnlightenmentBonus = "EnlightenmentBonus";

	// Token: 0x04001D62 RID: 7522
	private const string Str_Friends = "Friends";

	// Token: 0x04001D63 RID: 7523
	private const string Str_Headset = "Headset";

	// Token: 0x04001D64 RID: 7524
	private const string Str_FakeID = "FakeID";

	// Token: 0x04001D65 RID: 7525
	private const string Str_RaibaruLoner = "RaibaruLoner";

	// Token: 0x04001D66 RID: 7526
	private const string Str_Kills = "Kills";

	// Token: 0x04001D67 RID: 7527
	private const string Str_Numbness = "Numbness";

	// Token: 0x04001D68 RID: 7528
	private const string Str_NumbnessBonus = "NumbnessBonus";

	// Token: 0x04001D69 RID: 7529
	private const string Str_PantiesEquipped = "PantiesEquipped";

	// Token: 0x04001D6A RID: 7530
	private const string Str_PantyShots = "PantyShots";

	// Token: 0x04001D6B RID: 7531
	private const string Str_Photo = "Photo_";

	// Token: 0x04001D6C RID: 7532
	private const string Str_PhotoOnCorkboard = "PhotoOnCorkboard_";

	// Token: 0x04001D6D RID: 7533
	private const string Str_PhotoPosition = "PhotoPosition_";

	// Token: 0x04001D6E RID: 7534
	private const string Str_PhotoRotation = "PhotoRotation_";

	// Token: 0x04001D6F RID: 7535
	private const string Str_Reputation = "Reputation";

	// Token: 0x04001D70 RID: 7536
	private const string Str_Seduction = "Seduction";

	// Token: 0x04001D71 RID: 7537
	private const string Str_SeductionBonus = "SeductionBonus";

	// Token: 0x04001D72 RID: 7538
	private const string Str_SenpaiPhoto = "SenpaiPhoto_";

	// Token: 0x04001D73 RID: 7539
	private const string Str_SenpaiShots = "SenpaiShots";

	// Token: 0x04001D74 RID: 7540
	private const string Str_SocialBonus = "SocialBonus";

	// Token: 0x04001D75 RID: 7541
	private const string Str_SpeedBonus = "SpeedBonus";

	// Token: 0x04001D76 RID: 7542
	private const string Str_StealthBonus = "StealthBonus";

	// Token: 0x04001D77 RID: 7543
	private const string Str_StudentFriend = "StudentFriend_";

	// Token: 0x04001D78 RID: 7544
	private const string Str_StudentPantyShot = "StudentPantyShot_";

	// Token: 0x04001D79 RID: 7545
	private const string Str_ShrineCollectible = "ShrineCollectible";

	// Token: 0x04001D7A RID: 7546
	private const string Str_UsingGamepad = "UsingGamepad";
}
