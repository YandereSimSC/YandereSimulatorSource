using System;
using UnityEngine;

// Token: 0x02000283 RID: 643
[Serializable]
public class Club
{
	// Token: 0x060013B7 RID: 5047 RVA: 0x000AC247 File Offset: 0x000AA447
	public Club(ClubType type)
	{
		this.type = type;
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000AC256 File Offset: 0x000AA456
	// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000AC25E File Offset: 0x000AA45E
	public ClubType Type
	{
		get
		{
			return this.type;
		}
		set
		{
			this.type = value;
		}
	}

	// Token: 0x04001B55 RID: 6997
	[SerializeField]
	private ClubType type;

	// Token: 0x04001B56 RID: 6998
	public static readonly ClubTypeAndStringDictionary ClubNames = new ClubTypeAndStringDictionary
	{
		{
			ClubType.None,
			"No Club"
		},
		{
			ClubType.Cooking,
			"Cooking"
		},
		{
			ClubType.Drama,
			"Drama"
		},
		{
			ClubType.Occult,
			"Occult"
		},
		{
			ClubType.Art,
			"Art"
		},
		{
			ClubType.LightMusic,
			"Light Music"
		},
		{
			ClubType.MartialArts,
			"Martial Arts"
		},
		{
			ClubType.Photography,
			"Photography"
		},
		{
			ClubType.Science,
			"Science"
		},
		{
			ClubType.Sports,
			"Sports"
		},
		{
			ClubType.Gardening,
			"Gardening"
		},
		{
			ClubType.Gaming,
			"Gaming"
		},
		{
			ClubType.Council,
			"Student Council"
		},
		{
			ClubType.Delinquent,
			"Delinquent"
		},
		{
			ClubType.Bully,
			"No Club"
		},
		{
			ClubType.Nemesis,
			"?????"
		}
	};

	// Token: 0x04001B57 RID: 6999
	public static readonly IntAndStringDictionary TeacherClubNames = new IntAndStringDictionary
	{
		{
			0,
			"Gym Teacher"
		},
		{
			1,
			"School Nurse"
		},
		{
			2,
			"Guidance Counselor"
		},
		{
			3,
			"Headmaster"
		},
		{
			4,
			"?????"
		},
		{
			11,
			"Teacher of Class 1-1"
		},
		{
			12,
			"Teacher of Class 1-2"
		},
		{
			21,
			"Teacher of Class 2-1"
		},
		{
			22,
			"Teacher of Class 2-2"
		},
		{
			31,
			"Teacher of Class 3-1"
		},
		{
			32,
			"Teacher of Class 3-2"
		}
	};
}
