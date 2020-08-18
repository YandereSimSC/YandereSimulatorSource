using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000312 RID: 786
public class JsonScript : MonoBehaviour
{
	// Token: 0x060017BC RID: 6076 RVA: 0x000CFDA8 File Offset: 0x000CDFA8
	private void Start()
	{
		this.students = StudentJson.LoadFromJson(StudentJson.FilePath);
		this.topics = TopicJson.LoadFromJson(TopicJson.FilePath);
		if (SceneManager.GetActiveScene().name == "SchoolScene")
		{
			StudentManagerScript studentManagerScript = UnityEngine.Object.FindObjectOfType<StudentManagerScript>();
			this.ReplaceDeadTeachers(studentManagerScript.FirstNames, studentManagerScript.LastNames);
			return;
		}
		if (SceneManager.GetActiveScene().name == "CreditsScene")
		{
			this.credits = CreditJson.LoadFromJson(CreditJson.FilePath);
		}
	}

	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x060017BD RID: 6077 RVA: 0x000CFE30 File Offset: 0x000CE030
	public StudentJson[] Students
	{
		get
		{
			return this.students;
		}
	}

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x060017BE RID: 6078 RVA: 0x000CFE38 File Offset: 0x000CE038
	public CreditJson[] Credits
	{
		get
		{
			return this.credits;
		}
	}

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x060017BF RID: 6079 RVA: 0x000CFE40 File Offset: 0x000CE040
	public TopicJson[] Topics
	{
		get
		{
			return this.topics;
		}
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x000CFE48 File Offset: 0x000CE048
	private void ReplaceDeadTeachers(string[] firstNames, string[] lastNames)
	{
		for (int i = 90; i < 101; i++)
		{
			if (StudentGlobals.GetStudentDead(i))
			{
				StudentGlobals.SetStudentReplaced(i, true);
				StudentGlobals.SetStudentDead(i, false);
				string value = firstNames[UnityEngine.Random.Range(0, firstNames.Length)] + " " + lastNames[UnityEngine.Random.Range(0, lastNames.Length)];
				StudentGlobals.SetStudentName(i, value);
				StudentGlobals.SetStudentBustSize(i, UnityEngine.Random.Range(1f, 1.5f));
				StudentGlobals.SetStudentHairstyle(i, UnityEngine.Random.Range(1, 8).ToString());
				float r = UnityEngine.Random.Range(0f, 1f);
				float g = UnityEngine.Random.Range(0f, 1f);
				float b = UnityEngine.Random.Range(0f, 1f);
				StudentGlobals.SetStudentColor(i, new Color(r, g, b));
				r = UnityEngine.Random.Range(0f, 1f);
				g = UnityEngine.Random.Range(0f, 1f);
				b = UnityEngine.Random.Range(0f, 1f);
				StudentGlobals.SetStudentEyeColor(i, new Color(r, g, b));
				StudentGlobals.SetStudentAccessory(i, UnityEngine.Random.Range(1, 7).ToString());
			}
		}
		for (int j = 90; j < 101; j++)
		{
			if (StudentGlobals.GetStudentReplaced(j))
			{
				StudentJson studentJson = this.students[j];
				studentJson.Name = StudentGlobals.GetStudentName(j);
				studentJson.BreastSize = StudentGlobals.GetStudentBustSize(j);
				studentJson.Hairstyle = StudentGlobals.GetStudentHairstyle(j);
				studentJson.Accessory = StudentGlobals.GetStudentAccessory(j);
				if (j == 97)
				{
					studentJson.Accessory = "7";
				}
				if (j == 90)
				{
					studentJson.Accessory = "8";
				}
			}
		}
	}

	// Token: 0x040021AE RID: 8622
	[SerializeField]
	private StudentJson[] students;

	// Token: 0x040021AF RID: 8623
	[SerializeField]
	private CreditJson[] credits;

	// Token: 0x040021B0 RID: 8624
	[SerializeField]
	private TopicJson[] topics;
}
