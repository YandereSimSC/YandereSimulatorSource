using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class StudentEditorScript : MonoBehaviour
{
	// Token: 0x0600138A RID: 5002 RVA: 0x000A7DA0 File Offset: 0x000A5FA0
	private void Awake()
	{
		Dictionary<string, object>[] array = EditorManagerScript.DeserializeJson("Students.json");
		this.students = new StudentEditorScript.StudentData[array.Length];
		for (int i = 0; i < this.students.Length; i++)
		{
			this.students[i] = StudentEditorScript.StudentData.Deserialize(array[i]);
		}
		Array.Sort<StudentEditorScript.StudentData>(this.students, (StudentEditorScript.StudentData a, StudentEditorScript.StudentData b) => a.id - b.id);
		for (int j = 0; j < this.students.Length; j++)
		{
			StudentEditorScript.StudentData studentData = this.students[j];
			UILabel uilabel = UnityEngine.Object.Instantiate<UILabel>(this.studentLabelTemplate, this.listLabelsOrigin);
			uilabel.text = "(" + studentData.id.ToString() + ") " + studentData.name;
			Transform transform = uilabel.transform;
			transform.localPosition = new Vector3(transform.localPosition.x + (float)(uilabel.width / 2), transform.localPosition.y - (float)(j * uilabel.height), transform.localPosition.z);
			uilabel.gameObject.SetActive(true);
		}
		this.studentIndex = 0;
		this.bodyLabel.text = StudentEditorScript.GetStudentText(this.students[this.studentIndex]);
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000A7EFA File Offset: 0x000A60FA
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x000A7F38 File Offset: 0x000A6138
	private static ScheduleBlock[] DeserializeScheduleBlocks(Dictionary<string, object> dict)
	{
		string[] array = TFUtils.LoadString(dict, "ScheduleTime").Split(new char[]
		{
			'_'
		});
		string[] array2 = TFUtils.LoadString(dict, "ScheduleDestination").Split(new char[]
		{
			'_'
		});
		string[] array3 = TFUtils.LoadString(dict, "ScheduleAction").Split(new char[]
		{
			'_'
		});
		ScheduleBlock[] array4 = new ScheduleBlock[array.Length];
		for (int i = 0; i < array4.Length; i++)
		{
			array4[i] = new ScheduleBlock(float.Parse(array[i]), array2[i], array3[i]);
		}
		return array4;
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x000A7FD0 File Offset: 0x000A61D0
	private static string GetStudentText(StudentEditorScript.StudentData data)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Concat(new object[]
		{
			data.name,
			" (",
			data.id,
			"):\n"
		}));
		stringBuilder.Append("- Gender: " + (data.isMale ? "Male" : "Female") + "\n");
		stringBuilder.Append("- Class: " + data.attendanceInfo.classNumber + "\n");
		stringBuilder.Append("- Seat: " + data.attendanceInfo.seatNumber + "\n");
		stringBuilder.Append("- Club: " + data.attendanceInfo.club + "\n");
		stringBuilder.Append("- Persona: " + data.personality.persona + "\n");
		stringBuilder.Append("- Crush: " + data.personality.crush + "\n");
		stringBuilder.Append("- Breast size: " + data.cosmetics.breastSize + "\n");
		stringBuilder.Append("- Strength: " + data.stats.strength + "\n");
		stringBuilder.Append("- Hairstyle: " + data.cosmetics.hairstyle + "\n");
		stringBuilder.Append("- Color: " + data.cosmetics.color + "\n");
		stringBuilder.Append("- Eyes: " + data.cosmetics.eyes + "\n");
		stringBuilder.Append("- Stockings: " + data.cosmetics.stockings + "\n");
		stringBuilder.Append("- Accessory: " + data.cosmetics.accessory + "\n");
		stringBuilder.Append("- Schedule blocks: ");
		foreach (ScheduleBlock scheduleBlock in data.scheduleBlocks)
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				"[",
				scheduleBlock.time,
				", ",
				scheduleBlock.destination,
				", ",
				scheduleBlock.action,
				"]"
			}));
		}
		stringBuilder.Append("\n");
		stringBuilder.Append("- Info: \"" + data.info + "\"\n");
		return stringBuilder.ToString();
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x000A829C File Offset: 0x000A649C
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			this.mainPanel.gameObject.SetActive(true);
			this.studentPanel.gameObject.SetActive(false);
		}
		int num = 0;
		int num2 = this.students.Length - 1;
		bool tappedUp = this.inputManager.TappedUp;
		bool tappedDown = this.inputManager.TappedDown;
		if (tappedUp)
		{
			this.studentIndex = ((this.studentIndex > num) ? (this.studentIndex - 1) : num2);
		}
		else if (tappedDown)
		{
			this.studentIndex = ((this.studentIndex < num2) ? (this.studentIndex + 1) : num);
		}
		if (tappedUp || tappedDown)
		{
			this.bodyLabel.text = StudentEditorScript.GetStudentText(this.students[this.studentIndex]);
		}
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000A8358 File Offset: 0x000A6558
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001AB9 RID: 6841
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001ABA RID: 6842
	[SerializeField]
	private UIPanel studentPanel;

	// Token: 0x04001ABB RID: 6843
	[SerializeField]
	private UILabel bodyLabel;

	// Token: 0x04001ABC RID: 6844
	[SerializeField]
	private Transform listLabelsOrigin;

	// Token: 0x04001ABD RID: 6845
	[SerializeField]
	private UILabel studentLabelTemplate;

	// Token: 0x04001ABE RID: 6846
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001ABF RID: 6847
	private StudentEditorScript.StudentData[] students;

	// Token: 0x04001AC0 RID: 6848
	private int studentIndex;

	// Token: 0x04001AC1 RID: 6849
	private InputManagerScript inputManager;

	// Token: 0x0200069E RID: 1694
	private class StudentAttendanceInfo
	{
		// Token: 0x06002B7D RID: 11133 RVA: 0x001C52F2 File Offset: 0x001C34F2
		public static StudentEditorScript.StudentAttendanceInfo Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentAttendanceInfo
			{
				classNumber = TFUtils.LoadInt(dict, "Class"),
				seatNumber = TFUtils.LoadInt(dict, "Seat"),
				club = TFUtils.LoadInt(dict, "Club")
			};
		}

		// Token: 0x04004672 RID: 18034
		public int classNumber;

		// Token: 0x04004673 RID: 18035
		public int seatNumber;

		// Token: 0x04004674 RID: 18036
		public int club;
	}

	// Token: 0x0200069F RID: 1695
	private class StudentPersonality
	{
		// Token: 0x06002B7F RID: 11135 RVA: 0x001C532C File Offset: 0x001C352C
		public static StudentEditorScript.StudentPersonality Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentPersonality
			{
				persona = (PersonaType)TFUtils.LoadInt(dict, "Persona"),
				crush = TFUtils.LoadInt(dict, "Crush")
			};
		}

		// Token: 0x04004675 RID: 18037
		public PersonaType persona;

		// Token: 0x04004676 RID: 18038
		public int crush;
	}

	// Token: 0x020006A0 RID: 1696
	private class StudentStats
	{
		// Token: 0x06002B81 RID: 11137 RVA: 0x001C5355 File Offset: 0x001C3555
		public static StudentEditorScript.StudentStats Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentStats
			{
				strength = TFUtils.LoadInt(dict, "Strength")
			};
		}

		// Token: 0x04004677 RID: 18039
		public int strength;
	}

	// Token: 0x020006A1 RID: 1697
	private class StudentCosmetics
	{
		// Token: 0x06002B83 RID: 11139 RVA: 0x001C5370 File Offset: 0x001C3570
		public static StudentEditorScript.StudentCosmetics Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentCosmetics
			{
				breastSize = TFUtils.LoadFloat(dict, "BreastSize"),
				hairstyle = TFUtils.LoadString(dict, "Hairstyle"),
				color = TFUtils.LoadString(dict, "Color"),
				eyes = TFUtils.LoadString(dict, "Eyes"),
				stockings = TFUtils.LoadString(dict, "Stockings"),
				accessory = TFUtils.LoadString(dict, "Accessory")
			};
		}

		// Token: 0x04004678 RID: 18040
		public float breastSize;

		// Token: 0x04004679 RID: 18041
		public string hairstyle;

		// Token: 0x0400467A RID: 18042
		public string color;

		// Token: 0x0400467B RID: 18043
		public string eyes;

		// Token: 0x0400467C RID: 18044
		public string stockings;

		// Token: 0x0400467D RID: 18045
		public string accessory;
	}

	// Token: 0x020006A2 RID: 1698
	private class StudentData
	{
		// Token: 0x06002B85 RID: 11141 RVA: 0x001C53E8 File Offset: 0x001C35E8
		public static StudentEditorScript.StudentData Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentData
			{
				id = TFUtils.LoadInt(dict, "ID"),
				name = TFUtils.LoadString(dict, "Name"),
				isMale = (TFUtils.LoadInt(dict, "Gender") == 1),
				attendanceInfo = StudentEditorScript.StudentAttendanceInfo.Deserialize(dict),
				personality = StudentEditorScript.StudentPersonality.Deserialize(dict),
				stats = StudentEditorScript.StudentStats.Deserialize(dict),
				cosmetics = StudentEditorScript.StudentCosmetics.Deserialize(dict),
				scheduleBlocks = StudentEditorScript.DeserializeScheduleBlocks(dict),
				info = TFUtils.LoadString(dict, "Info")
			};
		}

		// Token: 0x0400467E RID: 18046
		public int id;

		// Token: 0x0400467F RID: 18047
		public string name;

		// Token: 0x04004680 RID: 18048
		public bool isMale;

		// Token: 0x04004681 RID: 18049
		public StudentEditorScript.StudentAttendanceInfo attendanceInfo;

		// Token: 0x04004682 RID: 18050
		public StudentEditorScript.StudentPersonality personality;

		// Token: 0x04004683 RID: 18051
		public StudentEditorScript.StudentStats stats;

		// Token: 0x04004684 RID: 18052
		public StudentEditorScript.StudentCosmetics cosmetics;

		// Token: 0x04004685 RID: 18053
		public ScheduleBlock[] scheduleBlocks;

		// Token: 0x04004686 RID: 18054
		public string info;
	}
}
