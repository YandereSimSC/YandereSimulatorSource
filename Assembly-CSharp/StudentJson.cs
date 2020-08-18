using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

// Token: 0x0200030F RID: 783
[Serializable]
public class StudentJson : JsonData
{
	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x06001794 RID: 6036 RVA: 0x000CF88A File Offset: 0x000CDA8A
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Students.json");
		}
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x000CF89C File Offset: 0x000CDA9C
	public static StudentJson[] LoadFromJson(string path)
	{
		StudentJson[] array = new StudentJson[101];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new StudentJson();
		}
		foreach (Dictionary<string, object> dictionary in JsonData.Deserialize(path))
		{
			int num = TFUtils.LoadInt(dictionary, "ID");
			if (num == 0)
			{
				break;
			}
			StudentJson studentJson = array[num];
			studentJson.name = TFUtils.LoadString(dictionary, "Name");
			studentJson.gender = TFUtils.LoadInt(dictionary, "Gender");
			studentJson.classID = TFUtils.LoadInt(dictionary, "Class");
			studentJson.seat = TFUtils.LoadInt(dictionary, "Seat");
			studentJson.club = (ClubType)TFUtils.LoadInt(dictionary, "Club");
			studentJson.persona = (PersonaType)TFUtils.LoadInt(dictionary, "Persona");
			studentJson.crush = TFUtils.LoadInt(dictionary, "Crush");
			studentJson.breastSize = TFUtils.LoadFloat(dictionary, "BreastSize");
			studentJson.strength = TFUtils.LoadInt(dictionary, "Strength");
			studentJson.hairstyle = TFUtils.LoadString(dictionary, "Hairstyle");
			studentJson.color = TFUtils.LoadString(dictionary, "Color");
			studentJson.eyes = TFUtils.LoadString(dictionary, "Eyes");
			studentJson.eyeType = TFUtils.LoadString(dictionary, "EyeType");
			studentJson.stockings = TFUtils.LoadString(dictionary, "Stockings");
			studentJson.accessory = TFUtils.LoadString(dictionary, "Accessory");
			studentJson.info = TFUtils.LoadString(dictionary, "Info");
			if (GameGlobals.LoveSick && studentJson.name == "Mai Waifu")
			{
				studentJson.name = "Mai Wakabayashi";
			}
			if (OptionGlobals.HighPopulation && studentJson.name == "Unknown")
			{
				studentJson.name = "Random";
			}
			float[] array3 = StudentJson.ConstructTempFloatArray(TFUtils.LoadString(dictionary, "ScheduleTime"));
			string[] array4 = StudentJson.ConstructTempStringArray(TFUtils.LoadString(dictionary, "ScheduleDestination"));
			string[] array5 = StudentJson.ConstructTempStringArray(TFUtils.LoadString(dictionary, "ScheduleAction"));
			studentJson.scheduleBlocks = new ScheduleBlock[array3.Length];
			for (int k = 0; k < studentJson.scheduleBlocks.Length; k++)
			{
				studentJson.scheduleBlocks[k] = new ScheduleBlock(array3[k], array4[k], array5[k]);
			}
			if (num == 10 || num == 11)
			{
				for (int l = 0; l < studentJson.scheduleBlocks.Length; l++)
				{
					studentJson.scheduleBlocks[l] = null;
				}
			}
			studentJson.success = true;
		}
		return array;
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x06001796 RID: 6038 RVA: 0x000CFB35 File Offset: 0x000CDD35
	// (set) Token: 0x06001797 RID: 6039 RVA: 0x000CFB3D File Offset: 0x000CDD3D
	public string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.name = value;
		}
	}

	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x06001798 RID: 6040 RVA: 0x000CFB46 File Offset: 0x000CDD46
	public int Gender
	{
		get
		{
			return this.gender;
		}
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x06001799 RID: 6041 RVA: 0x000CFB4E File Offset: 0x000CDD4E
	// (set) Token: 0x0600179A RID: 6042 RVA: 0x000CFB56 File Offset: 0x000CDD56
	public int Class
	{
		get
		{
			return this.classID;
		}
		set
		{
			this.classID = value;
		}
	}

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x0600179B RID: 6043 RVA: 0x000CFB5F File Offset: 0x000CDD5F
	// (set) Token: 0x0600179C RID: 6044 RVA: 0x000CFB67 File Offset: 0x000CDD67
	public int Seat
	{
		get
		{
			return this.seat;
		}
		set
		{
			this.seat = value;
		}
	}

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x0600179D RID: 6045 RVA: 0x000CFB70 File Offset: 0x000CDD70
	public ClubType Club
	{
		get
		{
			return this.club;
		}
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x0600179E RID: 6046 RVA: 0x000CFB78 File Offset: 0x000CDD78
	// (set) Token: 0x0600179F RID: 6047 RVA: 0x000CFB80 File Offset: 0x000CDD80
	public PersonaType Persona
	{
		get
		{
			return this.persona;
		}
		set
		{
			this.persona = value;
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000CFB89 File Offset: 0x000CDD89
	public int Crush
	{
		get
		{
			return this.crush;
		}
	}

	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x060017A1 RID: 6049 RVA: 0x000CFB91 File Offset: 0x000CDD91
	// (set) Token: 0x060017A2 RID: 6050 RVA: 0x000CFB99 File Offset: 0x000CDD99
	public float BreastSize
	{
		get
		{
			return this.breastSize;
		}
		set
		{
			this.breastSize = value;
		}
	}

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000CFBA2 File Offset: 0x000CDDA2
	// (set) Token: 0x060017A4 RID: 6052 RVA: 0x000CFBAA File Offset: 0x000CDDAA
	public int Strength
	{
		get
		{
			return this.strength;
		}
		set
		{
			this.strength = value;
		}
	}

	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000CFBB3 File Offset: 0x000CDDB3
	// (set) Token: 0x060017A6 RID: 6054 RVA: 0x000CFBBB File Offset: 0x000CDDBB
	public string Hairstyle
	{
		get
		{
			return this.hairstyle;
		}
		set
		{
			this.hairstyle = value;
		}
	}

	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000CFBC4 File Offset: 0x000CDDC4
	public string Color
	{
		get
		{
			return this.color;
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000CFBCC File Offset: 0x000CDDCC
	public string Eyes
	{
		get
		{
			return this.eyes;
		}
	}

	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x060017A9 RID: 6057 RVA: 0x000CFBD4 File Offset: 0x000CDDD4
	public string EyeType
	{
		get
		{
			return this.eyeType;
		}
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x060017AA RID: 6058 RVA: 0x000CFBDC File Offset: 0x000CDDDC
	public string Stockings
	{
		get
		{
			return this.stockings;
		}
	}

	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x060017AB RID: 6059 RVA: 0x000CFBE4 File Offset: 0x000CDDE4
	// (set) Token: 0x060017AC RID: 6060 RVA: 0x000CFBEC File Offset: 0x000CDDEC
	public string Accessory
	{
		get
		{
			return this.accessory;
		}
		set
		{
			this.accessory = value;
		}
	}

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x060017AD RID: 6061 RVA: 0x000CFBF5 File Offset: 0x000CDDF5
	public string Info
	{
		get
		{
			return this.info;
		}
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x060017AE RID: 6062 RVA: 0x000CFBFD File Offset: 0x000CDDFD
	public ScheduleBlock[] ScheduleBlocks
	{
		get
		{
			return this.scheduleBlocks;
		}
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x060017AF RID: 6063 RVA: 0x000CFC05 File Offset: 0x000CDE05
	public bool Success
	{
		get
		{
			return this.success;
		}
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x000CFC10 File Offset: 0x000CDE10
	private static float[] ConstructTempFloatArray(string str)
	{
		string[] array = str.Split(new char[]
		{
			'_'
		});
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			float num;
			if (float.TryParse(array[i], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num))
			{
				array2[i] = num;
			}
		}
		return array2;
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x000CFC61 File Offset: 0x000CDE61
	private static string[] ConstructTempStringArray(string str)
	{
		return str.Split(new char[]
		{
			'_'
		});
	}

	// Token: 0x04002199 RID: 8601
	[SerializeField]
	private string name;

	// Token: 0x0400219A RID: 8602
	[SerializeField]
	private int gender;

	// Token: 0x0400219B RID: 8603
	[SerializeField]
	private int classID;

	// Token: 0x0400219C RID: 8604
	[SerializeField]
	private int seat;

	// Token: 0x0400219D RID: 8605
	[SerializeField]
	private ClubType club;

	// Token: 0x0400219E RID: 8606
	[SerializeField]
	private PersonaType persona;

	// Token: 0x0400219F RID: 8607
	[SerializeField]
	private int crush;

	// Token: 0x040021A0 RID: 8608
	[SerializeField]
	private float breastSize;

	// Token: 0x040021A1 RID: 8609
	[SerializeField]
	private int strength;

	// Token: 0x040021A2 RID: 8610
	[SerializeField]
	private string hairstyle;

	// Token: 0x040021A3 RID: 8611
	[SerializeField]
	private string color;

	// Token: 0x040021A4 RID: 8612
	[SerializeField]
	private string eyes;

	// Token: 0x040021A5 RID: 8613
	[SerializeField]
	private string eyeType;

	// Token: 0x040021A6 RID: 8614
	[SerializeField]
	private string stockings;

	// Token: 0x040021A7 RID: 8615
	[SerializeField]
	private string accessory;

	// Token: 0x040021A8 RID: 8616
	[SerializeField]
	private string info;

	// Token: 0x040021A9 RID: 8617
	[SerializeField]
	private ScheduleBlock[] scheduleBlocks;

	// Token: 0x040021AA RID: 8618
	[SerializeField]
	private bool success;
}
